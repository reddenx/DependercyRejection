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
            Loaded,
        }

        private LoadState _currentLoadState;
        private LoadState CurrentLoadState
        {
            get { return _currentLoadState; }
            set
            {
                _currentLoadState = value;
                if (_currentLoadState == LoadState.Loaded)
                {
                    SetAssemblyControls(true);
                }
                else
                {
                    SetAssemblyControls(false);
                }
            }
        }


        public Form1()
        {
            InitializeComponent();

            CurrentLoadState = LoadState.NotLoaded;
        }

        private void SetAssemblyControls(bool enabled)
        {
            Button_SaveToCache.Enabled = enabled;
            ComboBox_AssemblySelector.Enabled = enabled;
            Button_LoadAssemblyInformation.Enabled = enabled;
            TreeView_AssemblyInformationTree.Enabled = enabled;
        }
    }
}
