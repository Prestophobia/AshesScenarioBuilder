using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Xml;
using System.IO;
using System.Text;
using System.Drawing;

namespace AshesScenarioBuilder1
{
    /// <summary>
    /// GUI elements for displaying and accessing a Dialog action's DialogEntry sub-actions within a dialog window
    /// </summary>
    class DialogEntryMini
    {
        /// <summary>
        /// The DialogEntry this GUI element is displaying
        /// </summary>
        DialogEntry selectedEntry;
        /// <summary>
        /// The Dialog Window this GUI element is displayed on
        /// </summary>
        DialogWindow DialogWin;
        public Panel pan;
        Button edit;
        PictureBox icon;

        public DialogEntryMini(DialogEntry dE,DialogWindow dW)
        {
            selectedEntry = dE;
            DialogWin = dW;
            pan = new Panel();
            pan.BackColor = Color.Lavender;
            pan.Size = new Size(336,80);

            edit = new Button();
            edit.Size = new Size(265,80);
            pan.Controls.Add(edit);
            edit.Location = new Point(pan.Width-edit.Width,0);
            edit.Click += edit_Click;
            edit.Text = (selectedEntry.index + 1)+ ": " + selectedEntry.icon;

            icon = new PictureBox();
            pan.Controls.Add(icon);
            icon.Location = new Point(0, 0);
            icon.Size = new Size(60, 80);
            icon.Image = getPortrait();
            icon.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        void edit_Click(object sender, EventArgs e)
        {
            DialogWin.selectedEntry = selectedEntry;
            //DialogWin.updateUI();
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
            if (selectedEntry.icon.Equals("Mac"))
            {
                return AshesScenarioBuilder1.Properties.Resources.mac;
            }
            else if (selectedEntry.icon.Equals("Haalee"))
            {
                return AshesScenarioBuilder1.Properties.Resources.haalee;
            }
            else if (selectedEntry.icon.Equals("Valen"))
            {
                return AshesScenarioBuilder1.Properties.Resources.Valen;
            }
            else if (selectedEntry.icon.Equals("Samuel"))
            {
                return AshesScenarioBuilder1.Properties.Resources.samuel;
            }
            else if (selectedEntry.icon.Equals("Athena"))
            {
                return AshesScenarioBuilder1.Properties.Resources.athena;
            }
            else if (selectedEntry.icon.Equals("Rygos"))
            {
                return AshesScenarioBuilder1.Properties.Resources.Rygos;
            }
            else if (selectedEntry.icon.Equals("Vexen"))
            {
                return AshesScenarioBuilder1.Properties.Resources.vexen;
            }
            else if (selectedEntry.icon.Equals("Relias"))
            {
                return AshesScenarioBuilder1.Properties.Resources.relias;
            }
            else if (selectedEntry.icon.Equals("Artix"))
            {
                return AshesScenarioBuilder1.Properties.Resources.artix;
            }
            else if (selectedEntry.icon.Equals("Ventrix"))
            {
                return AshesScenarioBuilder1.Properties.Resources.ventrix;
            }
            else
            {
                return AshesScenarioBuilder1.Properties.Resources.na;
            }
        }

        public void destroy()
        {
            edit.Dispose();
            icon.Dispose();
            pan.Dispose();
        }
    }
}
