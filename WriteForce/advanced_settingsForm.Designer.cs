#region
#endregion

namespace WriteForce
{
    partial class advanced_settingsForm
    {
        /// <summary>
        /// デザイナーで使用する必要があるすべての変数を管理するコンテナです。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// リソースのクリーンアップを行います。
        /// </summary>
        /// <param name="disposing">マネージ リソースを解放する場合は true、それ以外は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // InitializeComponent メソッドはフォームに配置する各種コントロールの初期化とレイアウトを定義します。
        private void InitializeComponent()
        {
            labelLanguage = new Label();
            comboBoxLanguage = new ComboBox();
            labelXShare = new Label();
            checkBoxXShare = new CheckBox();
            labelSaveAlarm = new Label();
            checkBoxSaveAlarm = new CheckBox();
            labelHideText = new Label();
            checkBoxHideText = new CheckBox();
            labelPreventExit = new Label();
            checkBoxPreventExit = new CheckBox();
            groupBoxStrictText = new GroupBox();
            labelSelectStrictLanguage = new Label();
            comboBoxSelectStrictLanguage = new ComboBox();
            numericUpDownMinInsertedChars = new NumericUpDown();
            labelMinInsertedChars = new Label();
            numericUpDownMinCharTypes = new NumericUpDown();
            labelMinCharTypes = new Label();
            numericUpDownHistoryLength = new NumericUpDown();
            labelHistoryLength = new Label();
            textBoxSimilarityThreshold = new TextBox();
            labelSimilarityThreshold = new Label();
            checkBoxStrictActive = new CheckBox();
            labelStrictActive = new Label();
            labelDeleteOriginal = new Label();
            checkBoxDeleteOriginal = new CheckBox();
            buttonOK_adv = new Button();
            groupBoxStrictText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinInsertedChars).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinCharTypes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHistoryLength).BeginInit();
            SuspendLayout();
            // 
            // labelLanguage
            // 
            labelLanguage.AutoSize = true;
            labelLanguage.Location = new Point(26, 30);
            labelLanguage.Name = "labelLanguage";
            labelLanguage.Size = new Size(140, 15);
            labelLanguage.TabIndex = 0;
            labelLanguage.Text = "advanced_labelLanguage";
            // 
            // comboBoxLanguage
            // 
            comboBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLanguage.FormattingEnabled = true;
            comboBoxLanguage.Items.AddRange(new object[] { "advanced_comboBoxLanguage_jp_item", "advanced_comboBoxLanguage_en_item" });
            comboBoxLanguage.Location = new Point(158, 25);
            comboBoxLanguage.Name = "comboBoxLanguage";
            comboBoxLanguage.Size = new Size(176, 23);
            comboBoxLanguage.TabIndex = 1;
            // 
            // labelXShare
            // 
            labelXShare.AutoSize = true;
            labelXShare.Location = new Point(26, 70);
            labelXShare.Name = "labelXShare";
            labelXShare.Size = new Size(124, 15);
            labelXShare.TabIndex = 2;
            labelXShare.Text = "advanced_labelXShare";
            // 
            // checkBoxXShare
            // 
            checkBoxXShare.AutoSize = true;
            checkBoxXShare.Location = new Point(258, 70);
            checkBoxXShare.Name = "checkBoxXShare";
            checkBoxXShare.Size = new Size(169, 19);
            checkBoxXShare.TabIndex = 3;
            checkBoxXShare.Text = "advanced_checkBoxXShare";
            checkBoxXShare.UseVisualStyleBackColor = true;
            // 
            // labelSaveAlarm
            // 
            labelSaveAlarm.AutoSize = true;
            labelSaveAlarm.Location = new Point(26, 110);
            labelSaveAlarm.Name = "labelSaveAlarm";
            labelSaveAlarm.Size = new Size(143, 15);
            labelSaveAlarm.TabIndex = 4;
            labelSaveAlarm.Text = "advanced_labelSaveAlarm";
            // 
            // checkBoxSaveAlarm
            // 
            checkBoxSaveAlarm.AutoSize = true;
            checkBoxSaveAlarm.Location = new Point(258, 110);
            checkBoxSaveAlarm.Name = "checkBoxSaveAlarm";
            checkBoxSaveAlarm.Size = new Size(188, 19);
            checkBoxSaveAlarm.TabIndex = 5;
            checkBoxSaveAlarm.Text = "advanced_checkBoxSaveAlarm";
            checkBoxSaveAlarm.UseVisualStyleBackColor = true;
            // 
            // labelHideText
            // 
            labelHideText.AutoSize = true;
            labelHideText.Location = new Point(26, 150);
            labelHideText.Name = "labelHideText";
            labelHideText.Size = new Size(134, 15);
            labelHideText.TabIndex = 6;
            labelHideText.Text = "advanced_labelHideText";
            // 
            // checkBoxHideText
            // 
            checkBoxHideText.AutoSize = true;
            checkBoxHideText.Location = new Point(258, 150);
            checkBoxHideText.Name = "checkBoxHideText";
            checkBoxHideText.Size = new Size(179, 19);
            checkBoxHideText.TabIndex = 7;
            checkBoxHideText.Text = "advanced_checkBoxHideText";
            checkBoxHideText.UseVisualStyleBackColor = true;
            // 
            // labelPreventExit
            // 
            labelPreventExit.AutoSize = true;
            labelPreventExit.Location = new Point(26, 190);
            labelPreventExit.Name = "labelPreventExit";
            labelPreventExit.Size = new Size(147, 15);
            labelPreventExit.TabIndex = 8;
            labelPreventExit.Text = "advanced_labelPreventExit";
            // 
            // checkBoxPreventExit
            // 
            checkBoxPreventExit.AutoSize = true;
            checkBoxPreventExit.Location = new Point(258, 190);
            checkBoxPreventExit.Name = "checkBoxPreventExit";
            checkBoxPreventExit.Size = new Size(192, 19);
            checkBoxPreventExit.TabIndex = 9;
            checkBoxPreventExit.Text = "advanced_checkBoxPreventExit";
            checkBoxPreventExit.UseVisualStyleBackColor = true;
            // 
            // groupBoxStrictText
            // 
            groupBoxStrictText.Controls.Add(labelSelectStrictLanguage);
            groupBoxStrictText.Controls.Add(comboBoxSelectStrictLanguage);
            groupBoxStrictText.Controls.Add(numericUpDownMinInsertedChars);
            groupBoxStrictText.Controls.Add(labelMinInsertedChars);
            groupBoxStrictText.Controls.Add(numericUpDownMinCharTypes);
            groupBoxStrictText.Controls.Add(labelMinCharTypes);
            groupBoxStrictText.Controls.Add(numericUpDownHistoryLength);
            groupBoxStrictText.Controls.Add(labelHistoryLength);
            groupBoxStrictText.Controls.Add(textBoxSimilarityThreshold);
            groupBoxStrictText.Controls.Add(labelSimilarityThreshold);
            groupBoxStrictText.Location = new Point(26, 265);
            groupBoxStrictText.Name = "groupBoxStrictText";
            groupBoxStrictText.Size = new Size(569, 170);
            groupBoxStrictText.TabIndex = 10;
            groupBoxStrictText.TabStop = false;
            groupBoxStrictText.Text = "advanced_groupBoxStrictText";
            // 
            // labelSelectStrictLanguage
            // 
            labelSelectStrictLanguage.AutoSize = true;
            labelSelectStrictLanguage.Location = new Point(23, 108);
            labelSelectStrictLanguage.Name = "labelSelectStrictLanguage";
            labelSelectStrictLanguage.Size = new Size(198, 15);
            labelSelectStrictLanguage.TabIndex = 15;
            labelSelectStrictLanguage.Text = "advanced_labelSelectStrictLanguage";
            // 
            // comboBoxSelectStrictLanguage
            // 
            comboBoxSelectStrictLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSelectStrictLanguage.FormattingEnabled = true;
            comboBoxSelectStrictLanguage.Items.AddRange(new object[] { "advanced_comboBoxSelectStrictLanguage_disable_item", "advanced_comboBoxSelectStrictLanguage_jp_item", "advanced_comboBoxSelectStrictLanguage_en_item" });
            comboBoxSelectStrictLanguage.Location = new Point(164, 105);
            comboBoxSelectStrictLanguage.Name = "comboBoxSelectStrictLanguage";
            comboBoxSelectStrictLanguage.Size = new Size(176, 23);
            comboBoxSelectStrictLanguage.TabIndex = 14;
            // 
            // numericUpDownMinInsertedChars
            // 
            numericUpDownMinInsertedChars.Location = new Point(209, 25);
            numericUpDownMinInsertedChars.Name = "numericUpDownMinInsertedChars";
            numericUpDownMinInsertedChars.Size = new Size(105, 23);
            numericUpDownMinInsertedChars.TabIndex = 3;
            numericUpDownMinInsertedChars.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // labelMinInsertedChars
            // 
            labelMinInsertedChars.AutoSize = true;
            labelMinInsertedChars.Location = new Point(23, 30);
            labelMinInsertedChars.Name = "labelMinInsertedChars";
            labelMinInsertedChars.Size = new Size(180, 15);
            labelMinInsertedChars.TabIndex = 2;
            labelMinInsertedChars.Text = "advanced_labelMinInsertedChars";
            // 
            // numericUpDownMinCharTypes
            // 
            numericUpDownMinCharTypes.Location = new Point(209, 58);
            numericUpDownMinCharTypes.Name = "numericUpDownMinCharTypes";
            numericUpDownMinCharTypes.Size = new Size(105, 23);
            numericUpDownMinCharTypes.TabIndex = 5;
            numericUpDownMinCharTypes.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // labelMinCharTypes
            // 
            labelMinCharTypes.AutoSize = true;
            labelMinCharTypes.Location = new Point(23, 60);
            labelMinCharTypes.Name = "labelMinCharTypes";
            labelMinCharTypes.Size = new Size(162, 15);
            labelMinCharTypes.TabIndex = 4;
            labelMinCharTypes.Text = "advanced_labelMinCharTypes";
            // 
            // numericUpDownHistoryLength
            // 
            numericUpDownHistoryLength.Location = new Point(250, 141);
            numericUpDownHistoryLength.Name = "numericUpDownHistoryLength";
            numericUpDownHistoryLength.Size = new Size(105, 23);
            numericUpDownHistoryLength.TabIndex = 7;
            numericUpDownHistoryLength.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // labelHistoryLength
            // 
            labelHistoryLength.AutoSize = true;
            labelHistoryLength.Location = new Point(23, 143);
            labelHistoryLength.Name = "labelHistoryLength";
            labelHistoryLength.Size = new Size(163, 15);
            labelHistoryLength.TabIndex = 6;
            labelHistoryLength.Text = "advanced_labelHistoryLength";
            // 
            // textBoxSimilarityThreshold
            // 
            textBoxSimilarityThreshold.Location = new Point(457, 52);
            textBoxSimilarityThreshold.Name = "textBoxSimilarityThreshold";
            textBoxSimilarityThreshold.Size = new Size(106, 23);
            textBoxSimilarityThreshold.TabIndex = 9;
            textBoxSimilarityThreshold.Text = "0.3";
            // 
            // labelSimilarityThreshold
            // 
            labelSimilarityThreshold.AutoSize = true;
            labelSimilarityThreshold.Location = new Point(375, 27);
            labelSimilarityThreshold.Name = "labelSimilarityThreshold";
            labelSimilarityThreshold.Size = new Size(188, 15);
            labelSimilarityThreshold.TabIndex = 8;
            labelSimilarityThreshold.Text = "advanced_labelSimilarityThreshold";
            // 
            // checkBoxStrictActive
            // 
            checkBoxStrictActive.AutoSize = true;
            checkBoxStrictActive.Location = new Point(258, 230);
            checkBoxStrictActive.Name = "checkBoxStrictActive";
            checkBoxStrictActive.Size = new Size(193, 19);
            checkBoxStrictActive.TabIndex = 1;
            checkBoxStrictActive.Text = "advanced_checkBoxStrictActive";
            checkBoxStrictActive.UseVisualStyleBackColor = true;
            checkBoxStrictActive.CheckedChanged += Change_strict_text_active;
            // 
            // labelStrictActive
            // 
            labelStrictActive.AutoSize = true;
            labelStrictActive.Location = new Point(26, 230);
            labelStrictActive.Name = "labelStrictActive";
            labelStrictActive.Size = new Size(148, 15);
            labelStrictActive.TabIndex = 0;
            labelStrictActive.Text = "advanced_labelStrictActive";
            // 
            // labelDeleteOriginal
            // 
            labelDeleteOriginal.AutoSize = true;
            labelDeleteOriginal.Location = new Point(26, 442);
            labelDeleteOriginal.Name = "labelDeleteOriginal";
            labelDeleteOriginal.Size = new Size(163, 15);
            labelDeleteOriginal.TabIndex = 11;
            labelDeleteOriginal.Text = "advanced_labelDeleteOriginal";
            // 
            // checkBoxDeleteOriginal
            // 
            checkBoxDeleteOriginal.AutoSize = true;
            checkBoxDeleteOriginal.Location = new Point(195, 442);
            checkBoxDeleteOriginal.Name = "checkBoxDeleteOriginal";
            checkBoxDeleteOriginal.Size = new Size(208, 19);
            checkBoxDeleteOriginal.TabIndex = 12;
            checkBoxDeleteOriginal.Text = "advanced_checkBoxDeleteOriginal";
            checkBoxDeleteOriginal.UseVisualStyleBackColor = true;
            // 
            // buttonOK_adv
            // 
            buttonOK_adv.Location = new Point(240, 488);
            buttonOK_adv.Name = "buttonOK_adv";
            buttonOK_adv.Size = new Size(100, 30);
            buttonOK_adv.TabIndex = 23;
            buttonOK_adv.Text = "advanced_buttonOK_adv";
            buttonOK_adv.Click += exit_setting;
            // 
            // advanced_settingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(612, 530);
            Controls.Add(buttonOK_adv);
            Controls.Add(labelLanguage);
            Controls.Add(checkBoxStrictActive);
            Controls.Add(comboBoxLanguage);
            Controls.Add(labelStrictActive);
            Controls.Add(labelXShare);
            Controls.Add(checkBoxXShare);
            Controls.Add(labelSaveAlarm);
            Controls.Add(checkBoxSaveAlarm);
            Controls.Add(labelHideText);
            Controls.Add(checkBoxHideText);
            Controls.Add(labelPreventExit);
            Controls.Add(checkBoxPreventExit);
            Controls.Add(groupBoxStrictText);
            Controls.Add(labelDeleteOriginal);
            Controls.Add(checkBoxDeleteOriginal);
            Name = "advanced_settingsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Advanced Settings";
            groupBoxStrictText.ResumeLayout(false);
            groupBoxStrictText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinInsertedChars).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinCharTypes).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHistoryLength).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        /*
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
            */
        #region デザイナーが生成したコントロール一覧

        // 上部の設定項目
        private System.Windows.Forms.Label labelLanguage;
        // language は TextBox から ComboBox に変更
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.Label labelXShare;
        private System.Windows.Forms.CheckBox checkBoxXShare;
        private System.Windows.Forms.Label labelSaveAlarm;
        private System.Windows.Forms.CheckBox checkBoxSaveAlarm;
        private System.Windows.Forms.Label labelHideText;
        private System.Windows.Forms.CheckBox checkBoxHideText;
        private System.Windows.Forms.Label labelPreventExit;
        private System.Windows.Forms.CheckBox checkBoxPreventExit;

        // strict_text グループボックス内の各コントロール
        private System.Windows.Forms.GroupBox groupBoxStrictText;
        private System.Windows.Forms.Label labelStrictActive;
        private System.Windows.Forms.CheckBox checkBoxStrictActive;
        private System.Windows.Forms.Label labelMinInsertedChars;
        private System.Windows.Forms.NumericUpDown numericUpDownMinInsertedChars;
        private System.Windows.Forms.Label labelMinCharTypes;
        private System.Windows.Forms.NumericUpDown numericUpDownMinCharTypes;
        private System.Windows.Forms.Label labelHistoryLength;
        private System.Windows.Forms.NumericUpDown numericUpDownHistoryLength;
        private System.Windows.Forms.Label labelSimilarityThreshold;
        private System.Windows.Forms.TextBox textBoxSimilarityThreshold;

        // 下部の設定項目および決定ボタン
        private System.Windows.Forms.Label labelDeleteOriginal;
        private System.Windows.Forms.CheckBox checkBoxDeleteOriginal;

        #endregion

        private ComboBox comboBoxSelectStrictLanguage;
        private Label labelSelectStrictLanguage;
        private Button buttonOK_adv;
    }
}