using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace goolgedrivetest
{
    public partial class AboutGD : Form
    {
        public AboutGD()
        {
            InitializeComponent();
            this.Text = "About";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnAboutDlgClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lnkEmail_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("mailto:chandradurga.kts.kts@gmail.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim GoogleDrive Addin");
            }
        }

        private void lnkGithub_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://github.com/chandradurga");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim GoogleDrive Addin");
            }
        }

        private void lnkWebsite_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim GoogleDrive Addin");
            }
        }

    }
}
