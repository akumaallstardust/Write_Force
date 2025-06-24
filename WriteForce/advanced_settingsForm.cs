using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WriteForce
{
    public partial class advanced_settingsForm : Form
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

        private string[] language_list_value = ["japanese", "english"];
        private string language = (string?)MainForm.setting_dict["language"] ?? "japanese";
        public advanced_settingsForm()
        {
            try
            {
                if (language == "japanese")
                {
                    LanguageManager.LoadLanguage("ja");
                }
                else if (language == "english")
                {
                    LanguageManager.LoadLanguage("en");
                }
                InitializeComponent();
                
                labelLanguage.Text = LanguageManager.GetText("advanced_labelLanguage");
                comboBoxLanguage.Items.Clear();
                comboBoxLanguage.Items.Add(LanguageManager.GetText("advanced_comboBoxLanguage_jp_item"));
                comboBoxLanguage.Items.Add(LanguageManager.GetText("advanced_comboBoxLanguage_en_item"));
                labelXShare.Text = LanguageManager.GetText("advanced_labelXShare");
                checkBoxXShare.Text = LanguageManager.GetText("advanced_checkBoxXShare");
                labelSaveAlarm.Text = LanguageManager.GetText("advanced_labelSaveAlarm");
                checkBoxSaveAlarm.Text = LanguageManager.GetText("advanced_checkBoxSaveAlarm");
                labelHideText.Text = LanguageManager.GetText("advanced_labelHideText");
                checkBoxHideText.Text = LanguageManager.GetText("advanced_checkBoxHideText");
                labelPreventExit.Text = LanguageManager.GetText("advanced_labelPreventExit");
                checkBoxPreventExit.Text = LanguageManager.GetText("advanced_checkBoxPreventExit");
                groupBoxStrictText.Text = LanguageManager.GetText("advanced_groupBoxStrictText");
                labelSelectStrictLanguage.Text = LanguageManager.GetText("advanced_labelSelectStrictLanguage");
                comboBoxSelectStrictLanguage.Items.Clear();
                comboBoxSelectStrictLanguage.Items.Add(LanguageManager.GetText("advanced_comboBoxSelectStrictLanguage_disable_item"));
                comboBoxSelectStrictLanguage.Items.Add(LanguageManager.GetText("advanced_comboBoxSelectStrictLanguage_jp_item"));
                comboBoxSelectStrictLanguage.Items.Add(LanguageManager.GetText("advanced_comboBoxSelectStrictLanguage_en_item"));
                labelMinInsertedChars.Text = LanguageManager.GetText("advanced_labelMinInsertedChars");
                labelMinCharTypes.Text = LanguageManager.GetText("advanced_labelMinCharTypes");
                labelHistoryLength.Text = LanguageManager.GetText("advanced_labelHistoryLength");
                labelSimilarityThreshold.Text = LanguageManager.GetText("advanced_labelSimilarityThreshold");
                labelStrictActive.Text = LanguageManager.GetText("advanced_labelStrictActive");
                checkBoxStrictActive.Text = LanguageManager.GetText("advanced_checkBoxStrictActive");
                labelDeleteOriginal.Text = LanguageManager.GetText("advanced_labelDeleteOriginal");
                checkBoxDeleteOriginal.Text = LanguageManager.GetText("advanced_checkBoxDeleteOriginal");
                buttonOK_adv.Text = LanguageManager.GetText("advanced_buttonOK_adv");

                //["日本語", "english"];の順番、順番間違いに注意
                comboBoxLanguage.SelectedIndex = Array.IndexOf(language_list_value, (string?)MainForm.setting_dict["language"]);//変にいじられた際の調整はしない
                checkBoxXShare.Checked = (bool?)MainForm.setting_dict["x_shere_message"] ?? false;
                checkBoxSaveAlarm.Checked=(bool?)MainForm.setting_dict["save_alarm"] ?? false;
                checkBoxHideText.Checked = (bool?)MainForm.setting_dict["hide_text"] ?? false;
                checkBoxPreventExit.Checked = (bool?)MainForm.setting_dict["prevent_exit"] ?? false;
                checkBoxStrictActive.Checked = (bool?)MainForm.setting_dict["strict_text"]?["active"] ?? false;
                ChangeStrict_Mode_Controls((bool?)MainForm.setting_dict["strict_text"]?["active"] ?? false);
                numericUpDownMinInsertedChars.Value = (int?)MainForm.setting_dict["strict_text"]?["min_number_of_inserted_characters"] ?? 3;
                numericUpDownMinCharTypes.Value = (int?)MainForm.setting_dict["strict_text"]?["min_char_types"] ?? 2;
                textBoxSimilarityThreshold.Text = (Math.Floor(((float?)MainForm.setting_dict["strict_text"]?["similarityThreshold"] ?? 0.5f) * 100000f) / 100000f).ToString();
                //["invalid","japanese","english"]
                if ((bool?)MainForm.setting_dict["strict_text"]?["japanese_only"] ?? false)
                {
                    comboBoxSelectStrictLanguage.SelectedIndex = 1;
                }
                else if ((bool?)MainForm.setting_dict["strict_text"]?["english_only"] ?? false)
                {
                    comboBoxSelectStrictLanguage.SelectedIndex = 2;
                }
                else
                {
                    comboBoxSelectStrictLanguage.SelectedIndex = 0;
                }
                numericUpDownHistoryLength.Value = (int?)MainForm.setting_dict["strict_text"]?["history_length"] ?? 5;
                checkBoxDeleteOriginal.Checked = (bool?)MainForm.setting_dict["delete_originalFile"] ?? false;
            }
            catch (Exception ex)
            {
                // 修正：日本語文字列を JSON から取得 + 例外メッセージは連結
                MainForm.WriteLog(ex.Message);
            }

        }
        /// <summary>
        /// 指定した WinForms コントロール群をすべて無効化するメソッド
        /// </summary>
        private void ChangeStrict_Mode_Controls(bool change)
        {
            // ラベル：文章厳格モード
            labelSelectStrictLanguage.Enabled = change;

            // コンボボックス：文章厳格モードの言語選択
            comboBoxSelectStrictLanguage.Enabled = change;

            // numericUpDown：最小文字/単語数
            numericUpDownMinInsertedChars.Enabled = change;

            // ラベル：最小文字/単語数
            labelMinInsertedChars.Enabled = change;

            // numericUpDown：最小/単語種類
            numericUpDownMinCharTypes.Enabled = change;

            // ラベル：最小/単語種類
            labelMinCharTypes.Enabled = change;

            // numericUpDown：比較する過去の変更の数
            numericUpDownHistoryLength.Enabled = change;

            // ラベル：比較する過去の変更の数
            labelHistoryLength.Enabled = change;

            // textBox：コピペ判定閾値
            textBoxSimilarityThreshold.Enabled = change;

            // ラベル：コピペ判定閾値
            labelSimilarityThreshold.Enabled = change;
        }

        private void Change_strict_text_active(object sender, EventArgs e)
        {
            ChangeStrict_Mode_Controls(checkBoxStrictActive.Checked);
        }

        private void exit_setting(object sender, EventArgs e)
        {
            MainForm.setting_dict["language"] = language_list_value[comboBoxLanguage.SelectedIndex];
            MainForm.setting_dict["x_shere_message"] = checkBoxXShare.Checked;
            MainForm.setting_dict["save_alarm"] = checkBoxSaveAlarm.Checked;
            MainForm.setting_dict["hide_text"] = checkBoxHideText.Checked;
            MainForm.setting_dict["prevent_exit"] = checkBoxPreventExit.Checked;
            MainForm.setting_dict["strict_text"]!["active"] = checkBoxStrictActive.Checked;
            MainForm.setting_dict["strict_text"]!["min_number_of_inserted_characters"] = (int)numericUpDownMinInsertedChars.Value;
            MainForm.setting_dict["strict_text"]!["min_char_types"] = (int)numericUpDownMinCharTypes.Value;
            float _similarityThreshold;
            if (!float.TryParse(textBoxSimilarityThreshold.Text, out _similarityThreshold))
            {
                MessageBox.Show(LanguageManager.GetText("labelPenaltyMultiplier"));
                return;
            }
            else if (float.Parse(textBoxSimilarityThreshold.Text) < 0)
            {
                MessageBox.Show("閾値は0以上の数字です");
                return;
            }
            MainForm.setting_dict["strict_text"]!["similarityThreshold"] = float.Parse(textBoxSimilarityThreshold.Text);
            if ((bool?)MainForm.setting_dict["strict_text"]?["japanese_only"] ?? false)
            {
                comboBoxSelectStrictLanguage.SelectedIndex = 1;
            }
            else if ((bool?)MainForm.setting_dict["strict_text"]?["english_only"] ?? false)
            {
                comboBoxSelectStrictLanguage.SelectedIndex = 2;
            }
            else
            {
                comboBoxSelectStrictLanguage.SelectedIndex = 0;
            }

            MainForm.setting_dict["strict_text"]!["english_only"] = comboBoxSelectStrictLanguage.SelectedIndex == 1;
            MainForm.setting_dict["strict_text"]!["japanese_only"] = comboBoxSelectStrictLanguage.SelectedIndex == 2;
            MainForm.setting_dict["strict_text"]!["history_length"] = numericUpDownHistoryLength.Value;
            MainForm.setting_dict["delete_originalFile"] = checkBoxDeleteOriginal.Checked;

            File.WriteAllText(MainForm.setting_path, MainForm.setting_dict.ToString());

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
