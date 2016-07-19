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
            this.TreeView_AssemblyInformationTree = new System.Windows.Forms.TreeView();
            this.Button_FilterAssembly = new System.Windows.Forms.Button();
            this.ComboBox_FilterAssembly = new System.Windows.Forms.ComboBox();
            this.Button_LoadAssemblyInformation = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.howTheShitDoIUseThisThingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.grpProjectTypes = new System.Windows.Forms.GroupBox();
            this.btnExportCurrent = new System.Windows.Forms.Button();
            this.Out = new System.Windows.Forms.Button();
            this.chkOutputTypes = new System.Windows.Forms.CheckedListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ToolStatusLabel_OutputStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnRemoveProjectFromSolns = new System.Windows.Forms.Button();
            this.btnFixDeadbeatSolutions = new System.Windows.Forms.Button();
            this.btnDeadBeatSolns = new System.Windows.Forms.Button();
            this.btnCleanReferences = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.btnAddReference = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDestProjects = new System.Windows.Forms.TextBox();
            this.chkProjectTypes = new DependercyRejectionUI.ColoredCheckListBox();
            this.btnRemoveFromAllSolns = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.grpProjectTypes.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox_DirectoryInputText
            // 
            this.TextBox_DirectoryInputText.Location = new System.Drawing.Point(12, 39);
            this.TextBox_DirectoryInputText.Name = "TextBox_DirectoryInputText";
            this.TextBox_DirectoryInputText.Size = new System.Drawing.Size(180, 20);
            this.TextBox_DirectoryInputText.TabIndex = 0;
            this.TextBox_DirectoryInputText.Text = "C:\\dev\\cci\\747_Arch_JumboJoanJett";
            // 
            // Button_BuildFromDirectory
            // 
            this.Button_BuildFromDirectory.Location = new System.Drawing.Point(198, 37);
            this.Button_BuildFromDirectory.Name = "Button_BuildFromDirectory";
            this.Button_BuildFromDirectory.Size = new System.Drawing.Size(119, 23);
            this.Button_BuildFromDirectory.TabIndex = 1;
            this.Button_BuildFromDirectory.Text = "Look For Projects";
            this.Button_BuildFromDirectory.UseVisualStyleBackColor = true;
            this.Button_BuildFromDirectory.Click += new System.EventHandler(this.Button_BuildFromDirectory_Click);
            // 
            // Button_LoadFromCache
            // 
            this.Button_LoadFromCache.Location = new System.Drawing.Point(323, 37);
            this.Button_LoadFromCache.Name = "Button_LoadFromCache";
            this.Button_LoadFromCache.Size = new System.Drawing.Size(121, 23);
            this.Button_LoadFromCache.TabIndex = 2;
            this.Button_LoadFromCache.Text = "Load From Cache";
            this.Button_LoadFromCache.UseVisualStyleBackColor = true;
            this.Button_LoadFromCache.Click += new System.EventHandler(this.Button_LoadFromCache_Click);
            // 
            // Button_SaveToCache
            // 
            this.Button_SaveToCache.Location = new System.Drawing.Point(450, 37);
            this.Button_SaveToCache.Name = "Button_SaveToCache";
            this.Button_SaveToCache.Size = new System.Drawing.Size(106, 23);
            this.Button_SaveToCache.TabIndex = 3;
            this.Button_SaveToCache.Text = "Save To Cache";
            this.Button_SaveToCache.UseVisualStyleBackColor = true;
            this.Button_SaveToCache.Click += new System.EventHandler(this.Button_SaveToCache_Click);
            // 
            // ComboBox_AssemblySelector
            // 
            this.ComboBox_AssemblySelector.FormattingEnabled = true;
            this.ComboBox_AssemblySelector.Location = new System.Drawing.Point(12, 66);
            this.ComboBox_AssemblySelector.Name = "ComboBox_AssemblySelector";
            this.ComboBox_AssemblySelector.Size = new System.Drawing.Size(368, 21);
            this.ComboBox_AssemblySelector.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TreeView_AssemblyInformationTree);
            this.groupBox1.Location = new System.Drawing.Point(17, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(663, 628);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lookup Assemblies";
            // 
            // TreeView_AssemblyInformationTree
            // 
            this.TreeView_AssemblyInformationTree.Location = new System.Drawing.Point(6, 19);
            this.TreeView_AssemblyInformationTree.Name = "TreeView_AssemblyInformationTree";
            this.TreeView_AssemblyInformationTree.Size = new System.Drawing.Size(651, 594);
            this.TreeView_AssemblyInformationTree.TabIndex = 6;
            // 
            // Button_FilterAssembly
            // 
            this.Button_FilterAssembly.Location = new System.Drawing.Point(388, 91);
            this.Button_FilterAssembly.Name = "Button_FilterAssembly";
            this.Button_FilterAssembly.Size = new System.Drawing.Size(75, 23);
            this.Button_FilterAssembly.TabIndex = 8;
            this.Button_FilterAssembly.Text = "Filter";
            this.Button_FilterAssembly.UseVisualStyleBackColor = true;
            this.Button_FilterAssembly.Click += new System.EventHandler(this.Button_FilterAssembly_Click);
            // 
            // ComboBox_FilterAssembly
            // 
            this.ComboBox_FilterAssembly.FormattingEnabled = true;
            this.ComboBox_FilterAssembly.Location = new System.Drawing.Point(12, 93);
            this.ComboBox_FilterAssembly.Name = "ComboBox_FilterAssembly";
            this.ComboBox_FilterAssembly.Size = new System.Drawing.Size(370, 21);
            this.ComboBox_FilterAssembly.TabIndex = 7;
            this.ComboBox_FilterAssembly.Text = "Select a filter assembly";
            this.ComboBox_FilterAssembly.SelectedIndexChanged += new System.EventHandler(this.ComboBox_FilterAssembly_SelectedIndexChanged);
            // 
            // Button_LoadAssemblyInformation
            // 
            this.Button_LoadAssemblyInformation.Location = new System.Drawing.Point(388, 62);
            this.Button_LoadAssemblyInformation.Name = "Button_LoadAssemblyInformation";
            this.Button_LoadAssemblyInformation.Size = new System.Drawing.Size(75, 23);
            this.Button_LoadAssemblyInformation.TabIndex = 5;
            this.Button_LoadAssemblyInformation.Text = "Load";
            this.Button_LoadAssemblyInformation.UseVisualStyleBackColor = true;
            this.Button_LoadAssemblyInformation.Click += new System.EventHandler(this.Button_LoadAssemblyInformation_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1037, 24);
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
            this.grpProjectTypes.Controls.Add(this.btnExportCurrent);
            this.grpProjectTypes.Controls.Add(this.Out);
            this.grpProjectTypes.Controls.Add(this.chkOutputTypes);
            this.grpProjectTypes.Controls.Add(this.chkProjectTypes);
            this.grpProjectTypes.Location = new System.Drawing.Point(686, 37);
            this.grpProjectTypes.Name = "grpProjectTypes";
            this.grpProjectTypes.Size = new System.Drawing.Size(281, 628);
            this.grpProjectTypes.TabIndex = 7;
            this.grpProjectTypes.TabStop = false;
            this.grpProjectTypes.Text = "Project Types";
            // 
            // btnExportCurrent
            // 
            this.btnExportCurrent.Location = new System.Drawing.Point(143, 590);
            this.btnExportCurrent.Name = "btnExportCurrent";
            this.btnExportCurrent.Size = new System.Drawing.Size(110, 23);
            this.btnExportCurrent.TabIndex = 3;
            this.btnExportCurrent.Text = "Export Current to CSV";
            this.btnExportCurrent.UseVisualStyleBackColor = true;
            this.btnExportCurrent.Click += new System.EventHandler(this.btnExportCurrent_Click);
            // 
            // Out
            // 
            this.Out.Location = new System.Drawing.Point(7, 589);
            this.Out.Name = "Out";
            this.Out.Size = new System.Drawing.Size(129, 23);
            this.Out.TabIndex = 2;
            this.Out.Text = "Export all CSV";
            this.Out.UseVisualStyleBackColor = true;
            this.Out.Click += new System.EventHandler(this.Out_Click);
            // 
            // chkOutputTypes
            // 
            this.chkOutputTypes.FormattingEnabled = true;
            this.chkOutputTypes.Location = new System.Drawing.Point(6, 489);
            this.chkOutputTypes.Name = "chkOutputTypes";
            this.chkOutputTypes.Size = new System.Drawing.Size(269, 94);
            this.chkOutputTypes.TabIndex = 1;
            this.chkOutputTypes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkOutputTypes_ItemCheck);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStatusLabel_OutputStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 742);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(569, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // ToolStatusLabel_OutputStatus
            // 
            this.ToolStatusLabel_OutputStatus.Name = "ToolStatusLabel_OutputStatus";
            this.ToolStatusLabel_OutputStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Location = new System.Drawing.Point(12, 129);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(981, 700);
            this.tabControl.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grpProjectTypes);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(973, 674);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Analysis";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnRemoveFromAllSolns);
            this.tabPage1.Controls.Add(this.btnRemoveProjectFromSolns);
            this.tabPage1.Controls.Add(this.btnFixDeadbeatSolutions);
            this.tabPage1.Controls.Add(this.btnDeadBeatSolns);
            this.tabPage1.Controls.Add(this.btnCleanReferences);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtResults);
            this.tabPage1.Controls.Add(this.btnAddReference);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtDestProjects);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(973, 674);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Modify Projects";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnRemoveProjectFromSolns
            // 
            this.btnRemoveProjectFromSolns.Location = new System.Drawing.Point(6, 296);
            this.btnRemoveProjectFromSolns.Name = "btnRemoveProjectFromSolns";
            this.btnRemoveProjectFromSolns.Size = new System.Drawing.Size(125, 36);
            this.btnRemoveProjectFromSolns.TabIndex = 8;
            this.btnRemoveProjectFromSolns.Text = "Remove Project From My Solns";
            this.btnRemoveProjectFromSolns.UseVisualStyleBackColor = true;
            this.btnRemoveProjectFromSolns.Click += new System.EventHandler(this.btnRemoveProjectFromSolns_Click);
            // 
            // btnFixDeadbeatSolutions
            // 
            this.btnFixDeadbeatSolutions.Location = new System.Drawing.Point(7, 244);
            this.btnFixDeadbeatSolutions.Name = "btnFixDeadbeatSolutions";
            this.btnFixDeadbeatSolutions.Size = new System.Drawing.Size(124, 23);
            this.btnFixDeadbeatSolutions.TabIndex = 7;
            this.btnFixDeadbeatSolutions.Text = "Fix Deadbeat Solutions";
            this.btnFixDeadbeatSolutions.UseVisualStyleBackColor = true;
            this.btnFixDeadbeatSolutions.Click += new System.EventHandler(this.btnFixDeadbeatSolutions_Click);
            // 
            // btnDeadBeatSolns
            // 
            this.btnDeadBeatSolns.Location = new System.Drawing.Point(6, 181);
            this.btnDeadBeatSolns.Name = "btnDeadBeatSolns";
            this.btnDeadBeatSolns.Size = new System.Drawing.Size(124, 23);
            this.btnDeadBeatSolns.TabIndex = 6;
            this.btnDeadBeatSolns.Text = "DeadBeat Solutions";
            this.btnDeadBeatSolns.UseVisualStyleBackColor = true;
            this.btnDeadBeatSolns.Click += new System.EventHandler(this.btnDeadBeatSolns_Click);
            // 
            // btnCleanReferences
            // 
            this.btnCleanReferences.Location = new System.Drawing.Point(7, 140);
            this.btnCleanReferences.Name = "btnCleanReferences";
            this.btnCleanReferences.Size = new System.Drawing.Size(124, 23);
            this.btnCleanReferences.TabIndex = 5;
            this.btnCleanReferences.Text = "Clean References";
            this.btnCleanReferences.UseVisualStyleBackColor = true;
            this.btnCleanReferences.Click += new System.EventHandler(this.btnCleanReferences_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(380, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Results";
            // 
            // txtResults
            // 
            this.txtResults.Location = new System.Drawing.Point(383, 72);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ReadOnly = true;
            this.txtResults.Size = new System.Drawing.Size(553, 583);
            this.txtResults.TabIndex = 3;
            // 
            // btnAddReference
            // 
            this.btnAddReference.Location = new System.Drawing.Point(6, 76);
            this.btnAddReference.Name = "btnAddReference";
            this.btnAddReference.Size = new System.Drawing.Size(125, 23);
            this.btnAddReference.TabIndex = 2;
            this.btnAddReference.Text = "Add Reference to";
            this.btnAddReference.UseVisualStyleBackColor = true;
            this.btnAddReference.Click += new System.EventHandler(this.btnAddReference_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Destination(s)";
            // 
            // txtDestProjects
            // 
            this.txtDestProjects.AcceptsTab = true;
            this.txtDestProjects.Location = new System.Drawing.Point(147, 72);
            this.txtDestProjects.Multiline = true;
            this.txtDestProjects.Name = "txtDestProjects";
            this.txtDestProjects.Size = new System.Drawing.Size(217, 583);
            this.txtDestProjects.TabIndex = 0;
            // 
            // chkProjectTypes
            // 
            this.chkProjectTypes.FormattingEnabled = true;
            this.chkProjectTypes.Location = new System.Drawing.Point(6, 19);
            this.chkProjectTypes.Name = "chkProjectTypes";
            this.chkProjectTypes.Size = new System.Drawing.Size(269, 454);
            this.chkProjectTypes.TabIndex = 0;
            this.chkProjectTypes.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkProjectTypes_ItemCheck);
            // 
            // btnRemoveFromAllSolns
            // 
            this.btnRemoveFromAllSolns.Location = new System.Drawing.Point(7, 339);
            this.btnRemoveFromAllSolns.Name = "btnRemoveFromAllSolns";
            this.btnRemoveFromAllSolns.Size = new System.Drawing.Size(123, 37);
            this.btnRemoveFromAllSolns.TabIndex = 9;
            this.btnRemoveFromAllSolns.Text = "Remove project from ALL Solutions";
            this.btnRemoveFromAllSolns.UseVisualStyleBackColor = true;
            this.btnRemoveFromAllSolns.Click += new System.EventHandler(this.btnRemoveFromAllSolns_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 832);
            this.Controls.Add(this.Button_FilterAssembly);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.ComboBox_FilterAssembly);
            this.Controls.Add(this.Button_SaveToCache);
            this.Controls.Add(this.Button_LoadFromCache);
            this.Controls.Add(this.Button_LoadAssemblyInformation);
            this.Controls.Add(this.Button_BuildFromDirectory);
            this.Controls.Add(this.ComboBox_AssemblySelector);
            this.Controls.Add(this.TextBox_DirectoryInputText);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grpProjectTypes.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
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
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem howTheShitDoIUseThisThingToolStripMenuItem;
        private System.Windows.Forms.ComboBox ComboBox_FilterAssembly;
        private System.Windows.Forms.Button Button_FilterAssembly;
		private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel ToolStatusLabel_OutputStatus;
        private System.Windows.Forms.GroupBox grpProjectTypes;
        private System.Windows.Forms.CheckedListBox chkOutputTypes;
        private ColoredCheckListBox chkProjectTypes;
        private System.Windows.Forms.Button Out;
        private System.Windows.Forms.Button btnExportCurrent;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView TreeView_AssemblyInformationTree;
        private System.Windows.Forms.Button btnAddReference;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDestProjects;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.Button btnCleanReferences;
        private System.Windows.Forms.Button btnDeadBeatSolns;
        private System.Windows.Forms.Button btnFixDeadbeatSolutions;
        private System.Windows.Forms.Button btnRemoveProjectFromSolns;
        private System.Windows.Forms.Button btnRemoveFromAllSolns;
    }
}

