using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DirectXTexNet;

using Stardock.NitrousTool.Utilities;


namespace AshesScenarioBuilder1
{
    /// <summary>
    /// The main window for the Ashes of the Singularity Scenario Editor. If closed, the program is terminated
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// The scenario being edited
        /// </summary>
        public Scenario scen1;
        /// <summary>
        /// The datatable storing the scenario's UI text
        /// </summary>
        public CSVDataSheet csvData= new CSVDataSheet();
        private Button btnAdd = new Button();
        private TextBox txtBox = new TextBox();
        public Map mapScreen;
        /// <summary>
        /// Defunct loading animation
        /// </summary>
        LoadWindow loadingScreen;
        /// <summary>
        /// GUI elements for the Scenario's Triggers
        /// </summary>
        //TriggerMini[] tM;
        TriggerMenuOption[] tM;
        /// <summary>
        /// GUI elements for the Scenario's players
        /// </summary>
        //PlayerCard[] pC;
        PlayerMenuOption[] pC;
        /// <summary>
        /// The player being edited in the player tab
        /// </summary>
        public Player selectedPlayer;
        public string lastSelectedTrigger;
        bool setupOpen, playersOpen, triggersOpen;
        /// <summary>
        /// Default names for the scenario's name
        /// </summary>
        public string scenName = "untitled", csvName = "untitled", directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), csvDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public UserSettingsManager USM;
        public AshesAssetHandler AAH;
        public TriggerXMLPreview previewWin;

        //DEBUG
        public DebugForm debugConsole;

        DdsImage imageDDS;
        /// <summary>
        /// Initializes the main window
        /// </summary>
        public Form1()
        {

            InitializeComponent();


        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            previewWin = new TriggerXMLPreview();
            //debugConsole = new DebugForm(this);//DEBUG
            //debugConsole.Show();
           // mapScreen = new Map(this);
           // mapScreen.Show();
            //previewWin.Show();
            USM = UserSettingsManager.readXML();
            AAH = new AshesAssetHandler(USM);
            loadingScreen= new LoadWindow();
            //loadingScreen.Show();
            //Format controls. Note: Controls inherit color from parent form.
            setupOpen = true;
            playersOpen = triggersOpen = false;
            MetaPlayerPanel.Visible = false;
            MetaPlayerPanel.Top = scenAttributesPanel.Top;
            TriggerPanel.Visible = false;
            TriggerPanel.Top = MetaPlayerPanel.Top;
            this.btnAdd.BackColor = Color.Gray;
            this.btnAdd.Text = "Add";
            this.btnAdd.Location = new System.Drawing.Point(90, 25);
            this.btnAdd.Size = new System.Drawing.Size(50, 25);
            this.txtBox.Text = "Text";
            this.txtBox.Location = new System.Drawing.Point(10, 25);
            this.txtBox.Size = new System.Drawing.Size(70, 20);
            scen1 = new Scenario(this);
            csvData = new CSVDataSheet();
            updateControlsFromScenario(scen1);
            removePlayerButton.Enabled = false;
            readInXMLDirectory();
            readInCSVDirectory();
            pathLabel.Text="Path  "+directory+@"\";
            csvPathLabel.Text = "Path  " + csvDirectory + @"\";
            //csvData.DataChanged += csvData_DataChanged;
            refreshCSVList();
            adjustLoadControls();
            //MetaPlayerPanel.Dock = DockStyle.Right;
            loadingScreen.Visible = false;
        }

        /// <summary>
        /// Moves load controls to accomodate their changes in width
        /// </summary>
        void adjustLoadControls()
        {
            scenNameCB.Left = pathLabel.Left + pathLabel.Width + 5;
            labelXML.Left = scenNameCB.Left + scenNameCB.Width + 5;
            SaveButton1.Left = labelXML.Left + labelXML.Width + 5;
            LoadButton1.Left = SaveButton1.Left + SaveButton1.Width + 5;

            loadCSVBox.Left = csvPathLabel.Left + csvPathLabel.Width + 5;
            labelCSV.Left = loadCSVBox.Left + loadCSVBox.Width + 5;
            csvSaveButton.Left = labelCSV.Left + labelCSV.Width + 3;
            csvLoadButton.Left = csvSaveButton.Left + csvSaveButton.Width + 5;
        }

        void csvData_DataChanged(object sender, EventArgs e)
        {
            refreshCSVList();
        }
        void refreshCSVList()
        {
            TitleCB.Items.Clear();
            DescriptionCB.Items.Clear();
            synopsisCB.Items.Clear();
            if (csvData != null)
            {
                for (int i = 0; i < csvData.entries.Length; i++)
                {
                    if (csvData.entries[i] != null && csvData.entries[i].key!= null)
                    {
                        TitleCB.Items.Add(csvData.entries[i].key);
                        DescriptionCB.Items.Add(csvData.entries[i].key);
                        synopsisCB.Items.Add(csvData.entries[i].key);
                    }
                }
            }
           
        }
        /// <summary>
        /// Updates the main window to display and match a given scenario
        /// </summary>
        /// <param name="s">The scenario to be edited</param>
        public void updateControlsFromScenario(Scenario s)
        {
            
            
            //loadingScreen.Visible = true;
            //Visible = false;
            Text = "Ashes Scenario Builder  LOADING!!!";

            if(previewWin!=null)previewWin.update(scen1.toString());
            //debugConsole.update();//DEBUG
            //mainLoadBar.Visible = false;
            // mainLoadBar.Value = 0;
            //int y = enableCreepsBox.Location.Y+300;
            int y = 0;
            //int x = enableCreepsBox.Location.X - 20;
            //int x = 0;
            TitleCB.Text = s.title;
            TitleDisplay.Text = csvData.getTranslatedString(s.title);
            DescriptionCB.Text = s.description;
            DescriptionDisplay.Text = csvData.getTranslatedString(s.description);
            mapBox1.Text = s.map;
            imageBox.Text= s.image;
            imageFadeBox.Text = s.imageFade;
            compImageBox.Text = s.compImage;
            compImageFadeBox.Text = s.imageFade;
            imageBigBox.Text = s.imageBig;
            imageRadiusControl.Value = (decimal)s.imageRadius;
            synopsisCB.Text = s.synopsis;
            synopsisBox.Text = csvData.getTranslatedString(s.synopsis);
            planetPosXControl.Value = (decimal)s.planetPosition.x;
            planetPosYControl.Value = (decimal)s.planetPosition.y;
            enableCreepsBox.Checked = s.enableCreeps;
            noAttritionBox.Checked = s.noAttrition;
            noVPBox.Checked = s.noVpVictory;
            noSeedVictoryBox.Checked = s.noSeedVictory;
            hideTerrainBox.Checked = s.hideTerrain;
            hideDiffBox.Checked = s.hideDifficulty;
            preMovieBox.Text = scen1.preMovie;
            postMovieBox.Text = scen1.postMovie;
            Point p = new Point(6, 6);

            y = 0;
            if (s.triggers != null)
            {
                if (tM != null)
                {
                    for (int i = 0; i < tM.Length; i++)
                    {
                        //tM[i].destroy();
                        tM[i].Dispose();
                    }
                }
                int scrollto = -1;
                //tM = new TriggerMini[s.triggers.Length];
                tM = new TriggerMenuOption[s.triggers.Length];
                for (int i = 0; i < s.triggers.Length; i++)
                {
                    tM[i] = new TriggerMenuOption(s.triggers[i], this, new Point(AnchorLabel.Left, y));
                    TriggerPanel.Controls.Add(tM[i]);
                    /*tM[i] = new TriggerMini(s.triggers[i], this, AnchorLabel.Left, y);
                    TriggerPanel.Controls.Add(tM[i].pan);
                    tM[i].delete.Location = new Point(tM[i].pan.Width - tM[i].delete.Width, 0);
                    tM[i].edit.Location = new Point(tM[i].pan.Width - tM[i].edit.Width, tM[i].pan.Height - tM[i].edit.Height);
                    tM[i].icon.Location = new Point(0, tM[i].pan.Height - tM[i].icon.Height);
                    tM[i].value.Location = new Point(tM[i].icon.Width, tM[i].icon.Top);
                    tM[i].nameLabel.Location = new Point(tM[i].pan.Width - tM[i].delete.Width - tM[i].nameLabel.Width, 0);*/
                    //y += 100;
                    y += tM[i].Height+1;
                    if (lastSelectedTrigger != null && s.triggers[i].id.Equals(lastSelectedTrigger))
                    {
                        lastSelectedTrigger = null;
                        scrollto = i;
                    }
                }
                AddTriggerButton.Location= new Point(tM[0].Left,tM[tM.Length-1].Bottom+2);
                if(scrollto>=0)
                    panel2.ScrollControlIntoView(tM[scrollto].icon);
            }
            y = PlayerPanel.Top+20;
            int x = PlayerPanel.Left + 5;
            if (s.players != null)
            {
                if (pC != null)
                {
                    for (int i = 0; i < pC.Length; i++)
                    {
                        //pC[i].destroy();
                        pC[i].Dispose();
                    }
                }
                //pC = new PlayerCard[s.players.Length];
                pC= new PlayerMenuOption[s.players.Length];
                for (int i = 0; i < s.players.Length; i++)
                {
                    /*pC[i] = new PlayerCard(s.players[i], this);
                    pC[i].card.Location = new Point(x, y);
                    pC[i].icon.Location = new Point(0, pC[i].card.Height - pC[i].icon.Height);
                    pC[i].nameLabel.Location = new Point(pC[i].icon.Width, pC[i].icon.Top);
                    pC[i].delete.Location = new Point(pC[i].card.Width - pC[i].delete.Width,0);
                    pC[i].noSeed.Location = new Point(pC[i].card.Width - pC[i].noSeed.Width, pC[i].card.Height - pC[i].noSeed.Height);
                    pC[i].noEngi.Location = new Point(pC[i].noSeed.Left - pC[i].noEngi.Width, pC[i].noSeed.Top);
                    PlayerPanel.Controls.Add(pC[i].card);

                    y += 80;*/
                    pC[i] = new PlayerMenuOption(s.players[i], this);
                    pC[i].Location = new Point(x, y);
                    PlayerPanel.Controls.Add(pC[i]);
                    y += (pC[i].Height + 5);
                }
            }
            updatePlayerEditMenu();


            if (scen1.players.Length >= 8) addPlayerButton.Enabled = false;
            else addPlayerButton.Enabled = true;
            numPlayersLabel.Text = "(" + scen1.players.Length + "/8)";
            Text = "Ashes Scenario Builder - "+scenName;
        }
        /// <summary>
        /// Refreshes the player tab with accurate information
        /// </summary>
        public void updatePlayerEditMenu()
        {
            if (selectedPlayer == null)
            {
                //PlayerEditMenu.Visible = false;
                nameBox.Text = "Click on a Player to select it";
                factionCB.Text = "Click on a Player to select it";
                teamNUD.Value = 0;
                startLocNud.Value = 0;
                AITypeCB.Text = "off";
                AIDiffCB.Text = "";
                noEngiBox.Checked = false;
                noSeedBox.Checked = false;
                return;
            }
            PlayerEditMenu.Visible = true;
            nameBox.Text = selectedPlayer.name;
            factionCB.Text = selectedPlayer.faction;
            teamNUD.Value = (decimal)selectedPlayer.team;
            switch (selectedPlayer.color)
            {
                case 1: colorCB.Text = "Red";
                    break;
                case 2: colorCB.Text = "Blue";
                    break;
                case 3: colorCB.Text = "Brown";
                    break;
                case 4: colorCB.Text = "Orange";
                    break;
                case 5: colorCB.Text = "Yellow";
                    break;
                case 6: colorCB.Text = "Cyan";
                    break;
                case 7: colorCB.Text = "Green";
                    break;
            }
            startLocNud.Value = (decimal)selectedPlayer.startLoc;
            AITypeCB.Text = selectedPlayer.aiType;
            AIDiffCB.Text = selectedPlayer.aiDiff;
            noEngiBox.Checked = !selectedPlayer.noEngineer;
            noSeedBox.Checked = !selectedPlayer.noSeed;
            
        }
        /// <summary>
        /// Pulls information from the XML file for manipulating in the main GUI
        /// </summary>
        private void readInXMLDirectory()
        {
            scenNameCB.Items.Clear();
            
            string[] fileEntries = System.IO.Directory.GetFiles(directory);
            for (int i=0; i < fileEntries.Length; i++)
            {
                if (System.IO.Path.GetExtension(fileEntries[i]).Equals(".xml"))
                {
                    scenNameCB.Items.Add(System.IO.Path.GetFileNameWithoutExtension(fileEntries[i]));
                }
            }
        }
        /// <summary>
        /// Pulls information from the CSV file for manipulating in the main GUI
        /// </summary>
        private void readInCSVDirectory()
        {
            loadCSVBox.Items.Clear();
            string[] fileEntries = System.IO.Directory.GetFiles(csvDirectory);
            for (int i = 0; i < fileEntries.Length; i++)
            {
                if (System.IO.Path.GetExtension(fileEntries[i]).Equals(".csv"))
                {
                    loadCSVBox.Items.Add(System.IO.Path.GetFileNameWithoutExtension(fileEntries[i]));
                }
            }
        }

        //UI controls (edit in VS form designer controls)
        #region 
        private void player1ColorSelect_ValueChanged(object sender, EventArgs e)
        {
            
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scen1 = new Scenario(new Form1());
            updateControlsFromScenario(scen1);
        }
        private void synopsisBox_TextChanged(object sender, EventArgs e)
        {
            csvData.overwriteEntry(synopsisCB.Text, synopsisBox.Text);
        }
        private void mapBox1_TextChanged(object sender, EventArgs e)
        {
            scen1.map = mapBox1.Text;
            if(mapScreen!=null)mapScreen.update();
            displayDDS(USM.assetPath + "/Maps/" + scen1.map + "/MapPreview.dds", MapPreview);
            displayDDS(USM.assetPath + "/Maps/" + scen1.map + "/ErrosionMapGenerated.dds", ErosionMapGenerated);
            
            

        }
        private void imageBox_TextChanged(object sender, EventArgs e)
        {
     
            displayDDS(scen1.image, ImageDisplay);

            //ImageDisplay.Image = Image.FromFile(tView.getPngPath(imageBox.Text));
        }
        private void imageRadiusControl_ValueChanged(object sender, EventArgs e)
        {
            scen1.imageRadius = (float)imageRadiusControl.Value;
        }
        private void planetPosXControl_ValueChanged(object sender, EventArgs e)
        {
            scen1.planetPosition.x = (float)planetPosXControl.Value;
        }
        private void planetPosYControl_ValueChanged(object sender, EventArgs e)
        {
            scen1.planetPosition.y = (float)planetPosYControl.Value;
        }
        private void enableCreepsBox_CheckedChanged(object sender, EventArgs e)
        {
            scen1.enableCreeps = enableCreepsBox.Checked;
        }
        private void noAttritionBox_CheckedChanged(object sender, EventArgs e)
        {
            scen1.noAttrition = noAttritionBox.Checked;
        }
        private void noVPBox_CheckedChanged(object sender, EventArgs e)
        {
            scen1.noVpVictory = noVPBox.Checked;
        }
        private void noSeedVictoryBox_CheckedChanged(object sender, EventArgs e)
        {
            scen1.noSeedVictory = noSeedVictoryBox.Checked;
        }
        private void hideTerrainBox_CheckedChanged(object sender, EventArgs e)
        {
            scen1.hideTerrain = hideTerrainBox.Checked;
        }
        private void hideDiffBox_CheckedChanged(object sender, EventArgs e)
        {
            scen1.hideDifficulty = hideDiffBox.Checked;
        }
        private void addPlayerButton_Click(object sender, EventArgs e)
        {
            if (scen1.addPlayer())
            {  
                //addPlayerButton.Location = new System.Drawing.Point(addPlayerButton.Location.X, addPlayerButton.Location.Y + 25);
                //removePlayerButton.Location = new System.Drawing.Point(removePlayerButton.Location.X, addPlayerButton.Location.Y);
                updateControlsFromScenario(scen1);
            }
            else
            {
                addPlayerButton.Enabled = false;
            }
            removePlayerButton.Enabled = true;
            updatePlayerEditMenu();
        }  
        private void removePlayerButton_Click(object sender, EventArgs e)
        {
            if (scen1.removePlayer())
            {
                addPlayerButton.Location = new System.Drawing.Point(addPlayerButton.Location.X, addPlayerButton.Location.Y - 25);
                removePlayerButton.Location = new System.Drawing.Point(removePlayerButton.Location.X, addPlayerButton.Location.Y);
                updateControlsFromScenario(scen1);
            }
            else
            {
                removePlayerButton.Enabled = false;
            }
            addPlayerButton.Enabled = true;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            scen1.addTrigger();
            updateControlsFromScenario(scen1);
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folderpath = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                folderpath = fbd.SelectedPath;
            }

            if (folderpath != "")
            {
                csvDirectory = folderpath;
                csvName = "";
                csvPathLabel.Text = "Path  " + csvDirectory + @"\";
                adjustLoadControls();
                readInCSVDirectory();
            }
            
        }
        private void SaveButton1_Click(object sender, EventArgs e)
        {
            if (!scenNameCB.Text.Equals(""))
                scenName = scenNameCB.Text;
            ScenWriter.writeXML(scen1, directory + @"\" + scenName + ".xml");
            readInXMLDirectory();
        }
        private void pathBox_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void addTriggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scen1.addTrigger();
        }
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string folderpath = "";
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                folderpath = fbd.SelectedPath;
            }

            if (folderpath != "")
            {
                directory = folderpath;
                scenNameCB.Text = "";
                pathLabel.Text = "Path  " + directory + @"\";
                adjustLoadControls();
                readInXMLDirectory();
            }
        }
        private void LoadButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < scen1.players.Length; i++)
            {
                scen1.removePlayer();
            }
            
            if (scen1.triggers != null)
            {
                int temp = scen1.triggers.Length;
                for (int i = 0; i < temp; i++)
                {
                    if (scen1.triggers != null)
                    {
                      if (scen1.triggers[0] != null)
                      scen1.removeTrigger(0);
                    }
                   
                }
            }
            
            scen1 = ScenReader.readXML(this,directory + @"\" + scenName + ".xml");
            updateControlsFromScenario(scen1);
        }
        private void imageFadeBox_TextChanged(object sender, EventArgs e)
        {
            scen1.imageFade = imageFadeBox.Text;
            displayDDS(scen1.imageFade, ImageFadeDisplay);
        }
        private void compImageBox_TextChanged(object sender, EventArgs e)
        {
            scen1.compImage = compImageBox.Text;
            displayDDS(scen1.compImage, CompImageDisplay);
        }
        private void compImageFadeBox_TextChanged(object sender, EventArgs e)
        {
            scen1.compImageFade=compImageFadeBox.Text;
            displayDDS(scen1.compImageFade, CompImageFadeDisplay);
        }
        private void imageBigBox_TextChanged(object sender, EventArgs e)
        {
            scen1.imageBig = imageBigBox.Text;
            displayDDS(scen1.imageBig, ImageBigDisplay);
        }
        private void scenNameCB_TextChanged(object sender, EventArgs e)
        {
            scenName = scenNameCB.Text;
        }
        private void csvLoadButton_Click(object sender, EventArgs e)
        {
            csvData = new CSVDataSheet(csvDirectory + @"\" + csvName + ".csv");
            csvData.DataChanged+=csvData_DataChanged;
            refreshCSVList();
            csvData.triggerEvent();
            updateControlsFromScenario(scen1);
        }
        private void loadCSVBox_TextChanged(object sender, EventArgs e)
        {
            csvName = loadCSVBox.Text;
        }
        private void TitleCB_TextChanged(object sender, EventArgs e)
        {
            scen1.title = TitleCB.Text;
            if (csvData.hasEntry(scen1.title))
            {
                TitleDisplay.Text = csvData.getTranslatedString(scen1.title);
            }
            
        }

        private void DescriptionCB_TextChanged(object sender, EventArgs e)
        {
            scen1.description = DescriptionCB.Text;
            if (csvData.hasEntry(scen1.description))
            {
                DescriptionDisplay.Text = csvData.getTranslatedString(scen1.description);
            }
            
        }

        private void TitleDisplay_TextChanged(object sender, EventArgs e)
        {
            csvData.overwriteEntry(TitleCB.Text, TitleDisplay.Text);
        }

        private void csvSaveButton_Click(object sender, EventArgs e)
        {
            if (!loadCSVBox.Text.Equals(""))
                csvName = loadCSVBox.Text;
            else if (!scenNameCB.Text.Equals(""))
            {
                csvName = scenNameCB.Text;
            }
            csvData.writeCSVFile(csvDirectory+ @"\" + csvName + ".csv");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string filename = "";

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "Scenario Scripts and Dialog|*.CSV;*.XML|All files (*.*)|*.*";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                filename = ofd.FileName;
            }
            if (filename != "")
            {
                if (System.IO.Path.GetExtension(filename).Equals(".xml"))
                {
                    directory = System.IO.Path.GetDirectoryName(filename);
                    scenName = System.IO.Path.GetFileNameWithoutExtension(filename);
                    for (int i = 0; i < scen1.players.Length; i++)
                    {
                        scen1.removePlayer();
                    }

                    if (scen1.triggers != null)
                    {
                        int temp = scen1.triggers.Length;
                        for (int i = 0; i < temp; i++)
                        {
                            if (scen1.triggers != null)
                            {
                                if (scen1.triggers[0] != null)
                                    scen1.removeTrigger(0);
                            }

                        }
                    }

                    scen1 = ScenReader.readXML(this, directory + @"\" + scenName + ".xml");
                    updateControlsFromScenario(scen1);
                    pathLabel.Text = "Path  " + directory + @"\";
                    scenNameCB.Text = scenName;
                    adjustLoadControls();
                    readInXMLDirectory();
                }
                else
                {
                    csvDirectory = System.IO.Path.GetDirectoryName(filename);
                    csvName = System.IO.Path.GetFileNameWithoutExtension(filename);
                    csvData = new CSVDataSheet(csvDirectory + @"\" + csvName + ".csv");
                    csvData.DataChanged += csvData_DataChanged;
                    refreshCSVList();
                    csvData.triggerEvent();
                    csvPathLabel.Text = "Path  " + csvDirectory + @"\";
                    loadCSVBox.Text = csvName;
                    adjustLoadControls();
                    readInCSVDirectory();
                    updateControlsFromScenario(scen1);
                }
                
            }
            

        }

        private void menuSaveButton_Click(object sender, EventArgs e)
        {
            string filename = "";

            SaveFileDialog ofd = new SaveFileDialog();
            

            ofd.Filter = "Scenario Scripts and Dialog|*.CSV;*.XML|All files (*.*)|*.*";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                filename = ofd.FileName;
            }
            if (filename != "")
            {
                if (System.IO.Path.GetExtension(filename).Equals(".xml"))
                {
                    directory = System.IO.Path.GetDirectoryName(filename);
                    scenName = System.IO.Path.GetFileNameWithoutExtension(filename);

                    ScenWriter.writeXML(scen1, directory + @"\" + scenName + ".xml");
                    readInXMLDirectory();
                    updateControlsFromScenario(scen1);
                    pathLabel.Text = "Path  " + directory + @"\";
                    scenNameCB.Text = scenName;
                    adjustLoadControls();
                }
                else
                {
                    csvDirectory = System.IO.Path.GetDirectoryName(filename);
                    csvName = System.IO.Path.GetFileNameWithoutExtension(filename);
                    csvData.writeCSVFile(csvDirectory + @"\" + csvName + ".csv");
                    csvData.DataChanged += csvData_DataChanged;
                    refreshCSVList();
                    csvData.triggerEvent();
                    csvPathLabel.Text = "Path  " + csvDirectory + @"\";
                    loadCSVBox.Text = csvName;
                    adjustLoadControls();
                    readInCSVDirectory();
                }

            }
        }

        private void preMovieBox_TextChanged(object sender, EventArgs e)
        {
            scen1.preMovie = preMovieBox.Text;
        }

        private void postMovieBox_TextChanged(object sender, EventArgs e)
        {
            scen1.postMovie = postMovieBox.Text;
        }

        private void synopsisCB_TextChanged(object sender, EventArgs e)
        {
            scen1.synopsis = synopsisCB.Text;
            synopsisBox.Text = csvData.getTranslatedString(scen1.synopsis);
        }

        private void ImageOpen_Click(object sender, EventArgs e)
        {
            //string filename = "";

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "DirectX Textures|*.DDS|All files (*.*)|*.*";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                scen1.image = ofd.FileName;
                updateControlsFromScenario(scen1);
            }
            
        }

        private void ImageFadeOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "DirectX Textures|*.DDS|All files (*.*)|*.*";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                scen1.imageFade = ofd.FileName;
                updateControlsFromScenario(scen1);
            }
        }

        private void CompImageOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "DirectX Textures|*.DDS|All files (*.*)|*.*";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                scen1.compImage = ofd.FileName;
                updateControlsFromScenario(scen1);
            }
        }

        private void CompImageFadeOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "DirectX Textures|*.DDS|All files (*.*)|*.*";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                scen1.compImageFade = ofd.FileName;
                updateControlsFromScenario(scen1);
            }
        }

        private void ImageBigOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "DirectX Textures|*.DDS|All files (*.*)|*.*";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                scen1.imageBig = ofd.FileName;
                updateControlsFromScenario(scen1);
            }
        }

        private void PostMovieOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "WEBM Videos|*.WEBM|All files (*.*)|*.*";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                scen1.postMovie = ofd.FileName;
                updateControlsFromScenario(scen1);
            }
        }

        private void PreMovieOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "WEBM Videos|*.WEBM|All files (*.*)|*.*";
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                scen1.preMovie = ofd.FileName;
                updateControlsFromScenario(scen1);
            }
        }

        private void SetupOpenButton_Click(object sender, EventArgs e)
        {
            if (!setupOpen)
            {
                setupOpen = true;
                playersOpen = false;
                triggersOpen = false;
            }
            scenAttributesPanel.Visible = setupOpen;
            TriggerPanel.Visible = triggersOpen;
            MetaPlayerPanel.Visible = playersOpen;
            PlayerEditMenu.Visible = playersOpen;
        }

        private void PlayersOpenButton_Click(object sender, EventArgs e)
        {
            if (!playersOpen)
            {
                setupOpen = false;
                playersOpen = true;
                triggersOpen = false;
            }
            scenAttributesPanel.Visible = setupOpen;
            TriggerPanel.Visible = triggersOpen;
            MetaPlayerPanel.Visible = playersOpen;
            PlayerEditMenu.Visible = playersOpen;
        }

        private void TriggersOpenButton_Click(object sender, EventArgs e)
        {
            if (!triggersOpen)
            {
                setupOpen = false;
                playersOpen = false;
                triggersOpen = true;
            }
            scenAttributesPanel.Visible = setupOpen;
            TriggerPanel.Visible = triggersOpen;
            MetaPlayerPanel.Visible = playersOpen;
            PlayerEditMenu.Visible = playersOpen;
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void DescriptionDisplay_TextChanged(object sender, EventArgs e)
        {
            csvData.overwriteEntry(DescriptionCB.Text, DescriptionDisplay.Text);
        }

        private void AddTriggerButton_Click(object sender, EventArgs e)
        {
            scen1.addTrigger();
        }
        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InfoWindow iW = new InfoWindow();

            
            DialogResult dr = iW.ShowDialog();
        }

        #endregion

        //PlayerEditor
        #region
        private void nameBox_TextChanged(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                selectedPlayer.name = nameBox.Text;
            }
        }

        private void factionCB_TextChanged(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                selectedPlayer.faction = factionCB.Text;
            }
        }

        private void teamNUD_ValueChanged(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                selectedPlayer.team = (int)teamNUD.Value;
            }
        }

        private void colorCB_TextChanged(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                if (colorCB.Text.Equals("Red"))
                {
                    selectedPlayer.color = 1;
                }
                else if (colorCB.Text.Equals("Blue"))
                {
                    selectedPlayer.color = 2;
                }
                else if (colorCB.Text.Equals("Brown"))
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
        }

        private void startLocNud_ValueChanged(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                selectedPlayer.startLoc = (int)startLocNud.Value;
            }
        }

        private void AITypeCB_TextChanged(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                selectedPlayer.aiType = AITypeCB.Text;
            }
        }

        private void AIDiffCB_TextChanged(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                selectedPlayer.aiDiff = AIDiffCB.Text;
            }
        }

        private void mapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mapScreen == null || mapScreen.Disposing || mapScreen.IsDisposed)
            {
                mapScreen = new Map(this);
                mapScreen.Show();
                mapScreen.update();
            }

            else if (!previewWin.Visible)
            {
                mapScreen.Visible = true;
            }
            else
            {
                mapScreen.Visible = false;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void noSeedBox_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                selectedPlayer.noSeed = !noSeedBox.Checked;
            }
        }

        private void noEngiBox_CheckedChanged(object sender, EventArgs e)
        {
            if (selectedPlayer != null)
            {
                selectedPlayer.noEngineer = !noEngiBox.Checked;
            }
        }
        #endregion

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsMenu oM = new OptionsMenu(this);


            DialogResult dr = oM.ShowDialog();
        }

        private void xMLPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (previewWin == null || previewWin.Disposing || previewWin.IsDisposed)
            {
                previewWin = new TriggerXMLPreview();
                updateControlsFromScenario(scen1);
                previewWin.Show();
            }
            
            else if (!previewWin.Visible)
            {
                previewWin.Visible = true;
            }
            else
            {
                previewWin.Visible = false;
            }
        }

        private void displayDDS(string path, PictureBox pb)
        {
            if (File.Exists(path))
            {
                DdsImage dds = new DdsImage(path);
                pb.Image = (System.Drawing.Image)dds.BitmapImage.Clone();
            }
            else
            {
                string pathAttempt2 = USM.assetPath+"/"+path;
                if (File.Exists(pathAttempt2))
                {
                    DdsImage dds = new DdsImage(pathAttempt2);
                    pb.Image = (System.Drawing.Image)dds.BitmapImage.Clone();
                }
            }
        }




    }

}

