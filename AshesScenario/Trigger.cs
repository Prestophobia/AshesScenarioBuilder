using System;

namespace AshesScenario
{
    /// <summary>
    /// The class for the trigger, which is an event in an AOTS scenario containing actions
    /// </summary>
    public class Trigger
    {
        #region variables
        /// <summary>
        /// The triggers name, external triggers, and trigger type-specific attributes
        /// </summary>
        public string id, type, target, otherTrig, notOtherTrig;
        /// <summary>
        /// Trigger type-specific attributes
        /// </summary>
        public int timer, size, owner, player, difficulty, index;
        /// <summary>
        /// Trigger type-specific attributes
        /// </summary>
        public Position center, position;
        /// <summary>
        /// Trigger type-specific attributes
        /// </summary>
        public bool isBuilding, inactive;
        /// <summary>
        /// The actions that the trigger executes when activated
        /// </summary>
        public Action[] actions;
        /// <summary>
        /// The scenario the trigger belongs to
        /// </summary>
        public Scenario scen;
        #endregion

        #region constructors
        /// <summary>
        /// Constructs a blank timer trigger
        /// </summary>
        /// <param name="parent">The scenario the trigger is to be a part of</param>
        public Trigger(Scenario parent)
        {
            scen = parent;
            otherTrig = "";
            notOtherTrig = "";
            id = "";
            type = "Timer";
            target = "";
            position = new Position();
            center = new Position();
        }
        /// <summary>
        /// Constructs a trigger with a sepcified set of attributes
        /// </summary>
        /// <param name="parent">The scenario the trigger is a part of</param>
        /// <param name="Name">The internal name of the trigger(the user never sees these)</param>
        /// <param name="Type">The trigger type</param>
        /// <param name="TimerOwnerPlayerDiff">The value of either the timer, owner, player, or difficulty, depending on the trigger type</param>
        /// <param name="Target">The triggers target(only applicable with certain trigger types)</param>
        /// <param name="IsBuilding">If this is a destruction trigger this will tell it if it is destroying a building</param>
        /// <param name="PosOrCenter">The value of the position or the center, depending on the trigger type</param>
        /// <param name="Size">The size of the area affected(only applicable with certain trigger types)</param>
        /// <param name="Inactive">If the trigger is a timer, if true the timer won't activate on its own without another trigger activating it</param>
        /// <param name="OtherTrigger">Other trigger that also cause this trigger to activate</param>
        /// <param name="NotOtherTrigger">Another trigger that if activated prevents this trigger from activating</param>
        public Trigger(Scenario parent, string Name, string Type, int TimerOwnerPlayerDiff, string Target, bool IsBuilding, Position PosOrCenter, int Size, bool Inactive, string OtherTrigger, string NotOtherTrigger)
        {
            scen = parent;
            id = Name;
            type = Type;
            difficulty=timer = owner = player = TimerOwnerPlayerDiff;
            target = Target;
            isBuilding = IsBuilding;
            position = center = PosOrCenter;
            size = Size;
            inactive = Inactive;
            otherTrig = OtherTrigger;
            notOtherTrig = NotOtherTrigger;
        }
        #endregion

