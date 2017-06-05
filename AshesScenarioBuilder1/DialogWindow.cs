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
    /// <summary>
    /// A pop up window in the GUI containing a menu that allows the user to edit the dialogue sequence contained in a Dialog action
    /// </summary>
    public partial class DialogWindow : Form
    {
        /// <summary>
        /// The Dialog Action being edited by the GUI
        /// </summary>
        public Action selectedDialog;
        /// <summary>
        /// The Trigger-editing window this window came from
        /// </summary>
        public TriggerWindow trigWin;
        /// <summary>
        /// The DialogEntry currently being edited in the GUI
        /// </summary>
        public DialogEntry selectedEntry;
        //DialogEntryMini[] dMs;
        DialogMenuOption[] dMs;

        /// <summary>
        /// Default constructor that just build the window without any access to the data
        /// </summary>
        public DialogWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Constructs a window for editing a Dialog action with proper access to the window it came from and the data it is supposed to edit
        /// </summary>
        /// <param name="diag">The Dialog action bto be edited</param>
        /// <param name="tW">The trigger-editing window this window was opened from</param>
        public DialogWindow(Action diag, TriggerWindow tW)
        {
            InitializeComponent();
            selectedDialog = diag;
            trigWin = tW;
        }

        private void DialogWindow_Load(object sender, EventArgs e)
        {
            updateUI();
        }

        /// <summary>
        /// Updates the window's user interface to match the data after it's been altered
        /// </summary>
        public void updateUI()
        {
            selectedDialog.trig.scen.mainWindow.previewWin.update(selectedDialog.toString());
            int y = 6;
            int xAdjust = 12;
            if (dMs != null)
            {
                for (int i = 0; i < dMs.Length; i++)
                {
                    //dMs[i].destroy();
                    dMs[i].Dispose();
                }
            }
            if (selectedDialog.entries != null)
            {
                //dMs = new DialogEntryMini[selectedDialog.entries.Length];
                dMs= new DialogMenuOption[selectedDialog.entries.Length];
                for (int i = 0; i < selectedDialog.entries.Length; i++)
                {
                    /*dMs[i] = new DialogEntryMini(selectedDialog.entries[i], this);
                    dMs[i].pan.Location=new Point(xAdjust,y);
                    EntryDisplayPanel.Controls.Add(dMs[i].pan);
                    y += (dMs[i].pan.Height+6);*/
                    dMs[i] = new DialogMenuOption(selectedDialog.entries[i], this);
                    dMs[i].Location = new Point(xAdjust, y);
                    EntryDisplayPanel.Controls.Add(dMs[i]);
                    y += (dMs[i].Height + 6);
                }
            }
            if (selectedEntry != null)
            {
                EntryEditorPanel.Visible = true;
                SelectedPortrait.Image = getPortrait();
                SelectedCharacter.Text = selectedEntry.icon;
                SelectedKey.Text = selectedEntry.text;
                SelectedAudioPath.Text = selectedEntry.audio;
                SelectedKey.Items.Clear();
                if (trigWin.mainMenu.csvData != null)
                {
                    if (trigWin.mainMenu.csvData.entries != null)
                    {
                        for (int i = 0; i < trigWin.mainMenu.csvData.entries.Length; i++)
                        {
                            if (trigWin.mainMenu.csvData.entries[i] != null && trigWin.mainMenu.csvData.entries[i].key != null)
                            {
                                SelectedKey.Items.Add(trigWin.mainMenu.csvData.entries[i].key);
                            }
                        }
                    }
                    SelectedString.Text = trigWin.mainMenu.csvData.getTranslatedString(selectedEntry.text);
                }
            }
            else
            {
                EntryEditorPanel.Visible = false;
            }
            NewEntryButton.Location = new Point(NewEntryButton.Left, y);
        }

        public void updateDialogMinis()
        {
            if (dMs != null)
            {
                for (int i = 0; i < dMs.Length; i++)
                {
                    dMs[i].refresh();
                }
            }
            selectedDialog.trig.scen.mainWindow.previewWin.update(selectedDialog.toString());
        }
        /// <summary>
        /// Updates the UI for the selected dialog entry section only without touching anything else(good for performance and stability)
        /// </summary>
        public void updateSelectedUI()
        {
            selectedDialog.trig.scen.mainWindow.previewWin.update(selectedDialog.toString());
            if (selectedEntry != null)
            {
                EntryEditorPanel.Visible = true;
                SelectedPortrait.Image = getPortrait();
                SelectedCharacter.Text = selectedEntry.icon;
                SelectedKey.Text = selectedEntry.text;
                SelectedAudioPath.Text = selectedEntry.audio;
                SelectedKey.Items.Clear();
                if (trigWin.mainMenu.csvData != null)
                {
                    if (trigWin.mainMenu.csvData.entries != null)
                    {
                        for (int i = 0; i < trigWin.mainMenu.csvData.entries.Length; i++)
                        {
                            if (trigWin.mainMenu.csvData.entries[i] != null && trigWin.mainMenu.csvData.entries[i].key != null)
                            {
                                SelectedKey.Items.Add(trigWin.mainMenu.csvData.entries[i].key);
                            }
                        }
                    }
                    SelectedString.Text = trigWin.mainMenu.csvData.getTranslatedString(selectedEntry.text);
                }
            }
            else
            {
                EntryEditorPanel.Visible = false;
            }
        }

        private void NewEntryButton_Click(object sender, EventArgs e)
        {
            if (selectedDialog != null)
            {
                selectedDialog.addEntryGeneral();
                updateUI();
            }
            
        }

        private void DestroyBuildingDelete_Click(object sender, EventArgs e)
        {
            selectedEntry.dialogParent.removeEntry(selectedEntry.index);
            selectedEntry = null;
            updateUI();
        }
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
            else if (selectedEntry.icon.Equals("Adrastela"))
            {
                return AshesScenarioBuilder1.Properties.Resources.adrastela;
            }
            else if (selectedEntry.icon.Equals("Agememnon"))
            {
                return AshesScenarioBuilder1.Properties.Resources.agememnon;
            }
            else if (selectedEntry.icon.Equals("Agethon"))
            {
                return AshesScenarioBuilder1.Properties.Resources.agethon;
            }
            else
            {
                return AshesScenarioBuilder1.Properties.Resources.na;
            }
        }

        private void SelectedCharacter_TextChanged(object sender, EventArgs e)
        {
            selectedEntry.icon = SelectedCharacter.Text;
            //updateUI();
            updateSelectedUI();
            updateDialogMinis();
        }

        private void SelectedKey_TextChanged(object sender, EventArgs e)
        {
            selectedEntry.text = SelectedKey.Text;
            
        }

        private void SelectedString_TextChanged(object sender, EventArgs e)
        {
            if (trigWin.mainMenu.csvData != null)
            {
                trigWin.mainMenu.csvData.overwriteEntry(selectedEntry.text, SelectedString.Text);
            }
            //updateUI();
            updateSelectedUI();
        }

        private void SelectedAudioPath_TextChanged(object sender, EventArgs e)
        {
            selectedEntry.audio = SelectedAudioPath.Text;
            //updateUI();
            updateSelectedUI();
        }

        private void AudioOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Mono Audio Files|*.WAV|All files (*.*)|*.*";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                selectedEntry.audio = ofd.FileName;
                //updateUI();
                updateSelectedUI();
            }

        }

        private void SelectedKey_Leave(object sender, EventArgs e)
        {
            //updateUI();
            updateSelectedUI();
        }

        private void DeleteActionButton_Click(object sender, EventArgs e)
        {
            selectedDialog.trig.removeAction(selectedDialog.index);
            this.Close();
        }
    }
}
