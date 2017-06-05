
using System.Xml;
using System.IO;
namespace AshesScenario
{
    /// <summary>
    /// Class for functions related to file IO and reading Ashes scenario XML files and writing to files. 
    /// </summary>
    public class ScenReader
    {
        /// <summary>
        ///Reads an AOTS scenario XML file and converts it to a scenario object to be used by the GUI
        ///<para>
        ///Function steps through the XML file element-by-element in order to add every piece of data into the 
        ///Scenario object individually.
        ///</para>
        /// </summary>
        /// <param name="parent">The main window of the GUI</param>
        /// <param name="path">The search path for the XML file to be read</param>
        /// <returns>Returns a scenario object that can be used by the GUI</returns>
        public static Scenario readXML(Form1 parent, string path)
        {
            Scenario output = new Scenario(parent);//will be returned in the end
            output.triggers = null;
            output.players = null;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            XmlReader reader = XmlReader.Create(path, settings);
            Trigger currentTrigger = null;
            Dialog currentDialog = null;
            
            reader.Read();
            if (!reader.Name.Equals("Mission"))
             return output;           
            reader.MoveToFirstAttribute();
            reader.ReadOuterXml();
            reader.MoveToFirstAttribute();
            output.title = reader.Value;
            while (reader.MoveToNextAttribute())
            {
                if (reader.Name.Equals("Description")) output.description = reader.Value;
                else if (reader.Name.Equals("Map")) output.map = reader.Value;
                else if (reader.Name.Equals("Image"))  output.image = reader.Value;
                else if (reader.Name.Equals("ImageFade")) output.imageFade = reader.Value;
                else if (reader.Name.Equals("CompImage")) output.compImage = reader.Value;
                else if (reader.Name.Equals("CompImageFade")) output.compImageFade = reader.Value;
                else if (reader.Name.Equals("ImageBig")) output.imageBig = reader.Value;
                else if (reader.Name.Equals("ImageRadius")) output.imageRadius = float.Parse(reader.Value);
                else if (reader.Name.Equals("Synopsis")) output.synopsis = reader.Value;
                else if (reader.Name.Equals("PlanetPosition")) output.planetPosition = new Position(reader.Value);
                else if (reader.Name.Equals("EnableCreeps")) output.enableCreeps = Converter.stringToBool(reader.Value);
                else if (reader.Name.Equals("NoAttrition")) output.noAttrition = Converter.stringToBool(reader.Value);
                else if (reader.Name.Equals("NoVPVictory")) output.noVpVictory = Converter.stringToBool(reader.Value);
                else if (reader.Name.Equals("NoSeedVictory")) output.noSeedVictory = Converter.stringToBool(reader.Value);
                else if (reader.Name.Equals("HideTerrain")) output.hideTerrain = Converter.stringToBool(reader.Value);
                else if (reader.Name.Equals("HideDifficulty")) output.hideDifficulty = Converter.stringToBool(reader.Value);
                else if (reader.Name.Equals("PreMovie")) output.preMovie = reader.Value;
                else if (reader.Name.Equals("PostMovie")) output.postMovie = reader.Value;
                else if (reader.Name.Equals("Prereq")) output.prereq = reader.Value;              
            }
            while (reader.Read())
            {
                //if(reader.NodeType==XmlNodeType.Comment){}
                if (reader.Name.Equals("Player"))
                {
                    output.addPlayer(parsePlayer(reader));
                }              
                else if (reader.Name.Equals("Trigger")&&(reader.NodeType!=XmlNodeType.EndElement))
                {
                    output.addTriggerQuick(parseTrigger(reader,output));
                    currentTrigger = output.triggers[output.triggers.Length - 1];
                }
                else if (reader.Name.Equals("Dialog") && (reader.NodeType != XmlNodeType.EndElement))
                {
                    Dialog temp = new Dialog(currentTrigger);
                    currentDialog = temp;
                    currentTrigger.addActionQuick(temp);                    
                }
                else if (reader.Name.Equals("Entry"))
                {
                    currentDialog.addEntry(parseDialogEntry(reader, currentDialog));
                }
                else if(reader.NodeType==XmlNodeType.Element)
                {
                    Action temp = parseAction(reader, currentTrigger);
                    if(temp!=null)
                    currentTrigger.addActionQuick(temp);
                    //break;
                }
                else if (reader.Name.Equals("Mission") && (reader.NodeType == XmlNodeType.EndElement))
                {
                    break;
                }
            }
            reader.Close();
            return output;
        }

