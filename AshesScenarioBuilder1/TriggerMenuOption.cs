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
    public partial class TriggerMenuOption : UserControl
    {
        /// <summary>
        /// The Trigger this object is displaying and allowing access to
        /// </summary>
        public Trigger trig;
        public Button delete, edit, up, down;
        public Label nameLabel, value;
        public Panel pan;


        public PictureBox icon;
        public Form1 main;
        public TriggerMenuOption()
        {
            InitializeComponent();
            delete = deleteButton;
            edit = editButton;
            up = upButton;
            down = downButton;
            nameLabel = triggerName;
            value = triggerDetails;
            icon = triggerIcon;
        }
        public TriggerMenuOption(Trigger t, Form1 mainWindow,Point loc)
        {
            InitializeComponent();
            trig = t;
            main = mainWindow;
            delete = deleteButton;
            edit = editButton;
            up = upButton;
            down = downButton;
            nameLabel = triggerName;
            value = triggerDetails;
            icon = triggerIcon;
            icon.Image = getIcon();
            nameLabel.Text = trig.id;
            value.Text = getValue();
            Location = loc;
        }

        string getValue()
        {
            string output = "";
            if (trig.type.Equals("Timer"))
            {
                output += trig.timer + " seconds";
            }
            else if (trig.type.Equals("Area") || trig.type.Equals("ZoneCapture"))
            {
                output += "Center " + trig.position + " Size " + trig.size;
            }
            else if (trig.type.Equals("Destruction") || trig.type.Equals("NamedCreate"))
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

        public Bitmap getIcon()
        {
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


        private void deleteButton_Click(object sender, EventArgs e)
        {
            trig.scen.removeTrigger(trig.index);           
            main.updateControlsFromScenario(main.scen1);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            if (trig.scen.swapTriggers(trig.index, trig.index - 1))
             main.updateControlsFromScenario(main.scen1);          
        }

        private void downButton_Click(object sender, EventArgs e)
        {
            if (trig.scen.swapTriggers(trig.index, trig.index + 1))            
                main.updateControlsFromScenario(main.scen1);           
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            TriggerWindow tW = new TriggerWindow(trig, main);
            DialogResult dR = tW.ShowDialog();
            if (dR.Equals(DialogResult.Cancel) || dR.Equals(DialogResult.Abort))
            {
                main.lastSelectedTrigger = trig.id;
                main.updateControlsFromScenario(main.scen1);
            }
        }
    }
}
