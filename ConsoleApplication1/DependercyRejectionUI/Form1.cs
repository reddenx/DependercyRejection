using ConsoleApplication1;
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

        public Form1()
        {
            InitializeComponent();

            CurrentLoadState = LoadState.NotLoaded;

            Button_BuildFromDirectory.Click += Button_BuildFromDirectory_Click;
            Button_LoadAssemblyInformation.Click += Button_LoadAssemblyInformation_Click;
            Button_LoadFromCache.Click += Button_LoadFromCache_Click;
            Button_SaveToCache.Click += Button_SaveToCache_Click;
            Button_FilterAssembly.Click += Button_FilterAssembly_Click;
        }

        private void Button_FilterAssembly_Click(object sender, EventArgs e)
        {
            var selectedItem = (ComboBox_FilterAssembly.SelectedItem as ComboBoxItem);
            if (selectedItem != null)
            {
                if (ComboBox_AssemblySelector.SelectedItem is ComboBoxItem && ((ComboBoxItem)ComboBox_AssemblySelector.SelectedItem) != null)
                {
                    BuildTreeInfo(((ComboBoxItem)ComboBox_AssemblySelector.SelectedItem).Value, selectedItem.Value);
                }
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
				DependencyGraphFactory.SaveToFile(dialog.FileName, this.DependencyGraph);				
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
                this.DependencyGraph = DependencyGraphFactory.LoadFromFile(dialog.FileName);
                this.CurrentLoadState = LoadState.Loaded;
				PopulateComboBox();
			}
        }

        private void Button_LoadAssemblyInformation_Click(object sender, EventArgs e)
        {
            if (ComboBox_AssemblySelector.SelectedItem is ComboBoxItem && ((ComboBoxItem)ComboBox_AssemblySelector.SelectedItem) != null)
            {
                BuildTreeInfo(((ComboBoxItem)ComboBox_AssemblySelector.SelectedItem).Value);
            }
        }

        private void BuildTreeInfo(ProjectFile projectFile, ProjectFile? filterProject = null)
        {
            TreeView_AssemblyInformationTree.Nodes.Clear();
            var baseNodes = BuildTreeNodes(projectFile, filterProject);
            if (baseNodes != null)
            {
                TreeView_AssemblyInformationTree.Nodes.Add(baseNodes.Item1);
            }

            var solutionNames = DependencyGraphFactory.GetDependantsForProject(new[] { projectFile })
                .SolutionFiles.Distinct()
                .Select(sol => sol.FilePath)
                .OrderBy(s => s)
                .ToArray();
            
            var solutionsNode = new TreeNode() { Text = "Solutions " + solutionNames.Length };
            

            solutionsNode.Nodes.AddRange(solutionNames.Select(sol => new TreeNode() { Text = sol }).ToArray());

            TreeView_AssemblyInformationTree.Nodes.Add(solutionsNode);
        }

        //current
        //recurse into children to get children

        private Tuple<TreeNode, int> BuildTreeNodes(ProjectFile file, ProjectFile? filter)
        {
            var thisNode = new TreeNode();

            if(filter.HasValue)
            {
                if (filter.Value.AssemblyName == file.AssemblyName)
                {
                    thisNode.Text = file.AssemblyName;
                    return new Tuple<TreeNode, int>(thisNode, 1);
                }
                else if (!file.ReferencesProjects.Any())
                {
                    return null;
                }
            }

            int projectCount = 0;
            foreach (var project in file.ReferencesProjects.OrderBy(proj => proj.AssemblyName))
            {
                var info = BuildTreeNodes(project, filter);
                if (info != null)
                {
                    thisNode.Nodes.Add(info.Item1);
                    projectCount += info.Item2;
                }
                else
                {
                    //return null;
                }
            }

            if (filter.HasValue)
            {
                if (thisNode.Nodes.Count == 0)
                {
                    return null;
                }
            }
            else
            {
                projectCount += thisNode.Nodes.Count;
            }

            thisNode.Text = (string.Format("{0}: {1}", file.AssemblyName, projectCount));

            return new Tuple<TreeNode, int>(thisNode, projectCount);
        }

        private void Button_BuildFromDirectory_Click(object sender, EventArgs e)
        {
            DependencyGraph = ConsoleApplication1.DependencyGraphFactory.BuildFromDisk(TextBox_DirectoryInputText.Text);

            if (DependencyGraph != null)
            {
                PopulateComboBox();
                CurrentLoadState = LoadState.Loaded;
            }
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

        private void SetAssemblyControls(bool enabled)
        {
            Button_SaveToCache.Enabled = enabled;
            ComboBox_AssemblySelector.Enabled = enabled;
            ComboBox_FilterAssembly.Enabled = enabled;
            Button_LoadAssemblyInformation.Enabled = enabled;
            Button_FilterAssembly.Enabled = enabled;
            TreeView_AssemblyInformationTree.Enabled = enabled;
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
    }
}
