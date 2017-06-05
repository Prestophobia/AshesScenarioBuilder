namespace AshesScenario
{
    /// <summary>
    /// An object containing all of the data for the AOTS scenario that can be modified with the GUI
    /// <para> Contains the scenario attributes, players, triggers, actions, etc. that will eventually be written into an XML file</para>
    /// </summary>
    public class Scenario
    {
        #region variables
        /// <summary>
        /// Strings containing the attributes of the scenario itself relating to its map and how it is displayed in menus
        /// </summary>
        public string title, description, map, synopsis, image, imageFade, compImage, compImageFade, imageBig, prereq, postMovie,preMovie;
        /// <summary>
        /// The radius of the image in the scenario selection menu
        /// </summary>
        public float imageRadius;
        /// <summary>
        /// The position of the image in the scenario selection menu
        /// </summary>
        public Position planetPosition;
        /// <summary>
        /// Switches that determine gameplay aspects such as creeps, victory types, difficulty, and visibility
        /// </summary>
        public bool enableCreeps, noAttrition, noVpVictory, noSeedVictory, hideTerrain, hideDifficulty;
        /// <summary>
        /// The players in the scenario, both the PC and AI players
        /// </summary>
        public Player[] players;
        /// <summary>
        /// The triggers that make up the events in the scenario
        /// </summary>
        public Trigger[] triggers;
        /// <summary>
        /// The main window of the GUI application
        /// </summary>
        public Form1 mainWindow;
        #endregion

        #region constructors
        /// <summary>
        /// creates a blank scenario
        /// </summary>
        /// <param name="main">The main window of the GUI application</param>
        public Scenario(Form1 main)
        {
            mainWindow = main;
            planetPosition = new Position();
            players = new Player[1];
            players[0] = new Player(0);
            triggers = new Trigger[1];
            triggers[0] = new Trigger(this);

        }
        #endregion

        #region players

        /// <summary>
        /// Adds a simple AI player to the scenario
        /// </summary>
        /// <returns>A boolean based on whether the function succeeds in aadding a player or fails</returns>
        public bool addPlayer()
        {
            return addPlayer(new Player(0));
        }
        /// <summary>
        /// Adds a specified player to the scenario
        /// </summary>
        /// <param name="p">The player to be added</param>
        /// <returns>A boolean based on whether the function succeeds in aadding a player or fails</returns>
        public bool addPlayer(Player p)
        {
            if (players == null)
            {
                players = new Player[1];
                players[0] = new Player(0, p.name, p.faction, p.team, p.color, p.startLoc, p.aiType, p.aiDiff, p.noSeed, p.noEngineer);
                updatePlayerIndexes();
                return true;
            }
            if (players.Length < 8)
            {
                Player[] temp = new Player[players.Length + 1];
                for (int i = 0; i < players.Length; i++)
                {
                    temp[i] = players[i];
                }
                temp[players.Length] = new Player(players.Length, p.name, p.faction, p.team, p.color, p.startLoc, p.aiType, p.aiDiff, p.noSeed, p.noEngineer);

                players = temp;
                updatePlayerIndexes();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the last player in the array
        /// </summary>
        /// <returns>A bool that indicates if a player was removed</returns>
        public bool removePlayer()
        {
            if (players.Length > 0)
            {
                //players[players.Length - 1].destroy();
                Player[] temp = new Player[players.Length - 1];
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = players[i];
                }
                players = temp;
                updatePlayerIndexes();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Removes a specified player in the array by its index
        /// </summary>
        /// <param name="index">The index of the player to be removed</param>
        /// <returns>A bool indicating if a player was removed</returns>
        public bool removePlayer(int index)
        {
            if (players == null || index > players.Length || index < 0)
            {
                return false;
            }
            if (players.Length == 1)
            {

                players = null;
                return true;
            }
            Player[] temp = new Player[players.Length - 1];
            for (int i = 0; i < index; i++)
            {
                temp[i] = players[i];
            }
            for (int i = index; i < temp.Length; i++)
            {
                temp[i] = players[i + 1];
            }
            players = temp;
            mainWindow.updateControlsFromScenario(this);
            updatePlayerIndexes();
            return true;
        }

        /// <summary>
        /// Updates the indexes stored in the players so that they are in sync with their positions in the array
        /// </summary>
        public void updatePlayerIndexes()
        {
            if (players != null)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].index = i;
                }
            }
        }

        #endregion

        #region triggers

        /// <summary>
        /// Adds an empty trigger to the scenario
        /// </summary>
        public void addTrigger()
        {
            addTrigger(new Trigger(this));
        }

        /// <summary>
        /// Adds a specified trigger to the scenario
        /// </summary>
        /// <param name="t">The trigger to be added</param>
        public void addTrigger(Trigger t)
        {
            t.scen = this;
            if (triggers == null)
            {
                triggers = new Trigger[1];
                triggers[0] = t;
                return;
            }
            Trigger[] temp = new Trigger[triggers.Length + 1];
            for (int i = 0; i < triggers.Length; i++)
            {
                temp[i] = triggers[i];
            }
            temp[temp.Length - 1] = t;
            triggers = temp;
            mainWindow.updateControlsFromScenario(this);
            updateIndexes();
        }

        /// <summary>
        /// Adds an empty trigger at a specified index
        /// </summary>
        /// <param name="index">The index at which the trigger is to be added</param>
        public void addTrigger(int index)
        {
            if (triggers == null)
            {
                triggers = new Trigger[1];
                triggers[0] = new Trigger(this);
                return;
            }
            if (index < 0)
            {
                return;
            }
            if (index > triggers.Length - 1)
            {
                addTrigger();
                return;
            }
            Trigger[] temp = new Trigger[triggers.Length + 1];
            for (int i = 0; i < index; i++)
            {
                temp[i] = triggers[i];
            }
            temp[index] = new Trigger(this);
            for (int i = index; i < triggers.Length; i++)
            {
                temp[i + 1] = triggers[i];
            }
            triggers = temp;
            updateIndexes();
        }

        /// <summary>
        /// Adds a specified trigger to the scenario without calling the GUI update function
        /// </summary>
        /// <param name="t">Trigger to be added to the scenario</param>
        public void addTriggerQuick(Trigger t)
        {
            t.scen = this;
            if (triggers == null)
            {
                triggers = new Trigger[1];
                triggers[0] = t;
                return;
            }
            Trigger[] temp = new Trigger[triggers.Length + 1];
            for (int i = 0; i < triggers.Length; i++)
            {
                temp[i] = triggers[i];
            }
            temp[temp.Length - 1] = t;
            triggers = temp;
            updateIndexes();
        }
       
        /// <summary>
        /// Adds a specified trigger to a specified index in the scenario
        /// </summary>
        /// <param name="t">The trigger to be added to the scenario</param>
        /// <param name="index">The index at which the trigger is to be added</param>
        public void addTriggerQuick(Trigger t, int index)
        {
            t.scen = this;

            if (index < 0 || index > (triggers.Length - 1))
            {
                addTriggerQuick(t);
                return;
            }
            Trigger[] temp = new Trigger[triggers.Length + 1];
            for (int i = 0; i < index; i++)
            {
                temp[i] = triggers[i];
            }
            temp[index] = t;
            for (int i = index; i < triggers.Length; i++)
            {
                temp[i + 1] = triggers[i];
            }
            triggers = temp;
            updateIndexes();
        }
        
           
        /// <summary>
        /// Removes the trigger located at the specified index
        /// </summary>
        /// <param name="index">The index of the trigger to be removed</param>
        public void removeTrigger(int index)
        {
            if (triggers == null || index > triggers.Length || index < 0)
            {
                return;
            }
            //triggers[index].destroy();
            if (triggers.Length == 1)
            {

                triggers = null;
                return;
            }
            Trigger[] temp = new Trigger[triggers.Length - 1];
            for (int i = 0; i < index; i++)
            {
                temp[i] = triggers[i];
            }
            for (int i = index; i < temp.Length; i++)
            {
                temp[i] = triggers[i + 1];
            }
            triggers = temp;
            mainWindow.updateControlsFromScenario(this);
            updateIndexes();
        }
      
        /// <summary>
        /// Removes the trigger located at the specified index without updating the GUI
        /// </summary>
        /// <param name="index">The index of the trigger to be removed</param>
        public void removeTriggerQuick(int index)
        {
            if (triggers == null || index > triggers.Length || index < 0)
            {
                return;
            }
            // triggers[index].destroy();
            if (triggers.Length == 1)
            {

                triggers = null;
                return;
            }
            Trigger[] temp = new Trigger[triggers.Length - 1];
            for (int i = 0; i < index; i++)
            {
                temp[i] = triggers[i];
            }
            for (int i = index; i < temp.Length; i++)
            {
                temp[i] = triggers[i + 1];
            }
            triggers = temp;
            updateIndexes();
        }
     
        /// <summary>
        /// Moves a trigger from one spot in the array to another
        /// </summary>
        /// <param name="fromIndex">The index of the trigger to be moved</param>
        /// <param name="toIndex">The destination pf the trigger being moved</param>
        public void moveTrigger(int fromIndex, int toIndex)
        {
            Trigger temp = getTrigger(fromIndex);
            removeTrigger(fromIndex);
            addTriggerQuick(temp, toIndex);
        }
       
        /// <summary>
        /// Swaps the order of two triggers
        /// </summary>
        /// <param name="index1">The index of the first trigger</param>
        /// <param name="index2">The index of the second trigger</param>
        /// <returns>A bool indicating whether or not the swap occured</returns>
        public bool swapTriggers(int index1, int index2)
        {
            if (index1 < 0 || index2 < 0 || index1 > triggers.Length - 1 || index2 > triggers.Length - 1 || index1 == index2)
            {
                return false;
            }
            Trigger temp = triggers[index1];
            triggers[index1] = triggers[index2];
            triggers[index2] = temp;
            updateIndexes();
            return true;
        }
    
        /// <summary>
        /// Updates the indexes stored in the triggers so that they are in sync with their positions in the array
        /// </summary>
        public void updateIndexes()
        {
            if (triggers != null)
            {
                for (int i = 0; i < triggers.Length; i++)
                {
                    triggers[i].index = i;
                }
            }
        }

        /// <summary>
        /// Finds a trigger in the array by its name
        /// </summary>
        /// <param name="name">The name of the trigger being searched for</param>
        /// <returns>The first trigger with the name being serached for</returns>
        public Trigger getTrigger(string name)
        {
            if (triggers != null)
            {
                for (int i = 0; i < triggers.Length; i++)
                {
                    if (triggers[i].id.Equals(name))
                    {
                        return triggers[i];
                    }
                }
            }
            return null;
        }
       
        /// <summary>
        /// Retrieves the trigger stored in the array at a specified index
        /// </summary>
        /// <param name="index">The index of the trigger being searched for</param>
        /// <returns>The trigger at the specified idex</returns>
        public Trigger getTrigger(int index)
        {
            if (triggers != null)
            {
                if (index >= triggers.Length && index >= 0)
                {
                    return triggers[index];
                }
            }
            return null;
        }
    
        /// <summary>
        /// Gets all of the triggers with a specified target
        /// </summary>
        /// <param name="target">The coomon target of the triggers eing searched</param>
        /// <returns>An array of triggers with a common target</returns>
        public Trigger[] getTriggersByTarget(string target)
        {
            Trigger[] output = new Trigger[0];
            if (triggers != null)
            {
                for (int i = 0; i < triggers.Length; i++)
                {
                    if (triggers[i].target.Equals(target))
                    {
                        Trigger[] temp = new Trigger[output.Length + 1];
                        for (int z = 0; z < output.Length; z++)
                        {
                            temp[z] = output[z];
                        }
                        temp[output.Length] = triggers[i];
                        output = temp;
                    }
                }
            }
            if (output.Length < 1)
            {
                return null;
            }
            else
            {
                return output;
            }
        }
   
        /// <summary>
        /// Gets the index of the first trigger with a specified name
        /// </summary>
        /// <param name="name">The name of the trigger to be found</param>
        /// <returns>The index of the first trigger with the specified name</returns>
        public int getTriggerIndex(string name)
        {
            if (triggers != null)
            {
                for (int i = 0; i < triggers.Length; i++)
                {
                    if (triggers[i].id.Equals(name))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    
        /// <summary>
        /// Gets the names of all the triggers in the trigger array
        /// </summary>
        /// <returns>The names of all the triggers in the trigger array</returns>
        public string[] getTriggerNames()
        {
            if (triggers != null)
            {
                string[] names = new string[triggers.Length];
                for (int i = 0; i < triggers.Length; i++)
                {
                    names[i] = triggers[i].id;
                }
                return names;
            }
            return null;
        }

        public Trigger[] getInitialTriggers()
        {
            Trigger[] output= new Trigger[0];
            foreach(Trigger cur in triggers)
                if (cur.isInitialTrigger())
                    output=Trigger.appendTriggerArray(cur, output);
            return output;
        }

        public SpawnUnit findSpawnUnit(string name)
        {
            name = name.ToLower();
            foreach (Trigger curTrigger in triggers)
                if (curTrigger.hasSpawnUnits()>0)
                    foreach (Action curAction in curTrigger.actions)
                        if (curAction.GetType().Equals(typeof(SpawnUnit))&& curAction.getStringA().ToLower().Equals(name)) 
                            return (SpawnUnit)curAction;
            return null;
        }

        public SpawnBuilding findSpawnBuilding(string name)
        {
            name = name.ToLower();
            foreach (Trigger curTrigger in triggers)
                if (curTrigger.hasSpawnUnits() > 0)
                    foreach (Action curAction in curTrigger.actions)
                        if (curAction.GetType().Equals(typeof(SpawnBuilding))&&curAction.getStringA().ToLower().Equals(name))
                            return (SpawnBuilding)curAction;
            return null;
        }

        #endregion

        #region IO
        /// <summary>
        /// Produces a string containing the scenario's data in the format of an XML document for file IO purposes
        /// </summary>
        /// <returns>the scenario's data in the format of an XML document</returns>
        public string toString()
        {
            int eC = 0, nA = 0, nVP = 0, nSV = 0, hT = 0, hD = 0;
            if (enableCreeps)
                eC = 1;
            if (noAttrition)
                nA = 1;
            if (noVpVictory)
                nVP = 1;
            if (noSeedVictory)
                nSV = 1;
            if (hideDifficulty)
                hD = 1;
            if (hideTerrain)
                hT = 1;
            string output = "<Mission Title=\"" + title + "\" Description=\"" + description + "\" Map=\"" + map + "\"\n\tImage=\"" + image + "\"\n\tImageFade=\"" + imageFade + "\"\n\tCompImage=\"" + compImage + "\"\n\tCompImageFade=\"" + compImageFade + "\"\n\tImageBig=\"" + imageBig + "\"\n\tImageRadius=\"" + imageRadius + "\"\n\tSynopsis=\"" + synopsis + "\"\n\tPlanetPosition=\"" + planetPosition.toString() + "\"\n\tEnableCreeps=\"" + eC + "\"\n\tNoAttrition=\"" + nA + "\"\n\tNoVPVictory=\"" + nVP + "\"\n\tNoSeedVictory=\"" + nSV + "\"\n\tHideTerrain=\"" + hT + "\"\n\tHideDifficulty=\"" + hD + "\"\n";
            if (prereq != null&&!prereq.Equals(""))
            {
                output += "\tPreReq=\"" + prereq + "\"\n";
            }
            if (preMovie != null && !preMovie.Equals(""))
            {
                output += "\tPreMovie=\"" + preMovie + "\"\n";
            }
            if (postMovie != null&&!postMovie.Equals(""))
            {
                output += "\tPostMovie=\"" + postMovie + "\"\n";
            }
            output += ">\n";
            for (int i = 0; i < players.Length; i++)
            {
                output += "\t";
                output += players[i].toString();
                output += "\n";
            }
            output += "\n";
            for (int i = 0; i < triggers.Length; i++)
            {
                output += triggers[i].toString();
                output += "\n";
            }
            output += "</Mission>";
            return output;
        }
        #endregion
    }
}
