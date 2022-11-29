
namespace ESimBoxCloud
{
    partial class UploadForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFileUpload = new System.Windows.Forms.TextBox();
            this.txtBoxLocation = new System.Windows.Forms.TextBox();
            this.btnUpldFileBrowse = new System.Windows.Forms.Button();
            this.btnBoxUploadLoc = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusBarStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Upload File";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(0, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "BoxLocation";
            // 
            // txtFileUpload
            // 
            this.txtFileUpload.Location = new System.Drawing.Point(85, 37);
            this.txtFileUpload.Multiline = true;
            this.txtFileUpload.Name = "txtFileUpload";
            this.txtFileUpload.Size = new System.Drawing.Size(436, 21);
            this.txtFileUpload.TabIndex = 2;
            // 
            // txtBoxLocation
            // 
            this.txtBoxLocation.Location = new System.Drawing.Point(85, 87);
            this.txtBoxLocation.Multiline = true;
            this.txtBoxLocation.Name = "txtBoxLocation";
            this.txtBoxLocation.Size = new System.Drawing.Size(436, 20);
            this.txtBoxLocation.TabIndex = 3;
            // 
            // btnUpldFileBrowse
            // 
            this.btnUpldFileBrowse.Location = new System.Drawing.Point(527, 37);
            this.btnUpldFileBrowse.Name = "btnUpldFileBrowse";
            this.btnUpldFileBrowse.Size = new System.Drawing.Size(23, 21);
            this.btnUpldFileBrowse.TabIndex = 4;
            this.btnUpldFileBrowse.Text = "...";
            this.btnUpldFileBrowse.UseVisualStyleBackColor = true;
            this.btnUpldFileBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnBoxUploadLoc
            // 
            this.btnBoxUploadLoc.Location = new System.Drawing.Point(527, 87);
            this.btnBoxUploadLoc.Name = "btnBoxUploadLoc";
            this.btnBoxUploadLoc.Size = new System.Drawing.Size(23, 20);
            this.btnBoxUploadLoc.TabIndex = 5;
            this.btnBoxUploadLoc.Text = "...";
            this.btnBoxUploadLoc.UseVisualStyleBackColor = true;
            this.btnBoxUploadLoc.Click += new System.EventHandler(this.btnBoxUploadLoc_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpload);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnBoxUploadLoc);
            this.groupBox1.Controls.Add(this.txtFileUpload);
            this.groupBox1.Controls.Add(this.txtBoxLocation);
            this.groupBox1.Controls.Add(this.btnUpldFileBrowse);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(27, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(596, 171);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upload";
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpload.Location = new System.Drawing.Point(506, 137);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 27);
            this.btnUpload.TabIndex = 7;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClose.Location = new System.Drawing.Point(533, 200);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 31);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBarStatus,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 236);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(635, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusBarStatus
            // 
            this.StatusBarStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.StatusBarStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.StatusBarStatus.Name = "StatusBarStatus";
            this.StatusBarStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // UploadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 258);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Name = "UploadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UploadForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFileUpload;
        private System.Windows.Forms.TextBox txtBoxLocation;
        private System.Windows.Forms.Button btnUpldFileBrowse;
        private System.Windows.Forms.Button btnBoxUploadLoc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusBarStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}