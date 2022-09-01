
namespace Dropbox
{
    partial class DbSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbSettings));
            this.btnLogInOut = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDfltDownLocBrowse = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDfltFileDownLoc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAutoBackup = new System.Windows.Forms.CheckBox();
            this.btnDfltUpLocBrowse = new System.Windows.Forms.Button();
            this.txtDfltFileUpLoc = new System.Windows.Forms.TextBox();
            this.btnSettingsSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarAccName = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogInOut
            // 
            this.btnLogInOut.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnLogInOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogInOut.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnLogInOut.Location = new System.Drawing.Point(23, 253);
            this.btnLogInOut.Name = "btnLogInOut";
            this.btnLogInOut.Size = new System.Drawing.Size(116, 36);
            this.btnLogInOut.TabIndex = 1;
            this.btnLogInOut.Text = "Login";
            this.btnLogInOut.UseVisualStyleBackColor = false;
            this.btnLogInOut.Click += new System.EventHandler(this.BtnLogInOut_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnDfltDownLocBrowse);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtDfltFileDownLoc);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.chkAutoBackup);
            this.groupBox3.Controls.Add(this.btnDfltUpLocBrowse);
            this.groupBox3.Controls.Add(this.txtDfltFileUpLoc);
            this.groupBox3.Location = new System.Drawing.Point(12, 34);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(765, 213);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Dropbox Backup Settings";
            // 
            // btnDfltDownLocBrowse
            // 
            this.btnDfltDownLocBrowse.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDfltDownLocBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDfltDownLocBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDfltDownLocBrowse.Location = new System.Drawing.Point(715, 101);
            this.btnDfltDownLocBrowse.Name = "btnDfltDownLocBrowse";
            this.btnDfltDownLocBrowse.Size = new System.Drawing.Size(44, 35);
            this.btnDfltDownLocBrowse.TabIndex = 2;
            this.btnDfltDownLocBrowse.Text = "...";
            this.btnDfltDownLocBrowse.UseVisualStyleBackColor = false;
            this.btnDfltDownLocBrowse.Click += new System.EventHandler(this.BtnBrowseDownloadLoc_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Default Download Location:";
            // 
            // txtDfltFileDownLoc
            // 
            this.txtDfltFileDownLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDfltFileDownLoc.Location = new System.Drawing.Point(202, 101);
            this.txtDfltFileDownLoc.Multiline = true;
            this.txtDfltFileDownLoc.Name = "txtDfltFileDownLoc";
            this.txtDfltFileDownLoc.ReadOnly = true;
            this.txtDfltFileDownLoc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDfltFileDownLoc.Size = new System.Drawing.Size(507, 35);
            this.txtDfltFileDownLoc.TabIndex = 1;
            this.txtDfltFileDownLoc.TextChanged += new System.EventHandler(this.TxtDfltDownLoc_Changed);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Default Upload Location:";
            // 
            // chkAutoBackup
            // 
            this.chkAutoBackup.AutoSize = true;
            this.chkAutoBackup.Location = new System.Drawing.Point(11, 170);
            this.chkAutoBackup.Name = "chkAutoBackup";
            this.chkAutoBackup.Size = new System.Drawing.Size(316, 21);
            this.chkAutoBackup.TabIndex = 5;
            this.chkAutoBackup.Text = "Automatically backup to Dropbox while saving";
            this.chkAutoBackup.UseVisualStyleBackColor = true;
            this.chkAutoBackup.CheckedChanged += new System.EventHandler(this.ChkAutoSave_Changed);
            // 
            // btnDfltUpLocBrowse
            // 
            this.btnDfltUpLocBrowse.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDfltUpLocBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDfltUpLocBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDfltUpLocBrowse.Location = new System.Drawing.Point(715, 40);
            this.btnDfltUpLocBrowse.Name = "btnDfltUpLocBrowse";
            this.btnDfltUpLocBrowse.Size = new System.Drawing.Size(44, 35);
            this.btnDfltUpLocBrowse.TabIndex = 1;
            this.btnDfltUpLocBrowse.Text = "...";
            this.btnDfltUpLocBrowse.UseVisualStyleBackColor = false;
            this.btnDfltUpLocBrowse.Click += new System.EventHandler(this.BtnBrowseUploadLoc_Click);
            // 
            // txtDfltFileUpLoc
            // 
            this.txtDfltFileUpLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDfltFileUpLoc.Location = new System.Drawing.Point(202, 40);
            this.txtDfltFileUpLoc.Multiline = true;
            this.txtDfltFileUpLoc.Name = "txtDfltFileUpLoc";
            this.txtDfltFileUpLoc.ReadOnly = true;
            this.txtDfltFileUpLoc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDfltFileUpLoc.Size = new System.Drawing.Size(507, 35);
            this.txtDfltFileUpLoc.TabIndex = 0;
            this.txtDfltFileUpLoc.TextChanged += new System.EventHandler(this.TxtDfltUpLoc_Changed);
            // 
            // btnSettingsSave
            // 
            this.btnSettingsSave.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnSettingsSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettingsSave.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnSettingsSave.Location = new System.Drawing.Point(370, 253);
            this.btnSettingsSave.Name = "btnSettingsSave";
            this.btnSettingsSave.Size = new System.Drawing.Size(116, 36);
            this.btnSettingsSave.TabIndex = 6;
            this.btnSettingsSave.Text = "Save";
            this.btnSettingsSave.UseVisualStyleBackColor = false;
            this.btnSettingsSave.Click += new System.EventHandler(this.BtnSettingsSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnClose.Location = new System.Drawing.Point(655, 253);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(116, 36);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarStatus,
            this.statusBarAccName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 302);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(789, 40);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusBarStatus
            // 
            this.statusBarStatus.AutoSize = false;
            this.statusBarStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBarStatus.ForeColor = System.Drawing.SystemColors.Highlight;
            this.statusBarStatus.Name = "statusBarStatus";
            this.statusBarStatus.Padding = new System.Windows.Forms.Padding(1);
            this.statusBarStatus.Size = new System.Drawing.Size(450, 34);
            this.statusBarStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusBarAccName
            // 
            this.statusBarAccName.AutoSize = false;
            this.statusBarAccName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBarAccName.Name = "statusBarAccName";
            this.statusBarAccName.Padding = new System.Windows.Forms.Padding(1);
            this.statusBarAccName.Size = new System.Drawing.Size(280, 34);
            this.statusBarAccName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DbSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 342);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSettingsSave);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnLogInOut);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(807, 389);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(807, 389);
            this.Name = "DbSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DbSettings";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnLogInOut;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnDfltUpLocBrowse;
        private System.Windows.Forms.TextBox txtDfltFileUpLoc;
        private System.Windows.Forms.Button btnDfltDownLocBrowse;
        private System.Windows.Forms.TextBox txtDfltFileDownLoc;
        private System.Windows.Forms.CheckBox chkAutoBackup;
        private System.Windows.Forms.Button btnSettingsSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarStatus;
        private System.Windows.Forms.ToolStripStatusLabel statusBarAccName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}