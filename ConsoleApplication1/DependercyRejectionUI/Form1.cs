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
        }

        void Button_SaveToCache_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.InitialDirectory = TextBox_DirectoryInputText.Text;
            dialog.AddExtension = true;
            dialog.DefaultExt = "derp";
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
            }
        }

        private void Button_LoadAssemblyInformation_Click(object sender, EventArgs e)
        {
            if (ComboBox_AssemblySelector.SelectedItem is ComboBoxItem && ((ComboBoxItem)ComboBox_AssemblySelector.SelectedItem) != null)
            {
                BuildTreeInfo(((ComboBoxItem)ComboBox_AssemblySelector.SelectedItem).Value);
            }
        }

        private void BuildTreeInfo(ProjectFile projectFile)
        {
            TreeView_AssemblyInformationTree.Nodes.Clear();
            var baseNodes = BuildTreeNodes(projectFile);
            TreeView_AssemblyInformationTree.Nodes.Add(baseNodes.Item1);

            //traverse tree time...
        }

        private Tuple<TreeNode, int> BuildTreeNodes(ProjectFile file)
        {
            var thisNode = new TreeNode();

            int projectCount = file.ReferencesProjects.Count;
            foreach (var project in file.ReferencesProjects)
            {
                var info = BuildTreeNodes(project);
                thisNode.Nodes.Add(info.Item1);
                projectCount += info.Item2;
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
			var items = DependencyGraph.ProjectFiles.Select(project =>
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
			}

		}

        private void SetAssemblyControls(bool enabled)
        {
            Button_SaveToCache.Enabled = enabled;
            ComboBox_AssemblySelector.Enabled = enabled;
            Button_LoadAssemblyInformation.Enabled = enabled;
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
