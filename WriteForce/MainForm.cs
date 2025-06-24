using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Timers;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DiffMatchPatch;
using System.Media;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;


namespace WriteForce
{

    public partial class MainForm : Form
    {
        private static string language_file_dummy = "ja";
        public static string language_file { get => language_file_dummy!; set => language_file_dummy = value; }
        private string file1Path = "encryptedmessage.dat";
        public static string setting_path = "advanced_settings.json";

        // 32バイトのキーと16バイトのIV（例として設定）
        public static byte[] key = Encoding.UTF8.GetBytes("12345678901234567890123456789012");
        public static byte[] iv = Encoding.UTF8.GetBytes("1234567890123456");
        private int targetTime; // 目標時間（秒単位）
        private int updateInterval; // 更新間隔（秒単位）
        private string message_encrypted = ""; // メッセージ
        private string[] target_extensions = { }; // 対応拡張子
        private string workingFolderPath = ""; // 作業フォルダのパス
        private EncryptedInt cumulativeTime = new EncryptedInt(0); // 累計時間
        private int saved_cumulativeTime; // セーブ済み累計時間
        private int time_to_next_count;
        private int save_Interval;
        private int count_to_next_save;
        private float penalty_rate;

        private bool hide_text_flag = false;

        private bool strict_text_mode;

        // テキスト厳格判定モード限定の変数
        private int min_number_of_inserted_characters = 0;
        private int history_length = 0;
        private int min_char_types = 0;
        private bool japanese_only = false;
        private bool english_only = false;
        private float similarityThreshold = 0;
        private Queue<string> difference_history_fifo = new Queue<string>();

        private System.Timers.Timer timer;
        private System.Timers.Timer timer2;
        private static JObject setting_dict1 = new JObject();
        public static JObject setting_dict { get => setting_dict1!; set => setting_dict1 = value; }

        public static bool debug_mode { get; private set; } = false;

        // Windows APIの関数をインポートc
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // ------------------------------
        // 言語管理クラス
        // ------------------------------
        public static class LanguageManager
        {
            private static Dictionary<string, string> translations = new Dictionary<string, string>();
            public static void LoadLanguage(string lang)
            {
                string filePath = $"locales/{lang}.json";
                if (File.Exists(filePath))
                {
                    string jsonText = File.ReadAllText(filePath);
                    translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonText)!;

                }
            }
            public static string GetText(string key)
            {
                return translations.ContainsKey(key) ? translations[key] : key;
            }
        }

