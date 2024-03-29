﻿using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCSAddin
{
    public partial class ObjectBrowserForm : Form
    {
        public TreeNode m_selectedNode=null;
        OneDriveSdkMan m_OneDriveSdkMan;
        public string UploadFolderPath { get; set; }
        public ObjectBrowserForm(OneDriveSdkMan obj)
        {
            InitializeComponent();
            m_OneDriveSdkMan =obj;
        }
        public void UpdateTreeView(DriveItem item)
        {
            treeView1.Nodes.Add(item.Id, item.Name);
        }
       
        public void UpdateNode(DriveItem items)
        {
            TreeNode nod = new TreeNode();
            nod.Name = items.Id;
            nod.Text = items.Name;
            m_selectedNode.Nodes.Add(nod);

        }

        private async void OnTreeNodeClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            m_selectedNode = e.Node;
            m_selectedNode.Nodes.Clear();
            await m_OneDriveSdkMan.LoadFolderFromId(m_selectedNode.Name);
            textBoxSelectedFolder.Text = e.Node.FullPath;
            UploadFolderPath = textBoxSelectedFolder.Text;
            m_OneDriveSdkMan.strSelectedItem = e.Node.Name;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
