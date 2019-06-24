namespace RN3_PRO_TOOLKIT
{
    partial class RemoteService
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
            this.PelitabangsaBrowser = new System.Windows.Forms.WebBrowser();
            this.ServiceOption = new System.Windows.Forms.Panel();
            this.RSA = new MetroFramework.Controls.MetroTile();
            this.RSC = new MetroFramework.Controls.MetroTile();
            this.ServiceOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // PelitabangsaBrowser
            // 
            this.PelitabangsaBrowser.AllowWebBrowserDrop = false;
            this.PelitabangsaBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PelitabangsaBrowser.IsWebBrowserContextMenuEnabled = false;
            this.PelitabangsaBrowser.Location = new System.Drawing.Point(20, 60);
            this.PelitabangsaBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.PelitabangsaBrowser.Name = "PelitabangsaBrowser";
            this.PelitabangsaBrowser.Size = new System.Drawing.Size(325, 473);
            this.PelitabangsaBrowser.TabIndex = 0;
            this.PelitabangsaBrowser.WebBrowserShortcutsEnabled = false;
            this.PelitabangsaBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.PelitabangsaBrowser_DocumentCompleted);
            // 
            // ServiceOption
            // 
            this.ServiceOption.Controls.Add(this.RSA);
            this.ServiceOption.Controls.Add(this.RSC);
            this.ServiceOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServiceOption.Location = new System.Drawing.Point(20, 60);
            this.ServiceOption.Name = "ServiceOption";
            this.ServiceOption.Size = new System.Drawing.Size(325, 473);
            this.ServiceOption.TabIndex = 1;
            this.ServiceOption.Visible = false;
            // 
            // RSA
            // 
            this.RSA.ActiveControl = null;
            this.RSA.Location = new System.Drawing.Point(36, 260);
            this.RSA.Name = "RSA";
            this.RSA.Size = new System.Drawing.Size(253, 122);
            this.RSA.TabIndex = 1;
            this.RSA.Text = "I Must Help Some";
            this.RSA.UseSelectable = true;
            // 
            // RSC
            // 
            this.RSC.ActiveControl = null;
            this.RSC.Location = new System.Drawing.Point(36, 90);
            this.RSC.Name = "RSC";
            this.RSC.Size = new System.Drawing.Size(253, 122);
            this.RSC.Style = MetroFramework.MetroColorStyle.Green;
            this.RSC.TabIndex = 0;
            this.RSC.Text = "I Need Help";
            this.RSC.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.RSC.UseSelectable = true;
            // 
            // RemoteService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 553);
            this.Controls.Add(this.PelitabangsaBrowser);
            this.Controls.Add(this.ServiceOption);
            this.Name = "RemoteService";
            this.Resizable = false;
            this.Text = "RemoteService";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.ServiceOption.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser PelitabangsaBrowser;
        private System.Windows.Forms.Panel ServiceOption;
        private MetroFramework.Controls.MetroTile RSA;
        private MetroFramework.Controls.MetroTile RSC;
    }
}