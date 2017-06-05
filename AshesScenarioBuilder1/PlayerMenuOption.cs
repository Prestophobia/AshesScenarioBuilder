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
    public partial class PlayerMenuOption : UserControl
    {
        public Label nameLabel;
        public Button delete;
        public PictureBox icon, noEngi, noSeed;
        public Form1 main;
        public Player play;
        public PlayerMenuOption()
        {
            InitializeComponent();
        }
        public PlayerMenuOption(Player p, Form1 mainWindow)
        {
            InitializeComponent();
            main = mainWindow;
            play = p;

            icon = iconBox;
            icon.Image = getIcon();

            noEngi = noEngiDisplay;
            if (play.noEngineer) noEngi.Visible = true;
            

            noSeed = noSeedDisplay;
            if (play.noSeed) noSeed.Visible = true;
            

            nameLabel = name;
            nameLabel.Text = play.name;

            difficulty.Text = getDiffDisplay();
            BackColor = getCardColor();
                     
        }
        /// <summary>
        /// Gets a color corresponding to the team number
        /// </summary>
        /// <returns>A color corresponding to the team number</returns>
        public Color getCardColor()
        {
            if (play.team == 0)
            {
                return Color.LightGray;
            }
            else if (play.team == 1)
            {
                return Color.Orange;
            }
            else if (play.team == 2)
            {
                return Color.Yellow;
            }
            else if (play.team == 3)
            {
                return Color.Green;
            }
            else if (play.team == 4)
            {
                return Color.Blue;
            }
            else if (play.team == 5)
            {
                return Color.Violet;
            }
            else if (play.team == 6)
            {
                return Color.Pink;
            }

            return Color.White;
        }

        

        private void deleteButton_Click(object sender, EventArgs e)
        {
            main.scen1.removePlayer(play.index);
            main.selectedPlayer = null;
            main.updateControlsFromScenario(main.scen1);
            
        }

        private void PlayerMenuOption_Click(object sender, EventArgs e)
        {
            main.selectedPlayer = play;
            main.updateControlsFromScenario(main.scen1);
        }

        /// <summary>
        /// Gets an icon to fit the player's faction and color
        /// </summary>
        /// <returns>An icon to fit the player's faction and color</returns>
        public Bitmap getIcon()
        {
            if (play.faction == null)
            {
                return AshesScenarioBuilder1.Properties.Resources.na;
            }
            if (play.faction.ToLower().Equals("phc"))
            {
                if (play.color == 1)
                {
                    return AshesScenarioBuilder1.Properties.Resources.phcRed;
                }
                else if (play.color == 2)
                {
                    return AshesScenarioBuilder1.Properties.Resources.phcBlue;
                }
                else if (play.color == 3)
                {
                    return AshesScenarioBuilder1.Properties.Resources.phcBrown;
                }
                else if (play.color == 4)
                {
                    return AshesScenarioBuilder1.Properties.Resources.phcOrange;
                }
                else if (play.color == 5)
                {
                    return AshesScenarioBuilder1.Properties.Resources.phcYellow;
                }
                else if (play.color == 6)
                {
                    return AshesScenarioBuilder1.Properties.Resources.phcCyan;
                }
                else if (play.color == 7)
                {
                    return AshesScenarioBuilder1.Properties.Resources.phcGreen;
                }
            }
            else
            {
                if (play.color == 1)
                {
                    return AshesScenarioBuilder1.Properties.Resources.ssRed;
                }
                else if (play.color == 2)
                {
                    return AshesScenarioBuilder1.Properties.Resources.ssBlue;
                }
                else if (play.color == 3)
                {
                    return AshesScenarioBuilder1.Properties.Resources.ssBrown;
                }
                else if (play.color == 4)
                {
                    return AshesScenarioBuilder1.Properties.Resources.ssOrange;
                }
                else if (play.color == 5)
                {
                    return AshesScenarioBuilder1.Properties.Resources.ssYellow;
                }
                else if (play.color == 6)
                {
                    return AshesScenarioBuilder1.Properties.Resources.ssCyan;
                }
                else if (play.color == 7)
                {
                    return AshesScenarioBuilder1.Properties.Resources.ssGreen;
                }
            }

            return AshesScenarioBuilder1.Properties.Resources.na;
        }
        
       public string getDiffDisplay()
        {
            string output = "";
            if (play.aiType.ToLower().Equals("on"))
            {
                output = output + play.aiDiff;
            }else
            {
                output = output + play.aiType;
            }
            return output;
        }
    }
}
