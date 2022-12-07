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

namespace ManagerDll
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void linkLabelEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("mailto:chikkuclare.kts@gmail.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Addin");
            }
        }

        private void linkLabelGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://github.com/chikkuclarejoseph");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Addin");
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
