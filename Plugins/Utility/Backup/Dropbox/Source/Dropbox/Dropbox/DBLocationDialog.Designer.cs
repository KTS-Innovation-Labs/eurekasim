
namespace Dropbox
{
    partial class DBLocationDialog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBLocationDialog));
            this.btnDBLocDialogClose = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDBLocSelected = new System.Windows.Forms.TextBox();
            this.treeDBLocation = new System.Windows.Forms.TreeView();
            this.imageListTreeView = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBarAccName = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusBarStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDBLocDialogClose
            // 
            this.btnDBLocDialogClose.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnDBLocDialogClose.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnDBLocDialogClose.Location = new System.Drawing.Point(441, 504);
            this.btnDBLocDialogClose.Name = "btnDBLocDialogClose";
            this.btnDBLocDialogClose.Size = new System.Drawing.Size(145, 36);
            this.btnDBLocDialogClose.TabIndex = 1;
            this.btnDBLocDialogClose.Text = "OK";
            this.btnDBLocDialogClose.UseVisualStyleBackColor = false;
            this.btnDBLocDialogClose.Click += new System.EventHandler(this.BtnDBLocDialogClose_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtDBLocSelected);
            this.groupBox2.Controls.Add(this.treeDBLocation);
            this.groupBox2.Location = new System.Drawing.Point(13, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(596, 485);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dropbox Location";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 432);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Selection:";
            // 
            // txtDBLocSelected
            // 
            this.txtDBLocSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBLocSelected.Location = new System.Drawing.Point(111, 426);
            this.txtDBLocSelected.Multiline = true;
            this.txtDBLocSelected.Name = "txtDBLocSelected";
            this.txtDBLocSelected.Size = new System.Drawing.Size(462, 35);
            this.txtDBLocSelected.TabIndex = 0;
            // 
            // treeDBLocation
            // 
            this.treeDBLocation.Location = new System.Drawing.Point(22, 33);
            this.treeDBLocation.Name = "treeDBLocation";
            this.treeDBLocation.Size = new System.Drawing.Size(551, 370);
            this.treeDBLocation.TabIndex = 0;
            this.treeDBLocation.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeDBLoc_NodeMouseClick);
            this.treeDBLocation.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeDBLoc_NodeMouseDoubleClick);
            // 
            // imageListTreeView
            // 
            this.imageListTreeView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTreeView.ImageStream")));
            this.imageListTreeView.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTreeView.Images.SetKeyName(0, "folder_icon.png");
            this.imageListTreeView.Images.SetKeyName(1, "file_icon.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBarStatus,
            this.statusBarAccName});
            this.statusStrip1.Location = new System.Drawing.Point(0, 550);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(619, 40);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusBarAccName
            // 
            this.statusBarAccName.AutoSize = false;
            this.statusBarAccName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBarAccName.Name = "statusBarAccName";
            this.statusBarAccName.Size = new System.Drawing.Size(200, 34);
            this.statusBarAccName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusBarStatus
            // 
            this.statusBarStatus.AutoSize = false;
            this.statusBarStatus.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBarStatus.ForeColor = System.Drawing.SystemColors.Highlight;
            this.statusBarStatus.Name = "statusBarStatus";
            this.statusBarStatus.Size = new System.Drawing.Size(350, 34);
            this.statusBarStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DBLocationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(619, 590);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnDBLocDialogClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(637, 637);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(637, 637);
            this.Name = "DBLocationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DBLocationDialog";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDBLocDialogClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TreeView treeDBLocation;
        private System.Windows.Forms.TextBox txtDBLocSelected;
        private System.Windows.Forms.ImageList imageListTreeView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusBarAccName;
        private System.Windows.Forms.ToolStripStatusLabel statusBarStatus;
    }
}