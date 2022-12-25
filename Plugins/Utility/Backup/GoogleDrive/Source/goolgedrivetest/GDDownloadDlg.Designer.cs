namespace goolgedrivetest
{
    partial class GDDownloadDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GDDownloadDlg));
            this.txtGDDowloadFile = new System.Windows.Forms.TextBox();
            this.btnUploadDlgCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkOpenAftDownload = new System.Windows.Forms.CheckBox();
            this.btnGDDownload = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDBLocBrowse = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtGDDowloadFile
            // 
            this.txtGDDowloadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGDDowloadFile.Location = new System.Drawing.Point(176, 31);
            this.txtGDDowloadFile.Multiline = true;
            this.txtGDDowloadFile.Name = "txtGDDowloadFile";
            this.txtGDDowloadFile.ReadOnly = true;
            this.txtGDDowloadFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGDDowloadFile.Size = new System.Drawing.Size(526, 35);
            this.txtGDDowloadFile.TabIndex = 0;
            // 
            // btnUploadDlgCancel
            // 
            this.btnUploadDlgCancel.BackColor = System.Drawing.Color.Green;
            this.btnUploadDlgCancel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUploadDlgCancel.Location = new System.Drawing.Point(627, 97);
            this.btnUploadDlgCancel.Name = "btnUploadDlgCancel";
            this.btnUploadDlgCancel.Size = new System.Drawing.Size(112, 40);
            this.btnUploadDlgCancel.TabIndex = 10;
            this.btnUploadDlgCancel.Text = "Close";
            this.btnUploadDlgCancel.UseVisualStyleBackColor = false;
            this.btnUploadDlgCancel.Click += new System.EventHandler(this.btnUploadDlgCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUploadDlgCancel);
            this.groupBox1.Controls.Add(this.chkOpenAftDownload);
            this.groupBox1.Controls.Add(this.btnGDDownload);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnDBLocBrowse);
            this.groupBox1.Controls.Add(this.txtGDDowloadFile);
            this.groupBox1.Location = new System.Drawing.Point(3, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(745, 137);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download";
            // 
            // chkOpenAftDownload
            // 
            this.chkOpenAftDownload.AutoSize = true;
            this.chkOpenAftDownload.Location = new System.Drawing.Point(18, 90);
            this.chkOpenAftDownload.Name = "chkOpenAftDownload";
            this.chkOpenAftDownload.Size = new System.Drawing.Size(173, 20);
            this.chkOpenAftDownload.TabIndex = 4;
            this.chkOpenAftDownload.Text = "Open file after download";
            this.chkOpenAftDownload.UseVisualStyleBackColor = true;
            // 
            // btnGDDownload
            // 
            this.btnGDDownload.BackColor = System.Drawing.Color.Green;
            this.btnGDDownload.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnGDDownload.Location = new System.Drawing.Point(500, 97);
            this.btnGDDownload.Name = "btnGDDownload";
            this.btnGDDownload.Size = new System.Drawing.Size(112, 40);
            this.btnGDDownload.TabIndex = 1;
            this.btnGDDownload.Text = "Download";
            this.btnGDDownload.UseVisualStyleBackColor = false;
            this.btnGDDownload.Click += new System.EventHandler(this.btnDBDownload_Click_1);
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
            // btnDBLocBrowse
            // 
            this.btnDBLocBrowse.Location = new System.Drawing.Point(708, 31);
            this.btnDBLocBrowse.Name = "btnDBLocBrowse";
            this.btnDBLocBrowse.Size = new System.Drawing.Size(31, 35);
            this.btnDBLocBrowse.TabIndex = 2;
            this.btnDBLocBrowse.Text = "...";
            this.btnDBLocBrowse.UseVisualStyleBackColor = true;
            this.btnDBLocBrowse.Click += new System.EventHandler(this.btnDBLocBrowse_Click_1);
            // 
            // GDDownloadDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 214);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GDDownloadDlg";
            this.Text = "DBDownloadDlg";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtGDDowloadFile;
        private System.Windows.Forms.Button btnUploadDlgCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkOpenAftDownload;
        private System.Windows.Forms.Button btnGDDownload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDBLocBrowse;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}