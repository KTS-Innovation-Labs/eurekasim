using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EurekaSimLOGO
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private PictureBox pictureBoxPlanetaryMotionAddin;
        private PictureBox pictureBoxEurekaSim;
        private Button btnAboutDlgClose;
        private GroupBox groupBox2;
        private LinkLabel linkLabelGithub;
        private Label label4;
        private LinkLabel linkLabelEmail;
        private Label label3;
        private Label label2;
        private Label label1;
        private LinkLabel linkLabelEurekaSim;

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            this.pictureBoxPlanetaryMotionAddin = new System.Windows.Forms.PictureBox();
            this.pictureBoxEurekaSim = new System.Windows.Forms.PictureBox();
            this.btnAboutDlgClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.linkLabelGithub = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabelEmail = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabelEurekaSim = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlanetaryMotionAddin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEurekaSim)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxPlanetaryMotionAddin
            // 
            this.pictureBoxPlanetaryMotionAddin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxPlanetaryMotionAddin.Image = global::EurekaSimLOGO.Properties.Resources.pictureBoxPlanetaryMotionAddin_Image;
            this.pictureBoxPlanetaryMotionAddin.Location = new System.Drawing.Point(375, 11);
            this.pictureBoxPlanetaryMotionAddin.Name = "pictureBoxPlanetaryMotionAddin";
            this.pictureBoxPlanetaryMotionAddin.Size = new System.Drawing.Size(139, 111);
            this.pictureBoxPlanetaryMotionAddin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPlanetaryMotionAddin.TabIndex = 22;
            this.pictureBoxPlanetaryMotionAddin.TabStop = false;
            // 
            // pictureBoxEurekaSim
            // 
            this.pictureBoxEurekaSim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBoxEurekaSim.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxEurekaSim.Image")));
            this.pictureBoxEurekaSim.Location = new System.Drawing.Point(35, 10);
            this.pictureBoxEurekaSim.Name = "pictureBoxEurekaSim";
            this.pictureBoxEurekaSim.Size = new System.Drawing.Size(345, 112);
            this.pictureBoxEurekaSim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxEurekaSim.TabIndex = 21;
            this.pictureBoxEurekaSim.TabStop = false;
            // 
            // btnAboutDlgClose
            // 
            this.btnAboutDlgClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnAboutDlgClose.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnAboutDlgClose.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAboutDlgClose.Location = new System.Drawing.Point(398, 347);
            this.btnAboutDlgClose.Name = "btnAboutDlgClose";
            this.btnAboutDlgClose.Size = new System.Drawing.Size(116, 36);
            this.btnAboutDlgClose.TabIndex = 20;
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
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(35, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(479, 184);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "About";
            // 
            // linkLabelGithub
            // 
            this.linkLabelGithub.AutoSize = true;
            this.linkLabelGithub.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelGithub.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelGithub.Location = new System.Drawing.Point(157, 125);
            this.linkLabelGithub.Name = "linkLabelGithub";
            this.linkLabelGithub.Size = new System.Drawing.Size(243, 25);
            this.linkLabelGithub.TabIndex = 5;
            this.linkLabelGithub.TabStop = true;
            this.linkLabelGithub.Text = "https://github.com/vnth124";
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
            this.linkLabelEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelEmail.Location = new System.Drawing.Point(157, 78);
            this.linkLabelEmail.Name = "linkLabelEmail";
            this.linkLabelEmail.Size = new System.Drawing.Size(235, 25);
            this.linkLabelEmail.TabIndex = 3;
            this.linkLabelEmail.TabStop = true;
            this.linkLabelEmail.Text = "vineeth99.kts@gmail.com";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(157, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Vineeth Vijayan";
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
            // linkLabelEurekaSim
            // 
            this.linkLabelEurekaSim.AutoSize = true;
            this.linkLabelEurekaSim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLabelEurekaSim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelEurekaSim.Location = new System.Drawing.Point(67, 358);
            this.linkLabelEurekaSim.Name = "linkLabelEurekaSim";
            this.linkLabelEurekaSim.Size = new System.Drawing.Size(251, 25);
            this.linkLabelEurekaSim.TabIndex = 23;
            this.linkLabelEurekaSim.TabStop = true;
            this.linkLabelEurekaSim.Text = "https://www.eurekasim.com";
            // 
            // About
            // 
            this.ClientSize = new System.Drawing.Size(551, 410);
            this.Controls.Add(this.pictureBoxPlanetaryMotionAddin);
            this.Controls.Add(this.pictureBoxEurekaSim);
            this.Controls.Add(this.btnAboutDlgClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.linkLabelEurekaSim);
            this.Name = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlanetaryMotionAddin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxEurekaSim)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void btnAboutDlgClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
