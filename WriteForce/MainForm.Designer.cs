namespace WriteForce
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            lblCumulativeTime = new Label();
            messagebox = new TextBox();
            number_of_files = new Label();
            time_to_next_update_labal = new Label();
            folder_path = new Label();
            TargetTime_label = new Label();
            count_to_next_save_text = new Label();
            penalty_rate_text = new Label();
            target_extension_text = new Label();
            SuspendLayout();
            // 
            // lblCumulativeTime
            // 
            lblCumulativeTime.AutoSize = true;
            lblCumulativeTime.Location = new Point(14, 11);
            lblCumulativeTime.Margin = new Padding(4, 0, 4, 0);
            lblCumulativeTime.Name = "lblCumulativeTime";
            lblCumulativeTime.Size = new Size(104, 15);
            lblCumulativeTime.TabIndex = 0;
            lblCumulativeTime.Text = "lblCumulativeTime";
            // 
            // messagebox
            // 
            messagebox.Location = new Point(16, 79);
            messagebox.Margin = new Padding(4);
            messagebox.Multiline = true;
            messagebox.Name = "messagebox";
            messagebox.ReadOnly = true;
            messagebox.Size = new Size(493, 244);
            messagebox.TabIndex = 1;
            // 
            // number_of_files
            // 
            number_of_files.AutoSize = true;
            number_of_files.Location = new Point(15, 336);
            number_of_files.Margin = new Padding(4, 0, 4, 0);
            number_of_files.Name = "number_of_files";
            number_of_files.Size = new Size(89, 15);
            number_of_files.TabIndex = 2;
            number_of_files.Text = "number_of_files";
            // 
            // time_to_next_update_labal
            // 
            time_to_next_update_labal.AutoSize = true;
            time_to_next_update_labal.Location = new Point(285, 11);
            time_to_next_update_labal.Margin = new Padding(4, 0, 4, 0);
            time_to_next_update_labal.Name = "time_to_next_update_labal";
            time_to_next_update_labal.Size = new Size(146, 15);
            time_to_next_update_labal.TabIndex = 3;
            time_to_next_update_labal.Text = "time_to_next_update_labal";
            // 
            // folder_path
            // 
            folder_path.AutoSize = true;
            folder_path.Location = new Point(147, 336);
            folder_path.Margin = new Padding(4, 0, 4, 0);
            folder_path.Name = "folder_path";
            folder_path.Size = new Size(17, 15);
            folder_path.TabIndex = 4;
            folder_path.Text = "C:";
            // 
            // TargetTime_label
            // 
            TargetTime_label.AutoSize = true;
            TargetTime_label.Location = new Point(14, 39);
            TargetTime_label.Margin = new Padding(4, 0, 4, 0);
            TargetTime_label.Name = "TargetTime_label";
            TargetTime_label.Size = new Size(94, 15);
            TargetTime_label.TabIndex = 5;
            TargetTime_label.Text = "TargetTime_label";
            // 
            // count_to_next_save_text
            // 
            count_to_next_save_text.AutoSize = true;
            count_to_next_save_text.Location = new Point(285, 39);
            count_to_next_save_text.Margin = new Padding(4, 0, 4, 0);
            count_to_next_save_text.Name = "count_to_next_save_text";
            count_to_next_save_text.Size = new Size(135, 15);
            count_to_next_save_text.TabIndex = 6;
            count_to_next_save_text.Text = "count_to_next_save_text";
            // 
            // penalty_rate_text
            // 
            penalty_rate_text.AutoSize = true;
            penalty_rate_text.Location = new Point(15, 354);
            penalty_rate_text.Margin = new Padding(4, 0, 4, 0);
            penalty_rate_text.Name = "penalty_rate_text";
            penalty_rate_text.Size = new Size(96, 15);
            penalty_rate_text.TabIndex = 7;
            penalty_rate_text.Text = "penalty_rate_text";
            // 
            // target_extension_text
            // 
            target_extension_text.AutoSize = true;
            target_extension_text.Location = new Point(147, 354);
            target_extension_text.Margin = new Padding(4, 0, 4, 0);
            target_extension_text.Name = "target_extension_text";
            target_extension_text.Size = new Size(119, 15);
            target_extension_text.TabIndex = 8;
            target_extension_text.Text = "target_extension_text";
            // 
            // MainFrom
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(522, 376);
            Controls.Add(target_extension_text);
            Controls.Add(penalty_rate_text);
            Controls.Add(count_to_next_save_text);
            Controls.Add(TargetTime_label);
            Controls.Add(folder_path);
            Controls.Add(time_to_next_update_labal);
            Controls.Add(number_of_files);
            Controls.Add(messagebox);
            Controls.Add(lblCumulativeTime);
            Margin = new Padding(4);
            Name = "MainFrom";
            Text = "MainFormTitle";

            lblCumulativeTime.Text = LanguageManager.GetText("lblCumulativeTime");
            time_to_next_update_labal.Text = LanguageManager.GetText("time_to_next_update_labal");
            TargetTime_label.Text = LanguageManager.GetText("TargetTime_label");
            count_to_next_save_text.Text = LanguageManager.GetText("count_to_next_save_text");
            number_of_files.Text = LanguageManager.GetText("number_of_files");
            penalty_rate_text.Text = LanguageManager.GetText("penalty_rate_text");
            target_extension_text.Text = LanguageManager.GetText("target_extension_text");
            Text = LanguageManager.GetText("MainFormTitle"); // フォームタイトル


            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblCumulativeTime;
        private System.Windows.Forms.TextBox messagebox;
        private System.Windows.Forms.Label number_of_files;
        private System.Windows.Forms.Label time_to_next_update_labal;
        private System.Windows.Forms.Label folder_path;
        private System.Windows.Forms.Label TargetTime_label;
        private System.Windows.Forms.Label count_to_next_save_text;
        private System.Windows.Forms.Label penalty_rate_text;
        private Label target_extension_text;
    }
}
