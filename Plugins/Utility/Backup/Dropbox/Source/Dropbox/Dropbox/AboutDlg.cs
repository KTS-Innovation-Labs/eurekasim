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

namespace Dropbox
{
    public partial class AboutDlg : Form
    {
        public AboutDlg()
        {
            InitializeComponent();
            this.Text = "About";
        }

        //Dialog close.
        private void BtnAboutDlgClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lnkEmail_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("mailto:jayakrishnan.kts@gmail.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }
        }

        private void lnkGithub_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://github.com/PJayakrishnan");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }
        }

        private void lnkWebsite_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://jayakrishnanp.netlify.app/");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }
        }
    }
}
