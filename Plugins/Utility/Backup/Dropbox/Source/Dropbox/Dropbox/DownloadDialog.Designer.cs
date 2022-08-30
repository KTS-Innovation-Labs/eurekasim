
namespace Dropbox
{
    partial class DownloadDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadDialog));
            this.btnUploadDlgCancel = new System.Windows.Forms.Button();
            this.btnDBDownload = new System.Windows.Forms.Button();
            this.btnDBLocBrowse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkOpenAftDownload = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDBDowloadFile = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarAccName = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusBarAccCountry = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUploadDlgCancel
            // 
            this.btnUploadDlgCancel.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnUploadDlgCancel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUploadDlgCancel.Location = new System.Drawing.Point(637, 152);
            this.btnUploadDlgCancel.Name = "btnUploadDlgCancel";
            this.btnUploadDlgCancel.Size = new System.Drawing.Size(112, 40);
            this.btnUploadDlgCancel.TabIndex = 8;
            this.btnUploadDlgCancel.Text = "Close";
            this.btnUploadDlgCancel.UseVisualStyleBackColor = false;
            this.btnUploadDlgCancel.Click += new System.EventHandler(this.BtnDownloadDlgClose_Click);
            // 
            // btnDBDownload
            // 
            this.btnDBDownload.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnDBDownload.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDBDownload.Location = new System.Drawing.Point(627, 84);
            this.btnDBDownload.Name = "btnDBDownload";
            this.btnDBDownload.Size = new System.Drawing.Size(112, 40);
            this.btnDBDownload.TabIndex = 1;
            this.btnDBDownload.Text = "Download";
            this.btnDBDownload.UseVisualStyleBackColor = false;
            this.btnDBDownload.Click += new System.EventHandler(this.BtnDBDownload_Click);
            // 
            // btnDBLocBrowse
            // 
            this.btnDBLocBrowse.Enabled = false;
            this.btnDBLocBrowse.Location = new System.Drawing.Point(708, 31);
            this.btnDBLocBrowse.Name = "btnDBLocBrowse";
            this.btnDBLocBrowse.Size = new System.Drawing.Size(31, 35);
            this.btnDBLocBrowse.TabIndex = 2;
            this.btnDBLocBrowse.Text = "...";
            this.btnDBLocBrowse.UseVisualStyleBackColor = true;
            this.btnDBLocBrowse.Click += new System.EventHandler(this.BtnDBLocBrowse_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkOpenAftDownload);
            this.groupBox1.Controls.Add(this.btnDBDownload);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnDBLocBrowse);
            this.groupBox1.Controls.Add(this.txtDBDowloadFile);
            this.groupBox1.Location = new System.Drawing.Point(10, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(745, 137);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download";
            // 
            // chkOpenAftDownload
            // 
            this.chkOpenAftDownload.AutoSize = true;
            this.chkOpenAftDownload.Location = new System.Drawing.Point(18, 90);
            this.chkOpenAftDownload.Name = "chkOpenAftDownload";
            this.chkOpenAftDownload.Size = new System.Drawing.Size(184, 21);
            this.chkOpenAftDownload.TabIndex = 4;
            this.chkOpenAftDownload.Text = "Open file after download";
            this.chkOpenAftDownload.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Downloading File:";
            // 
            // txtDBDowloadFile
            // 
            this.txtDBDowloadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBDowloadFile.Location = new System.Drawing.Point(176, 31);
            this.txtDBDowloadFile.Multiline = true;
            this.txtDBDowloadFile.Name = "txtDBDowloadFile";
            this.txtDBDowloadFile.ReadOnly = true;
            this.txtDBDowloadFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDBDowloadFile.Size = new System.Drawing.Size(526, 35);
            this.txtDBDowloadFile.TabIndex = 0;
            this.txtDBDowloadFile.TextChanged += new System.EventHandler(this.TxtDowloadFile_Changed);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarStatus,
            this.statusBarAccName,
            this.StatusBarAccCountry});
            this.statusStrip1.Location = new System.Drawing.Point(0, 203);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(767, 40);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusBarStatus
            // 
            this.statusBarStatus.AutoSize = false;
            this.statusBarStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBarStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.statusBarStatus.Name = "statusBarStatus";
            this.statusBarStatus.Size = new System.Drawing.Size(360, 34);
            this.statusBarStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusBarAccName
            // 
            this.statusBarAccName.AutoSize = false;
            this.statusBarAccName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBarAccName.Name = "statusBarAccName";
            this.statusBarAccName.Size = new System.Drawing.Size(250, 34);
            this.statusBarAccName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusBarAccCountry
            // 
            this.StatusBarAccCountry.AutoSize = false;
            this.StatusBarAccCountry.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatusBarAccCountry.Name = "StatusBarAccCountry";
            this.StatusBarAccCountry.Size = new System.Drawing.Size(80, 34);
            this.StatusBarAccCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DownloadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 243);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnUploadDlgCancel);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(785, 290);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(785, 290);
            this.Name = "DownloadDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DownloadDialog";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnUploadDlgCancel;
        private System.Windows.Forms.Button btnDBLocBrowse;
        private System.Windows.Forms.Button btnDBDownload;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDBDowloadFile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarStatus;
        private System.Windows.Forms.ToolStripStatusLabel statusBarAccName;
        private System.Windows.Forms.ToolStripStatusLabel StatusBarAccCountry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkOpenAftDownload;
    }
}