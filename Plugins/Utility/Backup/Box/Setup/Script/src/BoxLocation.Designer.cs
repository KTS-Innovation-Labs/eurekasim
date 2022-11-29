
namespace ESimBoxCloud
{
    partial class BoxLocation
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeViewBoxLoc = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxLocSelect = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.imageListTreeview = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusBarStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeViewBoxLoc);
            this.groupBox1.Location = new System.Drawing.Point(12, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(399, 277);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "BoxLocation";
            // 
            // treeViewBoxLoc
            // 
            this.treeViewBoxLoc.Location = new System.Drawing.Point(22, 31);
            this.treeViewBoxLoc.Name = "treeViewBoxLoc";
            this.treeViewBoxLoc.Size = new System.Drawing.Size(365, 240);
            this.treeViewBoxLoc.TabIndex = 0;
            this.treeViewBoxLoc.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewBoxLocSingleMouseClick);
            this.treeViewBoxLoc.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewBoxLocDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Selection";
            // 
            // txtBoxLocSelect
            // 
            this.txtBoxLocSelect.Location = new System.Drawing.Point(72, 321);
            this.txtBoxLocSelect.Multiline = true;
            this.txtBoxLocSelect.Name = "txtBoxLocSelect";
            this.txtBoxLocSelect.Size = new System.Drawing.Size(327, 18);
            this.txtBoxLocSelect.TabIndex = 2;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnOK.Location = new System.Drawing.Point(324, 374);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 29);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // imageListTreeview
            // 
            this.imageListTreeview.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListTreeview.ImageSize = new System.Drawing.Size(16, 16);
            this.imageListTreeview.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBarStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 407);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(418, 22);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusBarStatus
            // 
            this.StatusBarStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.StatusBarStatus.Name = "StatusBarStatus";
            this.StatusBarStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // BoxLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 429);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtBoxLocSelect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "BoxLocation";
            this.Text = "BoxLocation";
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxLocSelect;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ImageList imageListTreeview;
        private System.Windows.Forms.TreeView treeViewBoxLoc;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusBarStatus;
    }
}