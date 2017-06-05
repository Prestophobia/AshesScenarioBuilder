using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AshesScenarioBuilder1
{
    public partial class DialogMenuOption : UserControl
    {
        /// <summary>
        /// The DialogEntry this GUI element is displaying
        /// </summary>
        DialogEntry selectedEntry;
        /// <summary>
        /// The Dialog Window this GUI element is displayed on
        /// </summary>
        DialogWindow DialogWin;
        Button edit;
        PictureBox icon;
        public DialogMenuOption(DialogEntry dE, DialogWindow dW)
        {
            InitializeComponent();
            edit = nameLabel;
            icon = portrait;

            selectedEntry = dE;
            DialogWin = dW;
            
            edit.Text = (selectedEntry.index + 1) + ": " + selectedEntry.icon;

            
            icon.Image = getPortrait();
        }

        private void nameLabel_Click(object sender, EventArgs e)
        {
            DialogWin.selectedEntry = selectedEntry;
            DialogWin.updateSelectedUI();
        }
        public void refresh()
        {
            edit.Text = (selectedEntry.index + 1) + ": " + selectedEntry.icon;
            icon.Image = getPortrait();
        }

        /// <summary>
        /// Gets the character portrait matching the character selected by the action
        /// </summary>
        /// <returns>The character portrait matching the character selected by the action(N/A by default)</returns>
        Bitmap getPortrait()
        {
            if (selectedEntry.icon.Equals("Mac")) return AshesScenarioBuilder1.Properties.Resources.mac;            
            else if (selectedEntry.icon.Equals("Haalee"))    return AshesScenarioBuilder1.Properties.Resources.haalee;            
            else if (selectedEntry.icon.Equals("Valen"))     return AshesScenarioBuilder1.Properties.Resources.Valen;            
            else if (selectedEntry.icon.Equals("Samuel"))    return AshesScenarioBuilder1.Properties.Resources.samuel;            
            else if (selectedEntry.icon.Equals("Athena"))    return AshesScenarioBuilder1.Properties.Resources.athena;
            else if (selectedEntry.icon.Equals("Rygos"))     return AshesScenarioBuilder1.Properties.Resources.Rygos;           
            else if (selectedEntry.icon.Equals("Vexen"))     return AshesScenarioBuilder1.Properties.Resources.vexen;            
            else if (selectedEntry.icon.Equals("Relias"))    return AshesScenarioBuilder1.Properties.Resources.relias;            
            else if (selectedEntry.icon.Equals("Artix"))     return AshesScenarioBuilder1.Properties.Resources.artix;            
            else if (selectedEntry.icon.Equals("Ventrix"))   return AshesScenarioBuilder1.Properties.Resources.ventrix;
            else if (selectedEntry.icon.Equals("Adrastela")) return AshesScenarioBuilder1.Properties.Resources.adrastela;
            else if (selectedEntry.icon.Equals("Agememnon")) return AshesScenarioBuilder1.Properties.Resources.agememnon;
            else if (selectedEntry.icon.Equals("Agethon"))   return AshesScenarioBuilder1.Properties.Resources.agethon;
            

            else return AshesScenarioBuilder1.Properties.Resources.na;
            
        }
    }
}
