
namespace ManagerDll
{
    partial class Upload
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Upload));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelProgress = new System.Windows.Forms.Label();
            this.buttonDestinationPathBrowseClick = new System.Windows.Forms.Button();
            this.buttonUploadFile = new System.Windows.Forms.Button();
            this.textBoxDestinationDir = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxLocalFilePath = new System.Windows.Forms.TextBox();
            this.buttonLocalPathBrowseClick = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelStatus);
            this.groupBox1.Controls.Add(this.labelProgress);
            this.groupBox1.Controls.Add(this.buttonDestinationPathBrowseClick);
            this.groupBox1.Controls.Add(this.buttonUploadFile);
            this.groupBox1.Controls.Add(this.textBoxDestinationDir);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxLocalFilePath);
            this.groupBox1.Controls.Add(this.buttonLocalPathBrowseClick);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(23, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(654, 226);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select the Upload Parameters";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(33, 183);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(43, 13);
            this.labelStatus.TabIndex = 8;
            this.labelStatus.Text = "Status";
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.ForeColor = System.Drawing.Color.Blue;
            this.labelProgress.Location = new System.Drawing.Point(30, 139);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(56, 13);
            this.labelProgress.TabIndex = 7;
            this.labelProgress.Text = "Progress";
            // 
            // buttonDestinationPathBrowseClick
            // 
            this.buttonDestinationPathBrowseClick.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonDestinationPathBrowseClick.Location = new System.Drawing.Point(593, 87);
            this.buttonDestinationPathBrowseClick.Name = "buttonDestinationPathBrowseClick";
            this.buttonDestinationPathBrowseClick.Size = new System.Drawing.Size(35, 23);
            this.buttonDestinationPathBrowseClick.TabIndex = 6;
            this.buttonDestinationPathBrowseClick.Text = "...";
            this.buttonDestinationPathBrowseClick.UseVisualStyleBackColor = false;
            this.buttonDestinationPathBrowseClick.Click += new System.EventHandler(this.buttonDestinationPathBrowseClick_Click);
            // 
            // buttonUploadFile
            // 
            this.buttonUploadFile.BackColor = System.Drawing.Color.MidnightBlue;
            this.buttonUploadFile.ForeColor = System.Drawing.Color.White;
            this.buttonUploadFile.Location = new System.Drawing.Point(488, 164);
            this.buttonUploadFile.Name = "buttonUploadFile";
            this.buttonUploadFile.Size = new System.Drawing.Size(128, 33);
            this.buttonUploadFile.TabIndex = 1;
            this.buttonUploadFile.Text = "Upload File";
            this.buttonUploadFile.UseVisualStyleBackColor = false;
            this.buttonUploadFile.Click += new System.EventHandler(this.buttonUpload_Click);
            // 
            // textBoxDestinationDir
            // 
            this.textBoxDestinationDir.Location = new System.Drawing.Point(196, 89);
            this.textBoxDestinationDir.Name = "textBoxDestinationDir";
            this.textBoxDestinationDir.Size = new System.Drawing.Size(391, 20);
            this.textBoxDestinationDir.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(188, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select the Destination Directory";
            // 
            // textBoxLocalFilePath
            // 
            this.textBoxLocalFilePath.Location = new System.Drawing.Point(196, 42);
            this.textBoxLocalFilePath.Name = "textBoxLocalFilePath";
            this.textBoxLocalFilePath.Size = new System.Drawing.Size(391, 20);
            this.textBoxLocalFilePath.TabIndex = 3;
            // 
            // buttonLocalPathBrowseClick
            // 
            this.buttonLocalPathBrowseClick.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonLocalPathBrowseClick.Location = new System.Drawing.Point(593, 40);
            this.buttonLocalPathBrowseClick.Name = "buttonLocalPathBrowseClick";
            this.buttonLocalPathBrowseClick.Size = new System.Drawing.Size(35, 23);
            this.buttonLocalPathBrowseClick.TabIndex = 2;
            this.buttonLocalPathBrowseClick.Text = "...";
            this.buttonLocalPathBrowseClick.UseVisualStyleBackColor = false;
            this.buttonLocalPathBrowseClick.Click += new System.EventHandler(this.buttonLocalPathBrowseClick_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the Source File";
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.MidnightBlue;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.ForeColor = System.Drawing.Color.White;
            this.buttonClose.Location = new System.Drawing.Point(511, 266);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(128, 33);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // Upload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 321);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Upload";
            this.Text = "Upload Files to Cloud";
            this.Load += new System.EventHandler(this.Upload_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonLocalPathBrowseClick;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonUploadFile;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonDestinationPathBrowseClick;
        private System.Windows.Forms.TextBox textBoxDestinationDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxLocalFilePath;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelProgress;
    }
}