
namespace MyCSAddin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_openfile = new System.Windows.Forms.CheckBox();
            this.button_downloadfilebrowse = new System.Windows.Forms.Button();
            this.textBox_downloadfile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxFilepath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_openfile);
            this.groupBox1.Controls.Add(this.button_downloadfilebrowse);
            this.groupBox1.Controls.Add(this.textBox_downloadfile);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonBrowse);
            this.groupBox1.Controls.Add(this.textBoxFilepath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(2, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(558, 109);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Download File From OneDrive";
            // 
            // checkBox_openfile
            // 
            this.checkBox_openfile.AutoSize = true;
            this.checkBox_openfile.Location = new System.Drawing.Point(128, 85);
            this.checkBox_openfile.Name = "checkBox_openfile";
            this.checkBox_openfile.Size = new System.Drawing.Size(147, 17);
            this.checkBox_openfile.TabIndex = 6;
            this.checkBox_openfile.Text = "Open File After Download";
            this.checkBox_openfile.UseVisualStyleBackColor = true;
           // this.checkBox_openfile.CheckedChanged += new System.EventHandler(this.checkBox_openfile_CheckedChanged);
            // 
            // button_downloadfilebrowse
            // 
            this.button_downloadfilebrowse.Location = new System.Drawing.Point(469, 57);
            this.button_downloadfilebrowse.Name = "button_downloadfilebrowse";
            this.button_downloadfilebrowse.Size = new System.Drawing.Size(75, 23);
            this.button_downloadfilebrowse.TabIndex = 5;
            this.button_downloadfilebrowse.Text = "......";
            this.button_downloadfilebrowse.UseVisualStyleBackColor = true;
            this.button_downloadfilebrowse.Click += new System.EventHandler(this.button_downloadfilebrowse_Click);
            // 
            // textBox_downloadfile
            // 
            this.textBox_downloadfile.Location = new System.Drawing.Point(128, 57);
            this.textBox_downloadfile.Name = "textBox_downloadfile";
            this.textBox_downloadfile.Size = new System.Drawing.Size(330, 20);
            this.textBox_downloadfile.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select Download File";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(469, 21);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = ".....";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxFilepath
            // 
            this.textBoxFilepath.Location = new System.Drawing.Point(128, 23);
            this.textBoxFilepath.Name = "textBoxFilepath";
            this.textBoxFilepath.Size = new System.Drawing.Size(330, 20);
            this.textBoxFilepath.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select the Local Folder";
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.SystemColors.Highlight;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonClose.Location = new System.Drawing.Point(451, 127);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(98, 23);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonDownload
            // 
            this.buttonDownload.BackColor = System.Drawing.SystemColors.HotTrack;
            this.buttonDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDownload.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonDownload.Location = new System.Drawing.Point(302, 127);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(97, 23);
            this.buttonDownload.TabIndex = 5;
            this.buttonDownload.Text = "Download";
            this.buttonDownload.UseVisualStyleBackColor = false;
            this.buttonDownload.Click += new System.EventHandler(this.buttonDownload_Click);
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 158);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonDownload);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OneDriveAddin | Download";
            this.Load += new System.EventHandler(this.DownloadForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxFilepath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonDownload;
        private System.Windows.Forms.Button button_downloadfilebrowse;
        private System.Windows.Forms.TextBox textBox_downloadfile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_openfile;
    }
}