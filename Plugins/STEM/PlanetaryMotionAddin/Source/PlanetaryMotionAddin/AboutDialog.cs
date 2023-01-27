using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace PlanetaryMotionAddin
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void pictureBoxEurekaSimLogo_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("https://www.eurekasim.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim");
            }
        }

        private void linkLabelEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("mailto:sam2elect.kts.infotech@gmail.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Planetary Motion Addin");
            }
        }

        private void linkLabelGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://github.com/sam2elect");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Planetary Motion Addin");
            }
        }

        private void btnAboutDlgClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
