
namespace ESimBoxCloud
{
    partial class DownloadForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBoxDowloadFile = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.chkOpenDownload = new System.Windows.Forms.CheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnDownLocBrws = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBoxDowloadFile);
            this.groupBox1.Controls.Add(this.statusStrip1);
            this.groupBox1.Controls.Add(this.chkOpenDownload);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnDownload);
            this.groupBox1.Controls.Add(this.btnDownLocBrws);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(40, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(641, 228);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download";
            // 
            // txtBoxDowloadFile
            // 
            this.txtBoxDowloadFile.Location = new System.Drawing.Point(133, 66);
            this.txtBoxDowloadFile.Multiline = true;
            this.txtBoxDowloadFile.Name = "txtBoxDowloadFile";
            this.txtBoxDowloadFile.Size = new System.Drawing.Size(435, 20);
            this.txtBoxDowloadFile.TabIndex = 7;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(3, 203);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(635, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ActiveLinkColor = System.Drawing.Color.Red;
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.toolStripStatusLabel1.LinkColor = System.Drawing.Color.MidnightBlue;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // chkOpenDownload
            // 
            this.chkOpenDownload.AutoSize = true;
            this.chkOpenDownload.Location = new System.Drawing.Point(31, 115);
            this.chkOpenDownload.Name = "chkOpenDownload";
            this.chkOpenDownload.Size = new System.Drawing.Size(127, 17);
            this.chkOpenDownload.TabIndex = 5;
            this.chkOpenDownload.Text = "Open after Download";
            this.chkOpenDownload.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnClose.Location = new System.Drawing.Point(516, 144);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(103, 30);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnDownload.Location = new System.Drawing.Point(516, 105);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(103, 33);
            this.btnDownload.TabIndex = 3;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnDownLocBrws
            // 
            this.btnDownLocBrws.Location = new System.Drawing.Point(574, 66);
            this.btnDownLocBrws.Name = "btnDownLocBrws";
            this.btnDownLocBrws.Size = new System.Drawing.Size(25, 20);
            this.btnDownLocBrws.TabIndex = 2;
            this.btnDownLocBrws.Text = "...";
            this.btnDownLocBrws.UseVisualStyleBackColor = true;
            this.btnDownLocBrws.Click += new System.EventHandler(this.btnDownLocBrws_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Downloading File";
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 252);
            this.Controls.Add(this.groupBox1);
            this.Name = "DownloadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DownloadForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkOpenDownload;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnDownLocBrws;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox txtBoxDowloadFile;
    }
}