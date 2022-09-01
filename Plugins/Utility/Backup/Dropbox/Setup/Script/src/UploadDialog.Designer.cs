
namespace Dropbox
{
    partial class UploadDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadDialog));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUploadFileBrowse = new System.Windows.Forms.Button();
            this.btnDBUpload = new System.Windows.Forms.Button();
            this.btnDBLocBrowse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDBUploadFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDBLocation = new System.Windows.Forms.TextBox();
            this.btnUploadDlgCancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarAccName = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarAccCountry = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUploadFileBrowse);
            this.groupBox1.Controls.Add(this.btnDBUpload);
            this.groupBox1.Controls.Add(this.btnDBLocBrowse);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDBUploadFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDBLocation);
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(695, 191);
            this.groupBox1.TabIndex = 1;
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
            this.btnUploadFileBrowse.Click += new System.EventHandler(this.BtnUploadFileDialog_Click);
            // 
            // btnDBUpload
            // 
            this.btnDBUpload.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnDBUpload.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDBUpload.Location = new System.Drawing.Point(538, 136);
            this.btnDBUpload.Name = "btnDBUpload";
            this.btnDBUpload.Size = new System.Drawing.Size(112, 40);
            this.btnDBUpload.TabIndex = 1;
            this.btnDBUpload.Text = "Upload";
            this.btnDBUpload.UseVisualStyleBackColor = false;
            this.btnDBUpload.Click += new System.EventHandler(this.BtnDBUpload_Click);
            // 
            // btnDBLocBrowse
            // 
            this.btnDBLocBrowse.Location = new System.Drawing.Point(655, 88);
            this.btnDBLocBrowse.Name = "btnDBLocBrowse";
            this.btnDBLocBrowse.Size = new System.Drawing.Size(31, 35);
            this.btnDBLocBrowse.TabIndex = 3;
            this.btnDBLocBrowse.Text = "...";
            this.btnDBLocBrowse.UseVisualStyleBackColor = true;
            this.btnDBLocBrowse.Click += new System.EventHandler(this.BtnDBLocBrowse_Click);
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
            // txtDBUploadFile
            // 
            this.txtDBUploadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBUploadFile.Location = new System.Drawing.Point(161, 32);
            this.txtDBUploadFile.Multiline = true;
            this.txtDBUploadFile.Name = "txtDBUploadFile";
            this.txtDBUploadFile.ReadOnly = true;
            this.txtDBUploadFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDBUploadFile.Size = new System.Drawing.Size(488, 35);
            this.txtDBUploadFile.TabIndex = 0;
            this.txtDBUploadFile.TextChanged += new System.EventHandler(this.TxtUploadFile_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Dropbox Location:";
            // 
            // txtDBLocation
            // 
            this.txtDBLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBLocation.Location = new System.Drawing.Point(162, 88);
            this.txtDBLocation.Multiline = true;
            this.txtDBLocation.Name = "txtDBLocation";
            this.txtDBLocation.ReadOnly = true;
            this.txtDBLocation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDBLocation.Size = new System.Drawing.Size(488, 35);
            this.txtDBLocation.TabIndex = 1;
            this.txtDBLocation.TextChanged += new System.EventHandler(this.TxtDropbocLoc_Changed);
            // 
            // btnUploadDlgCancel
            // 
            this.btnUploadDlgCancel.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnUploadDlgCancel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnUploadDlgCancel.Location = new System.Drawing.Point(545, 211);
            this.btnUploadDlgCancel.Name = "btnUploadDlgCancel";
            this.btnUploadDlgCancel.Size = new System.Drawing.Size(112, 40);
            this.btnUploadDlgCancel.TabIndex = 3;
            this.btnUploadDlgCancel.Text = "Close";
            this.btnUploadDlgCancel.UseVisualStyleBackColor = false;
            this.btnUploadDlgCancel.Click += new System.EventHandler(this.BtnDBUploadDlgCancel_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarStatus,
            this.statusBarAccName,
            this.statusBarAccCountry});
            this.statusStrip1.Location = new System.Drawing.Point(0, 258);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(715, 40);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusBarStatus
            // 
            this.statusBarStatus.AutoSize = false;
            this.statusBarStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBarStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.statusBarStatus.Name = "statusBarStatus";
            this.statusBarStatus.Padding = new System.Windows.Forms.Padding(1);
            this.statusBarStatus.Size = new System.Drawing.Size(350, 34);
            this.statusBarStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusBarAccName
            // 
            this.statusBarAccName.AutoSize = false;
            this.statusBarAccName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBarAccName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.statusBarAccName.Name = "statusBarAccName";
            this.statusBarAccName.Padding = new System.Windows.Forms.Padding(1);
            this.statusBarAccName.Size = new System.Drawing.Size(220, 34);
            this.statusBarAccName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusBarAccCountry
            // 
            this.statusBarAccCountry.AutoSize = false;
            this.statusBarAccCountry.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBarAccCountry.ForeColor = System.Drawing.SystemColors.ControlText;
            this.statusBarAccCountry.Name = "statusBarAccCountry";
            this.statusBarAccCountry.Padding = new System.Windows.Forms.Padding(1);
            this.statusBarAccCountry.Size = new System.Drawing.Size(80, 34);
            this.statusBarAccCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // UploadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 298);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnUploadDlgCancel);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(733, 345);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(733, 345);
            this.Name = "UploadDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UploadDialog";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDBUploadFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDBLocation;
        private System.Windows.Forms.Button btnUploadDlgCancel;
        private System.Windows.Forms.Button btnDBUpload;
        private System.Windows.Forms.Button btnDBLocBrowse;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarStatus;
        private System.Windows.Forms.ToolStripStatusLabel statusBarAccName;
        private System.Windows.Forms.ToolStripStatusLabel statusBarAccCountry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnUploadFileBrowse;
    }
}