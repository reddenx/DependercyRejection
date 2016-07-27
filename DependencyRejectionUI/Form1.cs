using AltSource.Utilities.VSSolution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
<<<<<<< HEAD
using System.Security.AccessControl;
using System.Xml.Linq;
=======
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
using AltSource.Utilities.VSSolution.Filters;

namespace DependercyRejectionUI
{
    public partial class Form1 : Form
    {
        private enum LoadState
        {
            NotLoaded,
            Loading,
            Loaded,
        }

        private List<IGraphFilter> _displayFilters = new List<IGraphFilter>();

        private ProjectFile _lastFilterProjectFile = null;

        private LoadState _currentLoadState;

        private LoadState CurrentLoadState
        {
            get { return _currentLoadState; }
            set
            {
                _currentLoadState = value;
                switch (_currentLoadState)
                {
                    case LoadState.Loaded:
                        SetAssemblyControls(true);
                        break;
                    default:
                        SetAssemblyControls(false);
                        break;
                }
            }
        }

        private DependencyGraph DependencyGraph;

        private DependencyGraphFactory GraphFactory;

        public Form1()
        {
            InitializeComponent();

            GraphFactory = new DependencyGraphFactory();
            GraphFactory.OutputLog += GraphFactory_OutputLog;
            CurrentLoadState = LoadState.NotLoaded;

            LoadChkProjectTypes();
            LoadChkOutputTypes();
            
        }

        private void GraphFactory_OutputLog(object sender, string msg)
        {
            ToolStatusLabel_OutputStatus.Text = msg;
            Update();
        }

        private void Button_BuildFromDirectory_Click(object sender, EventArgs e)
        {
            DependencyGraph = GraphFactory.BuildFromDisk(TextBox_DirectoryInputText.Text);

            if (DependencyGraph != null)
            {
                PopulateComboBox();
                CurrentLoadState = LoadState.Loaded;
            }
        }

        private void Button_SaveToCache_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.InitialDirectory = TextBox_DirectoryInputText.Text;
            dialog.AddExtension = true;
            dialog.DefaultExt = "derp";
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (File.Exists(dialog.FileName))
                {
                    File.Delete(dialog.FileName);
                }
                GraphFactory.SaveToFile(dialog.FileName, this.DependencyGraph);
            }
        }

        private void Button_LoadFromCache_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.InitialDirectory = TextBox_DirectoryInputText.Text;
            //Text files (*.txt)|*.txt|All files (*.*)|*.*
            dialog.Filter = "Derp files (*.derp)|*.derp";
            dialog.Multiselect = false;
            var result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                this.DependencyGraph = GraphFactory.LoadFromFile(dialog.FileName);
                this.CurrentLoadState = LoadState.Loaded;
                PopulateComboBox();
            }
        }

        private void Button_LoadAssemblyInformation_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
             BuildFilteredProjectTree(GetACtiveProjectFile(false),  this._displayFilters);
        }

=======
            if (ComboBox_AssemblySelector.SelectedItem is ComboBoxItem &&
                ((ComboBoxItem) ComboBox_AssemblySelector.SelectedItem) != null)
            {
                BuildFilteredProjectTree(((ComboBoxItem) ComboBox_AssemblySelector.SelectedItem).Value,
                    this._displayFilters);
            }
        }

        private void Button_FilterAssembly_Click(object sender, EventArgs e)
        {
            if (ComboBox_AssemblySelector.SelectedItem is ComboBoxItem &&
                ((ComboBoxItem) ComboBox_AssemblySelector.SelectedItem) != null)
            {
                BuildFilteredProjectTree(((ComboBoxItem) ComboBox_AssemblySelector.SelectedItem).Value,
                    this._displayFilters);
            }
        }


>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void howTheShitDoIUseThisThingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                @"Load projects from branch:
1) enter path in textbox
2) click load
3) wait a really long time for monster to populate
4) Select assembly in question
5) Watch and be amazed!", "How to use this shit");
        }

<<<<<<< HEAD
=======

