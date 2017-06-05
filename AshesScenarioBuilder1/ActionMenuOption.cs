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
    public partial class ActionMenuOption : UserControl
    {
        /// <summary>
        /// The action this object provides the GUI for
        /// </summary>
        public Action selectedAction;
        /// <summary>
        /// The Triggerwindow this GUI elemented is nested in
        /// </summary>
        TriggerWindow trigWin;
        public Button edit;
        PictureBox icon;
        public ActionMenuOption()
        {
            InitializeComponent();
        }
        public ActionMenuOption(Action act, TriggerWindow tW)
        {
            InitializeComponent();
            selectedAction = act;
            trigWin = tW;
            

            if (act.GetType() == typeof(Dialog)) BackColor = Color.Beige;


            icon = iconBox;
            icon.Image = specify();

            edit = editButton;
            edit.Text = selectedAction.getSummary();
        }

        private void editButton_Click(object sender, EventArgs e)
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
        public Bitmap specify()
        {
            if (selectedAction.GetType() == typeof(SpawnUnit) || selectedAction.GetType() == typeof(SpawnBuilding)) return AshesScenarioBuilder1.Properties.Resources.spawn;

            if (selectedAction.GetType() == typeof(DestroyUnit) || selectedAction.GetType() == typeof(DestroyBuilding)) return AshesScenarioBuilder1.Properties.Resources.destroy;

            if (selectedAction.GetType() == typeof(Dialog)) return AshesScenarioBuilder1.Properties.Resources.dialog;
            
            if (selectedAction.GetType() == typeof(Camera)) return AshesScenarioBuilder1.Properties.Resources.camera;
            
            if (selectedAction.GetType() == typeof(Reveal)) return AshesScenarioBuilder1.Properties.Resources.reveal;
           
            if (selectedAction.GetType() == typeof(AreaIndicator)) return AshesScenarioBuilder1.Properties.Resources.AreaIndicator;
            
            if (selectedAction.GetType() == typeof(Objective)) return AshesScenarioBuilder1.Properties.Resources.objective;
            
            if (selectedAction.GetType() == typeof(Restrict)) return AshesScenarioBuilder1.Properties.Resources.restrict;
            
            if (selectedAction.GetType() == typeof(AttackAttackMove)) return AshesScenarioBuilder1.Properties.Resources.attackAttackMove;
            
            if (selectedAction.GetType() == typeof(ActivateTrigger)) return AshesScenarioBuilder1.Properties.Resources.activateTrigger;
            
            if (selectedAction.GetType() == typeof(LetterBox)) return AshesScenarioBuilder1.Properties.Resources.letterbox;
            
            if (selectedAction.GetType() == typeof(HidePanel)) return AshesScenarioBuilder1.Properties.Resources.hidePanel;
            
            if (selectedAction.GetType() == typeof(GrantStuff)) return AshesScenarioBuilder1.Properties.Resources.grantstuff;
            
            if (selectedAction.GetType() == typeof(GrantTech)) return AshesScenarioBuilder1.Properties.Resources.grantTech;
            
            if (selectedAction.GetType() == typeof(CaptureNearest)) return AshesScenarioBuilder1.Properties.Resources.captureNearest;
            
            if (selectedAction.GetType() == typeof(ToggleAI)) return AshesScenarioBuilder1.Properties.Resources.toggleAI;
            
            if (selectedAction.GetType() == typeof(EndMission)) return AshesScenarioBuilder1.Properties.Resources.EndMission;
            
            if (selectedAction.GetType() == typeof(Select)) return AshesScenarioBuilder1.Properties.Resources.select;
            
            if (selectedAction.GetType() == typeof(Notifications)) return AshesScenarioBuilder1.Properties.Resources.notifications;
            
            if (selectedAction.GetType() == typeof(Highlight)) return AshesScenarioBuilder1.Properties.Resources.highlight;
            
            if (selectedAction.GetType() == typeof(ActivateAI)) return AshesScenarioBuilder1.Properties.Resources.ActivateAI;
            
            if (selectedAction.GetType() == typeof(PlaySound)) return AshesScenarioBuilder1.Properties.Resources.playsound;
            
            if (selectedAction.GetType() == typeof(Pause)) return AshesScenarioBuilder1.Properties.Resources.pause;
            
            if (selectedAction.GetType() == typeof(ChangeAIDifficulty)) return AshesScenarioBuilder1.Properties.Resources.changeAIDiff;
            
            if (selectedAction.GetType() == typeof(ChangeAIPersonality)) return AshesScenarioBuilder1.Properties.Resources.changeAIPersonality;          
            return AshesScenarioBuilder1.Properties.Resources.na;
        }
    }
}
