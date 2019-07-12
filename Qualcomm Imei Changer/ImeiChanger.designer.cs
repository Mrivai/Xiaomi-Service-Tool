namespace Qualcomm_Imei_Changer
{
    partial class ImeiChanger
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImeiChanger));
            this.SN = new MetroFramework.Controls.MetroTextBox();
            this.Imei1 = new MetroFramework.Controls.MetroTextBox();
            this.Imei2 = new MetroFramework.Controls.MetroTextBox();
            this.Meid = new MetroFramework.Controls.MetroTextBox();
            this.Mac = new MetroFramework.Controls.MetroTextBox();
            this.Blu = new MetroFramework.Controls.MetroTextBox();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.CbSN = new MetroFramework.Controls.MetroCheckBox();
            this.CbImei1 = new MetroFramework.Controls.MetroCheckBox();
            this.CbImei2 = new MetroFramework.Controls.MetroCheckBox();
            this.CbMeid = new MetroFramework.Controls.MetroCheckBox();
            this.CbMac = new MetroFramework.Controls.MetroCheckBox();
            this.CbBt = new MetroFramework.Controls.MetroCheckBox();
            this.BackupCbx = new MetroFramework.Controls.MetroCheckBox();
            this.RestrCbx = new MetroFramework.Controls.MetroCheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.worker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // SN
            // 
            // 
            // 
            // 
            this.SN.CustomButton.Image = null;
            this.SN.CustomButton.Location = new System.Drawing.Point(290, 2);
            this.SN.CustomButton.Name = "";
            this.SN.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.SN.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.SN.CustomButton.TabIndex = 1;
            this.SN.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.SN.CustomButton.UseSelectable = true;
            this.SN.CustomButton.Visible = false;
            this.SN.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.SN.Lines = new string[0];
            this.SN.Location = new System.Drawing.Point(23, 123);
            this.SN.MaxLength = 32767;
            this.SN.MinimumSize = new System.Drawing.Size(0, 30);
            this.SN.Name = "SN";
            this.SN.PasswordChar = '\0';
            this.SN.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.SN.SelectedText = "";
            this.SN.SelectionLength = 0;
            this.SN.SelectionStart = 0;
            this.SN.Size = new System.Drawing.Size(318, 30);
            this.SN.TabIndex = 38;
            this.SN.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.SN.UseSelectable = true;
            this.SN.UseStyleColors = true;
            this.SN.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.SN.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // Imei1
            // 
            // 
            // 
            // 
            this.Imei1.CustomButton.Image = null;
            this.Imei1.CustomButton.Location = new System.Drawing.Point(291, 2);
            this.Imei1.CustomButton.Name = "";
            this.Imei1.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.Imei1.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.Imei1.CustomButton.TabIndex = 1;
            this.Imei1.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Imei1.CustomButton.UseSelectable = true;
            this.Imei1.CustomButton.Visible = false;
            this.Imei1.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.Imei1.Lines = new string[0];
            this.Imei1.Location = new System.Drawing.Point(23, 193);
            this.Imei1.MaxLength = 32767;
            this.Imei1.MinimumSize = new System.Drawing.Size(0, 30);
            this.Imei1.Name = "Imei1";
            this.Imei1.PasswordChar = '\0';
            this.Imei1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.Imei1.SelectedText = "";
            this.Imei1.SelectionLength = 0;
            this.Imei1.SelectionStart = 0;
            this.Imei1.Size = new System.Drawing.Size(319, 30);
            this.Imei1.TabIndex = 40;
            this.Imei1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Imei1.UseSelectable = true;
            this.Imei1.UseStyleColors = true;
            this.Imei1.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.Imei1.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // Imei2
            // 
            // 
            // 
            // 
            this.Imei2.CustomButton.Image = null;
            this.Imei2.CustomButton.Location = new System.Drawing.Point(291, 2);
            this.Imei2.CustomButton.Name = "";
            this.Imei2.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.Imei2.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.Imei2.CustomButton.TabIndex = 1;
            this.Imei2.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Imei2.CustomButton.UseSelectable = true;
            this.Imei2.CustomButton.Visible = false;
            this.Imei2.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.Imei2.Lines = new string[0];
            this.Imei2.Location = new System.Drawing.Point(23, 260);
            this.Imei2.MaxLength = 32767;
            this.Imei2.MinimumSize = new System.Drawing.Size(0, 30);
            this.Imei2.Name = "Imei2";
            this.Imei2.PasswordChar = '\0';
            this.Imei2.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.Imei2.SelectedText = "";
            this.Imei2.SelectionLength = 0;
            this.Imei2.SelectionStart = 0;
            this.Imei2.Size = new System.Drawing.Size(319, 30);
            this.Imei2.TabIndex = 42;
            this.Imei2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Imei2.UseSelectable = true;
            this.Imei2.UseStyleColors = true;
            this.Imei2.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.Imei2.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // Meid
            // 
            // 
            // 
            // 
            this.Meid.CustomButton.Image = null;
            this.Meid.CustomButton.Location = new System.Drawing.Point(291, 2);
            this.Meid.CustomButton.Name = "";
            this.Meid.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.Meid.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.Meid.CustomButton.TabIndex = 1;
            this.Meid.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Meid.CustomButton.UseSelectable = true;
            this.Meid.CustomButton.Visible = false;
            this.Meid.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.Meid.Lines = new string[0];
            this.Meid.Location = new System.Drawing.Point(23, 327);
            this.Meid.MaxLength = 32767;
            this.Meid.MinimumSize = new System.Drawing.Size(0, 30);
            this.Meid.Name = "Meid";
            this.Meid.PasswordChar = '\0';
            this.Meid.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.Meid.SelectedText = "";
            this.Meid.SelectionLength = 0;
            this.Meid.SelectionStart = 0;
            this.Meid.Size = new System.Drawing.Size(319, 30);
            this.Meid.TabIndex = 44;
            this.Meid.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Meid.UseSelectable = true;
            this.Meid.UseStyleColors = true;
            this.Meid.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.Meid.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // Mac
            // 
            // 
            // 
            // 
            this.Mac.CustomButton.Image = null;
            this.Mac.CustomButton.Location = new System.Drawing.Point(291, 2);
            this.Mac.CustomButton.Name = "";
            this.Mac.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.Mac.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.Mac.CustomButton.TabIndex = 1;
            this.Mac.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Mac.CustomButton.UseSelectable = true;
            this.Mac.CustomButton.Visible = false;
            this.Mac.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.Mac.Lines = new string[0];
            this.Mac.Location = new System.Drawing.Point(23, 394);
            this.Mac.MaxLength = 32767;
            this.Mac.MinimumSize = new System.Drawing.Size(0, 30);
            this.Mac.Name = "Mac";
            this.Mac.PasswordChar = '\0';
            this.Mac.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.Mac.SelectedText = "";
            this.Mac.SelectionLength = 0;
            this.Mac.SelectionStart = 0;
            this.Mac.Size = new System.Drawing.Size(319, 30);
            this.Mac.TabIndex = 46;
            this.Mac.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Mac.UseSelectable = true;
            this.Mac.UseStyleColors = true;
            this.Mac.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.Mac.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // Blu
            // 
            // 
            // 
            // 
            this.Blu.CustomButton.Image = null;
            this.Blu.CustomButton.Location = new System.Drawing.Point(291, 2);
            this.Blu.CustomButton.Name = "";
            this.Blu.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.Blu.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.Blu.CustomButton.TabIndex = 1;
            this.Blu.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.Blu.CustomButton.UseSelectable = true;
            this.Blu.CustomButton.Visible = false;
            this.Blu.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.Blu.Lines = new string[0];
            this.Blu.Location = new System.Drawing.Point(23, 461);
            this.Blu.MaxLength = 32767;
            this.Blu.MinimumSize = new System.Drawing.Size(0, 30);
            this.Blu.Name = "Blu";
            this.Blu.PasswordChar = '\0';
            this.Blu.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.Blu.SelectedText = "";
            this.Blu.SelectionLength = 0;
            this.Blu.SelectionStart = 0;
            this.Blu.Size = new System.Drawing.Size(319, 30);
            this.Blu.TabIndex = 48;
            this.Blu.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Blu.UseSelectable = true;
            this.Blu.UseStyleColors = true;
            this.Blu.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.Blu.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroButton1
            // 
            this.metroButton1.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButton1.Highlight = true;
            this.metroButton1.Location = new System.Drawing.Point(23, 511);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(318, 28);
            this.metroButton1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroButton1.TabIndex = 49;
            this.metroButton1.Text = "Write";
            this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroButton1.UseCustomBackColor = true;
            this.metroButton1.UseSelectable = true;
            this.metroButton1.UseStyleColors = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // CbSN
            // 
            this.CbSN.AutoSize = true;
            this.CbSN.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.CbSN.Location = new System.Drawing.Point(23, 92);
            this.CbSN.Name = "CbSN";
            this.CbSN.Size = new System.Drawing.Size(140, 25);
            this.CbSN.Style = MetroFramework.MetroColorStyle.Teal;
            this.CbSN.TabIndex = 50;
            this.CbSN.Text = "Serial Number";
            this.CbSN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CbSN.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.CbSN.UseSelectable = true;
            this.CbSN.UseStyleColors = true;
            // 
            // CbImei1
            // 
            this.CbImei1.AutoSize = true;
            this.CbImei1.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.CbImei1.Location = new System.Drawing.Point(23, 162);
            this.CbImei1.Name = "CbImei1";
            this.CbImei1.Size = new System.Drawing.Size(77, 25);
            this.CbImei1.Style = MetroFramework.MetroColorStyle.Teal;
            this.CbImei1.TabIndex = 51;
            this.CbImei1.Text = "Imei 1";
            this.CbImei1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CbImei1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.CbImei1.UseSelectable = true;
            this.CbImei1.UseStyleColors = true;
            // 
            // CbImei2
            // 
            this.CbImei2.AutoSize = true;
            this.CbImei2.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.CbImei2.Location = new System.Drawing.Point(23, 229);
            this.CbImei2.Name = "CbImei2";
            this.CbImei2.Size = new System.Drawing.Size(77, 25);
            this.CbImei2.Style = MetroFramework.MetroColorStyle.Teal;
            this.CbImei2.TabIndex = 52;
            this.CbImei2.Text = "Imei 2";
            this.CbImei2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CbImei2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.CbImei2.UseSelectable = true;
            this.CbImei2.UseStyleColors = true;
            // 
            // CbMeid
            // 
            this.CbMeid.AutoSize = true;
            this.CbMeid.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.CbMeid.Location = new System.Drawing.Point(23, 296);
            this.CbMeid.Name = "CbMeid";
            this.CbMeid.Size = new System.Drawing.Size(68, 25);
            this.CbMeid.Style = MetroFramework.MetroColorStyle.Teal;
            this.CbMeid.TabIndex = 53;
            this.CbMeid.Text = "Meid";
            this.CbMeid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CbMeid.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.CbMeid.UseSelectable = true;
            this.CbMeid.UseStyleColors = true;
            // 
            // CbMac
            // 
            this.CbMac.AutoSize = true;
            this.CbMac.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.CbMac.Location = new System.Drawing.Point(23, 363);
            this.CbMac.Name = "CbMac";
            this.CbMac.Size = new System.Drawing.Size(131, 25);
            this.CbMac.Style = MetroFramework.MetroColorStyle.Teal;
            this.CbMac.TabIndex = 54;
            this.CbMac.Text = "Mac Address";
            this.CbMac.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CbMac.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.CbMac.UseSelectable = true;
            this.CbMac.UseStyleColors = true;
            // 
            // CbBt
            // 
            this.CbBt.AutoSize = true;
            this.CbBt.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.CbBt.Location = new System.Drawing.Point(23, 430);
            this.CbBt.Name = "CbBt";
            this.CbBt.Size = new System.Drawing.Size(175, 25);
            this.CbBt.Style = MetroFramework.MetroColorStyle.Teal;
            this.CbBt.TabIndex = 55;
            this.CbBt.Text = "Bluetooth Address";
            this.CbBt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.CbBt.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.CbBt.UseSelectable = true;
            this.CbBt.UseStyleColors = true;
            // 
            // BackupCbx
            // 
            this.BackupCbx.AutoSize = true;
            this.BackupCbx.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.BackupCbx.Location = new System.Drawing.Point(23, 61);
            this.BackupCbx.Name = "BackupCbx";
            this.BackupCbx.Size = new System.Drawing.Size(85, 25);
            this.BackupCbx.Style = MetroFramework.MetroColorStyle.Teal;
            this.BackupCbx.TabIndex = 56;
            this.BackupCbx.Text = "Backup";
            this.BackupCbx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BackupCbx.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.BackupCbx.UseSelectable = true;
            this.BackupCbx.UseStyleColors = true;
            // 
            // RestrCbx
            // 
            this.RestrCbx.AutoSize = true;
            this.RestrCbx.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.RestrCbx.Location = new System.Drawing.Point(254, 61);
            this.RestrCbx.Name = "RestrCbx";
            this.RestrCbx.Size = new System.Drawing.Size(87, 25);
            this.RestrCbx.Style = MetroFramework.MetroColorStyle.Teal;
            this.RestrCbx.TabIndex = 57;
            this.RestrCbx.Text = "Restore";
            this.RestrCbx.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.RestrCbx.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.RestrCbx.UseSelectable = true;
            this.RestrCbx.UseStyleColors = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // worker
            // 
            this.worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.worker_DoWork);
            this.worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.worker_RunWorkerCompleted);
            // 
            // ImeiChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 553);
            this.Controls.Add(this.RestrCbx);
            this.Controls.Add(this.BackupCbx);
            this.Controls.Add(this.CbBt);
            this.Controls.Add(this.CbMac);
            this.Controls.Add(this.CbMeid);
            this.Controls.Add(this.CbImei2);
            this.Controls.Add(this.CbImei1);
            this.Controls.Add(this.CbSN);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.Blu);
            this.Controls.Add(this.Mac);
            this.Controls.Add(this.Meid);
            this.Controls.Add(this.Imei2);
            this.Controls.Add(this.Imei1);
            this.Controls.Add(this.SN);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImeiChanger";
            this.Resizable = false;
            this.Text = "Imei Changer";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImeiChanger_FormClosing);
            this.Load += new System.EventHandler(this.ImeiChanger_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox SN;
        private MetroFramework.Controls.MetroTextBox Imei1;
        private MetroFramework.Controls.MetroTextBox Imei2;
        private MetroFramework.Controls.MetroTextBox Meid;
        private MetroFramework.Controls.MetroTextBox Mac;
        private MetroFramework.Controls.MetroTextBox Blu;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroCheckBox CbSN;
        private MetroFramework.Controls.MetroCheckBox CbImei1;
        private MetroFramework.Controls.MetroCheckBox CbImei2;
        private MetroFramework.Controls.MetroCheckBox CbMeid;
        private MetroFramework.Controls.MetroCheckBox CbMac;
        private MetroFramework.Controls.MetroCheckBox CbBt;
        private MetroFramework.Controls.MetroCheckBox BackupCbx;
        private MetroFramework.Controls.MetroCheckBox RestrCbx;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker worker;
    }
}