>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
        private void SetAssemblyControls(bool enabled)
        {
            Button_SaveToCache.Enabled = enabled;
            ComboBox_AssemblySelector.Enabled = enabled;
            ComboBox_FilterAssembly.Enabled = enabled;
            Button_LoadAssemblyInformation.Enabled = enabled;
<<<<<<< HEAD
=======
            Button_FilterAssembly.Enabled = enabled;
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
            TreeView_AssemblyInformationTree.Enabled = enabled;
        }

        private void PopulateComboBox()
        {
            ComboBox_AssemblySelector.Items.Clear();
            var items = DependencyGraph.ProjectFiles
                .OrderBy(proj => proj.AssemblyName)
                .Select(project =>
                {
                    return new ComboBoxItem()
                    {
<<<<<<< HEAD
                        Text = ((project.Exists) ? string.Empty : "   ### (in sln but missing) - " ) +   project.AssemblyName,
=======
                        Text = project.AssemblyName,
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
                        Value = project
                    };
                }).ToArray();

            foreach (var item in items.Where(item => item.Text != null))
            {
                ComboBox_AssemblySelector.Items.Add(item);
                ComboBox_FilterAssembly.Items.Add(item);
            }
        }

        private void BuildFilteredProjectTree(ProjectFile projectFile, List<IGraphFilter> displayFilters)
        {
            TreeView_AssemblyInformationTree.Nodes.Clear();

            var down = TreeNodeBuilder.BuildTreeNodes(projectFile, displayFilters, false);
            if (down != null)
            {
                var node = new TreeNode() {Text = "Dependencies Down " + down.ChildCount};
                node.Nodes.Add(down.Node);
                TreeView_AssemblyInformationTree.Nodes.Add(node);
            }

            var up = TreeNodeBuilder.BuildTreeNodes(projectFile, displayFilters, true);
            if (up != null)
            {
                var node = new TreeNode() {Text = "Dependencies Up " + up.ChildCount};
                node.Nodes.Add(up.Node);
                TreeView_AssemblyInformationTree.Nodes.Add(node);
            }

            var dependents = GraphFactory.GetDependantsForProject(new[] {projectFile});
            var solutionNames = dependents
                .SolutionFiles.Distinct()
                .Select(sol => sol.FilePath)
                .OrderBy(s => s)
                .ToArray();
            var solutionsNode = new TreeNode() {Text = "Solutions " + solutionNames.Length};
            solutionsNode.Nodes.AddRange(solutionNames.Select(sol => new TreeNode() {Text = sol}).ToArray());
            TreeView_AssemblyInformationTree.Nodes.Add(solutionsNode);

        }
<<<<<<< HEAD
        
=======

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public ProjectFile Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
        private void LoadChkProjectTypes()
        {
            chkProjectTypes.Items.AddRange(
                ProjectTypeDict.All.Select(i => new ProjectType(i.Key, i.Value.TypeName, i.Value.Color))
                    .ToArray());

            chkProjectTypes.DisplayMember = "TypeName";

            for (int i = 0; i < chkProjectTypes.Items.Count; i++)
            {
                chkProjectTypes.SetItemChecked(i, true);
            }
        }

        private void LoadChkOutputTypes()
        {
            var i = 0;
            foreach (var outputType in Enum.GetValues(typeof (ProjectOutputType)))
            {
                chkOutputTypes.Items.Add(outputType);
                chkOutputTypes.SetItemChecked(i++, true);
            }
        }

        private void chkProjectTypes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var selectedGuid = ((ProjectType) ((CheckedListBox) sender).Items[e.Index]).ID;

            ProjectTypeFilter filter = new ProjectTypeFilter()
            {
                ProjectType = ProjectTypeDict.Get(selectedGuid)
            };

            if (e.NewValue == CheckState.Checked)
            {
                this._displayFilters.Remove(filter);
            }
            else
            {
                this._displayFilters.Add(filter);
            }

<<<<<<< HEAD
            var assemblyToAnalyze = GetACtiveProjectFile(false);
            if (assemblyToAnalyze != null)
            {
                BuildFilteredProjectTree(assemblyToAnalyze, _displayFilters);
=======
            var assemblyToAnalyze = ComboBox_AssemblySelector.SelectedItem;
            if (assemblyToAnalyze != null)
            {
                BuildFilteredProjectTree(((ComboBoxItem) assemblyToAnalyze).Value, _displayFilters);
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
            }
        }

        private void chkOutputTypes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var outputType = ((ProjectOutputType) ((CheckedListBox) sender).Items[e.Index]);

            var filter = new ProjectOutputTypeFilter()
            {
                OutputType = outputType
            };

            if (e.NewValue == CheckState.Checked)
            {
                this._displayFilters.Remove(filter);
            }
            else
            {
                this._displayFilters.Add(filter);
            }

