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
    /// A WinForms GUI element for displaying a player's attributes
    /// </summary>
    class PlayerCard
    {
        public Label nameLabel;
        public Panel card;
        public Button delete;
        public PictureBox icon, noEngi, noSeed,diff;
        public Form1 main;
        public Player play;

        public PlayerCard(Player p, Form1 mainWindow)
        {
            main = mainWindow;
            play = p;

            card = new Panel();
            card.Size = new Size(300, 70);
            card.BackColor = getCardColor();
            card.Click += card_Click;

            icon = new PictureBox();
            icon.Image = getIcon();
            icon.SizeMode = PictureBoxSizeMode.StretchImage;
            icon.Size = new Size(50,50);
            icon.Click += card_Click;

            card.Controls.Add(icon);

            delete = new Button();
            delete.Size = new Size(20,20);
            delete.Text = "x";
            delete.BackColor = Color.Red;
            delete.ForeColor = Color.White;
            delete.Click += delete_Click;

            card.Controls.Add(delete);

            noEngi = new PictureBox();
            noEngi.Image = AshesScenarioBuilder1.Properties.Resources.noEngi;
            noEngi.SizeMode = PictureBoxSizeMode.StretchImage;
            noEngi.Size = new Size(20,20);
            noEngi.Click += card_Click;
            if (!play.noEngineer)
            {
                noEngi.Visible = false;
            }

            card.Controls.Add(noEngi);

            noSeed = new PictureBox();
            noSeed.Image = AshesScenarioBuilder1.Properties.Resources.noSeed;
            noSeed.SizeMode = PictureBoxSizeMode.StretchImage;
            noSeed.Size = new Size(20, 20);
            noSeed.Click += card_Click;
            if (!play.noSeed)
            {
                noSeed.Visible = false;
            }

            card.Controls.Add(noSeed);

            nameLabel = new Label();
            nameLabel.Text = play.name;
            nameLabel.Size = new Size(200,13);
            nameLabel.BackColor = Color.Black;
            nameLabel.ForeColor = getNameColor();
            nameLabel.Text = play.name;
            nameLabel.Click += card_Click;

            card.Controls.Add(nameLabel);

            diff = new PictureBox();
            card.Controls.Add(diff);
            diff.Location = new Point(0, 0);
            diff.Size = new Size(80, 10);
            diff.Image = getStars();
        }

        void delete_Click(object sender, EventArgs e)
        {
            main.scen1.removePlayer(play.index);
            main.selectedPlayer = null;
            main.updateControlsFromScenario(main.scen1);
        }

        void card_Click(object sender, EventArgs e)
        {
            main.selectedPlayer = play;
            main.updateControlsFromScenario(main.scen1);
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
        /// <summary>
        /// Gets a color matching the AI difficulty
        /// </summary>
        /// <returns>A color matching the AI difficulty</returns>
        public Color getNameColor()
        {
            if (play.aiDiff == null)
            {
                return Color.White;
            }
            if (play.aiDiff.ToLower().Equals("beginner"))
            {
                return Color.Pink;
            }
            else if (play.aiDiff.ToLower().Equals("novice"))
            {
                return Color.Purple;
            }
            else if (play.aiDiff.ToLower().Equals("easy"))
            {
                return Color.Cyan;
            }
            else if (play.aiDiff.ToLower().Equals("intermediate"))
            {
                return Color.LightGreen;
            }
            else if (play.aiDiff.ToLower().Equals("normal"))
            {
                return Color.GreenYellow;
            }
            else if (play.aiDiff.ToLower().Equals("challenging"))
            {
                return Color.Yellow;
            }
            else if (play.aiDiff.ToLower().Equals("tough"))
            {
                return Color.Orange;
            }
            else if (play.aiDiff.ToLower().Equals("painful"))
            {
                return Color.OrangeRed;
            }
            else if (play.aiDiff.ToLower().Equals("insane"))
            {
                return Color.Red;
            }
            return Color.White;
        }
        /// <summary>
        /// Gets an image with a number of stars corresponding to AI difficulty
        /// </summary>
        /// <returns>An image with a number of stars corresponding to AI difficulty</returns>
        public Bitmap getStars()
        {
            if (play.aiDiff == null)
            {
                return AshesScenarioBuilder1.Properties.Resources._1star;
            }
            if (play.aiDiff.ToLower().Equals("beginner"))
            {
                return AshesScenarioBuilder1.Properties.Resources._1star;
            }
            else if (play.aiDiff.ToLower().Equals("novice"))
            {
                return AshesScenarioBuilder1.Properties.Resources._2star;
            }
            else if (play.aiDiff.ToLower().Equals("easy"))
            {
                return AshesScenarioBuilder1.Properties.Resources._3star;
            }
            else if (play.aiDiff.ToLower().Equals("intermediate"))
            {
                return AshesScenarioBuilder1.Properties.Resources._4star;
            }
            else if (play.aiDiff.ToLower().Equals("normal"))
            {
               return AshesScenarioBuilder1.Properties.Resources._5star;
            }
            else if (play.aiDiff.ToLower().Equals("challenging"))
            {
                return AshesScenarioBuilder1.Properties.Resources._6star;
            }
            else if (play.aiDiff.ToLower().Equals("tough"))
            {
                return AshesScenarioBuilder1.Properties.Resources._7star;
            }
            else if (play.aiDiff.ToLower().Equals("painful"))
            {
                return AshesScenarioBuilder1.Properties.Resources._8star;
            }
            else if (play.aiDiff.ToLower().Equals("insane"))
            {
                return AshesScenarioBuilder1.Properties.Resources._9star;
            }
            return AshesScenarioBuilder1.Properties.Resources._1star;
        }

        public void destroy()
        {
            card.Dispose();
        }
    }
}
