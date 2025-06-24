namespace WriteForce
{
    partial class BLE_Device_Form
    {
        /// <summary>UI コンポーネント</summary>
        private ListBox lstDevices;
        private Button btnScan;
        private Button btnConnect;
        private Button btnOk;
        private Label lblStatus;

        /// <summary>
        ///   UI 初期化（Designer 専用）
        /// </summary>
        private void InitializeComponent()
        {
            this.lstDevices  = new System.Windows.Forms.ListBox();
    this.btnScan     = new System.Windows.Forms.Button();
    this.btnConnect  = new System.Windows.Forms.Button();
    this.btnOk       = new System.Windows.Forms.Button();
    this.lblStatus   = new System.Windows.Forms.Label();

    this.SuspendLayout();
    // -----------------------------------------------------------------
    // lstDevices
    this.lstDevices.Anchor = System.Windows.Forms.AnchorStyles.Top
                            | System.Windows.Forms.AnchorStyles.Left
                            | System.Windows.Forms.AnchorStyles.Right
                            | System.Windows.Forms.AnchorStyles.Bottom;
    this.lstDevices.Location = new System.Drawing.Point(10, 10);
    this.lstDevices.Size     = new System.Drawing.Size(400, 200);
    this.lstDevices.TabIndex = 0;
    this.lstDevices.SelectedIndexChanged += new System.EventHandler(this.LstDevices_SelectedIndexChanged);
    // -----------------------------------------------------------------
    // btnScan
    this.btnScan.Location = new System.Drawing.Point(10, 220);
    this.btnScan.Size     = new System.Drawing.Size(85, 30);
    this.btnScan.TabIndex = 1;
    this.btnScan.Text     = "スキャン(&S)";
    this.btnScan.UseVisualStyleBackColor = true;
    this.btnScan.Click   += new System.EventHandler(this.BtnScan_Click);
    // -----------------------------------------------------------------
    // btnConnect
    this.btnConnect.Location = new System.Drawing.Point(110, 220);
    this.btnConnect.Size     = new System.Drawing.Size(85, 30);
    this.btnConnect.TabIndex = 2;
    this.btnConnect.Text     = "接続";
    this.btnConnect.UseVisualStyleBackColor = true;
    this.btnConnect.Enabled  = false;
    this.btnConnect.Click   += new System.EventHandler(this.BtnConnect_Click);
    // -----------------------------------------------------------------
    // btnOk
    this.btnOk.Location = new System.Drawing.Point(210, 220);
    this.btnOk.Size     = new System.Drawing.Size(85, 30);
    this.btnOk.TabIndex = 3;
    this.btnOk.Text     = "OK";
    this.btnOk.UseVisualStyleBackColor = true;
    this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
    // -----------------------------------------------------------------
    // lblStatus
    this.lblStatus.AutoSize = true;
    this.lblStatus.Location = new System.Drawing.Point(10, 260);
    this.lblStatus.Size     = new System.Drawing.Size(380, 15);
    this.lblStatus.TabIndex = 4;
    // -----------------------------------------------------------------
    // MainForm
    this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
    this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
    this.ClientSize          = new System.Drawing.Size(420, 300);
    this.Controls.AddRange(new System.Windows.Forms.Control[]
    {
        this.lstDevices, this.btnScan, this.btnConnect, this.btnOk, this.lblStatus
    });
    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
    this.MaximizeBox     = false;
    this.MinimizeBox     = false;
    this.Name            = "MainForm";
    this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;
    this.Text            = "BLE デバイス選択";
    this.FormClosing    += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
    this.ResumeLayout(false);
    this.PerformLayout();
        }
    }
}