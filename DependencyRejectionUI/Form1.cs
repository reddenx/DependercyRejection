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


        private void SetAssemblyControls(bool enabled)
        {
            Button_SaveToCache.Enabled = enabled;
            ComboBox_AssemblySelector.Enabled = enabled;
            ComboBox_FilterAssembly.Enabled = enabled;
            Button_LoadAssemblyInformation.Enabled = enabled;
            Button_FilterAssembly.Enabled = enabled;
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
                        Text = project.AssemblyName,
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

        private class ComboBoxItem
        {
            public string Text { get; set; }
            public ProjectFile Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

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

            var assemblyToAnalyze = ComboBox_AssemblySelector.SelectedItem;
            if (assemblyToAnalyze != null)
            {
                BuildFilteredProjectTree(((ComboBoxItem) assemblyToAnalyze).Value, _displayFilters);
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

            var assemblyToAnalyze = ComboBox_AssemblySelector.SelectedItem;
            if (assemblyToAnalyze != null)
            {
                BuildFilteredProjectTree(((ComboBoxItem) assemblyToAnalyze).Value, _displayFilters);
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

            var assemblyToAnalyze = ComboBox_AssemblySelector.SelectedItem;
            if (assemblyToAnalyze != null)
            {
                BuildFilteredProjectTree(((ComboBoxItem) assemblyToAnalyze).Value, _displayFilters);
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
            var currentlySelectedPRoject = ((ComboBoxItem) ComboBox_AssemblySelector.SelectedItem).Value;

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
            txtResults.Text = string.Empty;
            if (TextBox_DirectoryInputText.Text.ToLower().EndsWith("trunk"))
            {
                var confirmResult = MessageBox.Show("You are about to update trunk! you sure?",
                                     "Confirm update!!",
                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.No)
                    return;
            }
            if (null == ComboBox_AssemblySelector.SelectedItem)
            {
                txtResults.Text = "No source project selected";
                return;
            }

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
                }
            }
        }

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

            foreach (var deadBeatSolutionFile in currentlySelectedProject.GetDeadBeatSolutionFiles())
            {
                AppendLog(deadBeatSolutionFile.FileName);
            }
        }

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
        }
    }

}
