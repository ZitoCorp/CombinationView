namespace EZView
{
    partial class Viewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Viewer));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OfficeCombo = new System.Windows.Forms.ComboBox();
            this.DepartmentCombo = new System.Windows.Forms.ComboBox();
            this.TitleCombo = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.OfficeBtn = new System.Windows.Forms.Button();
            this.DepartmentBtn = new System.Windows.Forms.Button();
            this.TitleBtn = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(906, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(194, 30);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(194, 30);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 29);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(73, 26);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(72, 22);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(126, 34);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(125, 30);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.Paste);
            // 
            // OfficeCombo
            // 
            this.OfficeCombo.FormattingEnabled = true;
            this.OfficeCombo.Location = new System.Drawing.Point(11, 50);
            this.OfficeCombo.Name = "OfficeCombo";
            this.OfficeCombo.Size = new System.Drawing.Size(212, 28);
            this.OfficeCombo.Sorted = true;
            this.OfficeCombo.TabIndex = 2;
            this.OfficeCombo.Text = "Offices";
            this.OfficeCombo.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // DepartmentCombo
            // 
            this.DepartmentCombo.FormattingEnabled = true;
            this.DepartmentCombo.Location = new System.Drawing.Point(326, 50);
            this.DepartmentCombo.Name = "DepartmentCombo";
            this.DepartmentCombo.Size = new System.Drawing.Size(212, 28);
            this.DepartmentCombo.Sorted = true;
            this.DepartmentCombo.TabIndex = 3;
            this.DepartmentCombo.Text = "Departments";
            this.DepartmentCombo.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // TitleCombo
            // 
            this.TitleCombo.FormattingEnabled = true;
            this.TitleCombo.Location = new System.Drawing.Point(631, 50);
            this.TitleCombo.Name = "TitleCombo";
            this.TitleCombo.Size = new System.Drawing.Size(212, 28);
            this.TitleCombo.Sorted = true;
            this.TitleCombo.TabIndex = 4;
            this.TitleCombo.Text = "Titles";
            this.TitleCombo.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeight = 25;
            this.dataGridView1.Location = new System.Drawing.Point(11, 98);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(886, 457);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // OfficeBtn
            // 
            this.OfficeBtn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.OfficeBtn.BackgroundImage = global::EZView.Properties.Resources.add;
            this.OfficeBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.OfficeBtn.Location = new System.Drawing.Point(229, 42);
            this.OfficeBtn.Name = "OfficeBtn";
            this.OfficeBtn.Size = new System.Drawing.Size(45, 42);
            this.OfficeBtn.TabIndex = 6;
            this.OfficeBtn.UseVisualStyleBackColor = false;
            this.OfficeBtn.Click += new System.EventHandler(this.OfficeBtn_Click);
            // 
            // DepartmentBtn
            // 
            this.DepartmentBtn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.DepartmentBtn.BackgroundImage = global::EZView.Properties.Resources.add;
            this.DepartmentBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DepartmentBtn.Location = new System.Drawing.Point(544, 42);
            this.DepartmentBtn.Name = "DepartmentBtn";
            this.DepartmentBtn.Size = new System.Drawing.Size(45, 42);
            this.DepartmentBtn.TabIndex = 7;
            this.DepartmentBtn.UseVisualStyleBackColor = false;
            this.DepartmentBtn.Click += new System.EventHandler(this.DepartmentBtn_Click);
            // 
            // TitleBtn
            // 
            this.TitleBtn.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.TitleBtn.BackgroundImage = global::EZView.Properties.Resources.add;
            this.TitleBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TitleBtn.Location = new System.Drawing.Point(849, 42);
            this.TitleBtn.Name = "TitleBtn";
            this.TitleBtn.Size = new System.Drawing.Size(45, 42);
            this.TitleBtn.TabIndex = 8;
            this.TitleBtn.UseVisualStyleBackColor = false;
            this.TitleBtn.Click += new System.EventHandler(this.TitleBtn_Click);
            // 
            // Viewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(906, 562);
            this.Controls.Add(this.TitleBtn);
            this.Controls.Add(this.DepartmentBtn);
            this.Controls.Add(this.OfficeBtn);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.TitleCombo);
            this.Controls.Add(this.DepartmentCombo);
            this.Controls.Add(this.OfficeCombo);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Viewer";
            this.Text = "CombinationView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ComboBox OfficeCombo;
        private System.Windows.Forms.ComboBox DepartmentCombo;
        private System.Windows.Forms.ComboBox TitleCombo;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button OfficeBtn;
        private System.Windows.Forms.Button DepartmentBtn;
        private System.Windows.Forms.Button TitleBtn;
    }
}

