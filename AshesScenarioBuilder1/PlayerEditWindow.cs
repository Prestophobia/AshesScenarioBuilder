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
    /// Unimplimented window containing controls for editing a player
    /// </summary>
    public partial class PlayerEditWindow : Form
    {
        Player selectedPlayer;
        /// <summary>
        /// Creates a useless window
        /// </summary>
        public PlayerEditWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Constructs a window that can properly edit the attributes of a given player
        /// </summary>
        /// <param name="p">The player to be edited</param>
        public PlayerEditWindow(Player p)
        {
            InitializeComponent();
            selectedPlayer = p;
        }

        private void PlayerEditWindow_Load(object sender, EventArgs e)
        {
            nameBox.Text = selectedPlayer.name;
            factionCB.Text = selectedPlayer.faction;
            teamNUD.Value = (decimal)selectedPlayer.team;
            startLocNud.Value = (decimal)selectedPlayer.startLoc;
            AITypeCB.Text = selectedPlayer.aiType;
            AIDiffCB.Text = selectedPlayer.aiDiff;
            noSeedBox.Checked = selectedPlayer.noSeed;
            noEngiBox.Checked = selectedPlayer.noEngineer;
            Text = "Player " + selectedPlayer.index;
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            selectedPlayer.name = nameBox.Text;
        }

        private void factionCB_TextChanged(object sender, EventArgs e)
        {
            selectedPlayer.faction = factionCB.Text;
        }

        private void teamNUD_ValueChanged(object sender, EventArgs e)
        {
            selectedPlayer.team = (int)teamNUD.Value;
        }

        private void colorCB_TextChanged(object sender, EventArgs e)
        {
            if(colorCB.Text.Equals("Red"))
            {
                selectedPlayer.color = 1;
            }
            else if (colorCB.Text.Equals("Blue"))
            {
                selectedPlayer.color = 2;
            }
            else if (colorCB.Text.Equals("Light Brown"))
            {
                selectedPlayer.color = 3;
            }
            else if (colorCB.Text.Equals("Orange"))
            {
                selectedPlayer.color = 4;
            }
            else if (colorCB.Text.Equals("Yellow"))
            {
                selectedPlayer.color = 5;
            }
            else if (colorCB.Text.Equals("Cyan"))
            {
                selectedPlayer.color = 6;
            }
            else if (colorCB.Text.Equals("Green"))
            {
                selectedPlayer.color = 7;
            }
        }

        private void startLocNud_ValueChanged(object sender, EventArgs e)
        {
            selectedPlayer.startLoc = (int)startLocNud.Value;
        }

        private void AITypeCB_TextChanged(object sender, EventArgs e)
        {
            selectedPlayer.aiType = AITypeCB.Text;
        }

        private void AIDiffCB_TextChanged(object sender, EventArgs e)
        {
            selectedPlayer.aiDiff = AIDiffCB.Text;
        }

        private void noSeedBox_CheckedChanged(object sender, EventArgs e)
        {
            selectedPlayer.noSeed = noSeedBox.Checked;
        }

        private void noEngiBox_CheckedChanged(object sender, EventArgs e)
        {
            selectedPlayer.noEngineer = noEngiBox.Checked;
        }
    }
}