        public MainForm()
        {
            if (File.Exists(setting_path))
            {
                try
                {
                    setting_dict = JObject.Parse(File.ReadAllText(setting_path));

                    if (((string?)setting_dict["language"] ?? "") == "japanese")
                    {
                        language_file = "ja"; // .jsonは付けない
                    }
                    else if (((string?)setting_dict["language"] ?? "") == "english")
                    {
                        language_file = "en";
                    }
                    debug_mode = (bool?)setting_dict["debug"] ?? false;
                }
                catch (JsonReaderException)
                {
                    // 修正：日本語文字列を JSON から取得
                    MessageBox.Show(LanguageManager.GetText("advanced_setting_json_error"));
                }
                catch (Exception ex)
                {
                    // 修正：日本語文字列を JSON から取得 + 例外メッセージは連結
                    MessageBox.Show(LanguageManager.GetText("unexpected_error_prefix") + ex.Message);
                }
            }


            // 言語ファイルをロード
            LanguageManager.LoadLanguage(language_file);

            InitializeComponent();


            // 起動時の処理
            if (!File.Exists(file1Path))
            {
                // ファイルがない場合は設定入力画面
                InputSettings();
            }
            else
            {
                // すでにある場合は読み込み
                LoadSettings();
            }

            // テキスト厳格モードの場合の初期チェック
            if (strict_text_mode)
            {
                if (InputSettingsForm.IsExecutableFolderInside(workingFolderPath!))
                {
                    // 修正：出力される日本語を JSON 参照
                    WriteLog(LanguageManager.GetText("error_including_software_folder"));
                    MessageBox.Show(LanguageManager.GetText("error_including_software_folder"),
                                    LanguageManager.GetText("error"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    System.Windows.Forms.Application.Exit();
                }
                // 修正：日本語文字列を JSON から取得
                // 「日本語 / english」+「テキスト厳格モードオン」
                messagebox.Text = (japanese_only ? LanguageManager.GetText("japanese_text_strict_mode")
                                                 : english_only ? LanguageManager.GetText("english_text_strict_mode")
                                                                : "")
                                  + " " + LanguageManager.GetText("text_strict_mode_on");
            }

            saved_cumulativeTime = cumulativeTime;
            // 修正：各種ラベルをJSON化
            lblCumulativeTime.Text = LanguageManager.GetText("lblCumulativeTimePrefix")
                                    + ConvertSecondsToTimeString(cumulativeTime)
                                    + "  " + (save_Interval == 1 ? ""
                                       : LanguageManager.GetText("save_done") + ConvertSecondsToTimeString(saved_cumulativeTime));

            TargetTime_label.Text = LanguageManager.GetText("lblTargetTimePrefix") + ConvertSecondsToTimeString(targetTime);

            folder_path.Text = workingFolderPath;

            target_extension_text.Text = LanguageManager.GetText("target_extension_text_prefix")
                                         + string.Join(",", target_extensions!);

            string[] files = target_extensions!
                .SelectMany(ext => Directory.GetFiles(workingFolderPath!, "*." + ext, SearchOption.AllDirectories))
                .ToArray();
            number_of_files.Text = LanguageManager.GetText("file_count_prefix") + files.Length.ToString();

            count_to_next_save = save_Interval;
            // 修正：メッセージを JSON で管理
            if (save_Interval == 1)
            {
                count_to_next_save_text.Text = LanguageManager.GetText("count_to_next_save_text_always");
            }
            else
            {
                // 「次のセーブまで後{0}回の更新が必要」を組み立て
                count_to_next_save_text.Text = LanguageManager.GetText("count_to_next_save_text_prefix")
                                               + count_to_next_save.ToString()
                                               + LanguageManager.GetText("count_to_next_save_text_suffix");
            }

            penalty_rate_text.Text = LanguageManager.GetText("penalty_rate_text_prefix")
                                     + Math.Round(penalty_rate, 2).ToString();

            time_to_next_count = updateInterval;

            if (hide_text_flag)
            {
                time_to_next_update_labal.ForeColor = SystemColors.Control;
            }

            // タイマーの設定（ユーザー指定の更新間隔）
            timer = new System.Timers.Timer(updateInterval * 1000);
            timer.Elapsed += Timer_Elapsed!;
            timer.Start();

            timer2 = new System.Timers.Timer(1000);
            timer2.Elapsed += Timer_Elapsed2!;
            timer2.Start();
            if (cumulativeTime >= targetTime)
            {
                end_write();
            }


            this.FormClosing += (s, e) =>
            {
                bool prevent_exit_flag = (bool?)setting_dict["prevent_exit"] ?? false;
                if (prevent_exit_flag & cumulativeTime < targetTime)
                {
                    e.Cancel = true; // 閉じるのをキャンセル
                }
            };
        }

        private void InputSettings()
        {
            using (var settingsForm = new InputSettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    targetTime = settingsForm.targetTime;
                    updateInterval = settingsForm.updateInterval;
                    save_Interval = settingsForm.save_Interval;
                    penalty_rate = settingsForm.penalty_rate;
                    message_encrypted = settingsForm.message;
                    workingFolderPath = settingsForm.workingFolderPath;
                    target_extensions = settingsForm.target_extensions;

                    hide_text_flag = (bool?)setting_dict["hide_text"] ?? false;

                    strict_text_mode = (bool?)setting_dict["strict_text"]?["active"] ?? false;

                    min_number_of_inserted_characters = (int?)setting_dict["strict_text"]?["min_number_of_inserted_characters"] ?? 3;
                    history_length = (int?)setting_dict["strict_text"]?["history_length"] ?? 5;
                    min_char_types = (int?)setting_dict["strict_text"]?["min_char_types"] ?? 2;
                    japanese_only = (bool?)setting_dict["strict_text"]?["japanese_only"] ?? false;
                    english_only = (bool?)setting_dict["strict_text"]?["english_only"] ?? false;
                    similarityThreshold = (float?)setting_dict["strict_text"]?["similarityThreshold"] ?? 0.4f;
                    cumulativeTime = 0;
                    save_param();
                }
                else
                {
                    System.Windows.Forms.Application.Exit();
                }
            }
        }

        private void LoadSettings()
        {
            try
            {
                string encryptedData = File.ReadAllText(file1Path);
                JObject decryptedData = JObject.Parse(Decrypt(encryptedData));
                cumulativeTime = (int?)decryptedData["cumulativeTime"] ?? 0;
                targetTime = (int?)decryptedData["targetTime"] ?? 0;
                updateInterval = (int?)decryptedData["updateInterval"] ?? 0;
                save_Interval = (int?)decryptedData["save_Interval"] ?? 0;
                penalty_rate = (float?)decryptedData["penalty_rate"] ?? 0;
                workingFolderPath = (string?)decryptedData["workingFolderPath"] ?? "";
                target_extensions = decryptedData["target_extensions"]?.ToObject<string[]>() ?? Array.Empty<string>();
                message_encrypted = (string?)decryptedData["message"] ?? "";

                hide_text_flag = (bool?)decryptedData["hide_text"] ?? false;

                strict_text_mode = (bool?)decryptedData["strict_text"]?["active"] ?? false;
                if (strict_text_mode)
                {
                    min_number_of_inserted_characters = (int?)decryptedData["strict_text"]?["min_number_of_inserted_characters"] ?? 3;
                    history_length = (int?)decryptedData["strict_text"]?["history_length"] ?? 5;
                    min_char_types = (int?)decryptedData["strict_text"]?["min_char_types"] ?? 2;
                    japanese_only = (bool?)decryptedData["strict_text"]?["japanese_only"] ?? false;
                    english_only = (bool?)decryptedData["strict_text"]?["english_only"] ?? false;
                    similarityThreshold = (float?)decryptedData["strict_text"]?["similarityThreshold"] ?? 0.4f;
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
            }
        }

        static string ConvertSecondsToTimeString(int totalSeconds)
        {
            // 日、時間、分、秒を計算
            int days = totalSeconds / (24 * 60 * 60);
            totalSeconds %= (24 * 60 * 60);
            int hours = totalSeconds / (60 * 60);
            totalSeconds %= (60 * 60);
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;

            // "日:時間:分:秒" の形式
            // ここは日本語文字列ではなくフォーマット動的部分なのでそのまま
            return $"{days}:{hours:D2}:{minutes:D2}:{seconds:D2}";
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            time_to_next_count = updateInterval;

            bool hasRecentFile = HasRecentFiles();

            if (hasRecentFile)
            {
                cumulativeTime += updateInterval;
                count_to_next_save--;
                if (count_to_next_save <= 0)
                {
                    save_param();
                    if ((bool?)setting_dict["save_alarm"] ?? false)
                    {
                        SystemSounds.Asterisk.Play();
                    }
                    saved_cumulativeTime = cumulativeTime;
                    count_to_next_save = save_Interval;

                }

                if (save_Interval == 1)
                {
                    count_to_next_save_text.Text = LanguageManager.GetText("count_to_next_save_text_always");
                }
                else
                {
                    if (hide_text_flag)
                    {
                        count_to_next_save_text.Text = LanguageManager.GetText("count_to_next_save_text_prefix")
                                                   + LanguageManager.GetText("hide_text_prefix") + (((count_to_next_save + 9) / 10) * 10).ToString()//後で"以下"のテキストを付ける
                                                   + LanguageManager.GetText("count_to_next_save_text_suffix");
                    }
                    else
                    {
                        count_to_next_save_text.Text = LanguageManager.GetText("count_to_next_save_text_prefix")
                                                   + count_to_next_save.ToString()
                                                   + LanguageManager.GetText("count_to_next_save_text_suffix");
                    }
                }
            }
            else
            {
                cumulativeTime = cumulativeTime - (int)(penalty_rate * updateInterval);
                if (cumulativeTime < saved_cumulativeTime)
                {
                    cumulativeTime = saved_cumulativeTime;
                }
            }

            if (hide_text_flag)
            {
                List<int> separator_list = new List<int> { 20, 30, 60, 180, 600, 1200 }; //どこの数字以下かでまとめる単位が決まる
                int left = 0, right = separator_list.Count - 1;
                int resultIndex = right; // 初期値を最大のインデックスに

                while (left <= right)
                {
                    int mid = left + (right - left) / 2;

                    if (separator_list[mid] > updateInterval)
                    {
                        resultIndex = mid;
                        right = mid - 1; // より小さい値を探す
                    }
                    else
                    {
                        left = mid + 1;
                    }
                }//update_intervalは不変なので実は毎回実行する必要はない、めんどくさいからここにまとめてる

                int separator = 60;//実際は区切りというよりまとめる単位、変数名が正しいかは微妙

                switch (separator_list[resultIndex])//ここでどれぐらいの単位でまとめるかを決める
                {
                    case 20:
                        separator = 10;//2分
                        break;
                    case 30:
                        separator = 300;//5分
                        break;
                    case 60:
                        separator = 600;//10分
                        break;
                    case 180:
                        separator = 1200;//20分
                        break;
                    case 600:
                        separator = 3600;//1時間
                        break;
                    case 1200:
                        separator = 5400;//1.5時間
                        break;
                }


                lblCumulativeTime.Text = LanguageManager.GetText("lblCumulativeTimePrefix")
                                     + " " + LanguageManager.GetText("hide_text_prefix") +
                                     ConvertSecondsToTimeString(((cumulativeTime) / separator) * separator)//端数を切り捨てる
                                     + "  " + LanguageManager.GetText("save_done") + ConvertSecondsToTimeString(saved_cumulativeTime);
                //(save_Interval == 1 ? "": LanguageManager.GetText("save_done") + ConvertSecondsToTimeString(saved_cumulativeTime));
            }
            else
            {
                lblCumulativeTime.Text = LanguageManager.GetText("lblCumulativeTimePrefix")
                                     + "  " + ConvertSecondsToTimeString(cumulativeTime)
                                     + (save_Interval == 1 ? ""
                                        : LanguageManager.GetText("save_done") + ConvertSecondsToTimeString(saved_cumulativeTime));
            }


            if (cumulativeTime >= targetTime)
            {
                end_write();
            }
        }


        private void end_write()
        {
            timer.Stop();
            timer2.Stop();
            string message_true = "";
            save_param();
            try
            {
                message_true = Decrypt(message_encrypted);
            }
            catch
            {
                message_true = message_encrypted;
            }
            WriteStringToFile(message_true!);//ある場合は上書きされる
            messagebox.Text = message_true!;
            this.Controls.Remove(this.Controls["penalty_rate_text"]);
            this.Controls.Remove(this.Controls["number_of_files"]);
            this.Controls.Remove(this.Controls["folder_path"]);
            this.Controls.Remove(this.Controls["target_extension_text"]);


            if (this.IsHandleCreated)
            {
                try
                {
                    this.Invoke((MethodInvoker)delegate
                        {
                            bool x_spread = (bool?)setting_dict["x_shere_message"] ?? false;
                            Button restartbutton = new Button();
                            restartbutton.Location = new Point(16, 331);
                            restartbutton.Name = "restartbutton";
                            restartbutton.Size = new Size(493, 38);
                            restartbutton.TabIndex = 9;
                            // 修正：日本語文字列を JSON から取得
                            restartbutton.Text = LanguageManager.GetText("restart_button");
                            restartbutton.UseVisualStyleBackColor = true;
                            restartbutton.Click += restart!;
                            this.Controls.Add(restartbutton);
                            string msg = "";
                            if (x_spread)
                            {
                                msg = LanguageManager.GetText("arrived_targettime_spread");
                                var spread_decision = MessageBox.Show(msg, LanguageManager.GetText("arrived_targettime_title"), MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                                if (spread_decision == DialogResult.Yes)
                                {
                                    OpenUrl($"https://x.com/intent/post?text={(cumulativeTime / 3600).ToString() + LanguageManager.GetText("labelHours") + ((cumulativeTime % 3600) / 60).ToString() + LanguageManager.GetText("labelMinutes") + LanguageManager.GetText("X_message")}");
                                }
                                else
                                {
                                    MessageBox.Show(LanguageManager.GetText("arrived_targettime_spread_denied"));
                                }
                            }
                            else
                            {
                                msg = LanguageManager.GetText("arrived_targettime").Replace("{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                                MessageBox.Show(msg, LanguageManager.GetText("arrived_targettime_title"));
                            }
                        });

                }
                catch (Exception ex)
                {
                    WriteLog($"ログの書き込みに失敗しました: {ex.Message}");
                }
            }
            else
            {//立ち上げ時の実行の場合はinvokeなしでも動作する
                Button restartbutton = new Button();
                restartbutton.Location = new Point(16, 331);
                restartbutton.Name = "restartbutton";
                restartbutton.Size = new Size(493, 38);
                restartbutton.TabIndex = 9;
                // 修正：日本語文字列を JSON から取得
                restartbutton.Text = LanguageManager.GetText("restart_button");
                restartbutton.UseVisualStyleBackColor = true;
                restartbutton.Click += restart!;
                this.Controls.Add(restartbutton);
            }
        }


        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true  // .NET Core 3.0以降では必須
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"URLを開く際にエラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Timer_Elapsed2(object sender, ElapsedEventArgs e)
        {
            // 修正：文字列を JSON から参照
            time_to_next_update_labal.Text = LanguageManager.GetText("time_to_next_judgement_prefix")
                                             + ConvertSecondsToTimeString(time_to_next_count);
            time_to_next_count--;
        }

        private bool HasRecentFiles()
        {
            try
            {
                var files = target_extensions
                    .SelectMany(ext => Directory.GetFiles(workingFolderPath, "*." + ext, SearchOption.AllDirectories))
                    .ToArray();

                if (InputSettingsForm.IsExecutableFolderInside(workingFolderPath))
                {
                    // 修正：ここも JSON化
                    MessageBox.Show(LanguageManager.GetText("error_including_software_folder"),
                                    LanguageManager.GetText("error"),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    System.Windows.Forms.Application.Exit();
                }

                DateTime now = DateTime.Now;
                foreach (var file in files)
                {
                    DateTime lastWriteTime = File.GetLastWriteTime(file);
                    if ((now - lastWriteTime).TotalSeconds <= updateInterval && lastWriteTime <= now)
                    {
                        if (strict_text_mode)
                        {
                            if (CheckFile(file)) return true;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                // エラー時はログだけ
            }
            return false;
        }

        private bool CheckFile(string workingFilePath)
        {
            try
            {
                if (debug_mode)
                {
                    WriteLog($"対象ファイル:{workingFilePath}");
                }
                string exeFolderPath = AppDomain.CurrentDomain.BaseDirectory;
                string workingFolderName = new DirectoryInfo(workingFolderPath).Name;
                string copiedFolderPath = Path.Combine(exeFolderPath, workingFolderName);

                if (!Directory.Exists(copiedFolderPath))
                {
                    InputSettingsForm.CopyFolderToExecutableLocation(workingFolderPath);
                    return false;
                }

                string relativeFilePath = Path.GetRelativePath(workingFolderPath, workingFilePath);
                string copiedFilePath = Path.Combine(copiedFolderPath, relativeFilePath);

                if (File.Exists(copiedFilePath))
                {
                    string charsToRemove = japanese_only ? " \n「」\r、。"
                                        : english_only ? ".,?!"
                                        : "";

                    string japanese_pattern = @"[\p{IsCJKUnifiedIdeographs}\p{IsHiragana}\p{IsKatakana}]";

                    string before_text = RemoveCharacters(File.ReadAllText(copiedFilePath), charsToRemove);
                    string after_text = RemoveCharacters(File.ReadAllText(workingFilePath), charsToRemove);

                    var differences = GetTextDifferences(before_text, after_text);

                    File.Copy(workingFilePath, copiedFilePath, true);

                    foreach (var diff in differences)
                    {
                        diff.text = diff.text.Trim();
                        int word_or_char_count = english_only ? Regex.Matches(diff.text, @"\b\w+\b").Count : diff.text.Length;
                        if (debug_mode && diff.operation == Operation.INSERT)
                        {
                            WriteLog($"操作{diff.operation}   テキスト : {diff.text}");
                            if (english_only) { WriteLog($"単語数:{word_or_char_count},{min_number_of_inserted_characters},{CountUniqueCharacters(diff.text)}"); }
                        }


                        if (diff.operation == Operation.INSERT
                            && word_or_char_count >= min_number_of_inserted_characters
                            && CountUniqueCharacters(diff.text) >= min_char_types
                            && (!japanese_only || Regex.IsMatch(diff.text, japanese_pattern)))
                        {
                            bool written_flag = true;
                            if (japanese_only || english_only)
                            {
                                foreach (string pastdiffer in difference_history_fifo)
                                {
                                    string pastdiffer_a = pastdiffer.Trim();

                                    float similarity = japanese_only
                                       ? CalculateOverlapRatio(pastdiffer_a, diff.text)
                                       : CalculateWordOverlapRatio(pastdiffer_a, diff.text);

                                    if (debug_mode)
                                    {
                                        WriteLog($"過去の変更:{pastdiffer_a}   類似度:{similarity}   閾値:{similarityThreshold + (1f - similarityThreshold) * (1f - Math.Exp((2f - pastdiffer_a.Length - diff.text.Length) / 50f))}");
                                    }
                                    if (similarity >= similarityThreshold + (1f - similarityThreshold) * (1f - Math.Exp((2f - pastdiffer_a.Length - diff.text.Length) / 50f)))//単語の場合は変えてもいいかも
                                    {
                                        written_flag = false;
                                    }
                                }
                                if (written_flag)
                                {
                                    var deletedList = differences.Where(d => d.operation == Operation.DELETE);
                                    foreach (Diff deleteddiffer in deletedList)
                                    {
                                        if ((float)deleteddiffer.text.Length / diff.text.Length <= 3)//コピペ防止なので削除された量がコピペした量を超えることは少ない
                                        {
                                            deleteddiffer.text = deleteddiffer.text.Trim();
                                            float similarity = japanese_only
                                       ? CalculateOverlapRatio(deleteddiffer.text, diff.text)
                                       : CalculateWordOverlapRatio(deleteddiffer.text, diff.text);
                                            float deleted_text_Threshold = 2 * similarityThreshold - similarityThreshold * similarityThreshold;

                                            if (debug_mode)
                                            {
                                                WriteLog($"削除:{deleteddiffer.text}   類似度:{similarity}   閾値:{(float)(deleted_text_Threshold + (1f - deleted_text_Threshold) * (1f - Math.Exp((2f - deleteddiffer.text.Length - diff.text.Length) / 50f)))}");
                                            }
                                            if (similarity >= deleted_text_Threshold + (1f - deleted_text_Threshold) * (1f - Math.Exp((2f - deleteddiffer.text.Length - diff.text.Length) / 50f)))
                                            {
                                                written_flag = false;
                                            }
                                        }
                                    }
                                }
                                if (written_flag)
                                {
                                    difference_history_fifo.Enqueue(diff.text);
                                    if (difference_history_fifo.Count > history_length)
                                    {
                                        difference_history_fifo.Dequeue();
                                    }
                                }
                            }
                            if (written_flag)
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                }
                else
                {
                    string copiedFileDirectory = Path.GetDirectoryName(copiedFilePath)!;
                    if (!Directory.Exists(copiedFileDirectory))
                    {
                        Directory.CreateDirectory(copiedFileDirectory!);
                    }
                    File.Copy(workingFilePath, copiedFilePath);
                    Console.WriteLine($"ファイルをコピーしました: {copiedFilePath}");
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        static List<Diff> GetTextDifferences(string textA, string textB)
        {
            var dmp = new diff_match_patch();
            var diffs = dmp.diff_main(textA, textB);
            dmp.diff_cleanupSemantic(diffs);
            return diffs;
        }

        private string RemoveCharacters(string input, string charsToRemove)
        {
            HashSet<char> removeSet = new HashSet<char>(charsToRemove);
            StringBuilder result = new StringBuilder();
            foreach (char c in input)
            {
                if (!removeSet.Contains(c))
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        private int CountUniqueCharacters(string input)
        {
            HashSet<char> uniqueChars = new HashSet<char>();
            foreach (char c in input)
            {
                uniqueChars.Add(c);
            }
            return uniqueChars.Count;
        }

        public static float CalculateOverlapRatio(string str1, string str2)
        {
            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
            {
                return 0f;
            }
            Dictionary<char, (int count1, int count2)> freq = new Dictionary<char, (int, int)>();

            foreach (char c in str1)
            {
                if (!freq.ContainsKey(c))
                {
                    freq[c] = (0, 0);
                }
                (int current1, int current2) = freq[c];
                freq[c] = (current1 + 1, current2);
            }
            foreach (char c in str2)
            {
                if (!freq.ContainsKey(c))
                {
                    freq[c] = (0, 0);
                }
                (int current1, int current2) = freq[c];
                freq[c] = (current1, current2 + 1);
            }

            int commonCountSum = 0;
            foreach (var kv in freq)
            {
                (int c1, int c2) = kv.Value;
                if (c1 > 0 && c2 > 0)
                {
                    commonCountSum += (c1 + c2);
                }
            }

            float overlapRatio = (float)commonCountSum / (str1.Length + str2.Length);
            return overlapRatio;
        }

        public static float CalculateWordOverlapRatio(string text1, string text2)
        {
            if (string.IsNullOrEmpty(text1) || string.IsNullOrEmpty(text2))
            {
                return 0f;
            }
            string[] words1 = text1.Split(new char[] { ' ', '\t', '\r', '\n' },
                                          StringSplitOptions.RemoveEmptyEntries);
            string[] words2 = text2.Split(new char[] { ' ', '\t', '\r', '\n' },
                                          StringSplitOptions.RemoveEmptyEntries);

            Dictionary<string, (int count1, int count2)> freq = new Dictionary<string, (int, int)>();

            foreach (var word in words1)
            {
                if (!freq.ContainsKey(word))
                {
                    freq[word] = (0, 0);
                }
                (int current1, int current2) = freq[word];
                freq[word] = (current1 + 1, current2);
            }

            foreach (var word in words2)
            {
                if (!freq.ContainsKey(word))
                {
                    freq[word] = (0, 0);
                }
                (int current1, int current2) = freq[word];
                freq[word] = (current1, current2 + 1);
            }

            int commonCountSum = 0;
            foreach (var kv in freq)
            {
                (int c1, int c2) = kv.Value;
                if (c1 > 0 && c2 > 0)
                {
                    commonCountSum += (c1 + c2);
                }
            }

            float overlapRatio = (float)commonCountSum / (words1.Length + words2.Length);
            return overlapRatio;
        }

        private void save_param()
        {
            JObject Data_to_be_encrypted = new JObject
            {
                { "targetTime", targetTime },
                { "updateInterval", updateInterval },
                { "message", message_encrypted.ToString() },
                { "target_extensions", new JArray(target_extensions) },
                { "workingFolderPath", workingFolderPath },
                { "cumulativeTime", cumulativeTime.ToInt() },
                { "save_Interval", save_Interval },
                { "penalty_rate", penalty_rate },
                {"hide_text",hide_text_flag },
                {
                    "strict_text", new JObject
                    {
                        { "active", strict_text_mode },
                        { "min_number_of_inserted_characters", min_number_of_inserted_characters },
                        { "history_length", history_length },
                        { "min_char_types", min_char_types },
                        { "japanese_only", japanese_only },
                        { "english_only", english_only },
                        { "similarityThreshold", similarityThreshold }
                    }
                }
            };
            string encryptedData = Encrypt(Data_to_be_encrypted.ToString());
            File.WriteAllText(file1Path, encryptedData);

        }

        public static string Encrypt(string plainText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                    swEncrypt.Close();
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        private string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        private void WriteStringToFile(string content)
        {
            DateTime now = DateTime.Now;
            string fileName = $"rewordtext{now:yyyy-MM-dd HH-mm-ss}.txt";
            try
            {
                File.WriteAllText(fileName, content);
            }
            catch (Exception ex)
            {
                // 修正：エラーメッセージの先頭を JSON から参照、例外文は連結
                WriteLog(LanguageManager.GetText("error_occured").Replace("{0}", ex.Message));
            }
        }

        public static void WriteLog(string message)
        {
            try
            {
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH-mm-ss} - {message}";
                File.AppendAllText("log.txt", logEntry + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"ログの書き込みに失敗しました: {ex.Message}");
            }
        }

        private void restart(object sender, EventArgs e)
        {
            try
            {
                string executablePath = System.Windows.Forms.Application.ExecutablePath;
                string directory = Path.GetDirectoryName(executablePath)!;
                string targetFilePath = Path.Combine(directory!, file1Path);
                if (File.Exists(targetFilePath))
                {
                    File.Delete(targetFilePath);
                }
                Process.Start(executablePath!);
                System.Windows.Forms.Application.Exit();
            }
            catch (Exception ex)
            {
                // 修正：エラーメッセージを JSON から参照しつつ連結
                MessageBox.Show(LanguageManager.GetText("error_occured").Replace("{0}", ex.Message),
                                LanguageManager.GetText("error"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }

    public class EncryptedInt
    {
        private string encryptedValue = "";
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("1234567890123456"); // 16バイトのキー
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("6543210987654321");  // 16バイトのIV

        // コンストラクタ
        public EncryptedInt(int value)
        {
            this.Value = value;
        }

        // 値のセット・取得
        public int Value
        {
            get => Decrypt(encryptedValue);
            set => encryptedValue = Encrypt(value);
        }

        // 暗号化
        private static string Encrypt(int value)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                // MemoryStreamを最初に宣言
                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cryptoStream))
                    {
                        writer.Write(value.ToString());
                    }
                    // MemoryStreamからデータを取得
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }


        // 復号化
        private static int Decrypt(string encryptedText)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = IV;
                    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedText)))
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (StreamReader reader = new StreamReader(cryptoStream))
                    {
                        return int.Parse(reader.ReadToEnd());
                    }
                }
            }
            catch
            {
                throw new Exception("データの復号化に失敗しました。");
            }
        }

        // int との変換
        public static implicit operator int(EncryptedInt encryptedInt) => encryptedInt.Value;
        public static implicit operator EncryptedInt(int value) => new EncryptedInt(value);

        // 演算子オーバーロード (EncryptedInt 同士)
        public static EncryptedInt operator +(EncryptedInt a, EncryptedInt b) => new EncryptedInt(a.Value + b.Value);
        public static EncryptedInt operator -(EncryptedInt a, EncryptedInt b) => new EncryptedInt(a.Value - b.Value);
        public static EncryptedInt operator *(EncryptedInt a, EncryptedInt b) => new EncryptedInt(a.Value * b.Value);
        public static EncryptedInt operator /(EncryptedInt a, EncryptedInt b)
        {
            if (b.Value == 0) throw new DivideByZeroException("ゼロ除算エラー");
            return new EncryptedInt(a.Value / b.Value);
        }
        public static EncryptedInt operator %(EncryptedInt a, EncryptedInt b) => new EncryptedInt(a.Value % b.Value);

        public static EncryptedInt operator ++(EncryptedInt a) => new EncryptedInt(a.Value + 1);
        public static EncryptedInt operator --(EncryptedInt a) => new EncryptedInt(a.Value - 1);

        // int との演算（返り値は int）
        public static int operator +(EncryptedInt a, int b) => a.Value + b;
        public static int operator -(EncryptedInt a, int b) => a.Value - b;
        public static int operator *(EncryptedInt a, int b) => a.Value * b;
        public static int operator /(EncryptedInt a, int b)
        {
            if (b == 0) throw new DivideByZeroException("ゼロ除算エラー");
            return a.Value / b;
        }
        public static int operator %(EncryptedInt a, int b) => a.Value % b;

        // float との演算（返り値は float）
        public static float operator +(EncryptedInt a, float b) => a.Value + b;
        public static float operator -(EncryptedInt a, float b) => a.Value - b;
        public static float operator *(EncryptedInt a, float b) => a.Value * b;
        public static float operator /(EncryptedInt a, float b)
        {
            if (b == 0) throw new DivideByZeroException("ゼロ除算エラー");
            return a.Value / b;
        }

        // double との演算（返り値は double）
        public static double operator +(EncryptedInt a, double b) => a.Value + b;
        public static double operator -(EncryptedInt a, double b) => a.Value - b;
        public static double operator *(EncryptedInt a, double b) => a.Value * b;
        public static double operator /(EncryptedInt a, double b)
        {
            if (b == 0) throw new DivideByZeroException("ゼロ除算エラー");
            return a.Value / b;
        }

        // 比較演算
        public static bool operator ==(EncryptedInt a, int b) => a.Value == b;
        public static bool operator !=(EncryptedInt a, int b) => a.Value != b;
        public static bool operator <(EncryptedInt a, int b) => a.Value < b;
        public static bool operator >(EncryptedInt a, int b) => a.Value > b;
        public static bool operator <=(EncryptedInt a, int b) => a.Value <= b;
        public static bool operator >=(EncryptedInt a, int b) => a.Value >= b;


        public int ToInt()
        {
            return this.Value; // 内部で復号化し int を返す
        }
        // ToString() のオーバーライド
        public override string ToString() => Value.ToString();

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }

}
