namespace goolgedrivetest
{
    partial class GDUploadDlg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GDUploadDlg));
            this.label2 = new System.Windows.Forms.Label();
            this.txtGDUploadFile = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUploadFileBrowse = new System.Windows.Forms.Button();
            this.btnUploadDlgCancel = new System.Windows.Forms.Button();
            this.btnGDuploadLocBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGDLocation = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnGDUpload = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Uploading File:";
            // 
            // txtGDUploadFile
            // 
            this.txtGDUploadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGDUploadFile.Location = new System.Drawing.Point(161, 32);
            this.txtGDUploadFile.Multiline = true;
            this.txtGDUploadFile.Name = "txtGDUploadFile";
            this.txtGDUploadFile.ReadOnly = true;
            this.txtGDUploadFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGDUploadFile.Size = new System.Drawing.Size(488, 35);
            this.txtGDUploadFile.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUploadFileBrowse);
            this.groupBox1.Controls.Add(this.btnUploadDlgCancel);
            this.groupBox1.Controls.Add(this.btnGDUpload);
            this.groupBox1.Controls.Add(this.btnGDuploadLocBrowse);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtGDUploadFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtGDLocation);
            this.groupBox1.Location = new System.Drawing.Point(12, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(695, 191);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upload";
            // 
            // btnUploadFileBrowse
            // 
            this.btnUploadFileBrowse.Location = new System.Drawing.Point(655, 32);
            this.btnUploadFileBrowse.Name = "btnUploadFileBrowse";
            this.btnUploadFileBrowse.Size = new System.Drawing.Size(31, 35);
            this.btnUploadFileBrowse.TabIndex = 4;
            this.btnUploadFileBrowse.Text = "...";
            this.btnUploadFileBrowse.UseVisualStyleBackColor = true;
            this.btnUploadFileBrowse.Click += new System.EventHandler(this.btnUploadFileBrowse_Click);
            // 
            // btnUploadDlgCancel
            // 
            this.btnUploadDlgCancel.BackColor = System.Drawing.Color.Green;
            this.btnUploadDlgCancel.ForeColor = System.Drawing.Color.Transparent;
            this.btnUploadDlgCancel.Location = new System.Drawing.Point(574, 145);
            this.btnUploadDlgCancel.Name = "btnUploadDlgCancel";
            this.btnUploadDlgCancel.Size = new System.Drawing.Size(112, 40);
            this.btnUploadDlgCancel.TabIndex = 8;
            this.btnUploadDlgCancel.Text = "Close";
            this.btnUploadDlgCancel.UseVisualStyleBackColor = false;
            this.btnUploadDlgCancel.Click += new System.EventHandler(this.btnUploadDlgCancel_Click);
            // 
            // btnGDuploadLocBrowse
            // 
            this.btnGDuploadLocBrowse.Location = new System.Drawing.Point(655, 88);
            this.btnGDuploadLocBrowse.Name = "btnGDuploadLocBrowse";
            this.btnGDuploadLocBrowse.Size = new System.Drawing.Size(31, 35);
            this.btnGDuploadLocBrowse.TabIndex = 3;
            this.btnGDuploadLocBrowse.Text = "...";
            this.btnGDuploadLocBrowse.UseVisualStyleBackColor = true;
            this.btnGDuploadLocBrowse.Click += new System.EventHandler(this.btnDBLocBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gdrive Location:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtGDLocation
            // 
            this.txtGDLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGDLocation.Location = new System.Drawing.Point(162, 88);
            this.txtGDLocation.Multiline = true;
            this.txtGDLocation.Name = "txtGDLocation";
            this.txtGDLocation.ReadOnly = true;
            this.txtGDLocation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGDLocation.Size = new System.Drawing.Size(488, 35);
            this.txtGDLocation.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(726, 31);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnGDUpload
            // 
            this.btnGDUpload.BackColor = System.Drawing.Color.Green;
            this.btnGDUpload.ForeColor = System.Drawing.Color.Transparent;
            this.btnGDUpload.Location = new System.Drawing.Point(440, 145);
            this.btnGDUpload.Name = "btnGDUpload";
            this.btnGDUpload.Size = new System.Drawing.Size(112, 40);
            this.btnGDUpload.TabIndex = 1;
            this.btnGDUpload.Text = "Upload";
            this.btnGDUpload.UseVisualStyleBackColor = false;
            this.btnGDUpload.Click += new System.EventHandler(this.btnGDUpload_Click);
            // 
            // GDUploadDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 237);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GDUploadDlg";
            this.Text = "GDUploadDlg";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGDUploadFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUploadFileBrowse;
        private System.Windows.Forms.Button btnUploadDlgCancel;
        private System.Windows.Forms.Button btnGDuploadLocBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGDLocation;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button btnGDUpload;
    }
}