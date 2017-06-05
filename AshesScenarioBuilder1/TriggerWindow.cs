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
    /// A window containing controls for editing a Scenario's Trigger and its Actions
    /// </summary>
    public partial class TriggerWindow : Form
    {
        Trigger trig;
        //ActionMini[] aM;
        ActionMenuOption[] aM;
        Action selectedAction;
        /// <summary>
        /// Constructs a useless window taht does nothing
        /// </summary>
        public TriggerWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// The panel containing controls for editing the selected action
        /// </summary>
        public Panel actionDisplay;
        /// <summary>
        /// The main window of the Scenario editor
        /// </summary>
        public Form1 mainMenu;
        /// <summary>
        /// Constructs a current window belonging to the main window editing a specified Trigger
        /// </summary>
        /// <param name="t">The Trigger to be edited</param>
        /// <param name="main">Thye main window of the Scenario editor</param>
        public TriggerWindow(Trigger t, Form1 main)
        {
            trig = t;
            mainMenu = main;
            InitializeComponent();
        }

        private void TriggerWindow_Load(object sender, EventArgs e)
        {
           
            updateUI();
            actionDisplay = ActionEditorPanel;
            ActionEditorPanel.Controls.Add(DuplicateButton);
            ActionEditorPanel.Controls.Add(CameraPanel);
            DuplicateButton.Location = new Point(0,CameraPanel.Height);
            CameraPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(ToggleAIPanel);
            ToggleAIPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(ActivateTriggerPanel);
            ActivateTriggerPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(RevealPanel);
            RevealPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(NotificationsPanel);
            NotificationsPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(AreaIndicatorPanel);
            AreaIndicatorPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(DestroyUnitPanel);
            DestroyUnitPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(PlaySoundPanel);
            PlaySoundPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(ObjectivePanel);
            ObjectivePanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(RestrictPanel);
            RestrictPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(EndMissionPanel);
            EndMissionPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(SpawnUnitPanel);
            SpawnUnitPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(DestroyBuildingPanel);
            DestroyBuildingPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(PausePanel);
            PausePanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(AttackAttackMovePanel);
            AttackAttackMovePanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(SpawnBuildingPanel);
            SpawnBuildingPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(ChangeAIDifficultyPanel);
            ChangeAIDifficultyPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(LetterBoxPanel);
            LetterBoxPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(HidePanelPanel);
            HidePanelPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(GrantStuffPanel);
            GrantStuffPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(GrantTechPanel);
            GrantTechPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(CaptureNearestPanel);
            CaptureNearestPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(SelectPanel);
            SelectPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(HighlightPanel);
            HighlightPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(ActivateAIPanel);
            ActivateAIPanel.Location = new Point(0, 0);

            ActionEditorPanel.Controls.Add(ChangeAIPersonalityPanel);
            ChangeAIPersonalityPanel.Location = new Point(0, 0);

            SpawnUnitTemplate.Items.Clear();
            string[] unitNames = mainMenu.AAH.getUnitNames();
            for (int i = 0; i < unitNames.Length; i++)
            {
                if (unitNames[i]!=null)
                SpawnUnitTemplate.Items.Add(unitNames[i]);
            }

            SpawnBuildingTemplate.Items.Clear();
            string[] buildingNames = mainMenu.AAH.getBuildingNames();
            for (int i = 0; i < buildingNames.Length; i++)
            {
                if (buildingNames[i] != null)
                    SpawnBuildingTemplate.Items.Add(buildingNames[i]);
            }

            

            PlaySoundSound.Items.Clear();
            string[] sfxNames = mainMenu.AAH.getSFXNames();
            for (int i = 0; i < sfxNames.Length; i++)
            {
                if (sfxNames[i] != null)
                    PlaySoundSound.Items.Add(sfxNames[i]);
            }

            ChangeAIPersonalityName.Items.Clear();
            string[] aINames= mainMenu.AAH.getAINames();
            for (int i = 0; i < aINames.Length; i++)
            {
                if (aINames[i] != null)
                    ChangeAIPersonalityName.Items.Add(aINames[i]);
            }

            ActivateTriggerTarget.Items.Clear();
            for (int i = 0; i < trig.scen.triggers.Length; i++)
            {
                if (trig.scen.triggers[i] != null && trig.scen.triggers[i].id!=null)
                    ActivateTriggerTarget.Items.Add(trig.scen.triggers[i].id);
            }


            TargetCB.Left = targetTxtBox.Left;
            TargetCB.Top = targetTxtBox.Top;
        }
        /// <summary>
        /// Refreshes the Trigger Window's UI with accurate information regarding the attributes of both the Trigger itself and its attributes
        /// </summary>
        public void updateUI()
        {
            mainMenu.Text = "Ashes Scenario Builder  LOADING!!!";
            Text = "Ashes Scenario Builder  LOADING!!!";

            mainMenu.previewWin.update(trig.toString());
          
            if (trig != null && mainMenu.mapScreen != null)
            {
                mainMenu.mapScreen.trig = trig;
                mainMenu.mapScreen.update();
                mainMenu.mapScreen.Activate();
            }
            mainMenu.Text = "Ashes Scenario Builder - " + mainMenu.scenName;
            nameBox.Text = trig.id;
            otherTrig.Text = trig.otherTrig;
            notOtherTrigBox.Text = trig.notOtherTrig;
            typeCB.Text = trig.type;
            //timerOwnerPlayerDiff.Value = trig.timerOwnerPlayerDiff.Value;
            //timerOwnerPlayerDiff.Value = trig.difficulty;
            inactiveCheck.Checked = trig.inactive;
            targetTxtBox.Text = trig.target;
            isBuildingCheck.Checked = trig.isBuilding;
            xNUD.Value = (decimal)trig.position.x;
            yNUD.Value = (decimal)trig.position.y;
            zNUD.Value = (decimal)trig.position.z;
            sizeNUD.Value = (decimal)trig.size;

            TargetCB.Visible = false;
            TargetCB.Enabled = false;

            targetTxtBox.Visible = true;

            if (trig.type.Equals("Timer"))
            {
                timerOwnerPlayerDiffLabel.Text = "Timer";
                timerOwnerPlayerDiff.Maximum = 999;
                timerOwnerPlayerDiff.Enabled = true;

                targetTxtBox.Enabled = false;
                isBuildingCheck.Enabled = false;

                centerLabel.Visible = false;
                xNUD.Enabled = false;
                yNUD.Enabled = false;
                
                sizeNUD.Enabled = false;

                inactiveCheck.Enabled = true;

                timerOwnerPlayerDiff.Value = trig.timer;

                timerOwnerPlayerDiffPanel.Visible = true;
                targetPanel.Visible = false;
                centerBox.Visible = false;
                sizePanel.Visible = false;
            }
            else if (trig.type.Equals("Area"))
            {
                centerLabel.Text = "Position";

                timerOwnerPlayerDiff.Enabled = false;
                targetTxtBox.Enabled = false;
                isBuildingCheck.Enabled = false;
                
                xNUD.Enabled = true;
                
                yNUD.Enabled = true;
                
                sizeNUD.Enabled = true;
                inactiveCheck.Enabled = false;

                timerOwnerPlayerDiffPanel.Visible = false;
                targetPanel.Visible = false;
                centerBox.Visible = true;
                sizePanel.Visible = true;
            }
            else if (trig.type.Equals("Destruction"))
            {

                timerOwnerPlayerDiff.Enabled = false;
                targetTxtBox.Enabled = true;
                isBuildingCheck.Enabled = true;
                xNUD.Enabled = false;
                yNUD.Enabled = false;
                sizeNUD.Enabled = false;
                inactiveCheck.Enabled = false;

                timerOwnerPlayerDiffPanel.Visible = false;
                targetPanel.Visible = true;
                centerBox.Visible = false;
                sizePanel.Visible = false;
            }
            else if (trig.type.Equals("Build"))
            {
                centerLabel.Text = "Center";
                timerOwnerPlayerDiff.Enabled = false;
                targetTxtBox.Enabled = true;
                isBuildingCheck.Enabled = false;
                xNUD.Enabled = true;
                yNUD.Enabled = true;
                sizeNUD.Enabled = true;
                inactiveCheck.Enabled = false;

                targetTxtBox.Enabled = false;
                targetTxtBox.Visible = false;
                TargetCB.Visible = true;
                TargetCB.Enabled = true;

                TargetCB.Items.Clear();
                string[] units = mainMenu.AAH.getBuildingAndUnitNames();
                for (int i = 0; i < units.Length; i++)
                {
                    if (units[i] != null)
                        TargetCB.Items.Add(units[i]);
                }

                timerOwnerPlayerDiffPanel.Visible = false;
                targetPanel.Visible = true;
                centerBox.Visible = true;
                sizePanel.Visible = true;

                TargetCB.Text = mainMenu.AAH.findAnyDisplayedName(trig.target);
            }
            else if (trig.type.Equals("NamedCreate"))
            {

                timerOwnerPlayerDiff.Enabled = false;
                targetTxtBox.Enabled = true;
                isBuildingCheck.Enabled = false;
                xNUD.Enabled = false;
                yNUD.Enabled = false;
                sizeNUD.Enabled = false;
                inactiveCheck.Enabled = false;

                timerOwnerPlayerDiffPanel.Visible = false;
                targetPanel.Visible = true;
                centerBox.Visible = false;
                sizePanel.Visible = false;
            }
            else if (trig.type.Equals("ZoneCapture"))
            {
                centerLabel.Text = "Position";
                timerOwnerPlayerDiff.Maximum = 7;
                timerOwnerPlayerDiffLabel.Text = "Owner";
                timerOwnerPlayerDiff.Enabled = true;
                targetTxtBox.Enabled = false;
                isBuildingCheck.Enabled = false;
                xNUD.Enabled = true;
                yNUD.Enabled = true;
                sizeNUD.Enabled = false;
                inactiveCheck.Enabled = false;
                timerOwnerPlayerDiff.Value = trig.owner;

                timerOwnerPlayerDiffPanel.Visible = true;
                targetPanel.Visible = false;
                centerBox.Visible = true;
                sizePanel.Visible = false;
            }
            else if (trig.type.Equals("Research"))
            {
                timerOwnerPlayerDiff.Maximum = 7;
                timerOwnerPlayerDiffLabel.Text = "Player";
                timerOwnerPlayerDiff.Enabled = true;
                targetTxtBox.Enabled = true;
                isBuildingCheck.Enabled = false;
                xNUD.Enabled = false;
                yNUD.Enabled = false;
                sizeNUD.Enabled = false;
                inactiveCheck.Enabled = false;
                timerOwnerPlayerDiff.Value = trig.player;

                targetTxtBox.Enabled = false;
                targetTxtBox.Visible = false;
                TargetCB.Visible = true;
                TargetCB.Enabled = true;

                TargetCB.Items.Clear();
                string[] researchNames = mainMenu.AAH.getResearchNames();
                for (int i = 0; i < researchNames.Length; i++)
                {
                    if (researchNames[i] != null)
                        TargetCB.Items.Add(researchNames[i]);
                }

                TargetCB.Text = mainMenu.AAH.getResearchDisplayedName(trig.target);

                timerOwnerPlayerDiffPanel.Visible = true;
                targetPanel.Visible = true;
                centerBox.Visible = false;
                sizePanel.Visible = false;
            }
            else if (trig.type.Equals("Difficulty"))
            {
                timerOwnerPlayerDiff.Maximum = 4;
                timerOwnerPlayerDiffLabel.Text = "Difficulty";
                timerOwnerPlayerDiff.Enabled = true;
                targetTxtBox.Enabled = false;
                isBuildingCheck.Enabled = false;
                xNUD.Enabled = false;
                yNUD.Enabled = false;
                sizeNUD.Enabled = false;
                inactiveCheck.Enabled = false;
                timerOwnerPlayerDiff.Value = trig.difficulty;

                timerOwnerPlayerDiffPanel.Visible = true;
                targetPanel.Visible = false;
                centerBox.Visible = false;
                sizePanel.Visible = false;
            }
            EndMissionString.Items.Clear();
            ObjectiveString.Items.Clear();
            if (mainMenu.csvData != null && mainMenu.csvData.entries != null)
            {
                for (int i = 0; i < mainMenu.csvData.entries.Length; i++)
                {
                    if (mainMenu.csvData.entries[i] != null && mainMenu.csvData.entries[i].key != null)
                    {
                        EndMissionString.Items.Add(mainMenu.csvData.entries[i].key);
                        ObjectiveString.Items.Add(mainMenu.csvData.entries[i].key);
                    }
                    
                }
            }
            
            int y = ActionSelectPanel.Top + 10;
            int x = ActionSelectPanel.Left + 5;

            if (aM != null)
            {
                for (int i = 0; i < aM.Length; i++)
                {
                    if (aM[i] != null)
                        aM[i].Dispose();
                    //aM[i].destroy();
                }
            }
            if (trig.actions != null)
            {
                //aM = new ActionMini[trig.actions.Length];
                aM = new ActionMenuOption[trig.actions.Length];
                for (int i = 0; i < aM.Length; i++)
                {
                    /*aM[i] = new ActionMini(trig.actions[i], this);
                    aM[i].pan.Location = new Point(x, y);
                    ActionSelectPanel.Controls.Add(aM[i].pan);
                    y += (aM[i].pan.Height + 5);*/
                    aM[i] = new ActionMenuOption(trig.actions[i], this);
                    aM[i].Location = new Point(x, y);
                    ActionSelectPanel.Controls.Add(aM[i]);
                    y += aM[i].Height + 5;
                }
            }
            Text = "Ashes Scenario Builder - " + trig.id;
        }
        /// <summary>
        /// Changes the selected action to the specified one
        /// </summary>
        /// <param name="act">The action to be selected</param>
        public void setSelectedAction(Action act)
        {
            updateUI();
            if (selectedAction != null)
            {
                CameraPanel.Visible = false;
                ToggleAIPanel.Visible = false;
                ActivateTriggerPanel.Visible = false;
                RevealPanel.Visible = false;
                NotificationsPanel.Visible = false;
                AreaIndicatorPanel.Visible = false;
                DestroyUnitPanel.Visible = false;
                PlaySoundPanel.Visible = false;
                ObjectivePanel.Visible = false;
                RestrictPanel.Visible = false;
                EndMissionPanel.Visible = false;
                SpawnUnitPanel.Visible = false;
                DestroyBuildingPanel.Visible = false;
                ChangeAIDifficultyPanel.Visible = false;
                AttackAttackMovePanel.Visible = false;
                SpawnBuildingPanel.Visible = false;
                PausePanel.Visible = false;
                LetterBoxPanel.Visible = false;
                HidePanelPanel.Visible = false;
                GrantStuffPanel.Visible = false;
                GrantTechPanel.Visible = false;
                CaptureNearestPanel.Visible = false;
                SelectPanel.Visible = false;
                HighlightPanel.Visible = false;
                ActivateAIPanel.Visible = false;
                ChangeAIPersonalityPanel.Visible = false;

                DuplicateButton.Visible = false;
            }
            selectedAction = act;
            if (selectedAction == null)
            {
                return;
            }

            DuplicateButton.Visible = true;
            if (selectedAction.GetType() == typeof(Camera))
            {
                CameraPanel.Visible = true;
                if (selectedAction.getBoolD()&&!selectedAction.getBoolA()&&!selectedAction.getBoolB())
                {
                    CameraPositionCheck.Checked = true;
                    CameraPosX.Enabled = true;
                    CameraPosY.Enabled = true;
                    CameraPosZ.Enabled = true;
                }
                else
                {
                    CameraPositionCheck.Checked = false;
                    CameraPosX.Enabled = false;
                    CameraPosY.Enabled = false;
                    CameraPosZ.Enabled = false;
                }
                CameraPosX.Value = (decimal)selectedAction.getPositionAX();
                CameraPosY.Value = (decimal)selectedAction.getPositionAY();
                CameraPosZ.Value = (decimal)selectedAction.getPositionAZ();

                if (selectedAction.getBoolC() && !selectedAction.getBoolA() && !selectedAction.getBoolB())
                {
                    CameraRTPCheck.Checked = true;
                    CameraRTPX.Enabled = true;
                    CameraRTPY.Enabled = true;
                    CameraRTPZ.Enabled = true;
                }
                else
                {
                    CameraRTPCheck.Checked = false;
                    CameraRTPX.Enabled = false;
                    CameraRTPY.Enabled = false;
                    CameraRTPZ.Enabled = false;
                }
                CameraRTPX.Value = (decimal)selectedAction.getPositionBX();
                CameraRTPY.Value = (decimal)selectedAction.getPositionBY();
                CameraRTPZ.Value = (decimal)selectedAction.getPositionBZ();

                CameraSpeed.Value = (decimal)selectedAction.getFloatA();
                CameraSave.Checked = selectedAction.getBoolA();
                CameraLoad.Checked = selectedAction.getBoolB();

                CameraSnap.Checked = selectedAction.getBoolE();


            }
            if (selectedAction.GetType() == typeof(Reveal))
            {
                RevealPanel.Visible = true;
                RevealName.Text = selectedAction.getStringA();
                RevealPosX.Value = (decimal)selectedAction.getPositionAX();
                RevealPosY.Value = (decimal)selectedAction.getPositionAY();
                RevealSize.Value = (decimal)selectedAction.getFloatA();
                RevealRadarSize.Value = (decimal)selectedAction.getFloatB();
                RevealEnable.Checked = selectedAction.getBoolA();
            }
            if (selectedAction.GetType() == typeof(AreaIndicator))
            {
                AreaIndicatorPanel.Visible = true;
                AreaIndicatorName.Text = selectedAction.getStringA();
                AreaIndicatorColor.Text = selectedAction.getStringB();
                AreaIndicatorPosX.Value=(decimal)selectedAction.getPositionAX();
                AreaIndicatorPosY.Value = (decimal)selectedAction.getPositionAY();
                AreaIndicatorSize.Value = (decimal)selectedAction.getFloatA();
                AreaIndicatorOn.Checked = selectedAction.getBoolA();
            }
            if (selectedAction.GetType() == typeof(Objective))
            {
                ObjectivePanel.Visible = true;
                ObjectiveName.Text = selectedAction.getStringA();
                ObjectiveString.Text = selectedAction.getStringB();
                ObjectiveSetCheck.Checked = selectedAction.getBoolA();
            }
            if (selectedAction.GetType() == typeof(Restrict))
            {
                RestrictPanel.Visible = true;
                RestrictType.Text = selectedAction.getStringA();
                string displayedName = mainMenu.AAH.findAnyDisplayedName(selectedAction.getStringB());
                if (displayedName != "")
                {
                    RestrictID.Text = displayedName;
                }
                else
                RestrictID.Text = selectedAction.getStringB();
                RestrictEnable.Checked = selectedAction.getBoolA();
            }
            if (selectedAction.GetType() == typeof(SpawnUnit))
            {
                SpawnUnitPanel.Visible = true;
                SpawnUnitName.Text = selectedAction.getStringA();
                SpawnUnitTemplate.Text = mainMenu.AAH.getUnitDisplayedName(selectedAction.getStringB());
                SpawnUnitParent.Text = selectedAction.getStringC();
                SpawnUnitPlayer.Value = (decimal)selectedAction.getIntA();
                SpawnUnitX.Value = (decimal)selectedAction.getPositionAX();
                SpawnUnitY.Value = (decimal)selectedAction.getPositionAY();
                SpawnUnitNoDeath.Checked = selectedAction.getBoolA();
                string[] spawnUnitNames = trig.getSpawnUnitNames();
                SpawnUnitParent.Items.Clear();
                for (int i = 0; i < spawnUnitNames.Length; i++)
                {
                    if (spawnUnitNames[i]!=null)
                    SpawnUnitParent.Items.Add(spawnUnitNames[i]);
                }
            }
            if (selectedAction.GetType() == typeof(SpawnBuilding))
            {
                SpawnBuildingPanel.Visible = true;
                SpawnBuildingName.Text = selectedAction.getStringA();
                SpawnBuildingTemplate.Text = mainMenu.AAH.getBuildingDisplayedName(selectedAction.getStringB());
                SpawnBuildingPlayer.Value = (decimal)selectedAction.getIntA();
                SpawnBuildingX.Value = (decimal)selectedAction.getPositionAX();
                SpawnBuildingY.Value = (decimal)selectedAction.getPositionAY();
            }
            if (selectedAction.GetType() == typeof(DestroyUnit) )
            {
                DestroyUnitPanel.Visible = true;
                DestroyUnitName.Text = selectedAction.getStringA();
                DestroyUnitTime.Value = (decimal)selectedAction.getIntA();
                //TODO: make the name a dropdown that displays the PREVIOUSLY SPAWNED, UNDESTROYED units
            }
            if (selectedAction.GetType() == typeof(DestroyBuilding))
            {
                DestroyBuildingPanel.Visible = true;
                DestroyBuildingName.Text = selectedAction.getStringA();
                DestroyBuildingTime.Value = (decimal)selectedAction.getIntA();
            }
            if (selectedAction.GetType() == typeof(AttackAttackMove))
            {
                AttackAttackMovePanel.Visible = true;
                AttackAttackName.Text = selectedAction.getStringA();
                AttackAttackMoveX.Value = (decimal)selectedAction.getPositionAX();
                AttackAttackMoveY.Value = (decimal)selectedAction.getPositionAY();
                //TODO: make the name a dropdown that displays the PREVIOUSLY SPAWNED, UNDESTROYED units
            }
            if (selectedAction.GetType() == typeof(ActivateTrigger))
            {
                ActivateTriggerPanel.Visible = true;
                ActivateTriggerTarget.Text = selectedAction.getStringA();
            }
            if (selectedAction.GetType() == typeof(LetterBox))
            {
                LetterBoxPanel.Visible = true;
                LetterBoxEnable.Checked = selectedAction.getBoolA();
                LetterBoxSnap.Checked = selectedAction.getBoolB();
            }
            if (selectedAction.GetType() == typeof(HidePanel))
            {
                HidePanelPanel.Visible = true;
                HidePlayer.Checked = selectedAction.getBoolA();
                HideRank.Checked = selectedAction.getBoolB();
                HideResource.Checked = selectedAction.getBoolC();
            }
            if (selectedAction.GetType() == typeof(GrantStuff))
            {
                GrantStuffPanel.Visible = true;
                GrantStuffPlayerCheck.Checked = selectedAction.getBoolA();
                GrantStuffPlayer.Value = (decimal)selectedAction.getIntA();
                if (!GrantStuffPlayerCheck.Checked)
                {
                    GrantStuffPlayer.Enabled = false;
                }
                GrantStuffQuanta.Value = (decimal)selectedAction.getIntB();
                GrantStuffMetal.Value = (decimal)selectedAction.getIntC();
                GrantStuffRadioactives.Value = (decimal)selectedAction.getIntD();
                GrantStuffLogistics.Value = (decimal)selectedAction.getIntE();
                GrantStuffTech.Value = (decimal)selectedAction.getIntF();
            }
            if (selectedAction.GetType() == typeof(GrantTech))
            {
                GrantTechPanel.Visible = true;
                GrantTechX.Value = (decimal)selectedAction.getIntB();
                GrantTechPlayer.Value = (decimal)selectedAction.getIntA();
                GrantTechTech.Text = selectedAction.getStringA();
            }
            if (selectedAction.GetType() == typeof(CaptureNearest))
            {
                CaptureNearestPanel.Visible = true;
                CaptureNearestName.Text = selectedAction.getStringA();
                CaptureNearestMin.Value = (decimal)selectedAction.getIntA();
                CaptureNearestMax.Value = (decimal)selectedAction.getIntB();
                CaptureNearestRepeat.Checked = selectedAction.getBoolA();
            }
            if (selectedAction.GetType() == typeof(ToggleAI))
            {
                ToggleAIPanel.Visible = true;
                ToggleAIDisableAI.Checked = selectedAction.getBoolA();
            }
            if (selectedAction.GetType() == typeof(EndMission))
            {
                EndMissionPanel.Visible = true;
                EndMissionVictory.Checked = selectedAction.getBoolA();
                EndMissionString.Text = selectedAction.getStringA();
                EndMissionTranslatedString.Text = mainMenu.csvData.getTranslatedString(EndMissionString.Text);
            }
            if (selectedAction.GetType() == typeof(Select))
            {
                SelectPanel.Visible = true;
                SelectTarget.Text = selectedAction.getStringA();
            }
            if (selectedAction.GetType() == typeof(Notifications))
            {
                NotificationsPanel.Visible = true;
                NotificationsEnable.Checked = selectedAction.getBoolA();
            }
            if (selectedAction.GetType() == typeof(Highlight))
            {
                HighlightPanel.Visible = true;
                HighlightName.Text = selectedAction.getStringA();
                HighlightButton.Text = selectedAction.getStringB();
                HighlightEnable.Checked = selectedAction.getBoolA();
                HighlightButtonCheck.Checked = selectedAction.getBoolB();
                if (HighlightButtonCheck.Checked)
                {
                    HighlightButton.Enabled = true;
                }
                else
                {
                    HighlightButton.Enabled = false;
                }
            }
            if (selectedAction.GetType() == typeof(ActivateAI))
            {
                ActivateAIPanel.Visible = true;
                ActivateAIName.Text = selectedAction.getStringA();
                ActivateAIPlayer.Value = (int)selectedAction.getIntA();
            }
            if (selectedAction.GetType() == typeof(PlaySound))
            {
                PlaySoundPanel.Visible = true;
                PlaySoundSound.Text = selectedAction.getStringA();
            }
            if (selectedAction.GetType() == typeof(Pause))
            {
                PausePanel.Visible = true;
                PauseEnable.Checked = selectedAction.getBoolA();
            }
            if (selectedAction.GetType() == typeof(ChangeAIDifficulty))
            {
                ChangeAIDifficultyPanel.Visible = true;
                ChangeAIDifficultyPlayer.Value = (decimal)selectedAction.getIntA();
                ChangeAIDifficultyDifficulty.Text = selectedAction.getStringA();
            }
            if (selectedAction.GetType() == typeof(ChangeAIPersonality))
            {
               ChangeAIPersonalityPanel.Visible=true;
               ChangeAIPersonalityName.Text = selectedAction.getStringA();
               ChangeAIPersonalityPlayer.Value = (decimal)selectedAction.getIntA();
            }
            
        }

        //Trigger Config UI
        #region
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void addActionButton_Click(object sender, EventArgs e)
        {
            if (actionSelect.Text.Equals("ActivateTrigger"))
            {
                trig.addAction(new ActivateTrigger(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("Camera"))
            {
                trig.addAction(new Camera(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("Reveal"))
            {
                trig.addAction(new Reveal(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("AreaIndicator"))
            {
                trig.addAction(new AreaIndicator(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("Objective"))
            {
                trig.addAction(new Objective(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("Restrict"))
            {
                trig.addAction(new Restrict(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("SpawnUnit"))
            {
                trig.addAction(new SpawnUnit(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("DestroyUnit"))
            {
                trig.addAction(new DestroyUnit(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("AttackAttackMove"))
            {
                trig.addAction(new AttackAttackMove(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("SpawnBuilding"))
            {
                trig.addAction(new SpawnBuilding(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("DestroyBuilding"))
            {
                trig.addAction(new DestroyBuilding(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("LetterBox"))
            {
                trig.addAction(new LetterBox(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("HidePanel"))
            {
                trig.addAction(new HidePanel(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("GrantStuff"))
            {
                trig.addAction(new GrantStuff(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("GrantTech"))
            {
                trig.addAction(new GrantTech(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("CaptureNearest"))
            {
                trig.addAction(new CaptureNearest(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("ToggleAI"))
            {
                trig.addAction(new ToggleAI(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("EndMission"))
            {
                trig.addAction(new EndMission(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("Select"))
            {
                trig.addAction(new Select(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("Notifications"))
            {
                trig.addAction(new Notifications(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("Highlight"))
            {
                trig.addAction(new Highlight(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("ActivateAI"))
            {
                trig.addAction(new ActivateAI(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("PlaySound"))
            {
                trig.addAction(new PlaySound(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("Pause"))
            {
                trig.addAction(new Pause(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("ChangeAIPersonality"))
            {
                trig.addAction(new ChangeAIPersonality(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("ChangeAIDifficulty"))
            {
                trig.addAction(new ChangeAIDifficulty(trig));
                updateUI();
                return;
            }
            if (actionSelect.Text.Equals("Dialog"))
            {
                trig.addAction(new Dialog(trig));
                updateUI();
                return;
            }
        }

        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            if(!nameBox.Text.Equals(""))
            trig.id = nameBox.Text;
        }

        private void otherTrig_TextChanged(object sender, EventArgs e)
        {
            
                trig.otherTrig = otherTrig.Text;
            
        }

        private void notOtherTrigBox_TextChanged(object sender, EventArgs e)
        {
            
                trig.notOtherTrig = notOtherTrigBox.Text;
            
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void targetTxtBox_TextChanged(object sender, EventArgs e)
        {
            trig.target = targetTxtBox.Text;
        }
        private void TargetCB_TextChanged(object sender, EventArgs e)
        {
            trig.target = mainMenu.AAH.findAnyInternalName(TargetCB.Text);
        }
        private void sizeNUD_ValueChanged(object sender, EventArgs e)
        {
            trig.size = (int)sizeNUD.Value;
        }
        private void xNUD_ValueChanged(object sender, EventArgs e)
        {
            trig.position.x = (float)xNUD.Value;
        }

        private void yNUD_ValueChanged(object sender, EventArgs e)
        {
            trig.position.y = (float)yNUD.Value;
        }

        private void isBuildingCheck_CheckedChanged(object sender, EventArgs e)
        {
            trig.isBuilding = isBuildingCheck.Checked;
        }

        private void inactiveCheck_CheckedChanged(object sender, EventArgs e)
        {
            trig.inactive = inactiveCheck.Checked;
        }

        private void timerOwnerPlayerDiff_ValueChanged(object sender, EventArgs e)
        {
            trig.timer = trig.owner = trig.player = trig.difficulty = (int)timerOwnerPlayerDiff.Value;
        }
        #endregion

        //Action config panels
        #region

        //CameraPanel
        #region
        private void CameraPositionCheck_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolD(CameraPositionCheck.Checked);
            if (CameraPositionCheck.Checked)
            {
                CameraPosX.Enabled = true;
                CameraPosY.Enabled = true;
                CameraPosZ.Enabled = true;
                CameraSave.Checked = false;
                CameraLoad.Checked = false;
                selectedAction.setBoolA(false);//save
                selectedAction.setBoolB(false);//load
            }
            else
            {
                CameraPosX.Enabled = false;
                CameraPosY.Enabled = false;
                CameraPosZ.Enabled = false;
            }
        }

        private void CameraPosX_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAX((float)CameraPosX.Value);
        }

        private void CameraPosY_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAY((float)CameraPosY.Value);
        }

        private void CameraPosZ_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAZ((float)CameraPosZ.Value);
        }

        private void CameraRTPCheck_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolC(CameraPositionCheck.Checked);
            if (CameraRTPCheck.Checked)
            {
                CameraRTPX.Enabled = true;
                CameraRTPY.Enabled = true;
                CameraRTPZ.Enabled = true;
                CameraSave.Checked = false;
                CameraLoad.Checked = false;
                selectedAction.setBoolA(false);//save
                selectedAction.setBoolB(false);//load
            }
            else
            {
                CameraRTPX.Enabled = false;
                CameraRTPY.Enabled = false;
                CameraRTPZ.Enabled = false;
            }
        }
        

        private void CameraRTPX_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionBX((float)CameraRTPX.Value);
        }

        private void CameraRTPY_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionBY((float)CameraRTPY.Value);
        }

        private void CameraRTPZ_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionBZ((float)CameraRTPZ.Value);
        }

        private void CameraSpeed_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setFloatA((float)CameraSpeed.Value);
        }

        private void CameraSave_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(CameraSave.Checked);

            if(CameraSave.Checked){
                CameraPositionCheck.Checked = false;
                CameraPosX.Enabled = false;
                CameraPosY.Enabled = false;
                CameraPosZ.Enabled = false;

                CameraRTPCheck.Checked = false;
                CameraRTPX.Enabled = false;
                CameraRTPY.Enabled = false;
                CameraRTPZ.Enabled = false;

                CameraLoad.Checked = false;
                selectedAction.setBoolB(false);
                selectedAction.setBoolC(false);
                selectedAction.setBoolD(false);
            }

        }

        private void CameraLoad_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolB(CameraSave.Checked);
            //TODO: make Load checking less finicky
            if (CameraLoad.Checked)
            {
                CameraPositionCheck.Checked = false;
                CameraPosX.Enabled = false;
                CameraPosY.Enabled = false;
                CameraPosZ.Enabled = false;

                CameraRTPCheck.Checked = false;
                CameraRTPX.Enabled = false;
                CameraRTPY.Enabled = false;
                CameraRTPZ.Enabled = false;

                CameraSave.Checked = false;

                selectedAction.setBoolA(false);
                selectedAction.setBoolC(false);
                selectedAction.setBoolD(false);
            }
        }

        private void CameraSnap_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolE(CameraSnap.Checked);
        }

        private void CameraDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //ToggleAIPanel
        #region
        private void ToggleAIDisableAI_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(ToggleAIDisableAI.Checked);
        }

        private void ToggleAIDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //ActivateTriggerPanel
        #region
        private void ActivateTriggerTarget_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(ActivateTriggerTarget.Text);
        }

        private void ActivateTriggerDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //RevealPanel
        #region
        private void RevealName_TextChanged(object sender, EventArgs e)
        {
            if (!RevealName.Text.Equals(""))
            selectedAction.setStringA(RevealName.Text);
        }

        private void RevealPosX_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAX((float)RevealPosX.Value);
        }

        private void RevealPosY_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAY((float)RevealPosY.Value);
        }

        private void RevealSize_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setFloatA((float)RevealSize.Value);
        }

        private void RevealRadarSize_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setFloatB((float)RevealRadarSize.Value);
        }

        private void RevealEnable_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(RevealEnable.Checked);
        }

        private void RevealDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //NotificationsPanel
        #region
        private void NotificationsEnable_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(NotificationsEnable.Checked);
        }

        private void NotificationsDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion


        //AreaIndicatorPanel
        #region
        private void AreaIndicatorName_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(AreaIndicatorName.Text);
        }

        private void AreaIndicatorColor_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringB(AreaIndicatorColor.Text);
        }

        private void AreaIndicatorPosX_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAX((float)AreaIndicatorPosX.Value);
        }

        private void AreaIndicatorPosY_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAY((float)AreaIndicatorPosY.Value);
        }

        private void AreaIndicatorSize_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setFloatA((float)AreaIndicatorSize.Value);
        }

        private void AreaIndicatorOn_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(AreaIndicatorOn.Checked);
        }

        private void AreaIndicatorDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //DestroyUnitPanel
        #region
        private void DestroyUnitName_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(DestroyUnitName.Text);
        }

        private void DestroyUnitTime_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntA((int)DestroyUnitTime.Value);
        }

        private void DestroyUnitDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion


        //PlaySoundPanel
        #region
        private void PlaySoundSound_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(PlaySoundSound.Text);
        }

        private void PlaySoundDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //ObjectivePanel
        #region
        private void ObjectiveName_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(ObjectiveName.Text);
        }

        private void ObjectiveString_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringB(ObjectiveString.Text);
            ObjectiveTranslatedString.Text = mainMenu.csvData.getTranslatedString(ObjectiveString.Text);
        }

        private void ObjectiveSetCheck_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(ObjectiveSetCheck.Checked);
        }

        private void ObjectiveDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        private void ObjectiveTranslatedString_TextChanged(object sender, EventArgs e)
        {
            mainMenu.csvData.overwriteEntry(ObjectiveString.Text, ObjectiveTranslatedString.Text);
            updateUI();
        }
        #endregion

        //RestrictPanel
        #region
        private void RestrictType_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(RestrictType.Text);

            RestrictID.Items.Clear();

            if (RestrictType.Text.Equals("Unit"))
            {
                string[] unitNames = mainMenu.AAH.getUnitNames();
                for (int i = 0; i < unitNames.Length; i++)
                {
                    if (unitNames[i] != null)
                        RestrictID.Items.Add(unitNames[i]);
                }
            }
            else if(RestrictType.Text.Equals("Building"))
            {
                string[] buildingNames = mainMenu.AAH.getBuildingNames();
                 for (int i = 0; i < buildingNames.Length; i++)
                 {
                     if (buildingNames[i] != null)
                           RestrictID.Items.Add(buildingNames[i]);
                 }
            }
            else if (RestrictType.Text.Equals("Research"))
            {
                string[] researchNames = mainMenu.AAH.getResearchNames();
                for (int i = 0; i < researchNames.Length; i++)
                {
                    if (researchNames[i] != null)
                        RestrictID.Items.Add(researchNames[i]);
                }
            }
            else{
                string[] orbitalNames = mainMenu.AAH.getOrbitalNames();
                for (int i = 0; i < orbitalNames.Length; i++)
                {
                    if (orbitalNames[i] != null)
                        RestrictID.Items.Add(orbitalNames[i]);
                }
            }

            
        }

        private void RestrictID_TextChanged(object sender, EventArgs e)
        {
            string internalName= mainMenu.AAH.findAnyInternalName(RestrictID.Text);
            if (internalName != "")
            {
                selectedAction.setStringB(internalName);
            }
            else
            selectedAction.setStringB(RestrictID.Text);
        }

        private void RestrictEnable_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(RestrictEnable.Checked);
        }

        private void RestrictDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //EndMissionPanel
        #region
        private void EndMissionVictory_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(EndMissionVictory.Checked);
        }

        private void EndMissionString_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(EndMissionString.Text);
            EndMissionTranslatedString.Text = mainMenu.csvData.getTranslatedString(EndMissionString.Text);
        }

        private void EndMissionDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        private void EndMissionTranslatedString_TextChanged(object sender, EventArgs e)
        {
            mainMenu.csvData.overwriteEntry(EndMissionString.Text, EndMissionTranslatedString.Text);
            updateUI();
        }
        #endregion

        //SpawnUnitPanel
        #region
        private void SpawnUnitName_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(SpawnUnitName.Text);
        }

        private void SpawnUnitTemplate_TextChanged(object sender, EventArgs e)
        {
            //selectedAction.setStringB(SpawnUnitTemplate.Text);
            selectedAction.setStringB(mainMenu.AAH.getUnitTemplateName(SpawnUnitTemplate.Text));
        }
        private void SpawnUnitParent_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringC(SpawnUnitParent.Text);
        }

        private void SpawnUnitPlayer_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntA((int)SpawnUnitPlayer.Value);
        }

        private void SpawnUnitX_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAX((float)SpawnUnitX.Value);
        }

        private void SpawnUnitY_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAY((float)SpawnUnitY.Value);
        }

        private void SpawnUnitNoDeath_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(SpawnUnitNoDeath.Checked);
        }

        private void SpawnUnitDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //DestroyBuildingPanel
        #region
        private void DestroyBuildingName_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(DestroyBuildingName.Text);
        }

        private void DestroyBuildingTime_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntA((int)DestroyBuildingTime.Value);
        }

        private void DestroyBuildingDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //ChangeAIDifficultyPanel
        #region
        private void ChangeAIDifficultyDifficulty_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(ChangeAIDifficultyDifficulty.Text);
        }

        private void ChangeAIDifficultyPlayer_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntA((int)ChangeAIDifficultyPlayer.Value);
        }

        private void ChangeAIDifficultyDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //AttackAttackMovePanel
        #region
        private void AttackAttackName_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(AttackAttackName.Text);
        }

        private void AttackAttackMoveX_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAX((float)AttackAttackMoveX.Value);
        }

        private void AttackAttackMoveY_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAY((float)AttackAttackMoveY.Value);
        }

        private void AttackAttackMoveDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //SpawnBuildingPanel
        #region
        private void SpawnBuildingName_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(SpawnBuildingName.Text);
        }

        private void SpawnBuildingTemplate_TextChanged(object sender, EventArgs e)
        {
            //selectedAction.setStringB(SpawnBuildingTemplate.Text);
            selectedAction.setStringB(mainMenu.AAH.getBuildingTemplateName(SpawnBuildingTemplate.Text));
        }

        private void SpawnBuildingPlayer_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntA((int)SpawnBuildingPlayer.Value);
        }

        private void SpawnBuildingX_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAX((float)SpawnBuildingX.Value);
        }

        private void SpawnBuildingY_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setPositionAY((float)SpawnBuildingY.Value);
        }

        private void SpawnBuildingDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }

        #endregion

        //PausePanel
        #region
        private void PauseEnable_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(PauseEnable.Checked);
        }

        private void PauseDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //LetterBoxPanel
        #region
        private void LetterBoxEnable_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(LetterBoxEnable.Checked);
        }

        private void LetterBoxSnap_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolB(LetterBoxSnap.Checked);
        }

        private void LetterBoxDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }

        #endregion

        //HidePanelPanel
        #region
        private void HidePlayer_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(HidePlayer.Checked);
        }

        private void HideRank_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolB(HideRank.Checked);
        }

        private void HideResource_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolC(HideResource.Checked);
        }

        private void HidePanelDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }

        #endregion

        //GrantStuffPanel
        #region
        private void GrantStuffPlayerCheck_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(GrantStuffPlayerCheck.Checked);
            if (GrantStuffPlayerCheck.Checked)
            {
                GrantStuffPlayer.Enabled = true;
            }
            else
            {
                GrantStuffPlayer.Enabled = false;
            }
        }

        private void GrantStuffPlayer_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntA((int)GrantStuffPlayer.Value);
        }

        private void GrantStuffQuanta_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntB((int)GrantStuffQuanta.Value);
        }

        private void GrantStuffMetal_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntC((int)GrantStuffMetal.Value);
        }

        private void GrantStuffRadioactives_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntD((int)GrantStuffRadioactives.Value);
        }

        private void GrantStuffLogistics_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntE((int)GrantStuffLogistics.Value);
        }

        private void GrantStuffTech_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntF((int)GrantStuffTech.Value);
        }

        private void GrantStuffDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //GrantTechPanel
        #region
        private void GrantTechX_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntB((int)GrantTechX.Value);
        }

        private void GrantTechPlayer_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntA((int)GrantTechPlayer.Value);
        }

        private void GrantTechTech_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(GrantTechTech.Text);
        }

        private void GrantTechDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }

        #endregion

        //CaptureNearestPanel
        #region
        private void CaptureNearestName_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(CaptureNearestName.Text);
        }

        private void CaptureNearestMin_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntA((int)CaptureNearestMin.Value);
        }

        private void CaptureNearestMax_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntB((int)CaptureNearestMax.Value);
        }

        private void CaptureNearestRepeat_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(CaptureNearestRepeat.Checked);
        }

        private void CaptureNearestDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //SelectPanel
        #region
        private void SelectTarget_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(SelectTarget.Text);
        }

        private void SelectDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //HighlightPanel
        #region
        private void HighlightName_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(HighlightName.Text);
        }

        private void HighlightButtonCheck_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolB(HighlightButtonCheck.Checked);
            if (HighlightButtonCheck.Checked)
            {
                HighlightButton.Enabled = true;
            }
            else
            {
                HighlightButton.Enabled = false;
            }
        }

        private void HighlightButton_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringB(HighlightButton.Text);
        }

        private void HighlightEnable_CheckedChanged(object sender, EventArgs e)
        {
            selectedAction.setBoolA(HighlightEnable.Checked);
        }

        private void HighlightDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion


        //ActivateAIPanel
        #region
        private void ActivateAIName_TextChanged(object sender, EventArgs e)
        {
            selectedAction.setStringA(ActivateAIName.Text);
        }

        private void ActivateAIPlayer_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntA((int)ActivateAIPlayer.Value);
        }

        private void ActivateAIDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        //ChangeAIPersonalityPanel
        #region
        private void ChangeAIPersonalityName_TextChanged(object sender, EventArgs e)
        {
            /*string temp = "";
            if (ChangeAIPersonalityName.Text.Contains("(PHC)"))
            {
                char[] temp2 = ChangeAIPersonalityName.Text.ToCharArray();
                for (int i = 0; i < ChangeAIPersonalityName.Text.Length - 5; i++)
                {
                    temp += temp2[i];
                }
            }
            else if (ChangeAIPersonalityName.Text.Contains("(SS)"))
            {
                char[] temp2 = ChangeAIPersonalityName.Text.ToCharArray();
                for (int i = 0; i < ChangeAIPersonalityName.Text.Length - 4; i++)
                {
                    temp += temp2[i];
                }
            }
            else
            {
                temp = ChangeAIPersonalityName.Text;
            }*/
            string[] temp = ChangeAIPersonalityName.Text.Split('(');
            selectedAction.setStringA(temp[0]);
        }

        private void ChangeAIPersonalityPlayer_ValueChanged(object sender, EventArgs e)
        {
            selectedAction.setIntA((int)ChangeAIPersonalityPlayer.Value);
        }

        private void ChangeAIPersonalityDelete_Click(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }
        #endregion

        #endregion

        private void DuplicateButton_Click(object sender, EventArgs e)
        {
            //TODO: Make cloning work with Action.clone()
            //trig.addAction(selectedAction.clone());
            trig.addAction(selectedAction);
            updateUI();
        }

        private void ObjectiveString_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CameraDelete_Click_1(object sender, EventArgs e)
        {
            selectedAction.trig.removeAction(selectedAction.index);
            setSelectedAction(null);
            updateUI();
        }

        private void typeCB_TextChanged(object sender, EventArgs e)
        {
            trig.type = typeCB.Text;
            updateUI();
        }
    }
        
}