<<<<<<< HEAD
            var assemblyToAnalyze = GetACtiveProjectFile(false);
            if (assemblyToAnalyze != null)
            {
                BuildFilteredProjectTree(assemblyToAnalyze, _displayFilters);
=======
            var assemblyToAnalyze = ComboBox_AssemblySelector.SelectedItem;
            if (assemblyToAnalyze != null)
            {
                BuildFilteredProjectTree(((ComboBoxItem) assemblyToAnalyze).Value, _displayFilters);
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
            }
        }

        private void ComboBox_FilterAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newFilterProject = (ComboBox_FilterAssembly.SelectedItem as ComboBoxItem);

            if (null != newFilterProject)
            {
                var filter = new ProjectIdFilter()
                {
                    FilterProjectGuid = newFilterProject.Value.ProjectId
                };
                this._displayFilters.Remove(filter);
            }
            else if (null != _lastFilterProjectFile)
            {
                var filter = new ProjectIdFilter()
                {
                    FilterProjectGuid = _lastFilterProjectFile.ProjectId
                };
                this._displayFilters.Remove(filter);
            }

            _lastFilterProjectFile = newFilterProject.Value;

<<<<<<< HEAD
            var assemblyToAnalyze = GetACtiveProjectFile(false);
            if (assemblyToAnalyze != null)
            {
                BuildFilteredProjectTree(assemblyToAnalyze, _displayFilters);
=======
            var assemblyToAnalyze = ComboBox_AssemblySelector.SelectedItem;
            if (assemblyToAnalyze != null)
            {
                BuildFilteredProjectTree(((ComboBoxItem) assemblyToAnalyze).Value, _displayFilters);
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
            }
        }

        private void Out_Click(object sender, EventArgs e)
        {
            var assemblies = DependencyGraph.ProjectFiles
                .OrderBy(proj => proj.AssemblyName);

            using (StreamWriter sw = new StreamWriter(@"D:\\projects.csv", false))
            {
                sw.WriteLine(@"AssemblyName,Path,OutputType,ProjectType,ReferencedBy,References");

                foreach (var project in assemblies.Where(item => item.AssemblyName != null))
                {
                    sw.WriteLine(string.Format(@"""{0}"",""{1}"",""{2}-{3}"",{4}",
                        project.AssemblyName,
                        project.FilePath,
                        project.OutputType.ToString() + "-" +
                        project.ProjectType.TypeName,
                        project.ReferencedByProjects.Count,
                        project.ReferencesProjects.Count
                        ));

                }
            }

        }

        private void btnExportCurrent_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            var currentlySelectedPRoject = GetACtiveProjectFile(false);
=======
            var currentlySelectedPRoject = ((ComboBoxItem) ComboBox_AssemblySelector.SelectedItem).Value;
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b

            using (StreamWriter sw = new StreamWriter(@"D:\\projectsCurrent.csv", false))
            {
                sw.WriteLine(@"AssemblyName,Path,OutputType-ProjectType,ReferencedBy,References");

                foreach (var project in currentlySelectedPRoject.ReferencesProjects)
                {
                    sw.WriteLine(string.Format(@"""{0}"",""{1}"",""{2}"",{3},{4}",
                        project.AssemblyName,
                        project.FilePath,
                        project.OutputType.ToString() + "-" + project.ProjectType.TypeName,
                        project.ReferencedByProjects.Count,
                        project.ReferencesProjects.Count
                        ));

                }
            }
        }

        private void btnAddReference_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
