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
            Trigger[] output = new Trigger[0];
            if (isInitialTrigger()) return output;

            AreaIndicator[]    areaIndicators = new AreaIndicator[0];
            Objective[]        objectives     = new Objective[0];
            DestroyUnit[]      units          = new DestroyUnit[0];
            DestroyBuilding[]  buildings      = new DestroyBuilding[0];
            AttackAttackMove[] aAMs           = new AttackAttackMove[0];

            if (actions != null) foreach (Action curAction in actions)
            {
              if      (curAction.GetType().Equals(typeof(AreaIndicator))&&!curAction.getBoolA())
                areaIndicators = appendActionToArray(curAction.toAreaIndicator(), areaIndicators);                    
              else if (curAction.GetType().Equals(typeof(Objective)) && curAction.getBoolA())
                objectives = appendActionToArray(curAction.toObjective(), objectives);                  
              else if (curAction.GetType().Equals(typeof(DestroyUnit)))
                units = appendActionToArray(curAction.toDestroyUnit(),units);                   
              else if (curAction.GetType().Equals(typeof(DestroyBuilding)))
                buildings = appendActionToArray(curAction.toDestroyBuilding(), buildings);                    
              else if (curAction.GetType().Equals(typeof(AttackAttackMove)))
                aAMs = appendActionToArray(curAction.toAttackAttackMove(), aAMs);
            }

            bool decided = false;
            if (scen.triggers != null) foreach (Trigger curTrig in scen.triggers)
                {
                    decided = false;
                    if(curTrig.actions != null) foreach (Action curAction in curTrig.actions)
                        {
                            if (curAction.GetType().Equals(typeof(AreaIndicator)))
                            {
                                AreaIndicator temp = curAction.toAreaIndicator();
                                foreach (AreaIndicator curAI in areaIndicators) if (temp.name.ToLower().Equals(curAI.name.ToLower()) && temp.on)
                                    {
                                        output = appendTriggerArray(curTrig, output);
                                        decided = true;
                                        break;
                                    }                                
                            }
                            else if(!decided && curAction.GetType().Equals(typeof(Objective)))
                            {
                                Objective temp = curAction.toObjective();
                                foreach (Objective curObj in objectives) if (temp.name.ToLower().Equals(curObj.name.ToLower()) && !temp.setCheck)
                                    {
                                        output = appendTriggerArray(curTrig, output);
                                        decided = true;
                                        break;
                                    }
                            }
                            else if (!decided && curAction.GetType().Equals(typeof(SpawnUnit)))
                            {
                                SpawnUnit temp = curAction.toSpawnUnit();
                                foreach (DestroyUnit curDU in units) if (temp.name.ToLower().Equals(curDU.name.ToLower()))
                                    {
                                        output = appendTriggerArray(curTrig, output);
                                        decided = true;
                                        break;
                                    }
                                if ((type.ToLower().Equals("destruction") || type.ToLower().Equals("namedcreate")) && !isBuilding)
                                {
                                    if (temp.name.ToLower().Equals(target.ToLower()))
                                    {
                                        output = appendTriggerArray(curTrig, output);
                                        decided = true;
                                        break;
                                    }
                                }
                            }
                            else if (!decided && curAction.GetType().Equals(typeof(SpawnBuilding)))
                            {
                                SpawnBuilding temp = curAction.toSpawnBuilding();
                                foreach (DestroyBuilding curDB in buildings) if (temp.name.ToLower().Equals(curDB.name.ToLower()))
                                    {
                                        output = appendTriggerArray(curTrig, output);
                                        decided = true;
                                        break;
                                    }
                                if ((type.ToLower().Equals("destruction") || type.ToLower().Equals("namedcreate")) && isBuilding)
                                {
                                    if (temp.name.ToLower().Equals(target.ToLower()))
                                    {
                                        output = appendTriggerArray(curTrig, output);
                                        decided = true;
                                        break;
                                    }
                                }
                            }
                            else if (!decided && curAction.GetType().Equals(typeof(ActivateTrigger)))
                            {
                                ActivateTrigger temp = curAction.toActivateTrigger();
                                if (temp.target.ToLower().Equals(id.ToLower()))
                                    {
                                        output = appendTriggerArray(curTrig, output);
                                        decided = true;
                                        break;
                                    }
                            }
                        }
                }//look for activateTrigger actions
                     
            return output;
        }


        public Trigger[] getAllPriorTriggers()
        {
            Trigger[] prior = getPriorTriggers();
            Trigger[] output = appendWithoutRedundancy(scen.getInitialTriggers(), getPriorTriggers());
            
            foreach (Trigger curTrigger in prior)
                output = appendTriggerArray(curTrigger.getAllPriorTriggers(), output);
            //get the prior triggers' prior triggers back to the initial trigger
            return output;
        }


        public Trigger[] getTriggersAfter()
        {
            Trigger[] output = new Trigger[0];

            //action lists
            SpawnUnit[] units = new SpawnUnit[0];
            SpawnBuilding[] buildings = new SpawnBuilding[0];
            ActivateTrigger[] activateTriggers = new ActivateTrigger[0];
            AreaIndicator[] areaIndicators = new AreaIndicator[0];
            Objective[] objectives = new Objective[0];

            if (actions != null) foreach (Action curAction in actions)
                {
                    if (curAction.GetType().Equals(typeof(SpawnUnit)))
                        units = appendActionToArray(curAction.toSpawnUnit(), units);
                    else if (curAction.GetType().Equals(typeof(SpawnBuilding)))
                        buildings = appendActionToArray(curAction.toSpawnBuilding(), buildings);
                    else if (curAction.GetType().Equals(typeof(ActivateTrigger)))
                        activateTriggers = appendActionToArray(curAction.toActivateTrigger(), activateTriggers);
                    else if (curAction.GetType().Equals(typeof(AreaIndicator)))
                    {
                        AreaIndicator temp = curAction.toAreaIndicator();
                        if (temp.on) areaIndicators = appendActionToArray(curAction.toAreaIndicator(), areaIndicators);
                    }
                    else if (curAction.GetType().Equals(typeof(Objective)))
                    {
                        Objective temp = curAction.toObjective();
                        if (!temp.setCheck) objectives = appendActionToArray(curAction.toObjective(), objectives);
                    }
                }//add relevant actions

            bool decidedFlag = false;
            foreach (Trigger curTrigger in scen.triggers) if (curTrigger.index != index)
                {
                    decidedFlag = false;
                    if (curTrigger.type.ToLower().Equals("namedcreate"))
                    {
                        if (units != null) foreach (SpawnUnit curUnit in units)
                            {
                                if (curTrigger.target.ToLower().Equals(curUnit.name.ToLower()))
                                {
                                    output = appendTriggerArray(curTrigger, output);
                                    decidedFlag = true;
                                    break;
                                }//curTrigger is destroying or being triggered by the spawning of the spawned unit
                            }
                        if (buildings != null && !decidedFlag) foreach (SpawnBuilding curBuilding in buildings)
                            {
                                if (curTrigger.target.ToLower().Equals(curBuilding.name.ToLower()))
                                {
                                    output = appendTriggerArray(curTrigger, output);
                                    decidedFlag = true;
                                    break;
                                }//curTrigger is destroying or being triggered by the spawning of the spawned unit
                            }
                    }
                    if (!decidedFlag && activateTriggers != null) foreach (ActivateTrigger curAT in activateTriggers)
                            if (curTrigger.id.ToLower().Equals(curAT.target.ToLower()))
                            {
                                output = appendTriggerArray(curTrigger, output);
                                decidedFlag = true;
                                break;
                            }
                    if (!decidedFlag && curTrigger.actions != null) foreach (Action curAction in curTrigger.actions)
                        {
                            if (!decidedFlag && curAction.GetType().Equals(typeof(DestroyUnit)))
                            {
                                if (units != null) foreach (SpawnUnit sU in units) if (sU.name.ToLower().Equals(curAction.getStringA().ToLower()))
                                        {
                                            output = appendTriggerArray(curTrigger, output);
                                            decidedFlag = true;
                                            break;
                                        }
                            }
                            else if (!decidedFlag && curAction.GetType().Equals(typeof(DestroyBuilding)))
                            {
                                if (buildings != null) foreach (SpawnBuilding sB in buildings) if (sB.name.ToLower().Equals(curAction.getStringA().ToLower()))
                                        {
                                            output = appendTriggerArray(curTrigger, output);
                                            decidedFlag = true;
                                            break;
                                        }
                            }
                            else if (!decidedFlag && curAction.GetType().Equals(typeof(AttackAttackMove)))
                            {
                                if (units != null) foreach (SpawnUnit sU in units) if (sU.name.ToLower().Equals(curAction.getStringA().ToLower()))
                                        {
                                            output = appendTriggerArray(curTrigger, output);
                                            decidedFlag = true;
                                            break;
                                        }
                            }
                            else if (!decidedFlag && curAction.GetType().Equals(typeof(AreaIndicator)))
                            {
                                AreaIndicator temp = curAction.toAreaIndicator();
                                if (areaIndicators != null) foreach (AreaIndicator curAI in areaIndicators) if (curAI.name.ToLower().Equals(temp.name.ToLower()))
                                        {
                                            output = appendTriggerArray(curTrigger, output);
                                            decidedFlag = true;
                                            break;
                                        }
                            }
                            else if (!decidedFlag && curAction.GetType().Equals(typeof(Objective)))
                            {
                                Objective temp = curAction.toObjective();
                                if (objectives != null) foreach (Objective curObj in objectives) if (curObj.name.ToLower().Equals(temp.name.ToLower()))
                                        {
                                            output = appendTriggerArray(curTrigger, output);
                                            decidedFlag = true;
                                            break;
                                        }
                            }
                            else if (!decidedFlag && curAction.GetType().Equals(typeof(EndMission)))
                            {
                                output = appendTriggerArray(curTrigger, output);
                                decidedFlag = true;
                                break;                                      
                            }
                            if (decidedFlag) break;
                        }
                }

            return output;
        }

        public Trigger[] getEveryTriggerAfter()
        {
            Trigger[] after = getTriggersAfter();
            Trigger[] output = getTriggersAfter();

            if (after != null) foreach (Trigger curTrig in after)
                    output = appendWithoutRedundancy(curTrig.getEveryTriggerAfter(), output);

            return output;
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

        #region array management
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

        public static T[] appendActionToArray<T>(T from, T[] to) where T : Action
        {
            T[] output = new T[to.Length + 1];
            for (int i = 0; i < to.Length; i++)
                output[i] = to[i];
            output[to.Length] = from;
            return output;
        }

        public static T[] appendActionToArray<T>(T[] from, T[] to) where T : Action
        {
            T[] output = new T[to.Length + from.Length];
            for (int i = 0; i < to.Length; i++)
                output[i] = to[i];
            for (int i = to.Length; i < output.Length; i++)
                output[i] = from[i - to.Length];
            return output;
        }
        public static Trigger[] appendWithoutRedundancy(Trigger[] t1, Trigger[] t2)
        {
            if (t1 == null) return t2;
            if (t2 == null) return t1;
            Trigger[] output = new Trigger[0];
            output = appendTriggerArray(output, t1);
            bool same = false;
            if (t2 != null)foreach(Trigger curTrig2 in t2)
            {
                same = false;
                foreach(Trigger curTrig1 in t1) if (curTrig1.id.ToLower().Equals(curTrig2.id.ToLower())) same = true;
                if (!same) output = appendTriggerArray(curTrig2, output);
            }
            return output;
        }
        #endregion

        public static DestroyUnit findDestroyUnit(Trigger[] trigs, string name)
        {
            if (name == null || trigs == null) return null;
            name = name.ToLower();
            if (trigs != null)  foreach (Trigger curTrig in trigs)
                if(curTrig.actions!=null) foreach(Action curAction in curTrig.actions)
                    if(curAction.GetType().Equals(typeof(DestroyUnit))&&curAction.getStringA().ToLower().Equals(name))
                        return (DestroyUnit)curAction;        
            return null;
        }

        public static DestroyBuilding findDestroyBuilding(Trigger[] trigs, string name)
        {
            if (name == null || trigs == null) return null;
            name = name.ToLower();
            if (trigs != null) foreach (Trigger curTrig in trigs)
                if(curTrig.actions!=null)foreach (Action curAction in curTrig.actions)
                    if (curAction.GetType().Equals(typeof(DestroyBuilding)) && curAction.getStringA().ToLower().Equals(name))
                        return (DestroyBuilding)curAction;
            return null;
        }

       

        public bool isBefore(Trigger other)
        {
            Trigger[] afterT1 = getTriggersAfter();
            foreach(Trigger curTrigger in afterT1)
             if (curTrigger.id.ToLower().Equals(other.id.ToLower()))
                    return true;         
            return false;
        }

        public static Position getCurrentPosition(string unitName, Trigger currentTrigger)
        {
            //if (!Action.isStillAlive(unitName, currentTrigger,false)) return null;
            Position output = new Position();
            Trigger[] history = currentTrigger.getPriorTriggers();
            history = sortTriggersChrono(history);
            for (int i = history.Length - 1; i >= 0; i--)
            {
                foreach (Action curAction in history[i].actions)
                {
                    if (curAction.GetType().Equals(typeof(AttackAttackMove))
                        && curAction.getStringA().ToLower().Equals(unitName.ToLower()))
                    {
                        output.x = curAction.getPositionAX();
                        output.y = curAction.getPositionAY();
                        output.z = curAction.getPositionAZ();
                        return output;
                    }
                }
            }
            return output;
        }

        #region Trigger merge sort
        public static Trigger[] sortTriggersChrono(Trigger[] t)
        {
            return sortSplit(t);
        }

        private static Trigger[] sortSplit(Trigger[] t)
        {
            if (t == null || t.Length <= 1)
                return t;
            
            Trigger[] t1 = new Trigger[t.Length / 2];
            Trigger[] t2 = new Trigger[t.Length - t1.Length];

            int j = 0;
            for (int i = 0; i < t1.Length; i++)
                t1[i] = t[j++];           
            for (int i = 0; i < t2.Length; i++)
                t2[i] = t[j++];
            
            return sortMerge(sortSplit(t1), sortSplit(t2));
        }

        private static Trigger[] sortMerge(Trigger[] t1, Trigger[] t2)
        {
            Trigger[] output = new Trigger[t1.Length + t2.Length];
            int j = 0;
            int k = 0;
            for(int i = 0; i < output.Length; i++)
            {
                if (j < t1.Length)
                {
                    if (k < t2.Length)
                    {
                        if (t1[j].isBefore(t2[k]))
                            output[i] = t1[j++];
                        else
                            output[i] = t2[k++];                       
                    }
                    else
                    {
                        output[i] = t1[j++];
                    }
                }
                else
                {
                    output[i] = t2[k++];
                }
            }
            return output;
        }
        #endregion

       

        #endregion
    }
}
