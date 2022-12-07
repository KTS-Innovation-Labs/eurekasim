
namespace ManagerDll
{
    partial class Download
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Download));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDestinationPathBrowse = new System.Windows.Forms.Button();
            this.textBoxDestinationDir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelProgress = new System.Windows.Forms.Label();
            this.chkAfterDownload = new System.Windows.Forms.CheckBox();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.buttonRemotePathBrowse = new System.Windows.Forms.Button();
            this.textBoxRemoteFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonDestinationPathBrowse);
            this.groupBox1.Controls.Add(this.textBoxDestinationDir);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelStatus);
            this.groupBox1.Controls.Add(this.labelProgress);
            this.groupBox1.Controls.Add(this.chkAfterDownload);
            this.groupBox1.Controls.Add(this.buttonDownload);
            this.groupBox1.Controls.Add(this.buttonRemotePathBrowse);
            this.groupBox1.Controls.Add(this.textBoxRemoteFilePath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(669, 247);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download";
            // 
            // buttonDestinationPathBrowse
            // 
            this.buttonDestinationPathBrowse.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonDestinationPathBrowse.Location = new System.Drawing.Point(625, 86);
            this.buttonDestinationPathBrowse.Name = "buttonDestinationPathBrowse";
            this.buttonDestinationPathBrowse.Size = new System.Drawing.Size(38, 24);
            this.buttonDestinationPathBrowse.TabIndex = 8;
            this.buttonDestinationPathBrowse.Text = "...";
            this.buttonDestinationPathBrowse.UseVisualStyleBackColor = false;
            this.buttonDestinationPathBrowse.Click += new System.EventHandler(this.buttonDestinationPathBrowse_Click);
            // 
            // textBoxDestinationDir
            // 
            this.textBoxDestinationDir.Location = new System.Drawing.Point(198, 90);
            this.textBoxDestinationDir.Name = "textBoxDestinationDir";
            this.textBoxDestinationDir.Size = new System.Drawing.Size(421, 20);
            this.textBoxDestinationDir.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Select Local Download Folder";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(48, 216);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(43, 13);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Status";
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProgress.ForeColor = System.Drawing.Color.Blue;
            this.labelProgress.Location = new System.Drawing.Point(48, 174);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(56, 13);
            this.labelProgress.TabIndex = 4;
            this.labelProgress.Text = "Progress";
            // 
            // chkAfterDownload
            // 
            this.chkAfterDownload.AutoSize = true;
            this.chkAfterDownload.Location = new System.Drawing.Point(19, 142);
            this.chkAfterDownload.Name = "chkAfterDownload";
            this.chkAfterDownload.Size = new System.Drawing.Size(165, 17);
            this.chkAfterDownload.TabIndex = 3;
            this.chkAfterDownload.Text = "Open file after download";
            this.chkAfterDownload.UseVisualStyleBackColor = true;
            // 
            // buttonDownload
            // 
            this.buttonDownload.BackColor = System.Drawing.Color.MidnightBlue;
            this.buttonDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDownload.ForeColor = System.Drawing.Color.White;
            this.buttonDownload.Location = new System.Drawing.Point(490, 195);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(150, 34);
            this.buttonDownload.TabIndex = 1;
            this.buttonDownload.Text = "Download";
            this.buttonDownload.UseVisualStyleBackColor = false;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // buttonRemotePathBrowse
            // 
            this.buttonRemotePathBrowse.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonRemotePathBrowse.Location = new System.Drawing.Point(625, 39);
            this.buttonRemotePathBrowse.Name = "buttonRemotePathBrowse";
            this.buttonRemotePathBrowse.Size = new System.Drawing.Size(38, 24);
            this.buttonRemotePathBrowse.TabIndex = 2;
            this.buttonRemotePathBrowse.Text = "...";
            this.buttonRemotePathBrowse.UseVisualStyleBackColor = false;
            this.buttonRemotePathBrowse.Click += new System.EventHandler(this.buttonRemotePathBrowse_Click);
            // 
            // textBoxRemoteFilePath
            // 
            this.textBoxRemoteFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxRemoteFilePath.Location = new System.Drawing.Point(198, 43);
            this.textBoxRemoteFilePath.Name = "textBoxRemoteFilePath";
            this.textBoxRemoteFilePath.Size = new System.Drawing.Size(421, 20);
            this.textBoxRemoteFilePath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Remote File Path";
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.MidnightBlue;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.ForeColor = System.Drawing.Color.White;
            this.buttonClose.Location = new System.Drawing.Point(502, 289);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(150, 34);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // Download
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 335);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Download";
            this.Text = "Download from IDrive";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonRemotePathBrowse;
        private System.Windows.Forms.TextBox textBoxRemoteFilePath;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.CheckBox chkAfterDownload;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Button buttonDestinationPathBrowse;
        private System.Windows.Forms.TextBox textBoxDestinationDir;
        private System.Windows.Forms.Label label2;
    }
}