=======
            txtResults.Text = string.Empty;
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
            if (TextBox_DirectoryInputText.Text.ToLower().EndsWith("trunk"))
            {
                var confirmResult = MessageBox.Show("You are about to update trunk! you sure?",
                                     "Confirm update!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.No)
                    return;
            }
<<<<<<< HEAD

            var currentlySelectedProject = GetACtiveProjectFile(false);

            if (null == currentlySelectedProject)
=======
            if (null == ComboBox_AssemblySelector.SelectedItem)
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
            {
                txtResults.Text = "No source project selected";
                return;
            }

<<<<<<< HEAD

            IEnumerable<ProjectFile> projList = txtDestProjects.Text.Split(new string[] { "\r", "\n", "\t" }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(p => DependencyGraph.FindFileByName(p));
            WriteLog("Updating listed projects");


            foreach (var projFile in projList)
            {
                if (null == projFile)
                {
                    AppendLog("[Dependency Graph does not contain file] ");
                }
                else
                {
                    AppendLog("[Found] " + projFile.AssemblyName);

                    int removed = projFile.AddReference(currentlySelectedProject);
                    if (removed < 0)
                    {
                        AppendLog("Project skipped.");
                    }

                    AppendLog("[Removed] " + removed.ToString( ) + " existing references.");
                    AppendLog("[Reference added] ");
=======
            var currentlySelectedProject = ((ComboBoxItem)ComboBox_AssemblySelector.SelectedItem).Value;
            
            foreach (var projectName in txtDestProjects.Text.Split(new string[] {Environment.NewLine}, StringSplitOptions.None))
            {
                var projFile = DependencyGraph.FindFileByName(projectName);
                if (null == projFile)
                {
                    txtResults.Text += "[Dependency Graph does not contain] " + projectName + Environment.NewLine;
                }
                else
                {
                    txtResults.Text += "[Found] " + projectName + Environment.NewLine;

                    int removed = projFile.AddReference(currentlySelectedProject);

                    txtResults.Text += "[Removed] " + removed.ToString( ) + " existing references." + Environment.NewLine;
                    txtResults.Text += "[Reference added] " + Environment.NewLine + Environment.NewLine;
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
                }
            }
        }

<<<<<<< HEAD
        private void btnDeadBeatSolns_Click(object sender, EventArgs e)
        {
            txtResults.Text = string.Empty;
            var currentlySelectedProject = GetACtiveProjectFile(true);
=======
        private void btnCleanReferences_Click(object sender, EventArgs e)
        {

            var currentlySelectedProject = ((ComboBoxItem)ComboBox_AssemblySelector.SelectedItem).Value;
            if (currentlySelectedProject.ReferencedByProjects.Count == 0)
            {
                txtResults.Text += currentlySelectedProject.AssemblyName + " is either not loaded or is unreferenced by any projects." + Environment.NewLine;
            }

            foreach (var referencedByProject in currentlySelectedProject.ReferencedByProjects)
            {
                referencedByProject.AddReference(currentlySelectedProject);
                txtResults.Text += referencedByProject.AssemblyName + " cleaned." + Environment.NewLine;
            }
        }

        private void btnDeadBeatSolns_Click(object sender, EventArgs e)
        {
            txtResults.Text = string.Empty;
            var currentlySelectedProject = ((ComboBoxItem)ComboBox_AssemblySelector.SelectedItem).Value;
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b

            foreach (var deadBeatSolutionFile in currentlySelectedProject.GetDeadBeatSolutionFiles())
            {
                AppendLog(deadBeatSolutionFile.FileName);
            }
        }

<<<<<<< HEAD
        private void btnFixDeadbeatSolutions_Click(object sender, EventArgs e)
        {
            txtResults.Text = string.Empty;
            var currentlySelectedProject = GetACtiveProjectFile(true);
=======
        private void AppendLog(string text)
        {
            txtResults.Text += text + Environment.NewLine;
        }
        private void WriteLog(string text)
        {
            txtResults.Text = text + Environment.NewLine;
        }

        private void btnFixDeadbeatSolutions_Click(object sender, EventArgs e)
        {
            txtResults.Text = string.Empty;
            var currentlySelectedProject = ((ComboBoxItem)ComboBox_AssemblySelector.SelectedItem).Value;
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b

            ProjectType defaultAddType = (currentlySelectedProject.ProjectType.ID == Guid.Empty) ? ProjectTypeDict.GetByName("c#") : currentlySelectedProject.ProjectType;

            foreach (var deadBeatSolutionFile in currentlySelectedProject.GetDeadBeatSolutionFiles())
            {
                if (deadBeatSolutionFile.AddProjectFileToSolution(currentlySelectedProject, defaultAddType))
                {
                    AppendLog("[Fixed] " + deadBeatSolutionFile.FileName);
                }
                else
                {
                    AppendLog("[Ignored] " + deadBeatSolutionFile.FileName);
                }
            }
        }
        
        private void btnRemoveProjectFromSolns_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            var currentlySelectedProject = GetACtiveProjectFile(true);

            var removed = currentlySelectedProject.Emancipate();
            foreach(var solutionFile in removed)
            {
                AppendLog("Updated " + solutionFile.FilePath + " soution files.");
            }
        }

        private void btnRemoveFromAllSolns_Click(object sender, EventArgs e)
        {
            var currentlySelectedProject = GetACtiveProjectFile(true);
                
            var removed = currentlySelectedProject.Emancipate(DependencyGraph.SolutionFiles);

            foreach (var solutionFile in removed)
            {
                AppendLog("Updated " + solutionFile.FilePath + " soution files.");
            }
        }

        private void btnRemoveReferences_Click(object sender, EventArgs e)
        {
            ProjectFile delProjFile = GetACtiveProjectFile(true);

            if (null != DependencyGraph)
            {
                foreach ( var projFile in txtDestProjects.Text.Split(new string[] {"\r", "\n", "\t"}, StringSplitOptions.RemoveEmptyEntries).Select(pn => DependencyGraph.FindFileByName(pn)))
                {
                    if (null == projFile)
                    {
                        txtResults.Text += "[Dependency Graph does not contain project] "  + Environment.NewLine;
                    }
                    else
                    {
                        int removed = projFile.RemoveExistingProjectReferences(delProjFile);

                        projFile.Xml.Save(projFile.FilePath, SaveOptions.OmitDuplicateNamespaces);

                        txtResults.Text += "[Removed from] " + removed.ToString() + " existing references." +
                                           Environment.NewLine;
                    }
                }
            }
            else
            {
                WriteLog("Gotta load first");
            }
        }

        private void btnFillWithAncestors_Click(object sender, EventArgs e)
        {
            var currentlySelectedProject = GetACtiveProjectFile(false);

            if (null == currentlySelectedProject)
            {
                txtResults.Text = "No source project selected";
                return;
            }

            txtDestProjects.Text = string.Join(Environment.NewLine,currentlySelectedProject.GetAncestors().Select(p => p.AssemblyName));

        }

        private void btnAddPackage_Click(object sender, EventArgs e)
        {
            foreach (
                var projFile in
                    txtDestProjects.Text.Split(new string[] {"\r", "\n", "\t"}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(pn => DependencyGraph.FindFileByName(pn)))
            {
                //projFile.Packages.Root.AddAfterSelf(
                //    new XElement());
            }
        }

        #region Helpers

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public ProjectFile Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
        private ProjectFile GetACtiveProjectFile(bool overrideWithManualProject)
        {
            ProjectFile rtnProj = null;
            if (overrideWithManualProject && (txtProjGuid.Text.Length > 0 || txtProjName.Text.Length > 0))
            {
                Guid guid;
                Guid.TryParse(txtProjGuid.Text, out guid);
                rtnProj = ProjectFile.Build(guid, txtProjName.Text, ProjectTypeDict.GetByName("C#"));


                //See if it is in Graph as a missing file in a solution 
                rtnProj = DependencyGraph.ProjectFiles.Where(p => p == rtnProj).DefaultIfEmpty(rtnProj).First();
            }
            else
            {
                var selectedItem = ComboBox_AssemblySelector.SelectedItem as ComboBoxItem;
                if (null != selectedItem)
                {
                    rtnProj = selectedItem.Value;
                }
            }

            return rtnProj;
        }


        private void AppendLog(string text)
        {
            txtResults.Text += text + Environment.NewLine;
        }
        private void WriteLog(string text)
        {
            txtResults.Text = text + Environment.NewLine;
        }
        #endregion

        private void txtMissing_Click(object sender, EventArgs e)
        {
            List<Tuple<SolutionFile, ProjectFile>> missTuples = new List<Tuple<SolutionFile, ProjectFile>>();
            foreach (var solutionFile in DependencyGraph.SolutionFiles)
            {
                foreach (var projectFile in solutionFile.Projects)
                {
                    if (!projectFile.Exists)
                    {
                        missTuples.Add(new Tuple<SolutionFile, ProjectFile>(solutionFile, projectFile));
                    }
                }
            }

            WriteLog("Unique Projects\r\n---------------\r\n");
            AppendLog(
                string.Join("\r\n", missTuples.Select(t => string.Format("{0} {1}", t.Item2.AssemblyName, t.Item2.ProjectId)).Distinct().ToArray())
                );
            AppendLog("");
            AppendLog(
                string.Join(
                    "\r\n===========================\r\n",
                    missTuples.Select(t => string.Format("{0}\r\n{1} {2}", t.Item1.FileName, t.Item2.AssemblyName, t.Item2.ProjectId.ToString())).ToArray())
                );
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

=======
            var selectedItem = ComboBox_AssemblySelector.SelectedItem;
            if (null == selectedItem)
            {
                AppendLog("No project selected");
            }
            else
            {
                var currentlySelectedProject = ((ComboBoxItem)selectedItem).Value;

                var removed = currentlySelectedProject.Emancipate();
                foreach(var solutionFile in removed)
                {
                    AppendLog("Updated " + solutionFile.FilePath + " soution files.");
                }
            }
        }

        private void btnRemoveFromAllSolns_Click(object sender, EventArgs e)
        {
            var selectedItem = ComboBox_AssemblySelector.SelectedItem;
            if (null == selectedItem)
            {
                AppendLog("No project selected");
            }
            else
            {
                var currentlySelectedProject = ((ComboBoxItem)selectedItem).Value;
                var removed = currentlySelectedProject.Emancipate(DependencyGraph.SolutionFiles);

                foreach (var solutionFile in removed)
                {
                    AppendLog("Updated " + solutionFile.FilePath + " soution files.");
                }

            }
>>>>>>> 7fb18f8102a2bb61da75472e0dcb20b8d717895b
        }
    }

}
