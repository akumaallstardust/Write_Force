using System;
using Microsoft.VisualBasic;
using Ionic.Zip; // DotNetZip ライブラリ
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using SevenZip;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography.Xml;

namespace WriteForce
{

     public partial class InputSettingsForm : Form
    {
        // =======================
        // ▼ 言語管理クラスを追加
        // =======================
        public static class LanguageManager
        {
            private static Dictionary<string, string> translations = new Dictionary<string, string>();

            public static void LoadLanguage(string lang)
            {
                // 例: "ja" → "locales/ja.json" を読み込む
                string filePath = $"locales/{lang}.json";
                if (File.Exists(filePath))
                {
                    string jsonText = File.ReadAllText(filePath);
                    translations = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(jsonText)!;
                }
            }

            public static string GetText(string key)
            {
                return translations.ContainsKey(key) ? translations[key] : key;
            }
        }
        // =======================
        // ▲ 言語管理クラスを追加
        // =======================

        public int targetTime { get; private set; } = 0; // 目標時間（秒単位の整数）
        public int updateInterval { get; private set; } = 0; // 更新間隔（秒単位の整数）
        public string message { get; private set; } = ""; // メッセージ
        public string[] target_extensions { get; private set; } = { }; // 対応拡張子
        public string workingFolderPath { get; private set; } = ""; // 作業フォルダのパス
        public int save_Interval { get; private set; } = 0;
        public float penalty_rate { get; private set; } = 0;

        private string default_value_file_path = "default_value.json";
        private string[] compressed_files = { };

        public InputSettingsForm()
        {
            if (((string?)MainForm.setting_dict["language"] ?? "") == "japanese")
            {
                LanguageManager.LoadLanguage("ja");
            }
            else if (((string?)MainForm.setting_dict["language"] ?? "") == "english")
            {
                LanguageManager.LoadLanguage("en");
            }

            InitializeComponent();
            // 言語をロード（例として"ja"を想定）

            if (File.Exists(default_value_file_path)) {
                try
                {
                    JObject default_vakue_dict = JObject.Parse(File.ReadAllText(default_value_file_path));

                    numericUpDownHours.Value = (int?)default_vakue_dict[nameof(targetTime)]?["hours"] ?? 1;
                    numericUpDownMinutes.Value = (int?)default_vakue_dict[nameof(targetTime)]?["minutes"] ?? 0;
                    numericUpDownSeconds.Value = (int?)default_vakue_dict[nameof(targetTime)]?["seconds"] ?? 0;

                    numericUpDownUpdateMinutes.Value = (int?)default_vakue_dict[nameof(updateInterval)]?["minutes"] ?? 1;
                    numericUpDownUpdateSeconds.Value = (int?)default_vakue_dict[nameof(updateInterval)]?["seconds"] ?? 0;

                    numericUpDownSaveInterval.Value= (int?)default_vakue_dict[nameof(save_Interval)] ?? 30;

                    textBoxPenaltyMultiplier.Text = ( Math.Floor(((float?)default_vakue_dict[nameof(penalty_rate)] ?? 0.3) * 100000f) / 100000f).ToString();

                    textBoxWorkFolder.Text= (string?)default_vakue_dict[nameof(workingFolderPath)] ?? "";
                    textBoxFileExtension.Text = (string?)default_vakue_dict["target_extensions_joind"] ?? "txt,py,cs";


                }
                catch (JsonReaderException)
                {
                    // 修正：日本語文字列を JSON から取得
                    MainForm.WriteLog("default_value.jsonの構文エラー");
                }
                catch (Exception ex)
                {
                    // 修正：日本語文字列を JSON から取得 + 例外メッセージは連結
                    MessageBox.Show(LanguageManager.GetText("unexpected_error_prefix") + ex.Message);
                }
            }
        }

