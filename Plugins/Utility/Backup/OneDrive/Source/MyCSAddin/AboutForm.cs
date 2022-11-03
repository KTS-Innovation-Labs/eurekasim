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

namespace MyCSAddin
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("mailto:silpasivaram@gmail.com");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim OneDrive PlugIn");
            }
        }


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("https://github.com/silpasivaram");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim OneDrive PlugIn");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
