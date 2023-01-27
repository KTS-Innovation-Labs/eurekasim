
namespace PlanetaryMotionAddin
{
    partial class AboutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.pictureBoxAddinLogo = new System.Windows.Forms.PictureBox();
            this.pictureBoxEurekaSimLogo = new System.Windows.Forms.PictureBox();
            this.btnAboutDlgClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.linkLabelGithub = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabelEmail = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.labelAuthorName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAddinLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEurekaSimLogo)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxAddinLogo
            // 
            this.pictureBoxAddinLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxAddinLogo.Image")));
            this.pictureBoxAddinLogo.Location = new System.Drawing.Point(404, 46);
            this.pictureBoxAddinLogo.Name = "pictureBoxAddinLogo";
            this.pictureBoxAddinLogo.Size = new System.Drawing.Size(128, 111);
            this.pictureBoxAddinLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAddinLogo.TabIndex = 12;
            this.pictureBoxAddinLogo.TabStop = false;
            // 
            // pictureBoxEurekaSimLogo
            // 
            this.pictureBoxEurekaSimLogo.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxEurekaSimLogo.ErrorImage")));
            this.pictureBoxEurekaSimLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxEurekaSimLogo.Image")));
            this.pictureBoxEurekaSimLogo.Location = new System.Drawing.Point(53, 45);
            this.pictureBoxEurekaSimLogo.Name = "pictureBoxEurekaSimLogo";
            this.pictureBoxEurekaSimLogo.Size = new System.Drawing.Size(345, 112);
            this.pictureBoxEurekaSimLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxEurekaSimLogo.TabIndex = 11;
            this.pictureBoxEurekaSimLogo.TabStop = false;
            this.pictureBoxEurekaSimLogo.Click += new System.EventHandler(this.pictureBoxEurekaSimLogo_Click);
            // 
            // btnAboutDlgClose
            // 
            this.btnAboutDlgClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnAboutDlgClose.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAboutDlgClose.Location = new System.Drawing.Point(416, 370);
            this.btnAboutDlgClose.Name = "btnAboutDlgClose";
            this.btnAboutDlgClose.Size = new System.Drawing.Size(116, 36);
            this.btnAboutDlgClose.TabIndex = 10;
            this.btnAboutDlgClose.Text = "Close";
            this.btnAboutDlgClose.UseVisualStyleBackColor = false;
            this.btnAboutDlgClose.Click += new System.EventHandler(this.btnAboutDlgClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.linkLabelGithub);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.linkLabelEmail);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.labelAuthorName);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(53, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 184);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "About";
            // 
            // linkLabelGithub
            // 
            this.linkLabelGithub.AutoSize = true;
            this.linkLabelGithub.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelGithub.Location = new System.Drawing.Point(157, 125);
            this.linkLabelGithub.Name = "linkLabelGithub";
            this.linkLabelGithub.Size = new System.Drawing.Size(262, 25);
            this.linkLabelGithub.TabIndex = 5;
            this.linkLabelGithub.TabStop = true;
            this.linkLabelGithub.Text = "https://github.com/sam2elect";
            this.linkLabelGithub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelGithub_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Github:";
            // 
            // linkLabelEmail
            // 
            this.linkLabelEmail.AutoSize = true;
            this.linkLabelEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelEmail.Location = new System.Drawing.Point(157, 78);
            this.linkLabelEmail.Name = "linkLabelEmail";
            this.linkLabelEmail.Size = new System.Drawing.Size(270, 25);
            this.linkLabelEmail.TabIndex = 3;
            this.linkLabelEmail.TabStop = true;
            this.linkLabelEmail.Text = "samir.kts.infotech@gmail.com";
            this.linkLabelEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelEmail_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Contact Email:";
            // 
            // labelAuthorName
            // 
            this.labelAuthorName.AutoSize = true;
            this.labelAuthorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAuthorName.Location = new System.Drawing.Point(157, 36);
            this.labelAuthorName.Name = "labelAuthorName";
            this.labelAuthorName.Size = new System.Drawing.Size(126, 25);
            this.labelAuthorName.TabIndex = 1;
            this.labelAuthorName.Text = "Samir Kumar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Author:";
            // 
            // AboutDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 447);
            this.Controls.Add(this.pictureBoxAddinLogo);
            this.Controls.Add(this.pictureBoxEurekaSimLogo);
            this.Controls.Add(this.btnAboutDlgClose);
            this.Controls.Add(this.groupBox2);
            this.Name = "AboutDialog";
            this.Text = "About Planetary Motion Addin";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAddinLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEurekaSimLogo)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxAddinLogo;
        private System.Windows.Forms.PictureBox pictureBoxEurekaSimLogo;
        private System.Windows.Forms.Button btnAboutDlgClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel linkLabelGithub;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabelEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelAuthorName;
        private System.Windows.Forms.Label label1;
    }
}