        private void select_working_foloder(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                // 「作業フォルダを選択してください。」 → JSON化
                folderDialog.Description = LanguageManager.GetText("select_folder_description");

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    textBoxWorkFolder.Text = folderDialog.SelectedPath;
                }
                else
                {
                    // 特にメッセージ表示無し
                }
            }
        }

        private void working_foloder_DragEnter(object sender, DragEventArgs e)
        {
            // ドラッグされたデータがファイル/フォルダを含んでいるか確認
            if (e.Data!.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop)!;

                // 最初のアイテムがフォルダであるか確認
                if (paths.Length > 0 && Directory.Exists(paths[0]))
                {
                    e.Effect = DragDropEffects.Copy;  // フォルダならドロップ可能
                }
                else
                {
                    e.Effect = DragDropEffects.None;  // フォルダ以外ならドロップ不可
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;  // データ形式が不明ならドロップ不可
            }
        }

        private void working_foloder_DragDrop(object sender, DragEventArgs e)
        {
            // ドラッグされたデータを取得
            if (e.Data!.GetDataPresent(DataFormats.FileDrop))
            {
                string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop)!;

                // 最初のアイテムがフォルダならテキストボックスにセット
                if (paths.Length > 0 && Directory.Exists(paths[0]))
                {
                    textBoxWorkFolder.Text = paths[0]; // フォルダのパスを設定
                }
            }
        }

        private void btnCreateZip_Click(object sender, EventArgs e)
        {
            var selectionOption = MessageBox.Show(
                LanguageManager.GetText("zip_or_file_question"),
                LanguageManager.GetText("option_title"),
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (selectionOption == DialogResult.Cancel)
            {
                return; // キャンセルされた場合は何もしない
            }

            string selectedPath = string.Empty;
            string zipFileName = string.Empty;

            if (selectionOption == DialogResult.Yes)
            {
                // フォルダ選択
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = LanguageManager.GetText("folder_dialog_description");
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        selectedPath = folderDialog.SelectedPath;
                        zipFileName = Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory,
                            Path.GetFileName(selectedPath) + $"{DateTime.Now:yyyy-MM-dd HH-mm-ss}.zip"
                        );
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else if (selectionOption == DialogResult.No)
            {
                // ファイル選択
                using (OpenFileDialog fileDialog = new OpenFileDialog())
                {
                    fileDialog.Title = LanguageManager.GetText("file_dialog_title");
                    fileDialog.Multiselect = true;
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        selectedPath = string.Join("<", fileDialog.FileNames);
                        if (fileDialog.FileNames.Length == 1)
                        {
                            zipFileName = Path.Combine(
                                AppDomain.CurrentDomain.BaseDirectory,
                                Path.GetFileNameWithoutExtension(fileDialog.FileName)
                                  + $"{DateTime.Now:yyyy-MM-dd HH-mm-ss}.zip"
                            );
                        }
                        else
                        {
                            zipFileName = Path.Combine(
                                AppDomain.CurrentDomain.BaseDirectory,
                                $"SelectedFiles{DateTime.Now:yyyy-MM-dd HH-mm-ss}.zip"
                            );
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }

            string password = Interaction.InputBox(
                LanguageManager.GetText("password_input_prompt"),
                LanguageManager.GetText("password_input_title"),
                "",
                -1,
                -1
            );

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(LanguageManager.GetText("no_password_error"),
                                LanguageManager.GetText("error"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.Password = password;
                    zip.AlternateEncoding = System.Text.Encoding.UTF8;
                    zip.AlternateEncodingUsage = ZipOption.Always;

                    if (selectionOption == DialogResult.Yes)
                    {//dotnetzipでの日本語対応のため仕方なくめんどくさいことをやる
                        var files = Directory.GetFiles(selectedPath, "*", SearchOption.AllDirectories);
                        foreach (var file in files)
                        {
                            string relativePath = Path.GetRelativePath(selectedPath, file);
                            zip.AddFile(file, Path.GetDirectoryName(relativePath));
                        }
                    }
                    else if (selectionOption == DialogResult.No)
                    {
                        string[] files = selectedPath.Split('<');
                        foreach (string file in files)
                        {
                            zip.AddFile(file, "");
                        }
                    }
                    zip.Save(zipFileName);
                }

                // 「パスワード付きZIPファイルを作成しました。\n保存先: 〜」 → JSON化
                messagebox.Text += (messagebox.Text == "")
                                   ? ""
                                   : (messagebox.Text[messagebox.Text.Length - 1] == '\n' ? "" : "\r\n");
                messagebox.Text += $"{zipFileName.Split('\\')[^1]} {LanguageManager.GetText("password_prefix")} {password}\r\n"
                                   + LanguageManager.GetText("original_file_location")
                                   + $"{selectedPath.Replace("<", "\r\n")}\r\n";

                MessageBox.Show(
                    LanguageManager.GetText("zipfile_created") + zipFileName,
                    LanguageManager.GetText("success"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                compressed_files = compressed_files.Concat(selectedPath.Split("<")).ToArray();
            }
            catch (Exception ex)
            {
                // 「エラーが発生しました: {0}」 → JSON化
                MessageBox.Show(
                    LanguageManager.GetText("error_occured").Replace("{0}", ex.Message),
                    LanguageManager.GetText("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void btnCreate7z_Click(object sender, EventArgs e)
        {
            // 圧縮対象の選択（フォルダ or ファイル）をユーザーに問い合わせる
            var selectionOption = MessageBox.Show(
                LanguageManager.GetText("zip_or_file_question"),
                LanguageManager.GetText("option_title"),
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question
            );

            if (selectionOption == DialogResult.Cancel)
            {
                return; // キャンセルされた場合は何もしない
            }

            string selectedPath = string.Empty;
            string archiveFileName = string.Empty;
            List<string> filesToCompress = new List<string>();

            if (selectionOption == DialogResult.Yes)
            {
                // フォルダ選択の場合
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = LanguageManager.GetText("folder_dialog_description");
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        selectedPath = folderDialog.SelectedPath;
                        if(死ね死ね死ね死ね(selectedPath)){
                            MessageBox.Show($"{LanguageManager.GetText("select_folder_empty")}");
                            return;
                        }
                        // アーカイブファイル名を作成（拡張子は .7z）
                        archiveFileName = Path.Combine(
                            AppDomain.CurrentDomain.BaseDirectory,
                            Path.GetFileName(selectedPath) + $"{DateTime.Now:yyyy-MM-dd HH-mm-ss}.7z"
                        );

                        // フォルダ内の全ファイル（サブフォルダも含む）を取得
                        filesToCompress.AddRange(Directory.GetFiles(selectedPath, "*", SearchOption.AllDirectories));
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else if (selectionOption == DialogResult.No)
            {
                // ファイル選択の場合（複数選択可）
                using (OpenFileDialog fileDialog = new OpenFileDialog())
                {
                    fileDialog.Title = LanguageManager.GetText("file_dialog_title");
                    fileDialog.Multiselect = true;
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        filesToCompress.AddRange(fileDialog.FileNames);

                        if (fileDialog.FileNames.Length == 1)
                        {
                            archiveFileName = Path.Combine(
                                AppDomain.CurrentDomain.BaseDirectory,
                                Path.GetFileNameWithoutExtension(fileDialog.FileName)
                                  + $"{DateTime.Now:yyyy-MM-dd HH-mm-ss}.7z"
                            );
                        }
                        else
                        {
                            archiveFileName = Path.Combine(
                                AppDomain.CurrentDomain.BaseDirectory,
                                $"SelectedFiles{DateTime.Now:yyyy-MM-dd HH-mm-ss}.7z"
                            );
                        }
                        // 複数ファイルの場合、元々のパス情報は（必要ならば）個別に保持できます
                        // ※ 以下の messagebox 表示用に selectedPath に結合しておく
                        selectedPath = string.Join(Environment.NewLine, fileDialog.FileNames);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            // パスワード入力（※パスワードは7zアーカイブの暗号化に利用される）
            string password = Interaction.InputBox(
                LanguageManager.GetText("password_input_prompt"),
                LanguageManager.GetText("password_input_title"),
                "",
                -1,
                -1
            );

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    LanguageManager.GetText("no_password_error"),
                    LanguageManager.GetText("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            try
            {
                // ※ 必ず 7z.dll のパスをセットしてください（プロジェクト初期化時やここで一度設定してもよい）
                // 例:
                string sevenZipPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "7z.dll");
                SevenZipBase.SetLibraryPath(sevenZipPath);

                SevenZipCompressor compressor = new SevenZipCompressor();
                try
                {
                    // アーカイブ形式は7zに設定
                    compressor.ArchiveFormat = OutArchiveFormat.SevenZip;
                    // 圧縮レベルやアルゴリズムはお好みで設定（ここでは Normal と LZMA2 を利用）
                    compressor.CompressionLevel = CompressionLevel.Normal;
                    compressor.CompressionMethod = SevenZip.CompressionMethod.Lzma2;
                    // ファイル名（ヘッダー情報）を暗号化する（※7z形式の特徴）
                    compressor.EncryptHeaders = true;

                    // フォルダ全体を圧縮する場合は CompressDirectory を利用すると、
                    // フォルダ構造を保持したまま圧縮できます。
                    if (selectionOption == DialogResult.Yes)
                    {
                        // CompressDirectory 内部で再帰的にファイルを追加します
                        // ※ 内部でファイル名のエンコードも行われるため、特別な設定は不要です
                        compressor.CompressDirectory(selectedPath, archiveFileName, password);
                    }
                    else if (selectionOption == DialogResult.No)
                    {
                        // 複数ファイルの場合は CompressFilesEncrypted メソッドを利用
                        // ※ ファイルのディレクトリ情報は保存されず、全てアーカイブ直下に配置されます
                        compressor.CompressFilesEncrypted(archiveFileName, password, filesToCompress.ToArray());
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show($"Unexpected error: {ex.Message}");
                }
                finally
                {
                    // compressor に Dispose() があれば呼ぶ、なければ不要
                }
                // SevenZipCompressor のインスタンスを生成

                

                // 結果表示（元のコードと同様にメッセージボックスへ情報出力）
                // ※ messagebox はここでは TextBox などに出力する例です
                messagebox.Text += (messagebox.Text == "")
                    ? ""
                    : (messagebox.Text[messagebox.Text.Length - 1] == '\n' ? "" : "\r\n");
                messagebox.Text += $"{Path.GetFileName(archiveFileName)} {LanguageManager.GetText("password_prefix")} {password}\r\n"
                                   + LanguageManager.GetText("original_file_location")
                                   + $"{selectedPath}\r\n";

                MessageBox.Show(
                    LanguageManager.GetText("zipfile_created") + archiveFileName,
                    LanguageManager.GetText("success"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                // 圧縮したファイルの一覧に追加
                compressed_files = compressed_files.Concat(filesToCompress).ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    LanguageManager.GetText("error_occured").Replace("{0}", ex.Message),
                    LanguageManager.GetText("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        static bool 死ね死ね死ね死ね(string path)
    {
        if (!Directory.Exists(path))
        {
            throw new DirectoryNotFoundException($"指定されたディレクトリが存在しません: {path}");
        }

        // 現在のディレクトリにファイルやフォルダがあるか確認
        if (Directory.EnumerateFiles(path).Any())
        {
            return false;
        }

        // サブフォルダを取得し、それぞれ中身を確認（再帰処理）
        foreach (var subDir in Directory.EnumerateDirectories(path))
        {
            if (!死ね死ね死ね死ね(subDir)) // 再帰的にチェック
            {
                return false;
            }
        }

        return true; // すべてのサブフォルダも空なら空と判定
    }

        public static void CopyFolderToExecutableLocation(string sourcePath)
        {
            try
            {
                if (IsExecutableFolderInside(sourcePath))
                {
                    // 「エラーが発生しました、作業フォルダ内にこのソフトが含まれないようにしてください」 → JSON化
                    MessageBox.Show(LanguageManager.GetText("including_software_folder"),
                                    LanguageManager.GetText("error"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                string exePath = AppDomain.CurrentDomain.BaseDirectory;
                string destinationPath = Path.Combine(exePath, Path.GetFileName(sourcePath));

                if (!Directory.Exists(sourcePath))
                {
                    throw new DirectoryNotFoundException(
                        LanguageManager.GetText("folder_not_found_prefix") + sourcePath
                    );
                }

                if (Directory.Exists(destinationPath))
                {
                    Console.WriteLine(LanguageManager.GetText("destination_folder_exists_prefix") + destinationPath);
                    Directory.Delete(destinationPath, true);
                }

                void CopyDirectory(string src, string dest)
                {
                    Directory.CreateDirectory(dest);
                    foreach (string filePath in Directory.GetFiles(src))
                    {
                        string destFilePath = Path.Combine(dest, Path.GetFileName(filePath));
                        File.Copy(filePath, destFilePath, true);
                    }
                    foreach (string directoryPath in Directory.GetDirectories(src))
                    {
                        string destDirectoryPath = Path.Combine(dest, Path.GetFileName(directoryPath));
                        CopyDirectory(directoryPath, destDirectoryPath);
                    }
                }

                CopyDirectory(sourcePath, destinationPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    LanguageManager.GetText("error_occured").Replace("{0}", ex.Message),
                    LanguageManager.GetText("error"),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        public static bool IsExecutableFolderInside(string folderPath)
        {
            try
            {
                string exeFolderPath = AppDomain.CurrentDomain.BaseDirectory;
                string fullFolderPath = Path.GetFullPath(folderPath);
                string fullExeFolderPath = Path.GetFullPath(exeFolderPath);
                return fullExeFolderPath.StartsWith(fullFolderPath, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                Console.WriteLine(LanguageManager.GetText("error_occured").Replace("{0}", ex.Message));
                return false;
            }
        }

        private void save_default_value()
        {
            string target_extensions_joind = textBoxFileExtension.Text;
            target_extensions_joind = target_extensions_joind.Replace(".", "");
            target_extensions_joind = target_extensions_joind.Replace(" ", "");
            JObject default_value_json_dict = new JObject
            {
                 { nameof(targetTime), new JObject{
                     {"hours",(int)numericUpDownHours.Value },
                     {"minutes",(int)numericUpDownMinutes.Value },
                     {"seconds",(int)numericUpDownSeconds.Value }
                 } },
                 { nameof(updateInterval), new JObject{
                     {"minutes",(int)numericUpDownUpdateMinutes.Value },
                     {"seconds",(int)numericUpDownUpdateSeconds.Value }
                 } },
                {nameof(save_Interval),(int)numericUpDownSaveInterval.Value },
                {nameof(penalty_rate),float.Parse(textBoxPenaltyMultiplier.Text) },
                {nameof(workingFolderPath),textBoxWorkFolder.Text },
                {nameof(target_extensions_joind),target_extensions_joind }
            };
            File.WriteAllText(default_value_file_path, default_value_json_dict.ToString());
        }

        private void exit_setting_form(object sender, EventArgs e)
        {
            targetTime = 3600 * (int)numericUpDownHours.Value
                       + 60 * (int)numericUpDownMinutes.Value
                       + (int)numericUpDownSeconds.Value;
            updateInterval = 60 * (int)numericUpDownUpdateMinutes.Value
                           + (int)numericUpDownUpdateSeconds.Value;

            if (targetTime <= 0)
            {
                // 「目標時間は1秒以上です」 → JSON化
                MessageBox.Show(LanguageManager.GetText("target_time_at_least_1sec"));
                return;
            }
            if (updateInterval <= 0)
            {
                // 「判定間隔は1秒以上です」 → JSON化
                MessageBox.Show(LanguageManager.GetText("update_interval_at_least_1sec"));
                return;
            }

            save_Interval = (int)numericUpDownSaveInterval.Value;

            float _penalty_rate;
            if (!float.TryParse(textBoxPenaltyMultiplier.Text, out _penalty_rate))
            {
                // 「ペナルティ倍率は0以上の数です」 → JSON化
                MessageBox.Show(LanguageManager.GetText("penalty_invalid"));
                return;
            }
            else if(float.Parse(textBoxPenaltyMultiplier.Text)<0){
                MessageBox.Show(LanguageManager.GetText("penalty_invalid"));
                return;
            }
            penalty_rate = float.Parse(textBoxPenaltyMultiplier.Text);

            message = MainForm.Encrypt(messagebox.Text);
            if (message == "")
            {
                // 「報酬テキストを入力してください」 → JSON化
                MessageBox.Show(LanguageManager.GetText("reward_text_empty"));
                return;
            }

            workingFolderPath = textBoxWorkFolder.Text;
            if (workingFolderPath == "")
            {
                // 「作業フォルダのパスを入力してください」 → JSON化
                MessageBox.Show(LanguageManager.GetText("work_folder_empty"));
                return;
            }

            string target_extensions_joind = textBoxFileExtension.Text;
            if (target_extensions_joind == "")
            {
                // 「対象の拡張子を入力してください(　, で区切る)」 → JSON化
                MessageBox.Show(LanguageManager.GetText("exts_empty"));
                return;
            }
            target_extensions_joind = target_extensions_joind.Replace(".", "");
            target_extensions_joind = target_extensions_joind.Replace(" ", "");
            target_extensions = target_extensions_joind.Split(',');

            if (compressed_files.Length > 0 && ((bool?)MainForm.setting_dict["delete_originalFile"] ?? true))
            {
                // 「ZIP化したファイルの元ファイルを削除しますか?(非推奨)」 → JSON化
                // 「選択オプション」 → JSON化
                var selectionOption_delete = MessageBox.Show(
                    LanguageManager.GetText("delete_original_prompt"),
                    LanguageManager.GetText("delete_option"),
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );
                if (selectionOption_delete == DialogResult.Yes)
                {
                    var selectionOption_delete2 = MessageBox.Show(
        LanguageManager.GetText("delete_warning"),
        LanguageManager.GetText("delete_option"),
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );
                    if (selectionOption_delete2 == DialogResult.Yes)
                    {
                        List<string> failedPaths = new List<string>();
                        foreach (string path in compressed_files)
                        {
                            try
                            {
                                if (File.Exists(path))
                                {
                                    File.Delete(path);
                                }
                                else if (Directory.Exists(path))
                                {
                                    Directory.Delete(path, true);
                                }
                                else
                                {
                                    failedPaths.Add(path);
                                }
                            }
                            catch (UnauthorizedAccessException)
                            {
                                failedPaths.Add(path);
                            }
                            catch (IOException)
                            {
                                failedPaths.Add(path);
                            }
                            catch (Exception ex)
                            {
                                failedPaths.Add(path);
                                Console.WriteLine($"Unexpected error: {ex.Message}");
                            }
                        }

                        if (failedPaths.Count > 0)
                        {
                            // 「以下のパスは削除できませんでした:\n」 → JSON化
                            // 「削除失敗」 → JSON化
                            string failedMessage = LanguageManager.GetText("delete_failed_prefix")
                                                   + string.Join("\n", failedPaths);
                            MessageBox.Show(
                                failedMessage,
                                LanguageManager.GetText("delete_failed_title"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                        }
                        else
                        {
                            // 「すべてのファイルとフォルダが正常に削除されました。」 → JSON化
                            // 「完了」 → JSON化
                            MessageBox.Show(
                                LanguageManager.GetText("delete_done"),
                                LanguageManager.GetText("delete_done_title"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information
                            );
                        }
                    }
                }

            }

            if ((bool?)MainForm.setting_dict["strict_text"]?["active"] ?? false)
            {
                CopyFolderToExecutableLocation(workingFolderPath);
            }
            save_default_value();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    
        private void edit_advanced_sttings(object sender, EventArgs e)
        {
            using (var advanced_settingsForm = new advanced_settingsForm())
            {
                if (advanced_settingsForm.ShowDialog() == DialogResult.OK)
                {
                }
                else
                {
                }
            }
        }
    }
}
