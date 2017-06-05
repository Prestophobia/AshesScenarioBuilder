using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AshesScenarioBuilder1
{
    public partial class OptionsMenu : Form
    {
        Form1 mainMenu;
        public OptionsMenu()
        {
            InitializeComponent();
        }
        public OptionsMenu(Form1 mW)
        {
            mainMenu = mW;
            InitializeComponent();
            AssetsPathTextBox.Text = mainMenu.USM.assetPath;
        }

        private void AssetOpenButton_Click(object sender, EventArgs e)
        {
            
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                AssetsPathTextBox.Text = fbd.SelectedPath;
            }
        }

        private void AssetsPathTextBox_TextChanged(object sender, EventArgs e)
        {
            if (mainMenu != null)
            {
                mainMenu.USM.assetPath = AssetsPathTextBox.Text;
                mainMenu.USM.writeXML(AssetsPathTextBox.Text);
                mainMenu.AAH.reload(mainMenu.USM);
            }
        }
    }
}