        static Action parseAction(XmlReader reader, Trigger t)
        {
            if (reader.Name.Equals("Camera"))
            {
                bool save = false, load = false,snap=false;
                Position pos = new Position();
                Position RTP = new Position();
                float speed = 0;
                reader.MoveToFirstAttribute();
                if(reader.Name.Equals("Save")) save = Converter.stringToBool(reader.Value);
                else if (reader.Name.Equals("Load")) load = Converter.stringToBool(reader.Value);               
                else if (reader.Name.ToLower().Equals("snap")) snap = Converter.stringToBool(reader.Value);                
                else if (reader.Name.Equals("Position")) pos = new Position(reader.Value);               
                else if (reader.Name.Equals("RTP")) RTP = new Position(reader.Value);                
                else if (reader.Name.Equals("Speed")) float.TryParse(reader.Value,out speed);
                
                while (reader.MoveToNextAttribute())
                {
                    if (reader.Name.Equals("Save")) save = Converter.stringToBool(reader.Value);
                    else if (reader.Name.Equals("Load")) load = Converter.stringToBool(reader.Value);
                    else if (reader.Name.ToLower().Equals("snap")) snap = Converter.stringToBool(reader.Value);
                    else if (reader.Name.Equals("Position")) pos = new Position(reader.Value);
                    else if (reader.Name.Equals("RTP")) RTP = new Position(reader.Value);
                    else if (reader.Name.Equals("Speed")) float.TryParse(reader.Value, out speed);
                }
                return new Camera(t,save,load,pos,RTP,speed,snap);
            }
            else if (reader.Name.Equals("Reveal"))
            {
                string name="";
                Position pos = new Position();
                int size=0,radarSize=0;
                bool enable = true;
                reader.MoveToFirstAttribute();
                if (reader.Name.Equals("Name"))           name = reader.Value;               
                else if (reader.Name.Equals("Position"))  pos = new Position(reader.Value);               
                else if (reader.Name.Equals("Size"))      int.TryParse(reader.Value, out size);            
                else if (reader.Name.Equals("RadarSize")) int.TryParse(reader.Value,out radarSize);
                else if (reader.Name.Equals("Enable"))    enable = Converter.stringToBool(reader.Value);
                
                while (reader.MoveToNextAttribute())
                {
                    if (reader.Name.Equals("Name"))           name = reader.Value;
                    else if (reader.Name.Equals("Position"))  pos = new Position(reader.Value);
                    else if (reader.Name.Equals("Size"))      int.TryParse(reader.Value, out size);
                    else if (reader.Name.Equals("RadarSize")) int.TryParse(reader.Value, out radarSize);
                    else if (reader.Name.Equals("Enable"))    enable = Converter.stringToBool(reader.Value);
                }
                return new Reveal(t, name, pos, size, radarSize, enable);
            }
            else if (reader.Name.Equals("AreaIndicator"))
            {
                string name = "", color = "";
                int size = 0;
                bool duration = true;
                Position pos = new Position();

                reader.MoveToFirstAttribute();
                if      (reader.Name.Equals("Name"))     name = reader.Value;
                else if (reader.Name.Equals("Color"))    color = reader.Value;
                else if (reader.Name.Equals("Size"))     int.TryParse(reader.Value, out size);
                else if (reader.Name.Equals("Position")) pos = new Position(reader.Value);
                else if (reader.Name.Equals("Duration")) duration = Converter.stringToBool(reader.Value);
                
                while (reader.MoveToNextAttribute())
                {
                    if (reader.Name.Equals("Name")) name = reader.Value;
                    else if (reader.Name.Equals("Color")) color = reader.Value;
                    else if (reader.Name.Equals("Size")) int.TryParse(reader.Value, out size);
                    else if (reader.Name.Equals("Position")) pos = new Position(reader.Value);
                    else if (reader.Name.Equals("Duration")) duration = Converter.stringToBool(reader.Value);
                }

                return new AreaIndicator(t,name,color,size,pos,duration);
            }
            else if (reader.Name.Equals("Objective"))
            {
                string name = "", text = "";
                bool setCheck = false;

                reader.MoveToFirstAttribute();

                if (reader.Name.Equals("Name")) name = reader.Value;
                else if (reader.Name.Equals("String")) text = reader.Value;
                else if (reader.Name.Equals("SetCheck")) setCheck = Converter.stringToBool(reader.Value);

                while (reader.MoveToNextAttribute())
                {
                    if      (reader.Name.Equals("Name"))     name = reader.Value;                  
                    else if (reader.Name.Equals("String"))   text = reader.Value;
                    else if (reader.Name.Equals("SetCheck")) setCheck = Converter.stringToBool(reader.Value);                   
                }

                return new Objective(t, name, text, setCheck);
            }
            else if (reader.Name.Equals("Restrict"))
            {
                string type = "", id = "";
                bool enable = false;

                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Type"))   type = reader.Value;
                else if (reader.Name.Equals("ID"))     id = reader.Value;              
                else if (reader.Name.Equals("Enable")) enable = Converter.stringToBool(reader.Value);
                
                while (reader.MoveToNextAttribute())
                {
                    if (reader.Name.Equals("Type")) type = reader.Value;
                    else if (reader.Name.Equals("ID")) id = reader.Value;
                    else if (reader.Name.Equals("Enable")) enable = Converter.stringToBool(reader.Value);
                }

                return new Restrict(t, type, id, enable);
            }
            else if (reader.Name.Equals("SpawnUnit"))
            {
                string name = "", template = "", parent = "";
                int player = 0;
                Position pos = new Position();
                bool noDeath = false;
                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Name"))     name = reader.Value;                
                else if (reader.Name.Equals("Template")) template = reader.Value;                
                else if (reader.Name.Equals("Parent"))   parent = reader.Value;                
                else if (reader.Name.Equals("Player"))   int.TryParse(reader.Value,out player);                
                else if (reader.Name.Equals("Position")) pos = new Position(reader.Value);                
                else if (reader.Name.Equals("NoDeath"))  noDeath = Converter.stringToBool(reader.Value);              

                while(reader.MoveToNextAttribute()){
                    if (reader.Name.Equals("Name")) name = reader.Value;
                    else if (reader.Name.Equals("Template")) template = reader.Value;
                    else if (reader.Name.Equals("Parent")) parent = reader.Value;
                    else if (reader.Name.Equals("Player")) int.TryParse(reader.Value, out player);
                    else if (reader.Name.Equals("Position")) pos = new Position(reader.Value);
                    else if (reader.Name.Equals("NoDeath")) noDeath = Converter.stringToBool(reader.Value);
                }

                return new SpawnUnit(t, name, template, parent, player, pos, noDeath);
            }
            else if (reader.Name.Equals("DestroyUnit"))
            {
                string name = "";
                int time = 0;

                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Name")) name = reader.Value;    
                else if (reader.Name.Equals("Time")) int.TryParse(reader.Value, out time);
                
                while (reader.MoveToNextAttribute())
                {
                    if      (reader.Name.Equals("Name")) name = reader.Value;
                    else if (reader.Name.Equals("Time")) int.TryParse(reader.Value, out time);
                }

                return new DestroyUnit(t,name,time);
            }
            else if (reader.Name.Equals("SpawnBuilding"))
            {
                string name = "", template = "";
                int player = 0;
                Position pos = new Position();

                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Name"))     name = reader.Value;
                else if (reader.Name.Equals("Template")) template = reader.Value;
                else if (reader.Name.Equals("Player"))   int.TryParse(reader.Value, out player);
                else if (reader.Name.Equals("Position")) pos = new Position(reader.Value);

                while (reader.MoveToNextAttribute())
                {
                    if (reader.Name.Equals("Name")) name = reader.Value;
                    else if (reader.Name.Equals("Template")) template = reader.Value;
                    else if (reader.Name.Equals("Player")) int.TryParse(reader.Value, out player);
                    else if (reader.Name.Equals("Position")) pos = new Position(reader.Value);
                }

                return new SpawnBuilding(t,name,template,player,pos);
            }
            else if (reader.Name.Equals("DestroyBuilding"))
            {
                string name = "";
                int time = 0;
                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Name")) name = reader.Value;                
                else if (reader.Name.Equals("Time")) int.TryParse(reader.Value, out time);                

                while (reader.MoveToNextAttribute())
                {
                    if (reader.Name.Equals("Name")) name = reader.Value;
                    else if (reader.Name.Equals("Time")) int.TryParse(reader.Value, out time);
                }

                return new DestroyBuilding(t, name, time);
            }
            else if (reader.Name.Equals("AttackAttackMove"))
            {
                string name = "";
                Position pos = new Position();

                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Name"))     name = reader.Value;               
                else if (reader.Name.Equals("Position")) pos = new Position(reader.Value);
                
                while (reader.MoveToNextAttribute())
                {
                    if (reader.Name.Equals("Name")) name = reader.Value;
                    else if (reader.Name.Equals("Position")) pos = new Position(reader.Value);
                }

                return new AttackAttackMove(t, name, pos);
            }
            else if (reader.Name.Equals("ActivateTrigger"))
            {
                string target = "Error";
                reader.MoveToFirstAttribute();
                if (reader.Name.Equals("Target")) target = reader.Value;             
                return new ActivateTrigger(t, target);
            }
            else if (reader.Name.ToLower().Equals("letterbox"))
            {
                bool enable = false, snap = false;

                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Enable")) enable = Converter.stringToBool(reader.Value);
                else if (reader.Name.Equals("Snap"))   snap   = Converter.stringToBool(reader.Value);
               
                while(reader.MoveToNextAttribute()){
                    if (reader.Name.Equals("Enable"))    enable = Converter.stringToBool(reader.Value);
                    else if (reader.Name.Equals("Snap")) snap   = Converter.stringToBool(reader.Value);
                }
                return new LetterBox(t, enable, snap);
            }
            else if (reader.Name.Equals("HidePanel"))
            {
                bool hidePlayer=false,hideRank=false,hideResource=false;
                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("HidePlayer"))    hidePlayer   = Converter.stringToBool(reader.Value);
                else if (reader.Name.Equals("HideRank"))      hideRank     = Converter.stringToBool(reader.Value);
                else if (reader.Name.Equals("HideResources")) hideResource = Converter.stringToBool(reader.Value);

                while (reader.MoveToNextAttribute())
                {
                    if      (reader.Name.Equals("HidePlayer"))    hidePlayer   = Converter.stringToBool(reader.Value);
                    else if (reader.Name.Equals("HideRank"))      hideRank     = Converter.stringToBool(reader.Value);
                    else if (reader.Name.Equals("HideResources")) hideResource = Converter.stringToBool(reader.Value);
                }

                return new HidePanel(t, hidePlayer, hideRank, hideResource);
            }
            else if (reader.Name.Equals("GrantStuff"))
            {
                int player = -1, metal = 0, rad = 0, log = 0, quanta = 0,tech=0;
                bool enable=true;
                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Player"))       int.TryParse(reader.Value, out player);                
                else if (reader.Name.Equals("Metal"))        int.TryParse(reader.Value, out metal );                
                else if (reader.Name.Equals("Radioactives")) int.TryParse(reader.Value, out rad   );                
                else if (reader.Name.Equals("Logistics"))    int.TryParse(reader.Value, out log   );                
                else if (reader.Name.Equals("Quanta"))       int.TryParse(reader.Value, out quanta);                
                else if (reader.Name.Equals("Tech"))         int.TryParse(reader.Value, out tech  );               

                while (reader.MoveToNextAttribute())
                {
                    if      (reader.Name.Equals("Player"))       int.TryParse(reader.Value, out player);
                    else if (reader.Name.Equals("Metal"))        int.TryParse(reader.Value, out metal );
                    else if (reader.Name.Equals("Radioactives")) int.TryParse(reader.Value, out rad   );
                    else if (reader.Name.Equals("Logistics"))    int.TryParse(reader.Value, out log   );
                    else if (reader.Name.Equals("Quanta"))       int.TryParse(reader.Value, out quanta);
                    else if (reader.Name.Equals("Tech"))         int.TryParse(reader.Value, out tech  );
                }
                if (player<0) enable=false;
                
                return new GrantStuff(t, player, enable, quanta, metal, rad, log, tech);
            }
            else if (reader.Name.Equals("GrantTech"))
            {
                int player = 0;
                string tech = "";
                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Player")) int.TryParse(reader.Value, out player);                
                else if (reader.Name.Equals("Tech"))   tech = reader.Value;
                
                while (reader.MoveToNextAttribute())
                {
                    if      (reader.Name.Equals("Player")) int.TryParse(reader.Value, out player);
                    else if (reader.Name.Equals("Tech")) tech = reader.Value;
                }
                return new GrantTech(t, player, tech, 1);
            }
            else if (reader.Name.Equals("CaptureNearest"))
            {
                string name = "";
                Position randomTime = new Position();
                bool repeat = false;
                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Name"))       name       = reader.Value;                
                else if (reader.Name.Equals("RandomTime")) randomTime = new Position(reader.Value);                
                else if (reader.Name.Equals("Repeat"))     repeat     = Converter.stringToBool(reader.Value);
                
                while(reader.MoveToNextAttribute()){
                    if      (reader.Name.Equals("Name"))       name       = reader.Value;
                    else if (reader.Name.Equals("RandomTime")) randomTime = new Position(reader.Value);
                    else if (reader.Name.Equals("Repeat"))     repeat     = Converter.stringToBool(reader.Value);
                }

                return new CaptureNearest(t, name, (int)randomTime.x, (int)randomTime.y, repeat);
            }
            else if (reader.Name.Equals("ToggleAI"))
            {
                bool disableAI = false;
                reader.MoveToFirstAttribute();
                disableAI = Converter.stringToBool(reader.Value);
            }
            else if (reader.Name.Equals("EndMission"))
            {
                bool victory = false;
                string text = "";
                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Victory")) victory = Converter.stringToBool(reader.Value);               
                else if (reader.Name.Equals("String"))  text    = reader.Value;
                
                while (reader.MoveToNextAttribute())
                {
                    if      (reader.Name.Equals("Victory")) victory = Converter.stringToBool(reader.Value);
                    else if (reader.Name.Equals("String"))  text    = reader.Value;
                }

                return new EndMission(t, victory, text);
            }
            else if (reader.Name.Equals("Select"))
            {
                string target = "";
                reader.MoveToFirstAttribute();
                target = reader.Value;
                return new Select(t, target);
            }
            else if (reader.Name.Equals("Notifications"))
            {
                bool enable = false;
                reader.MoveToFirstAttribute();
                enable = Converter.stringToBool(reader.Value);
                return new Notifications(t, enable);
            }
            else if (reader.Name.Equals("Highlight"))
            {
                string name = "",button="";
                bool enable = false;
                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Name"))    name   = reader.Value;              
                else if (reader.Name.Equals("Button"))  button = reader.Value;                
                else if (reader.Value.Equals("Enable")) enable = Converter.stringToBool(reader.Value);
                
                while (reader.MoveToNextAttribute())
                {
                    if      (reader.Name.Equals("Name"))    name   = reader.Value;
                    else if (reader.Name.Equals("Button"))  button = reader.Value;
                    else if (reader.Value.Equals("Enable")) enable = Converter.stringToBool(reader.Value);
                }

                return new Highlight(t, name, button, enable);
            }
            else if (reader.Name.Equals("ActivateAI"))
            {
                string name = "";
                int player = 0;

                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Name"))   name = reader.Value;                
                else if (reader.Name.Equals("Player")) int.TryParse(reader.Value, out player);
                
                while (reader.MoveToNextAttribute())
                {
                    if (reader.Name.Equals("Name")) name = reader.Value;
                    else if (reader.Name.Equals("Player")) int.TryParse(reader.Value, out player);
                }

                return new ActivateAI(t, name, player);
            }
            else if (reader.Name.Equals("PlaySound"))
            {
                reader.MoveToFirstAttribute();
                string sound = reader.Value;
                return new PlaySound(t, sound);
            }
            else if (reader.Name.Equals("Pause"))
            {
                reader.MoveToFirstAttribute();
                bool enable = Converter.stringToBool(reader.Value);
                return new Pause(t, enable);
            }
            else if (reader.Name.Equals("ChangeAIPersonality"))
            {
                string name = "";
                int player = 0;

                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Name"))   name = reader.Value;                
                else if (reader.Name.Equals("Player")) int.TryParse(reader.Value, out player);
                
                while (reader.MoveToNextAttribute())
                {
                    if      (reader.Name.Equals("Name"))   name = reader.Value;
                    else if (reader.Name.Equals("Player")) int.TryParse(reader.Value, out player);
                }

                return new ChangeAIPersonality(t, name, player);
            }
            else if (reader.Name.Equals("ChangeAIDifficulty"))
            {
                int player = 0;
                string diff = "";
                reader.MoveToFirstAttribute();

                if      (reader.Name.Equals("Player"))     int.TryParse(reader.Value, out player);               
                else if (reader.Name.Equals("Difficulty")) diff = reader.Value;
                
                while (reader.MoveToNextAttribute())
                {
                    if      (reader.Name.Equals("Player"))     int.TryParse(reader.Value, out player);
                    else if (reader.Name.Equals("Difficulty")) diff = reader.Value;
                }

                return new ChangeAIDifficulty(t, player, diff);
            }
            return null;
        }
        static Trigger parseTrigger(XmlReader reader,Scenario scen){
            string id="", type="", target="", otherTrig="", notOtherTrig="";
            int timerDiffEtc=0, size=0;
            Position centerPos = new Position();
            bool isBuilding=false, inactive=false;

            reader.MoveToFirstAttribute();
            id = reader.Value;
            reader.MoveToNextAttribute();
            type = reader.Value;
            while(reader.MoveToNextAttribute()){
              if (     reader.Name.Equals("Timer")
                    || reader.Name.Equals("Owner")
                    || reader.Name.Equals("Player")
                    || reader.Name.Equals("Difficulty"))      int.TryParse(reader.Value,out  timerDiffEtc);
              else if (reader.Name.Equals("Center")
                    || reader.Name.Equals("Position"))        centerPos = new Position(reader.Value); 
              else if (reader.Name.Equals("Inactive"))        inactive = Converter.stringToBool(reader.Value);
              else if (reader.Name.Equals("Target"))          target = reader.Value;
              else if (reader.Name.Equals("OtherTrigger"))    otherTrig = reader.Value;
              else if (reader.Name.Equals("NotOtherTrigger")) notOtherTrig = reader.Value;
              else if (reader.Name.Equals("Size"))            size = int.Parse(reader.Value);                                 
              else if (reader.Name.Equals("IsBuilding"))      isBuilding = Converter.stringToBool(reader.Value);            
            }            
            Trigger output= new Trigger(scen,id,type,timerDiffEtc,target,isBuilding,centerPos,size,inactive,otherTrig,notOtherTrig);
            return output;
        }
        static Player parsePlayer(XmlReader reader)
        {
            Player output = new Player();
            reader.MoveToFirstAttribute();
            output.name = reader.Value;
            while (reader.MoveToNextAttribute())
            {
                if(reader.Name.Equals("Faction")) output.faction = reader.Value;                
                else if (reader.Name.Equals("Team")) output.team = int.Parse(reader.Value);               
                else if (reader.Name.Equals("Color")) output.color = int.Parse(reader.Value);               
                else if (reader.Name.Equals("StartLocation")) output.startLoc = int.Parse(reader.Value);               
                else if (reader.Name.Equals("AIType")) output.aiType = reader.Value;              
                else if (reader.Name.Equals("AIDifficulty")) output.aiDiff = reader.Value;                
                else if (reader.Name.Equals("NoSeed")) output.noSeed = Converter.stringToBool(reader.Value);               
                else if (reader.Name.Equals("NoEngineer")) output.noEngineer = Converter.stringToBool(reader.Value);                
            }
            return output;
        }
        static DialogEntry parseDialogEntry(XmlReader reader, Dialog currentDialog)
        {
            string icon = "", text = "",audio="";
            reader.MoveToFirstAttribute();
            if (reader.Name.Equals("Icon")) icon = reader.Value;
            else if (reader.Name.Equals("Text")) text = reader.Value;
            else if (reader.Name.Equals("Audio")) audio = reader.Value;           
            while (reader.MoveToNextAttribute())
            {
                if (reader.Name.Equals("Icon")) icon = reader.Value;
                else if (reader.Name.Equals("Text")) text = reader.Value;
                else if (reader.Name.Equals("Audio")) audio = reader.Value;
            }
            return new DialogEntry(currentDialog,icon,text,audio);
        }
    }
    /// <summary>
    /// File IO as it relates to writing AOTS scenario XML files.
    /// </summary>
    public class ScenWriter
    {
        /// <summary>
        /// Converts a Scenario object used by the GUI to an XML file that can be read by AOTS
        /// </summary>
        /// <param name="scen">The scenaraio object to be written to a file</param>
        /// <param name="path">The path and file name to be written to</param>
        public static void writeXML(Scenario scen, string path)
        {
            string output = scen.toString();
            File.WriteAllText(path, output);
        }
    }
    /// <summary>
    /// An object that contains the coordinates of a point in 3D space
    /// </summary>
    public class Position
    {
        /// <summary>
        /// Dimensions of point
        /// </summary>
        public float x, y, z;
        /// <summary>
        /// Reads a string, typically from a file, and outputs a position object with that string's coordinate data
        /// </summary>
        /// <param name="s">String to be read for its coordinate data</param>
        public Position(string s)
        {
            //s = s.TrimStart().TrimEnd();
            string[] temp = s.Split(',');
            string[] tempX = new string[0], tempY = new string[0], tempZ = new string[0];
            for (int i = 0; i < temp.Length; i++)
            {
                if      (i == 0) float.TryParse(temp[i], out x);                
                else if (i == 1) float.TryParse(temp[i], out y);             
                else             float.TryParse(temp[i], out z);             
            }
        }
        /// <summary>
        /// Creates a point at (0,0,0)
        /// </summary>
        public Position()
        {
            x = y = z = 0.0f;
        }
        /// <summary>
        /// Converts the position data to a string tat can be printed into an XML file
        /// </summary>
        /// <returns>a string containing 2D coordinates</returns>
        public string toString()
        {
            return "" + x + "," + y;
        }
    }
    /// <summary>
    /// Class containing functions to convert one type to another
    /// </summary>
    public class Converter
    {
        /// <summary>
        /// Converts a string starting with a 1 or a 0 to a boolean
        /// </summary>
        /// <param name="s">A string starting with 1 or 0</param>
        /// <returns>A boolean depending on index 0 of s</returns>
        public static bool stringToBool(string s)
        {
            if (s[0] == '1') return true;            
            return false;
        }
    }
}
