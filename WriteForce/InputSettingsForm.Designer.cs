namespace WriteForce
{
    partial class InputSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // コントロールの宣言
        private System.Windows.Forms.Label labelTargetTime;
        private System.Windows.Forms.NumericUpDown numericUpDownHours;
        private System.Windows.Forms.NumericUpDown numericUpDownMinutes;
        private System.Windows.Forms.NumericUpDown numericUpDownSeconds;
        private System.Windows.Forms.Label labelHours;
        private System.Windows.Forms.Label labelMinutes;
        private System.Windows.Forms.Label labelSeconds;

        private System.Windows.Forms.Label labelUpdateInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownUpdateMinutes;
        private System.Windows.Forms.NumericUpDown numericUpDownUpdateSeconds;
        private System.Windows.Forms.Label labelUpdateMinutes;
        private System.Windows.Forms.Label labelUpdateSeconds;

        private System.Windows.Forms.Label labelSaveInterval;
        private System.Windows.Forms.NumericUpDown numericUpDownSaveInterval;
        private System.Windows.Forms.Label labelSaveIntervalUnit;

        private System.Windows.Forms.Label labelPenaltyMultiplier;
        private System.Windows.Forms.TextBox textBoxPenaltyMultiplier;
        private System.Windows.Forms.Label labelPenaltyMultiplierlUnit;

        private System.Windows.Forms.Label labelWorkFolder;
        private System.Windows.Forms.TextBox textBoxWorkFolder;
        private System.Windows.Forms.Button buttonSelectFolder;

        private System.Windows.Forms.Label labelFileExtension;
        private System.Windows.Forms.TextBox textBoxFileExtension;

        private System.Windows.Forms.Button buttonOk;



        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            labelTargetTime = new Label();
            numericUpDownHours = new NumericUpDown();
            labelHours = new Label();
            numericUpDownMinutes = new NumericUpDown();
            labelMinutes = new Label();
            numericUpDownSeconds = new NumericUpDown();
            labelSeconds = new Label();
            labelUpdateInterval = new Label();
            numericUpDownUpdateMinutes = new NumericUpDown();
            labelUpdateMinutes = new Label();
            numericUpDownUpdateSeconds = new NumericUpDown();
            labelUpdateSeconds = new Label();
            labelSaveInterval = new Label();
            numericUpDownSaveInterval = new NumericUpDown();
            labelSaveIntervalUnit = new Label();
            labelPenaltyMultiplier = new Label();
            textBoxPenaltyMultiplier = new TextBox();
            labelPenaltyMultiplierlUnit = new Label();
            labelWorkFolder = new Label();
            textBoxWorkFolder = new TextBox();
            buttonSelectFolder = new Button();
            labelFileExtension = new Label();
            textBoxFileExtension = new TextBox();
            buttonOk = new Button();
            messagebox = new TextBox();
            label1 = new Label();
            createzipbutton = new Button();
            create7zbutton = new Button();
            Button_open_advanced_settings = new Button();
            ((System.ComponentModel.ISupportInitialize)numericUpDownHours).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinutes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSeconds).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownUpdateMinutes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownUpdateSeconds).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSaveInterval).BeginInit();
            SuspendLayout();
            // 
            // labelTargetTime
            // 
            labelTargetTime.AutoSize = true;
            labelTargetTime.Location = new Point(20, 20);
            labelTargetTime.Name = "labelTargetTime";
            labelTargetTime.Size = new Size(89, 15);
            labelTargetTime.TabIndex = 0;
            labelTargetTime.Text = "labelTargetTime";
            // 
            // numericUpDownHours
            // 
            numericUpDownHours.Location = new Point(120, 20);
            numericUpDownHours.Name = "numericUpDownHours";
            numericUpDownHours.Size = new Size(60, 23);
            numericUpDownHours.TabIndex = 1;
            numericUpDownHours.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelHours
            // 
            labelHours.AutoSize = true;
            labelHours.Location = new Point(190, 22);
            labelHours.Name = "labelHours";
            labelHours.Size = new Size(64, 15);
            labelHours.TabIndex = 2;
            labelHours.Text = "labelHours";
            // 
            // numericUpDownMinutes
            // 
            numericUpDownMinutes.Location = new Point(230, 20);
            numericUpDownMinutes.Name = "numericUpDownMinutes";
            numericUpDownMinutes.Size = new Size(60, 23);
            numericUpDownMinutes.TabIndex = 3;
            // 
            // labelMinutes
            // 
            labelMinutes.AutoSize = true;
            labelMinutes.Location = new Point(300, 22);
            labelMinutes.Name = "labelMinutes";
            labelMinutes.Size = new Size(75, 15);
            labelMinutes.TabIndex = 4;
            labelMinutes.Text = "labelMinutes";
            // 
            // numericUpDownSeconds
            // 
            numericUpDownSeconds.Location = new Point(330, 20);
            numericUpDownSeconds.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            numericUpDownSeconds.Name = "numericUpDownSeconds";
            numericUpDownSeconds.Size = new Size(60, 23);
            numericUpDownSeconds.TabIndex = 5;
            // 
            // labelSeconds
            // 
            labelSeconds.AutoSize = true;
            labelSeconds.Location = new Point(396, 22);
            labelSeconds.Name = "labelSeconds";
            labelSeconds.Size = new Size(76, 15);
            labelSeconds.TabIndex = 6;
            labelSeconds.Text = "labelSeconds";
            // 
            // labelUpdateInterval
            // 
            labelUpdateInterval.AutoSize = true;
            labelUpdateInterval.Location = new Point(20, 60);
            labelUpdateInterval.Name = "labelUpdateInterval";
            labelUpdateInterval.Size = new Size(109, 15);
            labelUpdateInterval.TabIndex = 7;
            labelUpdateInterval.Text = "labelUpdateInterval";
            // 
            // numericUpDownUpdateMinutes
            // 
            numericUpDownUpdateMinutes.Location = new Point(120, 60);
            numericUpDownUpdateMinutes.Maximum = new decimal(new int[] { 6000, 0, 0, 0 });
            numericUpDownUpdateMinutes.Name = "numericUpDownUpdateMinutes";
            numericUpDownUpdateMinutes.Size = new Size(60, 23);
            numericUpDownUpdateMinutes.TabIndex = 8;
            numericUpDownUpdateMinutes.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelUpdateMinutes
            // 
            labelUpdateMinutes.AutoSize = true;
            labelUpdateMinutes.Location = new Point(190, 62);
            labelUpdateMinutes.Name = "labelUpdateMinutes";
            labelUpdateMinutes.Size = new Size(75, 15);
            labelUpdateMinutes.TabIndex = 9;
            labelUpdateMinutes.Text = "labelMinutes";
            // 
            // numericUpDownUpdateSeconds
            // 
            numericUpDownUpdateSeconds.Location = new Point(230, 60);
            numericUpDownUpdateSeconds.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            numericUpDownUpdateSeconds.Name = "numericUpDownUpdateSeconds";
            numericUpDownUpdateSeconds.Size = new Size(60, 23);
            numericUpDownUpdateSeconds.TabIndex = 10;
            // 
            // labelUpdateSeconds
            // 
            labelUpdateSeconds.AutoSize = true;
            labelUpdateSeconds.Location = new Point(300, 62);
            labelUpdateSeconds.Name = "labelUpdateSeconds";
            labelUpdateSeconds.Size = new Size(76, 15);
            labelUpdateSeconds.TabIndex = 11;
            labelUpdateSeconds.Text = "labelSeconds";
            // 
            // labelSaveInterval
            // 
            labelSaveInterval.AutoSize = true;
            labelSaveInterval.Location = new Point(20, 100);
            labelSaveInterval.Name = "labelSaveInterval";
            labelSaveInterval.Size = new Size(95, 15);
            labelSaveInterval.TabIndex = 12;
            labelSaveInterval.Text = "labelSaveInterval";
            // 
            // numericUpDownSaveInterval
            // 
            numericUpDownSaveInterval.Location = new Point(120, 100);
            numericUpDownSaveInterval.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDownSaveInterval.Name = "numericUpDownSaveInterval";
            numericUpDownSaveInterval.Size = new Size(60, 23);
            numericUpDownSaveInterval.TabIndex = 13;
            numericUpDownSaveInterval.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // labelSaveIntervalUnit
            // 
            labelSaveIntervalUnit.AutoSize = true;
            labelSaveIntervalUnit.Location = new Point(190, 102);
            labelSaveIntervalUnit.Name = "labelSaveIntervalUnit";
            labelSaveIntervalUnit.Size = new Size(117, 15);
            labelSaveIntervalUnit.TabIndex = 14;
            labelSaveIntervalUnit.Text = "labelSaveIntervalUnit";
            // 
            // labelPenaltyMultiplier
            // 
            labelPenaltyMultiplier.AutoSize = true;
            labelPenaltyMultiplier.Location = new Point(20, 140);
            labelPenaltyMultiplier.Name = "labelPenaltyMultiplier";
            labelPenaltyMultiplier.Size = new Size(122, 15);
            labelPenaltyMultiplier.TabIndex = 15;
            labelPenaltyMultiplier.Text = "labelPenaltyMultiplier";
            // 
            // textBoxPenaltyMultiplier
            // 
            textBoxPenaltyMultiplier.Location = new Point(120, 140);
            textBoxPenaltyMultiplier.Name = "textBoxPenaltyMultiplier";
            textBoxPenaltyMultiplier.Size = new Size(60, 23);
            textBoxPenaltyMultiplier.TabIndex = 16;
            textBoxPenaltyMultiplier.Text = "0.3";
            // 
            // labelPenaltyMultiplierlUnit
            // 
            labelPenaltyMultiplierlUnit.AutoSize = true;
            labelPenaltyMultiplierlUnit.Location = new Point(190, 143);
            labelPenaltyMultiplierlUnit.Name = "labelPenaltyMultiplierlUnit";
            labelPenaltyMultiplierlUnit.Size = new Size(147, 15);
            labelPenaltyMultiplierlUnit.TabIndex = 14;
            labelPenaltyMultiplierlUnit.Text = "labelPenaltyMultiplierlUnit";
            // 
            // labelWorkFolder
            // 
            labelWorkFolder.AutoSize = true;
            labelWorkFolder.Location = new Point(20, 180);
            labelWorkFolder.Name = "labelWorkFolder";
            labelWorkFolder.Size = new Size(93, 15);
            labelWorkFolder.TabIndex = 17;
            labelWorkFolder.Text = "labelWorkFolder";
            // 
            // textBoxWorkFolder
            // 
            textBoxWorkFolder.AllowDrop = true;
            textBoxWorkFolder.Location = new Point(120, 180);
            textBoxWorkFolder.Name = "textBoxWorkFolder";
            textBoxWorkFolder.ReadOnly = true;
            textBoxWorkFolder.Size = new Size(187, 23);
            textBoxWorkFolder.TabIndex = 18;
            textBoxWorkFolder.DragDrop += working_foloder_DragDrop;
            textBoxWorkFolder.DragEnter += working_foloder_DragEnter;
            // 
            // buttonSelectFolder
            // 
            buttonSelectFolder.Location = new Point(315, 173);
            buttonSelectFolder.Name = "buttonSelectFolder";
            buttonSelectFolder.Size = new Size(85, 34);
            buttonSelectFolder.TabIndex = 19;
            buttonSelectFolder.Text = "buttonSelectFolder";
            buttonSelectFolder.Click += select_working_foloder;
            // 
            // labelFileExtension
            // 
            labelFileExtension.AutoSize = true;
            labelFileExtension.Location = new Point(20, 220);
            labelFileExtension.Name = "labelFileExtension";
            labelFileExtension.Size = new Size(101, 15);
            labelFileExtension.TabIndex = 20;
            labelFileExtension.Text = "labelFileExtension";
            // 
            // textBoxFileExtension
            // 
            textBoxFileExtension.Location = new Point(160, 220);
            textBoxFileExtension.Name = "textBoxFileExtension";
            textBoxFileExtension.Size = new Size(240, 23);
            textBoxFileExtension.TabIndex = 21;
            textBoxFileExtension.Text = "txt,py,cs";
            // 
            // buttonOk
            // 
            buttonOk.Location = new Point(157, 454);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new Size(100, 30);
            buttonOk.TabIndex = 22;
            buttonOk.Text = "buttonOk";
            buttonOk.Click += exit_setting_form;
            // 
            // messagebox
            // 
            messagebox.Location = new Point(20, 308);
            messagebox.Multiline = true;
            messagebox.Name = "messagebox";
            messagebox.PlaceholderText = "messageboxPlaceholder";
            messagebox.Size = new Size(380, 114);
            messagebox.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Red;
            label1.Location = new Point(43, 434);
            label1.Name = "label1";
            label1.Size = new Size(86, 15);
            label1.TabIndex = 23;
            label1.Text = "labelDisclaimer";
            // 
            // createzipbutton
            // 
            createzipbutton.Location = new Point(20, 260);
            createzipbutton.Name = "createzipbutton";
            createzipbutton.Size = new Size(304, 32);
            createzipbutton.TabIndex = 24;
            createzipbutton.Text = "buttonCreateZip";
            createzipbutton.Click += btnCreateZip_Click;
            // 
            // create7zbutton
            // 
            create7zbutton.Location = new Point(330, 260);
            create7zbutton.Name = "create7zbutton";
            create7zbutton.Size = new Size(70, 32);
            create7zbutton.TabIndex = 25;
            create7zbutton.Text = "7Z";
            create7zbutton.Click += btnCreate7z_Click;
            // 
            // Button_open_advanced_settings
            // 
            Button_open_advanced_settings.Location = new Point(355, 466);
            Button_open_advanced_settings.Name = "Button_open_advanced_settings";
            Button_open_advanced_settings.Size = new Size(75, 30);
            Button_open_advanced_settings.TabIndex = 26;
            Button_open_advanced_settings.Text = "詳細設定";
            Button_open_advanced_settings.Click += edit_advanced_sttings;
            // 
            // InputSettingsForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(429, 496);
            Controls.Add(Button_open_advanced_settings);
            Controls.Add(create7zbutton);
            Controls.Add(createzipbutton);
            Controls.Add(label1);
            Controls.Add(messagebox);
            Controls.Add(labelTargetTime);
            Controls.Add(numericUpDownHours);
            Controls.Add(labelHours);
            Controls.Add(numericUpDownMinutes);
            Controls.Add(labelMinutes);
            Controls.Add(numericUpDownSeconds);
            Controls.Add(labelSeconds);
            Controls.Add(labelUpdateInterval);
            Controls.Add(numericUpDownUpdateMinutes);
            Controls.Add(labelUpdateMinutes);
            Controls.Add(numericUpDownUpdateSeconds);
            Controls.Add(labelUpdateSeconds);
            Controls.Add(labelSaveInterval);
            Controls.Add(numericUpDownSaveInterval);
            Controls.Add(labelSaveIntervalUnit);
            Controls.Add(labelPenaltyMultiplier);
            Controls.Add(textBoxPenaltyMultiplier);
            Controls.Add(labelPenaltyMultiplierlUnit);
            Controls.Add(labelWorkFolder);
            Controls.Add(textBoxWorkFolder);
            Controls.Add(buttonSelectFolder);
            Controls.Add(labelFileExtension);
            Controls.Add(textBoxFileExtension);
            Controls.Add(buttonOk);
            Name = "InputSettingsForm";
            Text = "formTitle";
            labelTargetTime.Text = LanguageManager.GetText("labelTargetTime");
            labelHours.Text = LanguageManager.GetText("labelHours");
            labelMinutes.Text = LanguageManager.GetText("labelMinutes");
            labelSeconds.Text = LanguageManager.GetText("labelSeconds");
            labelUpdateInterval.Text = LanguageManager.GetText("labelUpdateInterval");
            labelUpdateMinutes.Text = LanguageManager.GetText("labelMinutes");
            labelUpdateSeconds.Text = LanguageManager.GetText("labelSeconds");
            labelSaveInterval.Text = LanguageManager.GetText("labelSaveInterval");
            labelSaveIntervalUnit.Text = LanguageManager.GetText("labelSaveIntervalUnit");
            labelPenaltyMultiplier.Text = LanguageManager.GetText("labelPenaltyMultiplier");
            labelPenaltyMultiplierlUnit.Text = LanguageManager.GetText("labelPenaltyMultiplierlUnit");
            labelWorkFolder.Text = LanguageManager.GetText("labelWorkFolder");
            buttonSelectFolder.Text = LanguageManager.GetText("buttonSelectFolder");
            labelFileExtension.Text = LanguageManager.GetText("labelFileExtension");
            buttonOk.Text = LanguageManager.GetText("buttonOk");
            messagebox.PlaceholderText = LanguageManager.GetText("messageboxPlaceholder");
            label1.Text = LanguageManager.GetText("labelDisclaimer");
            createzipbutton.Text = LanguageManager.GetText("buttonCreateZip");
            Text = LanguageManager.GetText("formTitle"); // フォームタイトル
            Button_open_advanced_settings.Text=LanguageManager.GetText("open_advanced_settings");
            DragDrop += working_foloder_DragDrop;
            DragEnter += working_foloder_DragEnter;
            ((System.ComponentModel.ISupportInitialize)numericUpDownHours).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownMinutes).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSeconds).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownUpdateMinutes).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownUpdateSeconds).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownSaveInterval).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        /*
         labelTargetTime.Text = LanguageManager.GetText("labelTargetTime");
            labelHours.Text = LanguageManager.GetText("labelHours");
            labelMinutes.Text = LanguageManager.GetText("labelMinutes");
            labelSeconds.Text = LanguageManager.GetText("labelSeconds");
            labelUpdateInterval.Text = LanguageManager.GetText("labelUpdateInterval");
            labelUpdateMinutes.Text = LanguageManager.GetText("labelMinutes");
            labelUpdateSeconds.Text = LanguageManager.GetText("labelSeconds");
            labelSaveInterval.Text = LanguageManager.GetText("labelSaveInterval");
            labelSaveIntervalUnit.Text = LanguageManager.GetText("labelSaveIntervalUnit");
            labelPenaltyMultiplier.Text = LanguageManager.GetText("labelPenaltyMultiplier");
            labelPenaltyMultiplierlUnit.Text = LanguageManager.GetText("labelPenaltyMultiplierlUnit");
            labelWorkFolder.Text = LanguageManager.GetText("labelWorkFolder");
            buttonSelectFolder.Text = LanguageManager.GetText("buttonSelectFolder");
            labelFileExtension.Text = LanguageManager.GetText("labelFileExtension");
            buttonOk.Text = LanguageManager.GetText("buttonOk");
            messagebox.PlaceholderText = LanguageManager.GetText("messageboxPlaceholder");
            label1.Text = LanguageManager.GetText("labelDisclaimer");
            createzipbutton.Text = LanguageManager.GetText("buttonCreateZip");
            Text = LanguageManager.GetText("formTitle"); // フォームタイトル
        */

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        #endregion

        private TextBox messagebox;
        private Label label1;
        private Button createzipbutton;
        private Button create7zbutton;
        private Button Button_open_advanced_settings;
    }
}