        #region public methods
        /// <summary>
        /// Removes all of the actions from a trigger
        /// </summary>
        public void clearActions()
        {
            actions = new Action[0];
        }
        /// <summary>
        /// Updates the internal indexes of the actions so they are consistent with their index in the array
        /// </summary>
        public void updateIndexes()
        {
            if (actions != null)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i].index = i;
                }
            }
        }
        /// <summary>
        /// Adds a specified action to the end of the action array
        /// </summary>
        /// <param name="newAction">The action to be added</param>
        public void addAction(Action newAction)
        {
            if (actions == null)
            {
                actions = new Action[1];
                newAction.index = 0;
                actions[0] = newAction;
                return;
            }
            Action[] temp = new Action[actions.Length + 1];
            for (int i = 0; i < actions.Length; i++)
            {
                temp[i] = actions[i];
            }
            newAction.index = actions.Length;
            temp[actions.Length] = newAction;
            actions = temp;
            updateIndexes();
            scen.mainWindow.updateControlsFromScenario(scen);
        }
        /// <summary>
        /// Adds a specified action to the end of the array without updating the UI
        /// </summary>
        /// <param name="newAction">The action to be added to the Trigger</param>
        public void addActionQuick(Action newAction)
        {
            if (actions == null)
            {
                actions = new Action[1];
                newAction.index = 0;
                actions[0] = newAction;
                return;
            }
            Action[] temp = new Action[actions.Length + 1];
            for (int i = 0; i < actions.Length; i++)
            {
                temp[i] = actions[i];
            }
            newAction.index = actions.Length;
            temp[actions.Length] = newAction;
            actions = temp;
            updateIndexes();
        }
        /// <summary>
        /// Adds a specified action to a specified index in the actions array
        /// </summary>
        /// <param name="newAction">The action to be added to the array</param>
        /// <param name="index">The index at which the action will be inserted</param>
        public void addAction(Action newAction, int index)
        {
            if (actions == null)
            {
                actions = new Action[1];
                newAction.index = 0;
                actions[0] = newAction;
                return;
            }
            newAction.index = index;
            Action[] temp = new Action[actions.Length + 1];
            for (int i = 0; i < index; i++)
            {
                temp[i] = actions[i];
            }
            temp[index] = newAction;
            for (int i = index; i < temp.Length; i++)
            {
                temp[i + 1] = actions[i];
            }
            actions = temp;
            updateIndexes();
            scen.mainWindow.updateControlsFromScenario(scen);
        }
        /// <summary>
        /// Removes the last action in the array
        /// </summary>
        public void removeAction()
        {
            if (actions == null)
                return;
            Action[] temp = new Action[actions.Length - 1];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = actions[i];
            }
            updateIndexes();
            scen.mainWindow.updateControlsFromScenario(scen);
        }
        /// <summary>
        /// Removes the action at the specified index
        /// </summary>
        /// <param name="index">The index of the action to be removed</param>
        public void removeAction(int index)
        {

            if (actions == null || index < 0 || index >= actions.Length)
                return;
            if (actions.Length == 1)
            {
                //actions[0].destroy();
                actions = null;
                return;
            }
           // actions[index].destroy();
            Action[] temp = new Action[actions.Length - 1];
            for (int i = 0; i < index; i++)
            {
                temp[i] = actions[i];
            }
            for (int i = index; i < temp.Length; i++)
            {
                temp[i] = actions[i + 1];
            }
            actions = temp;
            updateIndexes();
            scen.mainWindow.updateControlsFromScenario(scen);
        }
        /// <summary>
        /// Replaces the action located at a given index with a new specified action
        /// </summary>
        /// <param name="index">The index of the old action being replaced</param>
        /// <param name="newAction">The new replacement action</param>
        public void replaceAction(int index, Action newAction)
        {
            if (index < 0 || index >= actions.Length)
            {
                return;
            }
            actions[index] = newAction;
            updateIndexes();
            scen.mainWindow.updateControlsFromScenario(scen);
        }
        /// <summary>
        /// Converts the data for the trigger into an XML format that can be used for file IO purposes
        /// </summary>
        /// <returns>A string containing the triggers data in XML format</returns>
        public string toString()
        {
            string output = "\t<Trigger Name=\"" + id + "\" Type=\"" + type + "\" ";
            if (type.Equals("Timer"))
            {
                
                output += "Timer=\"" + timer + "\"";
            }
            else if (type.Equals("Area"))
            {
                output += "Center=\"" + center.toString() + "\" Size=\"" + size + "\"";
            }
            else if (type.Equals("Destruction"))
            {
                int temp = 0;
                if (isBuilding)
                {
                    temp = 1;
                }
                output += "IsBuilding=\"" + temp + "\" Target=\"" + target + "\"";
            }
            else if (type.Equals("Build"))
            {
                output += "Target=\"" + target + "\"  Center=\"" + center.toString() + "\" Size=\"" + size + "\"";
            }
            else if (type.Equals("NamedCreate"))
            {
                output += "Target=\"" + target + "\"";
            }
            else if (type.Equals("ZoneCapture"))
            {
                output += "Position=\"" + position.toString() + "\" Owner=\"" + owner + "\"";
            }
            else if (type.Equals("Research"))
            {
                output += "Player=\"" + player + "\" Target=\"" + target + "\"";
            }
            else if (type.Equals("Difficulty"))
            {
                output += "Difficulty=\"" + difficulty + "\"";
            }
            else
            {
                return "<!--ERROR:INVALID TRIGGER-->";
            }
            if (inactive)
            {
                output += " Inactive=\"1\"";
            }
            if (!otherTrig.Equals(""))
            {
                output += " OtherTrigger=\"" + otherTrig + "\"";
            }
            if (!notOtherTrig.Equals(""))
            {
                output += " NotOtherTrigger=\"" + notOtherTrig + "\"";
            }

            output += ">\n";

            if (actions != null)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    output += "\t\t" + actions[i].toString() + "\n";
                }
            }

            return output += "\t</Trigger>\n";
        }
        /// <summary>
        /// Returns the total number of triggers activated by this trigger
        /// </summary>
        /// <returns>the total number of triggers activated by this trigger</returns>
        public int hasActivateTriggers()
        {
            int count = 0;
            ActivateAI template = new ActivateAI(this);
            if (actions != null)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    Type t = actions[i].GetType();
                    if (t.Equals(template.GetType()))
                    {
                        count++;
                    }
                }
            }


            return count;
        }
        /// <summary>
        /// Returns the total number of units spawned by the trigger
        /// </summary>
        /// <returns>The total number of units spawned by the trigger</returns>
        public int hasSpawnUnits()
        {
            int count = 0;
            SpawnUnit template = new SpawnUnit(this);
            if (actions != null)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    Type t = actions[i].GetType();
                    if (t.Equals(template.GetType()))
                    {
                        count++;
                    }
                }
            }


            return count;
        }
        /// <summary>
        /// Returns the total number of buildings spawned by the trigger
        /// </summary>
        /// <returns>The total number of buildings spawned by the trigger</returns>
        public int hasSpawnBuildings()
        {
            int count = 0;
            SpawnBuilding template = new SpawnBuilding(this);
            if (actions != null)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    Type t = actions[i].GetType();
                    if (t.Equals(template.GetType()))
                    {
                        count++;
                    }
                }
            }


            return count;
        }
        /// <summary>
        /// Combines and returns the total number of units and buildings spawned in the trigger
        /// </summary>
        /// <returns>The total number of spawned units and buildings</returns>
        public int hasSpawns()
        {
            return hasSpawnUnits() + hasSpawnBuildings();
        }

        public bool isInitialTrigger()
        {
            if ((type.ToLower().Equals("timer") && timer < 10 && !inactive) || type.ToLower().Equals("difficulty"))
                return true;            
            return false;
        }

        public Trigger[] getPriorTriggers()
        {
            Trigger[] output = scen.getInitialTriggers(); //the first Triggers are always prior
            if (type.ToLower().Equals("destruction")|| type.ToLower().Equals("namedcreate"))
            {
                if (isBuilding)
                {                  
                    SpawnBuilding temp =scen.findSpawnBuilding(target);
                    if (temp != null && temp.trig != null)
                        output = appendTriggerArray(temp.trig, output);                 
                }//look for the target building's spawn
                else
                {
                    SpawnUnit temp = scen.findSpawnUnit(target);
                    if (temp != null && temp.trig != null)
                        output = appendTriggerArray(temp.trig, output);                   
                }//look for the target unit's spawn
            }//the trigger is triggered by a spawned unit/building
            foreach(Trigger curTrig in scen.triggers)
            {
                if (curTrig.hasActivateTriggers()>0)
                {
                    string[] activateTriggers = curTrig.getActivateTriggerNames();
                    foreach(string curName in activateTriggers)
                    {
                        if (curName.ToLower().Equals(id.ToLower()))
                        {
                            output = appendTriggerArray(curTrig, output);
                            break;
                        }//is this trigger the target?
                    }//look through activateTrigger targets
                }//trigger has any activateTriggers
            }//look for activateTrigger actions
            foreach(Action curAction in actions)
            {
                if (curAction.GetType().Equals(typeof(AttackAttackMove))
                    || curAction.GetType().Equals(typeof(DestroyUnit)))
                {
                    SpawnUnit tempSU = scen.findSpawnUnit(curAction.getStringA());
                    if (tempSU != null && tempSU.trig != null)//we got a match?
                        output = appendTriggerArray(tempSU.trig, output);                   
                }//is the selected action manipulating a spawned unit?
                else if (curAction.GetType().Equals(typeof(DestroyBuilding)))
                {
                    SpawnBuilding tempSB = scen.findSpawnBuilding(curAction.getStringB());
                    if(tempSB != null && tempSB.trig != null)//we got a match?
                        output = appendTriggerArray(tempSB.trig, output);              
                }//is the selected action destroying a spawned building?
            }//look throught the actions to see if we are manipulating spawned units
            Trigger[] newOutput = new Trigger[0];
            newOutput = appendTriggerArray(output, newOutput);
            foreach(Trigger curTrigger in output)
            {
                Trigger[] temp = curTrigger.getPriorTriggers();
                newOutput = appendTriggerArray(temp, newOutput);
            }//get the prior triggers' prior triggers back to the initial trigger
            return newOutput;
        }

        

        //TODO make better spawn search algorithms(scenario scale, index sensitivity, destroyUnit sensitive)
        #endregion

        #region accessor methods

        /// <summary>
        /// Gets the names of the triggers activated by this trigger
        /// </summary>
        /// <returns>The names of the triggers activated by this trigger</returns>
        public string[] getActivateTriggerNames()
        {
            string[] output = new string[0];
            ActivateAI template = new ActivateAI(this);
            if (actions != null)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    Type t = actions[i].GetType();
                    if (t.Equals(template.GetType()))
                    {
                        string[] temp = new string[output.Length + 1];
                        for (int ii = 0; ii < output.Length; ii++)
                        {
                            temp[ii] = output[ii];
                        }
                        temp[output.Length] = actions[i].getVal();
                    }
                }
            }
            return output;
        }
        /// <summary>
        /// Gets all the names of the spawnUnit actions in the trigger
        /// </summary>
        /// <returns>All the names of the spawnUnit actions in the trigger</returns>
        public string[] getSpawnUnitNames()
        {
            string[] output = new string[0];
            SpawnUnit template = new SpawnUnit(this);
            if (actions != null)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    Type t = actions[i].GetType();
                    if (t.Equals(template.GetType()))
                    {
                        string[] temp = new string[output.Length + 1];
                        for (int ii = 0; ii < output.Length; ii++)
                        {
                            temp[ii] = output[ii];
                        }
                        temp[output.Length] = actions[i].getVal();
                        output = temp;
                    }
                }
            }
            return output;
        }
        /// <summary>
        /// Gets the names of all of the spawnBuilding actions in the trigger
        /// </summary>
        /// <returns>the names of all of the spawnBuilding actions in the scenario</returns>
        public string[] getSpawnBuildingNames()
        {
            string[] output = new string[0];
            SpawnBuilding template = new SpawnBuilding(this);
            if (actions != null)
            {
                for (int i = 0; i < actions.Length; i++)
                {
                    Type t = actions[i].GetType();
                    if (t.Equals(template.GetType()))
                    {
                        string[] temp = new string[output.Length + 1];
                        for (int ii = 0; ii < output.Length; ii++)
                        {
                            temp[ii] = output[ii];
                        }
                        temp[output.Length] = actions[i].getVal();
                    }
                }
            }
            return output;
        }
        /// <summary>
        /// Gets all of the names of the spawnUnit and spawnBuilding actions
        /// </summary>
        /// <returns>A string array containing all the names of the spawnUnit and spawnBuilding actions</returns>
        public string[] getSpawnNames()
        {
            string[] units = getSpawnUnitNames();
            string[] buildings = getSpawnBuildingNames();
            string[] output = new string[units.Length + buildings.Length];
            for (int i = 0; i < units.Length; i++)
            {
                output[i] = units[i];
            }
            for (int i = units.Length; i < output.Length; i++)
            {
                output[i] = buildings[i - (units.Length)];
            }
            return output;
        }

        #endregion

        #region static methods
        public static Trigger[] appendTriggerArray(Trigger from, Trigger[] to)
        {
            Trigger[] output = new Trigger[to.Length + 1];
            for (int i = 0; i < to.Length; i++)
             output[i] = to[i];
            output[to.Length] = from;
            return output;
        }
        public static Trigger[] appendTriggerArray(Trigger[] from, Trigger[] to)
        {
            Trigger[] output = new Trigger[to.Length + from.Length];
            for (int i = 0; i < to.Length; i++)
                output[i] = to[i];            
            for(int i = to.Length; i < output.Length; i++)
                output[i] = from[i - to.Length];           
            return output;
        }

        public static DestroyUnit findDestroyUnit(Trigger[] trigs, string name)
        {
            name = name.ToLower();
            foreach(Trigger curTrig in trigs)
                foreach(Action curAction in curTrig.actions)
                    if(curAction.GetType().Equals(typeof(DestroyUnit))&&curAction.getStringA().ToLower().Equals(name))
                        return (DestroyUnit)curAction;        
            return null;
        }

        public static bool isStillAlive(SpawnUnit unit, Trigger currentTrigger)
        {
            Trigger[] prior = currentTrigger.getPriorTriggers();
            if (findDestroyUnit(prior, unit.name) != null) return false;
            return true;
        }

        #endregion
    }
}
