
namespace ESimBoxCloud
{
    partial class SettingsForm
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDownloadLocBrowse = new System.Windows.Forms.Button();
            this.btnUploadLocBrowse = new System.Windows.Forms.Button();
            this.ChkAutoBackUp = new System.Windows.Forms.CheckBox();
            this.txtboxDefaultDownloadLocation = new System.Windows.Forms.TextBox();
            this.txtboxDefaultUploadLoc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LoginStatusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.NameStatusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnLogin.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnLogin.Location = new System.Drawing.Point(20, 19);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(81, 33);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDownloadLocBrowse);
            this.groupBox1.Controls.Add(this.btnUploadLocBrowse);
            this.groupBox1.Controls.Add(this.ChkAutoBackUp);
            this.groupBox1.Controls.Add(this.txtboxDefaultDownloadLocation);
            this.groupBox1.Controls.Add(this.txtboxDefaultUploadLoc);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(24, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(661, 195);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Box BackUp Settings";
            // 
            // btnDownloadLocBrowse
            // 
            this.btnDownloadLocBrowse.Location = new System.Drawing.Point(629, 114);
            this.btnDownloadLocBrowse.Name = "btnDownloadLocBrowse";
            this.btnDownloadLocBrowse.Size = new System.Drawing.Size(26, 21);
            this.btnDownloadLocBrowse.TabIndex = 6;
            this.btnDownloadLocBrowse.Text = "...";
            this.btnDownloadLocBrowse.UseVisualStyleBackColor = true;
            this.btnDownloadLocBrowse.Click += new System.EventHandler(this.btnDownloadLocBrowse_Click);
            // 
            // btnUploadLocBrowse
            // 
            this.btnUploadLocBrowse.Location = new System.Drawing.Point(629, 53);
            this.btnUploadLocBrowse.Name = "btnUploadLocBrowse";
            this.btnUploadLocBrowse.Size = new System.Drawing.Size(26, 21);
            this.btnUploadLocBrowse.TabIndex = 5;
            this.btnUploadLocBrowse.Text = "...";
            this.btnUploadLocBrowse.UseVisualStyleBackColor = true;
            this.btnUploadLocBrowse.Click += new System.EventHandler(this.btnUploadLocBrowse_Click);
            // 
            // ChkAutoBackUp
            // 
            this.ChkAutoBackUp.AutoSize = true;
            this.ChkAutoBackUp.Location = new System.Drawing.Point(20, 159);
            this.ChkAutoBackUp.Name = "ChkAutoBackUp";
            this.ChkAutoBackUp.Size = new System.Drawing.Size(226, 17);
            this.ChkAutoBackUp.TabIndex = 4;
            this.ChkAutoBackUp.Text = "Automatically BackUp to Box while Saving";
            this.ChkAutoBackUp.UseVisualStyleBackColor = true;
            this.ChkAutoBackUp.CheckedChanged += new System.EventHandler(this.ChkAutoBackUp_CheckedChanged);
            // 
            // txtboxDefaultDownloadLocation
            // 
            this.txtboxDefaultDownloadLocation.Location = new System.Drawing.Point(190, 114);
            this.txtboxDefaultDownloadLocation.Multiline = true;
            this.txtboxDefaultDownloadLocation.Name = "txtboxDefaultDownloadLocation";
            this.txtboxDefaultDownloadLocation.Size = new System.Drawing.Size(437, 21);
            this.txtboxDefaultDownloadLocation.TabIndex = 3;
            this.txtboxDefaultDownloadLocation.TextChanged += new System.EventHandler(this.txtboxDefaultDownloadLocation_TextChanged);
            // 
            // txtboxDefaultUploadLoc
            // 
            this.txtboxDefaultUploadLoc.Location = new System.Drawing.Point(190, 54);
            this.txtboxDefaultUploadLoc.Multiline = true;
            this.txtboxDefaultUploadLoc.Name = "txtboxDefaultUploadLoc";
            this.txtboxDefaultUploadLoc.Size = new System.Drawing.Size(437, 21);
            this.txtboxDefaultUploadLoc.TabIndex = 2;
            this.txtboxDefaultUploadLoc.TextChanged += new System.EventHandler(this.txtboxDefaultUploadLoc_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Default Download Location";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Default Upload Location";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnSave.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.Location = new System.Drawing.Point(287, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 33);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnClose.Location = new System.Drawing.Point(552, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 33);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnLogin);
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Location = new System.Drawing.Point(24, 241);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(661, 73);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoginStatusStrip,
            this.NameStatusStrip});
            this.statusStrip1.Location = new System.Drawing.Point(0, 343);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(709, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // LoginStatusStrip
            // 
            this.LoginStatusStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LoginStatusStrip.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.LoginStatusStrip.Name = "LoginStatusStrip";
            this.LoginStatusStrip.Size = new System.Drawing.Size(347, 17);
            this.LoginStatusStrip.Spring = true;
            this.LoginStatusStrip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NameStatusStrip
            // 
            this.NameStatusStrip.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.NameStatusStrip.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.NameStatusStrip.Name = "NameStatusStrip";
            this.NameStatusStrip.Size = new System.Drawing.Size(347, 17);
            this.NameStatusStrip.Spring = true;
            this.NameStatusStrip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 365);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ChkAutoBackUp;
        private System.Windows.Forms.TextBox txtboxDefaultDownloadLocation;
        private System.Windows.Forms.TextBox txtboxDefaultUploadLoc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDownloadLocBrowse;
        private System.Windows.Forms.Button btnUploadLocBrowse;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LoginStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel NameStatusStrip;
    }
}