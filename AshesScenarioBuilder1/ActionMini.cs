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
    /// A winforms GUI element that displays an action's basic information and serves as a select button which,
    /// when pressed, allows the user to modify that action
    /// </summary>
    class ActionMini
    {
        /// <summary>
        /// The action this object provides the GUI for
        /// </summary>
        public Action selectedAction;
        /// <summary>
        /// The Triggerwindow this GUI elemented is nested in
        /// </summary>
        TriggerWindow trigWin;
        public Panel pan;
        public Button edit;
        PictureBox icon;
        /// <summary>
        /// Unimplimented tree structure objects
        /// </summary>
        ActionMicro[] ams;

        public ActionMini(Action act, TriggerWindow tW)
        {
            selectedAction = act;
            trigWin = tW;

            pan = new Panel();
            pan.Size = new Size(250,50);
            pan.BackColor = Color.LightGray;

            if (act.GetType() == typeof(Dialog))
            {
                pan.BackColor = Color.Beige;
            }

            icon = new PictureBox();
            icon.Size = new Size(50, 50);
            icon.Location = new Point(0, 0);
            icon.Image = specify();

            pan.Controls.Add(icon);

            edit = new Button();
            edit.Size = new Size(200, 50);
            edit.Location = new Point(50,0);
            edit.Text = selectedAction.getSummary();
            edit.Click += edit_Click;

            pan.Controls.Add(edit);

            //if(selectedAction.GetType()==typeof(SpawnUnit))
            //addChildren();
        }

        void edit_Click(object sender, EventArgs e)
        {
            if (selectedAction.GetType() == typeof(Dialog))
            {
                DialogWindow dW = new DialogWindow(selectedAction, trigWin);
                DialogResult dR = dW.ShowDialog();
                if (dR.Equals(DialogResult.Cancel) || dR.Equals(DialogResult.Abort))
                {
                    trigWin.updateUI();
                }
                return;
            }
            

            trigWin.setSelectedAction(selectedAction);
        }

        /// <summary>
        /// Gets the icon corresponding to the selected action
        /// </summary>
        /// <returns>The icon corresponding to the selected action</returns>
        public Bitmap specify()
        {
            if (selectedAction.GetType() == typeof(Dialog))
            {
                return AshesScenarioBuilder1.Properties.Resources.dialog;
            }
            if (selectedAction.GetType() == typeof(Camera))
            {
                return AshesScenarioBuilder1.Properties.Resources.camera;
            }
            if (selectedAction.GetType() == typeof(Reveal))
            {
                return AshesScenarioBuilder1.Properties.Resources.reveal;
            }
            if (selectedAction.GetType() == typeof(AreaIndicator))
            {
                return AshesScenarioBuilder1.Properties.Resources.AreaIndicator;
            }
            if (selectedAction.GetType() == typeof(Objective))
            {
                return AshesScenarioBuilder1.Properties.Resources.objective;
            }
            if (selectedAction.GetType() == typeof(Restrict))
            {
                return AshesScenarioBuilder1.Properties.Resources.restrict;
            }
            if (selectedAction.GetType() == typeof(SpawnUnit) || selectedAction.GetType() == typeof(SpawnBuilding))
            {
                return AshesScenarioBuilder1.Properties.Resources.spawn;
            }
            if (selectedAction.GetType() == typeof(DestroyUnit) || selectedAction.GetType() == typeof(DestroyBuilding))
            {
                return AshesScenarioBuilder1.Properties.Resources.destroy;
            }
            if (selectedAction.GetType() == typeof(AttackAttackMove))
            {
                return AshesScenarioBuilder1.Properties.Resources.attackAttackMove;
            }
            if (selectedAction.GetType() == typeof(ActivateTrigger))
            {
                return AshesScenarioBuilder1.Properties.Resources.activateTrigger;
            }
            if (selectedAction.GetType() == typeof(LetterBox))
            {
                return AshesScenarioBuilder1.Properties.Resources.letterbox;
            }
            if (selectedAction.GetType() == typeof(HidePanel))
            {
                return AshesScenarioBuilder1.Properties.Resources.hidePanel;
            }
            if (selectedAction.GetType() == typeof(GrantStuff))
            {
                return AshesScenarioBuilder1.Properties.Resources.grantstuff;
            }
            if (selectedAction.GetType() == typeof(GrantTech))
            {
                return AshesScenarioBuilder1.Properties.Resources.grantTech;
            }
            if (selectedAction.GetType() == typeof(CaptureNearest))
            {
                return AshesScenarioBuilder1.Properties.Resources.captureNearest;
            }
            if (selectedAction.GetType() == typeof(ToggleAI))
            {
                return AshesScenarioBuilder1.Properties.Resources.toggleAI;
            }
            if (selectedAction.GetType() == typeof(EndMission))
            {
                return AshesScenarioBuilder1.Properties.Resources.EndMission;
            }
            if (selectedAction.GetType() == typeof(Select))
            {
                return AshesScenarioBuilder1.Properties.Resources.select;
            }
            if (selectedAction.GetType() == typeof(Notifications))
            {
                return AshesScenarioBuilder1.Properties.Resources.notifications;
            }
            if (selectedAction.GetType() == typeof(Highlight))
            {
                return AshesScenarioBuilder1.Properties.Resources.highlight;
            }
            if (selectedAction.GetType() == typeof(ActivateAI))
            {
                return AshesScenarioBuilder1.Properties.Resources.ActivateAI;
            }
            if (selectedAction.GetType() == typeof(PlaySound))
            {
                return AshesScenarioBuilder1.Properties.Resources.playsound;
            }
            if (selectedAction.GetType() == typeof(Pause))
            {
                return AshesScenarioBuilder1.Properties.Resources.pause;
            }
            if (selectedAction.GetType() == typeof(ChangeAIDifficulty))
            {
                return AshesScenarioBuilder1.Properties.Resources.changeAIDiff;
            }
            if (selectedAction.GetType() == typeof(ChangeAIPersonality))
            {
                return AshesScenarioBuilder1.Properties.Resources.changeAIPersonality;
            }
            return AshesScenarioBuilder1.Properties.Resources.na;
        }

        /// <summary>
        /// Erases the GUI elements
        /// </summary>
        public void destroy()
        {
            pan.Dispose();
        }

        /// <summary>
        /// Part of an unimplemented tree structure using ActionMicros
        /// </summary>
        public void addChildren()
        {
            
            if (selectedAction.hasChildren())
            {
                int y = 55;
                Action[] actions = selectedAction.getChildren();
                ams = new ActionMicro[actions.Length];
                for (int i = 0; i < actions.Length; i++)
                {
                    if (actions[i] != null)
                    {
                        ams[i] = new ActionMicro(actions[i], trigWin,0,this,null);
                        ams[i].pan.Location = new Point(150, y);
                        pan.Controls.Add(ams[i].pan);
                        pan.Height += (ams[i].pan.Height+5);
                        y += (ams[i].pan.Height + 5);
                    }
                    
                }
            }
        }
    }
}
