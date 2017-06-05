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
    /// A WinForms GUI element displaying a Trigger's basic information
    /// with controls to edit the Trigger via a Trigger window, change its order, or delete it
    /// </summary>
    class TriggerMini
    {
        /// <summary>
        /// The Trigger this object is displaying and allowing access to
        /// </summary>
        public Trigger trig;
        public Button delete, edit,up,down;
        public Label nameLabel, value;
        public PictureBox icon;
        public Panel pan;
        public Form1 main;
        public TriggerMini(Trigger t, Form1 mainWindow)
        {
            trig=t;
            main = mainWindow;
            int x=0,y = 0;
            int xM = 155, yM = 85;
            pan = new Panel();
            pan.Location = new Point(x, y);
            pan.Size= new Size(xM,yM);
            pan.BackColor = Color.PowderBlue;

            nameLabel = new Label();
            nameLabel.Size = new Size(130,13);
            nameLabel.Location = new Point(x, y);
            nameLabel.Text = trig.id;

            pan.Controls.Add(nameLabel);

            delete = new Button();
            delete.Location = new Point(xM-25,y);
            delete.Size = new Size(25,25);
            delete.BackColor = Color.Red;
            delete.Text = "X";
            delete.Click += delete_Click;

            pan.Controls.Add(delete);

            icon = new PictureBox();
            icon.Location = new Point(x,yM-50);
            icon.Size = new Size(50, 50);
            icon.SizeMode = PictureBoxSizeMode.StretchImage;
            icon.Image = AshesScenarioBuilder1.Properties.Resources.na;

            pan.Controls.Add(icon);

            edit = new Button();
            edit.Text = "Edit";
            edit.Size = new Size(66,55);
            edit.Location = new Point(xM - 66, yM - 55);
            edit.Click += edit_Click;

            pan.Controls.Add(edit);

            value = new Label();
            value.Size = new Size(xM-(x+50),13);
            value.Location = new Point(x+50,yM-70);
            value.Text = getValue();
            pan.Controls.Add(value);
        }
        public TriggerMini(Trigger t, Form1 mainWindow,int locX, int locY)
        {
            main = mainWindow;
            trig = t;
            int x = locX, y = locY;
            int xM = x+600, yM = y+85;
            pan = new Panel();
            pan.Location = new Point(x, y);
            pan.Size = new Size(xM-x, yM-y);
            pan.BackColor = Color.PowderBlue;

            nameLabel = new Label();
            nameLabel.Size = new Size(130, 13);
            nameLabel.Location = new Point(x+1, y+1);
            nameLabel.Text = trig.id;

            pan.Controls.Add(nameLabel);

            delete = new Button();
            delete.Location = new Point(xM - 25, y);
            delete.Size = new Size(25, 25);
            delete.BackColor = Color.Red;
            delete.Text = "X";
            delete.Click += delete_Click;

            pan.Controls.Add(delete);

            icon = new PictureBox();
            icon.Location = new Point(x, yM - 50);
            icon.Size = new Size(50, 50);
            icon.SizeMode = PictureBoxSizeMode.StretchImage;
            icon.Image = getIcon();

            pan.Controls.Add(icon);

            edit = new Button();
            edit.Text = "Edit";
            edit.Size = new Size(66, 22);
            edit.Location = new Point(xM - 66, yM - 55);
            edit.Click += edit_Click;

            pan.Controls.Add(edit);

            value = new Label();
            value.Size = new Size(xM - (x + 50+66), 26);
            value.Location = new Point(x + 50, yM - 70);
            value.Text = getValue();

            pan.Controls.Add(value);

            up = new Button();
            up.Size = new Size(66, 22);
            pan.Controls.Add(up);
            up.Location=new Point(pan.Width-66,22);
            up.BackColor = Color.Green;
            up.ForeColor = Color.White;
            up.Text = "▲";
            up.Click += up_Click;

            down = new Button();
            down.Size = new Size(66, 22);
            pan.Controls.Add(down);
            down.Location = new Point(pan.Width - 66, 44);
            down.BackColor = Color.Green;
            down.ForeColor = Color.White;
            down.Text = "▼";
            down.Click += down_Click;
        }

        void down_Click(object sender, EventArgs e)
        {
            if (trig.scen.swapTriggers(trig.index, trig.index + 1))
            {
                main.updateControlsFromScenario(main.scen1);
            }
        }

        void up_Click(object sender, EventArgs e)
        {
            if (trig.scen.swapTriggers(trig.index, trig.index - 1))
            {
                main.updateControlsFromScenario(main.scen1);
            }
        }
        void edit_Click(object sender, EventArgs e)
        {
            TriggerWindow tW = new TriggerWindow(trig,main);
            DialogResult dR = tW.ShowDialog();            
            if (dR.Equals(DialogResult.Cancel) || dR.Equals(DialogResult.Abort))
            {
                main.lastSelectedTrigger = trig.id;
                main.updateControlsFromScenario(main.scen1);
            }
        }

        void delete_Click(object sender, EventArgs e)
        {
            trig.scen.removeTrigger(trig.index);
            destroy();
            main.updateControlsFromScenario(main.scen1);
        }
        string getValue()
        {
            string output = "";
            if (trig.type.Equals("Timer"))
            {
                output+= trig.timer+" seconds";
            }
            else if (trig.type.Equals("Area") || trig.type.Equals("ZoneCapture"))
            {
                output += "Center " + trig.position + " Size " + trig.size;
            }
            else if (trig.type.Equals("Destruction")  || trig.type.Equals("NamedCreate") )
            {
                output += "Target: " + trig.target;
            }
            else if (trig.type.Equals("Build"))
            {
                output += "Target: " + main.AAH.findAnyDisplayedName(trig.target);
            }
            else if (trig.type.Equals("Research"))
            {
                output += "Target: " + main.AAH.getResearchDisplayedName(trig.target);
            }
            else if (trig.type.Equals("Difficulty"))
            {
                switch (trig.difficulty)
                {
                    case 0:
                        output += "Much Easier";
                        break;
                    case 1:
                        output += "Easier";
                        break;
                    case 2:
                        output += "Normal";
                        break;
                    case 3:
                        output += "Harder";
                        break;
                    case 4:
                        output += "Much Harder";
                        break;
                }
            }
            if (trig.actions != null)
            {
                for (int i = 0; i < trig.actions.Length; i++)
                {
                    if (trig.actions[i].GetType() == typeof(ActivateTrigger))
                    {
                        output += ", Activates:" + trig.actions[i].getStringA();
                    }
                    else if (trig.actions[i].GetType() == typeof(SpawnUnit))
                    {
                        for (int q = 0; q < trig.scen.triggers.Length; q++)
                        {
                            if (trig.scen.triggers[q].target.Equals(trig.actions[i].getStringA()))
                            {
                                output += ", Spawns:" + trig.actions[i].getStringA();
                            }
                        }
                            
                    }
                    else if (trig.actions[i].GetType() == typeof(SpawnBuilding))
                    {
                        for (int q = 0; q < trig.scen.triggers.Length; q++)
                        {
                            if (trig.scen.triggers[q].target.Equals(trig.actions[i].getStringA()))
                            {
                                output += ", Spawns:" + trig.actions[i].getStringA();
                            }
                        }

                    }
                }
            }
            return output;
        }
        public void destroy()
        {
            delete.Dispose();
            edit.Dispose();
            nameLabel.Dispose();
            value.Dispose();
            icon.Dispose();
            pan.Dispose();
        }

        public Bitmap getIcon(){
            if (trig.type.Equals("Timer"))
            {
                return AshesScenarioBuilder1.Properties.Resources.timer;
            }
            else if (trig.type.Equals("Area"))
            {
                return AshesScenarioBuilder1.Properties.Resources.area;
            }
            else if (trig.type.Equals("Destruction"))
            {
                return AshesScenarioBuilder1.Properties.Resources.destruction;
            }
            else if (trig.type.Equals("Build"))
            {
                return AshesScenarioBuilder1.Properties.Resources.build;
            }
            else if (trig.type.Equals("NamedCreate"))
            {
                return AshesScenarioBuilder1.Properties.Resources.namedCreate;
            }
            else if (trig.type.Equals("ZoneCapture"))
            {
                return AshesScenarioBuilder1.Properties.Resources.zoneCapture;
            }
            else if (trig.type.Equals("Research"))
            {
                return AshesScenarioBuilder1.Properties.Resources.research;
            }
            else if (trig.type.Equals("Difficulty"))
            {
                return AshesScenarioBuilder1.Properties.Resources.difficulty;
            }
            return AshesScenarioBuilder1.Properties.Resources.na;
        }
    }
}
