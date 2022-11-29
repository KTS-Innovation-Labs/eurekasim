using Box.V2;
using Box.V2.Config;
using Box.V2.JWTAuth;
using Box.V2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESimBoxCloud
{
    public partial class BoxLocation : Form
    {
       static  BoxClient m_Boxclient;
        public static string setvaluefortxtBoxLocSelect = "";
        public static string FileName = "";
        public static string FolderID="";
        public static string FolderPath="";

        public BoxLocation(BoxClient boxclient)
        {
            m_Boxclient = boxclient;

            InitializeComponent();
            PopulateTree();

        }
        private async void PopulateTree()
        {

            var rootfolderId = "0";
            BoxCollection<BoxItem> collections = await m_Boxclient.FoldersManager.GetFolderItemsAsync(rootfolderId,limit:1000,autoPaginate:true);
                       
            foreach (var item in collections.Entries)
            {
                TreeNode nod = new TreeNode();
                nod.Name = item.Id;
                nod.Text = item.Name;
                treeViewBoxLoc.Nodes.Add(nod);
               
            }
        }
                
        private void TreeViewBoxLocDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
           
            this.StatusBarStatus.Text = "Please wait..";

           var folderPath = e.Node.FullPath;
            var FolderPath = folderPath.Replace('\\', '/');
            PopulateTree(e.Node.Name);
           

        }
        private async void PopulateTree(string Id)
        {
            BoxCollection<BoxItem> collections = await m_Boxclient.FoldersManager.GetFolderItemsAsync(Id,limit: 1000,autoPaginate:true);
           
           
            foreach (var item in collections.Entries)
            {
                TreeNode nod = new TreeNode();
                nod.Name = item.Id;
                nod.Text = item.Name;
                treeViewBoxLoc.SelectedNode.Nodes.Add(nod);
                

            }
            treeViewBoxLoc.SelectedNode.ExpandAll();
           
        }

        private void TreeViewBoxLocSingleMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            var filePath = e.Node.FullPath;
            var newFilePath = filePath.Replace('\\', '/');
           
            txtBoxLocSelect.Text = newFilePath;
            //txtBoxLocSelect.Tag = e.Node.Name;
            Properties.Settings.Default.UpldId = e.Node.Name;
            setvaluefortxtBoxLocSelect = txtBoxLocSelect.Text;
            FolderID = e.Node.Name;
            FileName = e.Node.Text;
        }

        
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

}

