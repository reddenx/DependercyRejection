namespace DependercyRejectionUI
{
    partial class Form1
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
            this.TextBox_DirectoryInputText = new System.Windows.Forms.TextBox();
            this.Button_BuildFromDirectory = new System.Windows.Forms.Button();
            this.Button_LoadFromCache = new System.Windows.Forms.Button();
            this.Button_SaveToCache = new System.Windows.Forms.Button();
            this.ComboBox_AssemblySelector = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Button_FilterAssembly = new System.Windows.Forms.Button();
            this.ComboBox_FilterAssembly = new System.Windows.Forms.ComboBox();
            this.TreeView_AssemblyInformationTree = new System.Windows.Forms.TreeView();
            this.Button_LoadAssemblyInformation = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howTheShitDoIUseThisThingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpProjectTypes = new System.Windows.Forms.GroupBox();
            this.chkProjectTypes = new System.Windows.Forms.CheckedListBox();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.grpProjectTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox_DirectoryInputText
            // 
            this.TextBox_DirectoryInputText.Location = new System.Drawing.Point(12, 39);
            this.TextBox_DirectoryInputText.Name = "TextBox_DirectoryInputText";
            this.TextBox_DirectoryInputText.Size = new System.Drawing.Size(180, 20);
            this.TextBox_DirectoryInputText.TabIndex = 0;
            this.TextBox_DirectoryInputText.Text = "C:\\Dev\\CCI\\Trunk";
            // 
            // Button_BuildFromDirectory
            // 
            this.Button_BuildFromDirectory.Location = new System.Drawing.Point(198, 37);
            this.Button_BuildFromDirectory.Name = "Button_BuildFromDirectory";
            this.Button_BuildFromDirectory.Size = new System.Drawing.Size(119, 23);
            this.Button_BuildFromDirectory.TabIndex = 1;
            this.Button_BuildFromDirectory.Text = "Look For Projects";
            this.Button_BuildFromDirectory.UseVisualStyleBackColor = true;
            // 
            // Button_LoadFromCache
            // 
            this.Button_LoadFromCache.Location = new System.Drawing.Point(323, 37);
            this.Button_LoadFromCache.Name = "Button_LoadFromCache";
            this.Button_LoadFromCache.Size = new System.Drawing.Size(121, 23);
            this.Button_LoadFromCache.TabIndex = 2;
            this.Button_LoadFromCache.Text = "Load From Cache";
            this.Button_LoadFromCache.UseVisualStyleBackColor = true;
            // 
            // Button_SaveToCache
            // 
            this.Button_SaveToCache.Location = new System.Drawing.Point(450, 37);
            this.Button_SaveToCache.Name = "Button_SaveToCache";
            this.Button_SaveToCache.Size = new System.Drawing.Size(106, 23);
            this.Button_SaveToCache.TabIndex = 3;
            this.Button_SaveToCache.Text = "Save To Cache";
            this.Button_SaveToCache.UseVisualStyleBackColor = true;
            // 
            // ComboBox_AssemblySelector
            // 
            this.ComboBox_AssemblySelector.FormattingEnabled = true;
            this.ComboBox_AssemblySelector.Location = new System.Drawing.Point(6, 19);
            this.ComboBox_AssemblySelector.Name = "ComboBox_AssemblySelector";
            this.ComboBox_AssemblySelector.Size = new System.Drawing.Size(368, 21);
            this.ComboBox_AssemblySelector.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Button_FilterAssembly);
            this.groupBox1.Controls.Add(this.ComboBox_FilterAssembly);
            this.groupBox1.Controls.Add(this.TreeView_AssemblyInformationTree);
            this.groupBox1.Controls.Add(this.Button_LoadAssemblyInformation);
            this.groupBox1.Controls.Add(this.ComboBox_AssemblySelector);
            this.groupBox1.Location = new System.Drawing.Point(12, 89);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(469, 628);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lookup Assemblies";
            // 
            // Button_FilterAssembly
            // 
            this.Button_FilterAssembly.Location = new System.Drawing.Point(382, 44);
            this.Button_FilterAssembly.Name = "Button_FilterAssembly";
            this.Button_FilterAssembly.Size = new System.Drawing.Size(75, 23);
            this.Button_FilterAssembly.TabIndex = 8;
            this.Button_FilterAssembly.Text = "Filter";
            this.Button_FilterAssembly.UseVisualStyleBackColor = true;
            // 
            // ComboBox_FilterAssembly
            // 
            this.ComboBox_FilterAssembly.FormattingEnabled = true;
            this.ComboBox_FilterAssembly.Location = new System.Drawing.Point(6, 46);
            this.ComboBox_FilterAssembly.Name = "ComboBox_FilterAssembly";
            this.ComboBox_FilterAssembly.Size = new System.Drawing.Size(368, 21);
            this.ComboBox_FilterAssembly.TabIndex = 7;
            this.ComboBox_FilterAssembly.Text = "Select a filter assembly";
            // 
            // TreeView_AssemblyInformationTree
            // 
            this.TreeView_AssemblyInformationTree.Location = new System.Drawing.Point(6, 73);
            this.TreeView_AssemblyInformationTree.Name = "TreeView_AssemblyInformationTree";
            this.TreeView_AssemblyInformationTree.Size = new System.Drawing.Size(451, 540);
            this.TreeView_AssemblyInformationTree.TabIndex = 6;
            // 
            // Button_LoadAssemblyInformation
            // 
            this.Button_LoadAssemblyInformation.Location = new System.Drawing.Point(382, 15);
            this.Button_LoadAssemblyInformation.Name = "Button_LoadAssemblyInformation";
            this.Button_LoadAssemblyInformation.Size = new System.Drawing.Size(75, 23);
            this.Button_LoadAssemblyInformation.TabIndex = 5;
            this.Button_LoadAssemblyInformation.Text = "Load";
            this.Button_LoadAssemblyInformation.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(807, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howTheShitDoIUseThisThingToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // howTheShitDoIUseThisThingToolStripMenuItem
            // 
            this.howTheShitDoIUseThisThingToolStripMenuItem.Name = "howTheShitDoIUseThisThingToolStripMenuItem";
            this.howTheShitDoIUseThisThingToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.howTheShitDoIUseThisThingToolStripMenuItem.Text = "How the shit do I use this thing?";
            this.howTheShitDoIUseThisThingToolStripMenuItem.Click += new System.EventHandler(this.howTheShitDoIUseThisThingToolStripMenuItem_Click);
            // 
            // grpProjectTypes
            // 
            this.grpProjectTypes.Controls.Add(this.chkProjectTypes);
            this.grpProjectTypes.Location = new System.Drawing.Point(487, 89);
            this.grpProjectTypes.Name = "grpProjectTypes";
            this.grpProjectTypes.Size = new System.Drawing.Size(281, 628);
            this.grpProjectTypes.TabIndex = 7;
            this.grpProjectTypes.TabStop = false;
            this.grpProjectTypes.Text = "Project Types";
            // 
            // chkProjectTypes
            // 
            this.chkProjectTypes.FormattingEnabled = true;
            this.chkProjectTypes.Location = new System.Drawing.Point(6, 19);
            this.chkProjectTypes.Name = "chkProjectTypes";
            this.chkProjectTypes.Size = new System.Drawing.Size(269, 589);
            this.chkProjectTypes.TabIndex = 0;
            this.chkProjectTypes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkProjectTypes_ItemCheck);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 863);
            this.Controls.Add(this.grpProjectTypes);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.Button_SaveToCache);
            this.Controls.Add(this.Button_LoadFromCache);
            this.Controls.Add(this.Button_BuildFromDirectory);
            this.Controls.Add(this.TextBox_DirectoryInputText);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpProjectTypes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_DirectoryInputText;
        private System.Windows.Forms.Button Button_BuildFromDirectory;
        private System.Windows.Forms.Button Button_LoadFromCache;
        private System.Windows.Forms.Button Button_SaveToCache;
        private System.Windows.Forms.ComboBox ComboBox_AssemblySelector;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Button_LoadAssemblyInformation;
        private System.Windows.Forms.TreeView TreeView_AssemblyInformationTree;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howTheShitDoIUseThisThingToolStripMenuItem;
        private System.Windows.Forms.ComboBox ComboBox_FilterAssembly;
        private System.Windows.Forms.Button Button_FilterAssembly;
        private System.Windows.Forms.GroupBox grpProjectTypes;
        private System.Windows.Forms.CheckedListBox chkProjectTypes;
    }
}

