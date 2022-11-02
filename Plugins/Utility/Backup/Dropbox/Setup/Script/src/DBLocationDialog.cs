using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dropbox.Api;
using Dropbox.Api.Users;
using Dropbox.Api.Files;
using System.Net.Http;
using System.IO;

namespace Dropbox
{
    public partial class DBLocationDialog : Form
    {
        DropboxClient m_client;
        UploadDialog m_uploadDlg = null;
        DownloadDialog m_downDlg = null;
        DbSettings m_settingsDlg = null;
        public DBLocationDialog(DropboxClient dbClient, string displayName, UploadDialog dlg)
        {
            InitializeComponent();
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(768 | 3072);
            this.Text = "Select Dropbox Upload Location";
            treeDBLocation.ImageList = imageListTreeView;

            m_client = dbClient;
            m_uploadDlg = dlg;
            statusBarAccName.Text = displayName;
            this.statusBarStatus.Text = "Please wait..";
            PopulateDBLocationTree();
        }

        public DBLocationDialog(DropboxClient dbClient, string displayName, DownloadDialog dlg)
        {
            InitializeComponent();
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(768 | 3072);

            this.Text = "Select Dropbox Download Location";
            treeDBLocation.ImageList = imageListTreeView;

            m_client = dbClient;
            m_downDlg = dlg;
            statusBarAccName.Text = displayName;
            this.statusBarStatus.Text = "Please wait..";

            PopulateDBLocationTree();
        }

        public DBLocationDialog(DropboxClient dbClient, string displayName, DbSettings dlg)
        {
            InitializeComponent();
            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)(768 | 3072);

            this.Text = "Select Dropbox Default Upload Location";
            treeDBLocation.ImageList = imageListTreeView;

            m_client = dbClient;
            m_settingsDlg = dlg;
            statusBarAccName.Text = displayName;
            this.statusBarStatus.Text = "Please wait..";

            PopulateDBLocationTree();
        }

        //Dialog close
        private void BtnDBLocDialogClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Populate tree view.
        private async void PopulateDBLocationTree()
        {
            if (m_client != null)
            {
                try
                {
                    var path = string.Empty;
                    var list = await m_client.Files.ListFolderAsync(path);
                    int j = 0;
                    if (this.Text == "Select Dropbox Upload Location")
                    {
                        foreach (var item in list.Entries.Where(i => i.IsFolder))
                        {
                            treeDBLocation.Nodes.Add(item.Name);
                            treeDBLocation.Nodes[j].ImageIndex = 0;
                            treeDBLocation.Nodes[j].SelectedImageIndex = 0;
                            j++;
                        }
                    }
                    else if (this.Text == "Select Dropbox Default Upload Location")
                    {
                        j = 0;
                        foreach (var item in list.Entries.Where(i => i.IsFolder))
                        {
                            treeDBLocation.Nodes.Add(item.Name);
                            treeDBLocation.Nodes[j].ImageIndex = 0;
                            treeDBLocation.Nodes[j].SelectedImageIndex = 0;
                            j++;
                        }
                    }
                    else if (this.Text == "Select Dropbox Download Location")
                    {
                        j = 0;
                        foreach (var item in list.Entries.Where(i => i.IsFolder))
                        {
                            treeDBLocation.Nodes.Add(item.Name);
                            treeDBLocation.Nodes[j].ImageIndex = 0;
                            treeDBLocation.Nodes[j].SelectedImageIndex = 0;
                            j++;
                        }
                        j = list.Entries.Where(i => i.IsFolder).Count();
                        foreach (var item in list.Entries.Where(i => i.IsFile))
                        {
                            var file = item.AsFile;
                           
                            treeDBLocation.Nodes.Add(file.Name);
                            treeDBLocation.Nodes[j].ImageIndex = 1;
                            treeDBLocation.Nodes[j].SelectedImageIndex = 1;
                            j++;

                        }
                    }
                    this.statusBarStatus.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
                }
            }
        }

        //Tree view click event.
        private void TreeDBLoc_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (this.Text == "Select Dropbox Upload Location")
                {
                    var pathh = e.Node.FullPath;
                    var newPathh = pathh.Replace('\\', '/');
                    m_uploadDlg.SetSelectedLoc(newPathh);
                    txtDBLocSelected.Text = newPathh;
                }
                else if (this.Text == "Select Dropbox Default Upload Location")
                {
                    var pathh = e.Node.FullPath;
                    var newPathh = pathh.Replace('\\', '/');
                    m_settingsDlg.SetDefaultUploadLoc(newPathh);
                    txtDBLocSelected.Text = newPathh;
                }
                else if (this.Text == "Select Dropbox Download Location")
                {
                    var filePath = e.Node.FullPath;
                    var newFilePath = filePath.Replace('\\', '/');
                    m_downDlg.SetDownloadFile(newFilePath, e.Node.Text);
                    txtDBLocSelected.Text = newFilePath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }
        }

        //Event handler to expand node.
        private void TreeDBLoc_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var path = "";
            path = "/" + e.Node.FullPath + "/";
            var newPath = path.Replace('\\', '/');
            this.statusBarStatus.Text = "Please wait..";

            PopulateTree(newPath);
        }
        private async void PopulateTree(string path)
        {
            try
            {
                var list = await m_client.Files.ListFolderAsync(path);
                int j = 0;
                if (this.Text == "Select Dropbox Upload Location" || this.Text == "Select Dropbox Default Upload Location")
                {
                    foreach (var item in list.Entries.Where(i => i.IsFolder))
                    {
                        treeDBLocation.SelectedNode.Nodes.Add(item.Name);
                        treeDBLocation.SelectedNode.Nodes[j].ImageIndex = 0;
                        treeDBLocation.SelectedNode.Nodes[j].SelectedImageIndex = 0;
                        j++;
                    }
                }
                else if (this.Text == "Select Dropbox Download Location")
                {
                    j = 0;
                    foreach (var item in list.Entries.Where(i => i.IsFolder))
                    {
                        treeDBLocation.SelectedNode.Nodes.Add(item.Name);
                        treeDBLocation.SelectedNode.Nodes[j].ImageIndex = 0;
                        treeDBLocation.SelectedNode.Nodes[j].SelectedImageIndex = 0;
                        j++;
                    }
                    j = list.Entries.Where(i => i.IsFolder).Count();
                    foreach (var item in list.Entries.Where(i => i.IsFile))
                    {
                        var file = item.AsFile;
                        
                        treeDBLocation.SelectedNode.Nodes.Add(file.Name);
                        treeDBLocation.SelectedNode.Nodes[j].ImageIndex = 1;
                        treeDBLocation.SelectedNode.Nodes[j].SelectedImageIndex = 1;
                        j++;
                    }
                }
                treeDBLocation.SelectedNode.Expand();
                this.statusBarStatus.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "EurekaSim Dropbox Addin");
            }
        }
    }
}
