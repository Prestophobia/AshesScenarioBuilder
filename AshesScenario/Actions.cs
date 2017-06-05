using System;

namespace AshesScenario
{
    /// <summary>
    /// An object representing an action that can be executed by AOTS triggered by the trigger object containing it
    /// </summary>
    public class Action
    {
        #region variables
        /// <summary>
        /// The action's index in its Trigger's action array
        /// </summary>
        public int index;

        /// <summary>
        /// The trigger the action is a part of
        /// </summary>
        public Trigger trig;

        /// <summary>
        /// The dialog entries belonging to the action(only usable by a dialog action)
        /// </summary>
        public DialogEntry[] entries;

        #endregion

        #region constructors
        /// <summary>
        /// Constructs an empty action belonging to a specified trigger
        /// </summary>
        /// <param name="t">The trigger this new action belongs to</param>
        public Action(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs an empty action unattached to anything
        /// </summary>
        public Action()
        {

        }

        #endregion

        /// <summary>
        /// Returns the action's most important attribute
        /// </summary>
        /// <returns>The action's most important attribute</returns>
        public virtual string getVal()
        {
            return "";
        }

        /// <summary>
        /// Retrieves a short summary of the action's most important data to be displayed by the UI
        /// </summary>
        /// <returns>A short summary of the action's most important data. Returns nothing by default</returns>
        public virtual string getSummary()
        {
            return "";
        }

        /// <summary>
        /// Produces a string that contains the action's data in an XML format readable by AOTS.
        /// </summary>
        /// <returns>A string containing the action's data in an XML format readable by AOTS. Returns an error message contained in an XML comment by default</returns>
        public virtual string toString()
        {
            return "<!--INVALID ACTION-->";
        }

        #region generic accessor methods
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="s">Unimplimented</param>
        public virtual void setStringA(string s)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="s">Unimplimented</param>
        public virtual void setStringB(string s)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="s">Unimplimented</param>
        public virtual void setStringC(string s)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="i">Unimplimented</param>
        public virtual void setIntA(int i)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="i">Unimplimented</param>
        public virtual void setIntB(int i)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="i">Unimplimented</param>
        public virtual void setIntC(int i)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="i">Unimplimented</param>
        public virtual void setIntD(int i)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="i">Unimplimented</param>
        public virtual void setIntE(int i)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="i">Unimplimented</param>
        public virtual void setIntF(int i)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="b">Unimplimented</param>
        public virtual void setBoolA(bool b)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="b">Unimplimented</param>
        public virtual void setBoolB(bool b)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="b">Unimplimented</param>
        public virtual void setBoolC(bool b)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="b">Unimplimented</param>
        public virtual void setBoolD(bool b)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="b">Unimplimented</param>
        public virtual void setBoolE(bool b)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="x">Unimplimented</param>
        public virtual void setPositionAX(float x)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="y">Unimplimented</param>
        public virtual void setPositionAY(float y)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="z">Unimplimented</param>
        public virtual void setPositionAZ(float z)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="x">Unimplimented</param>
        public virtual void setPositionBX(float x)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="y">Unimplimented</param>
        public virtual void setPositionBY(float y)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="z">Unimplimented</param>
        public virtual void setPositionBZ(float z)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="f">Unimplimented</param>
        public virtual void setFloatA(float f)
        {

        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <param name="f">Unimplimented</param>
        public virtual void setFloatB(float f)
        {

        }

        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>Empty String</returns>
        public virtual string getStringA()
        {
            return "";
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>Empty String</returns>
        public virtual string getStringB()
        {
            return "";
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>Empty String</returns>
        public virtual string getStringC()
        {
            return "";
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual int getIntA()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual int getIntB()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual int getIntC()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual int getIntD()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual int getIntE()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual int getIntF()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>false</returns>
        public virtual bool getBoolA()
        {
            return false;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>false</returns>
        public virtual bool getBoolB()
        {
            return false;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>false</returns>
        public virtual bool getBoolC()
        {
            return false;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>false</returns>
        public virtual bool getBoolD()
        {
            return false;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>false</returns>
        public virtual bool getBoolE()
        {
            return false;
        }
        
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual float getPositionAX()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual float getPositionAY()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual float getPositionAZ()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual float getPositionBX()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual float getPositionBY()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual float getPositionBZ()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual float getFloatA()
        {
            return 0;
        }
        /// <summary>
        /// Generic accessor function
        /// </summary>
        /// <returns>0</returns>
        public virtual float getFloatB()
        {
            return 0;
        }
        #endregion

        /// <summary>
        /// Generic accessor function
        /// </summary>

        #region public method
        public virtual void addEntryGeneral()
        {

        }
        /// <summary>
        /// Generic function to be overwritten in the spawnUnit action to support armies
        /// </summary>
        /// <returns>Always returns false</returns>
        public virtual bool isParent()
        {
            return false;
        }
        /// <summary>
        /// Generic function to be overwritten in the spawnUnit action to support armies
        /// </summary>
        /// <returns>Null</returns>
        public virtual Action[] getChildren()
        {
            return null;
        }
        /// <summary>
        /// Generic function to be overwritten in the spawnUnit action to support armies
        /// </summary>
        /// <returns>Always returns false</returns>
        public virtual bool hasParent()
        {
            return false;
        }
        /// <summary>
        /// Generic function to be overwritten in the spawnUnit action to support armies
        /// </summary>
        /// <returns>Always returns false</returns>
        public virtual bool hasChildren()
        {
            return false;
        }
        /// <summary>
        /// Creates an identical object located in a separate location in memory
        /// </summary>
        /// <returns>An identical object located in a separate location in memory</returns>
        #endregion
        //TODO: Optimize clone()
        public Action clone()
        {
            Action output = new Action(trig);

            Action[] actionTypes = new Action[]{new ActivateTrigger(trig), new Dialog(trig), new Camera(trig), new Reveal(trig),
                new AreaIndicator(trig), new Objective(trig), new Restrict(trig), new SpawnUnit(trig), new DestroyUnit(trig), new AttackAttackMove(trig),
                new SpawnBuilding(trig), new DestroyBuilding(trig), new LetterBox(trig), new HidePanel(trig), new GrantStuff(trig), new GrantTech(trig),
                new CaptureNearest(trig), new ToggleAI(trig), new EndMission(trig), new Select(trig), new Notifications(trig), new Highlight(trig), new ActivateAI(trig),
                new PlaySound(trig), new Pause(trig), new ChangeAIPersonality(trig), new ChangeAIDifficulty(trig)};
            for (int i = 0; i < actionTypes.Length; i++)
            {
                if (this.GetType() == actionTypes[i].GetType())
                {
                    output = actionTypes[i];
                    output.setBoolA(getBoolA());
                    output.setBoolB(getBoolB());
                    output.setBoolC(getBoolC());
                    output.setBoolD(getBoolD());
                    output.setBoolE(getBoolE());

                    output.setFloatA(output.getFloatA());
                    output.setFloatB(output.getFloatB());

                    output.setIntA(output.getIntA());
                    output.setIntB(output.getIntB());
                    output.setIntC(output.getIntC());
                    output.setIntD(output.getIntD());
                    output.setIntE(output.getIntE());
                    output.setIntF(output.getIntF());

                    output.setPositionAX(output.getPositionAX());
                    output.setPositionAY(output.getPositionAY());
                    output.setPositionAZ(output.getPositionAZ());

                    output.setPositionBX(output.getPositionBX());
                    output.setPositionBY(output.getPositionBY());
                    output.setPositionBZ(output.getPositionBZ());

                    output.setStringA(output.getStringA());
                    output.setStringB(output.getStringB());
                    output.setStringC(output.getStringC());
                }
            }
            return output;
        }
    }

    #region action types

    /// <summary>
    /// An action that activates a trigger
    /// </summary>
    public class ActivateTrigger : Action
    {
        /// <summary>
        /// The trigger being activated by this action
        /// </summary>
        public string target;

        /// <summary>
        /// Gets the value for the target trigger
        /// </summary>
        /// <returns>The targeted trigger activated by this action</returns>
        public override string getStringA()
        {
            return target;
        }
        /// <summary>
        /// Trigger to be activated by ths action
        /// </summary>
        /// <param name="s">The target trigger</param>
        public override void setStringA(string s)
        {
            target = s;
        }
        /// <summary>
        /// Constructs an ActivateTrigger action that belongs to a specified Trigger
        /// </summary>
        /// <param name="t">The trigger this new action belongs to</param>
        public ActivateTrigger(Trigger t)
        {
            trig = t;
            target = "";
        }
        /// <summary>
        /// Constructs a new ActivateTrigger action belonging to a specified trigger with a specified target
        /// </summary>
        /// <param name="t">The trigger this action is to belong to</param>
        /// <param name="Target">The trigger to be activated by this action</param>
        public ActivateTrigger(Trigger t, string Target)
        {
            trig = t;
            target = Target;
        }
        /// <summary>
        /// Constructs an ActivateTrigger action with a specified target trigger
        /// </summary>
        /// <param name="targetTrig">The trigger to be activated by the constructed action</param>
        public ActivateTrigger(string targetTrig)
        {
            target = targetTrig;
        }
        /// <summary>
        /// Gives an XML element for an ActivateTrigger action with the given target
        /// </summary>
        /// <returns>An XML element for an ActivateTrigger action with the given target</returns>
        public override string toString()
        {
            return "<ActivateTrigger Target=\"" + target + "\"/>";
        }
        /// <summary>
        /// Duplicate of getStringA()
        /// </summary>
        /// <returns>The target Trigger</returns>
        public override string getVal()
        {
            return target;
        }

        /// <summary>
        /// Produces a string stating the action's type and target
        /// </summary>
        /// <returns>a string stating the action's type and target</returns>
        public override string getSummary()
        {
            return "ActivateTrigger (" + target+ ")";
        }
    }
    /// <summary>
    /// An action that creates a dialogue sequence. 
    /// The object contains an array of DialogEntries, which contain the VO, portraits, and subtitles
    /// </summary>
    public class Dialog : Action
    {    
        /// <summary>
        /// Constructs an empty Dialog action belonging to the specified Trigger
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public Dialog(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs a Dialog action belonging to the specified Trigger containing the provided DialogEntries
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="DialogEntries">The DialogueEntries to be contained by the new Dialog action</param>
        public Dialog(Trigger t, DialogEntry[] DialogEntries)
        {
            trig = t;
            entries = DialogEntries;
        }
        /// <summary>
        /// Updates the internal indexes of the DialogueEntries to be consistent with their index in the array
        /// </summary>
        public void updateIndexes()
        {
            if (entries != null)
            {
                for (int i = 0; i < entries.Length; i++)
                {
                    entries[i].index = i;
                }
            }
        }
        /// <summary>
        /// Adds a specifed DialogEntry to the end of the array
        /// </summary>
        /// <param name="dE">DialogEntry to be added to the end of the array</param>
        public void addEntry(DialogEntry dE)
        {
            if (entries == null)
            {
                entries = new DialogEntry[1];
                entries[0] = dE;
            }
            else
            {
                DialogEntry[] temp = new DialogEntry[entries.Length + 1];
                entries.CopyTo(temp, 0);
                temp[temp.Length - 1] = dE;
                entries = temp;
            }
            updateIndexes();
            //trig.scen.mainWindow.updateControlsFromScenario(trig.scen);
        }
        /// <summary>
        /// Adds an empty DialogEntry to the end of the array
        /// </summary>
        public void addEntry()
        {
            if (entries == null)
            {
                entries = new DialogEntry[1];
                entries[0] = new DialogEntry(this);
            }
            else
            {
                DialogEntry[] temp = new DialogEntry[entries.Length + 1];
                entries.CopyTo(temp, 0);
                temp[temp.Length - 1] = new DialogEntry(this);
                entries = temp;
            }
            updateIndexes();
            //trig.scen.mainWindow.updateControlsFromScenario(trig.scen);
        }
        /// <summary>
        /// Removes a DialogEntry from the specified index
        /// </summary>
        /// <param name="entryIndex">Index of the DialogEntry to be removed</param>
        public void removeEntry(int entryIndex)
        {
            if (entryIndex < 0 || entryIndex >= entries.Length)
            {
                return;
            }
            DialogEntry[] temp = new DialogEntry[entries.Length - 1];
            for (int i = 0; i < entryIndex; i++)
            {
                temp[i] = entries[i];
            }
            for (int i = entryIndex; i < temp.Length; i++)
            {
                temp[i] = entries[i + 1];
            }
            //entries[entryIndex].destroy();
            entries = temp;
            updateIndexes();
            trig.scen.mainWindow.updateControlsFromScenario(trig.scen);
        }
        /// <summary>
        /// Calls the toString() function of its DialogEntries to construct an XML-compliant string containing the dialogue sequence
        /// </summary>
        /// <returns>An XML-compliant string representing the Dialog Action</returns>
        public override string toString()
        {
            if (entries == null)
            {
                return "\n";
            }
            string output;
            output = "<Dialog>";
            for (int i = 0; i < entries.Length; i++)
            {
                output += entries[i].toString();
            }
            output += "\n\t\t</Dialog>";
            return output;
        }
        /// <summary>
        /// Makes addEntry() more accessible by functions calling the Dialog object's superclass
        /// </summary>
        public override void addEntryGeneral()
        {
            addEntry();
        }
        /// <summary>
        /// Returns a string containing the action type and its DialogEntries' characters
        /// </summary>
        /// <returns>A string containing the action type and the characters in the dialogue sequence</returns>
        public override string getSummary()
        {
            string output = "Dialog";
            if (entries != null)
            {
                string[] actors = new string[0];
                for (int i = 0; i < entries.Length; i++)
                {
                    bool newGuy = true;
                    for (int z = 0; z < actors.Length; z++)
                    {
                        if (actors[z].Equals(entries[i].icon))
                        {
                            newGuy = false;
                        }
                    }
                    if (newGuy)
                    {
                        string[] temp = new string[actors.Length + 1];
                        for (int z = 0; z < actors.Length; z++)
                        {
                            temp[z] = actors[z];
                        }
                        temp[actors.Length] = entries[i].icon;
                    }
                    
                }
                for (int z = 0; z < actors.Length; z++)
                {
                    output+=" "+ actors[z];
                }
            }
            return output;
        }
    }
    /// <summary>
    /// A sub-action belonging to a Dialog action containing the portrait, subtitle key, and VO audio file
    /// </summary>
    public class DialogEntry : Action
    {
        /// <summary>
        /// The data contained within the action representing the icon, UI text, and audio file path
        /// </summary>
        public string icon, text, audio;
        /// <summary>
        /// The Dialog action this entry belongs to
        /// </summary>
        public Dialog dialogParent;
        /// <summary>
        /// Constructs an empty DialogEntry belonging to a specified Dialog action. The icon is Mac by default.
        /// </summary>
        /// <param name="parent">The Dialog Action containing the dialogue sequence in which this entry is a line in</param>
        public DialogEntry(Dialog parent)
        {
            dialogParent = parent;
            icon = "Mac";
            trig = parent.trig;     
        }
        /// <summary>
        /// Constructs a complete DialogEntry with a specified Dialog action, icon, UI text, and audio file path.
        /// </summary>
        /// <param name="parent">The Dialog Action containing the dialogue sequence in which this entry is a line in</param>
        /// <param name="characterPortrait">The UI icon for the speaker's portrait</param>
        /// <param name="textInternal">The CSV data table key for the subtitles</param>
        /// <param name="audioPath">The path for the audio file for the spoken dialogue</param>
        public DialogEntry(Dialog parent, string characterPortrait, string textInternal, string audioPath)
        {
            dialogParent = parent;
            icon = characterPortrait;
            text = textInternal;
            audio = audioPath;
            trig = parent.trig;        
        }

        /// <summary>
        /// Produces a string containing the data for the DialogEntry in an XML format readable by the AOTS scenario system.
        /// This function is to be use by the Dialog object's toString() function.
        /// </summary>
        /// <returns>A string containing the data for the DialogEntry in an XML format readable by the AOTS scenario system</returns>
        public override string toString()
        {
            if (audio != null && audio != "")
                return "\n\t\t\t<Entry Icon=\"" + icon + "\" Text=\"" + text + "\" Audio=\"" + audio + "\"/>";
            else
                return "\n\t\t\t<Entry Icon=\"" + icon + "\" Text=\"" + text + "\"/>";
        }
       
    }
    /// <summary>
    /// An action that either saves, loads, or moves the position and angle of the in-game camera.
    /// </summary>
    public class Camera : Action
    {
        /// <summary>
        /// Toggles whether this action is to save the camera position
        /// </summary>
        public bool save;
        /// <summary>
        /// Toggles whether this action is to load the camera position
        /// </summary>
        public bool load; 
        /// <summary>
        /// Toggles whether this action is to move the camera position
        /// </summary>
        public bool rtpE, posE;
        /// <summary>
        /// Toggles whether camera movement pans or cuts to a new position
        /// </summary>
        public bool snap;
        /// <summary>
        /// Describes the new camera position
        /// </summary>
        public Position pos, rtp;
        /// <summary>
        /// The panning speed of the camera movement
        /// </summary>
        public float speed;

        /// <summary>
        /// Returns the Camera action's save property
        /// </summary>
        /// <returns>Camera.save</returns>
        public override bool getBoolA()
        {
            return save;
        }
        /// <summary>
        /// Sets the Camera action's save property
        /// </summary>
        /// <param name="b">The new value for Camera.save</param>
        public override void setBoolA(bool b)
        {
            save = b;
        }
        /// <summary>
        /// Returns the Camera action's load property
        /// </summary>
        /// <returns>Camera.load</returns>
        public override bool getBoolB()
        {
            return load;
        }
        /// <summary>
        /// Sets the Camera action's load property
        /// </summary>
        /// <param name="b">The new value for Camera.load</param>
        public override void setBoolB(bool b)
        {
            load = b;
        }
        /// <summary>
        /// Returns the Camera object's rotation enable value
        /// </summary>
        /// <returns>Camera.rtpE</returns>
        public override bool getBoolC()
        {
            return rtpE;
        }
        /// <summary>
        /// Enables or disables the Camera object's rotation
        /// </summary>
        /// <param name="b">The new value for Camera.rtpE</param>
        public override void setBoolC(bool b)
        {
            rtpE=b;
        }
        /// <summary>
        /// Returns the Camera object's position enable value
        /// </summary>
        /// <returns>Camera.posE</returns>
        public override bool getBoolD()
        {
            return posE;
        }
        /// <summary>
        /// Enables or disables the Camera object's position
        /// </summary>
        /// <param name="b">The new value for Camera.posE</param>
        public override void setBoolD(bool b)
        {
            posE=b;
        }
        /// <summary>
        /// Returns the value for the Camera.snap
        /// </summary>
        /// <returns>Camera.snap</returns>
        public override bool getBoolE()
        {
            return snap;
        }
        /// <summary>
        /// Sets whether or not the camera snaps to its new position
        /// </summary>
        /// <param name="b">The new value for Camera.snap</param>
        public override void setBoolE(bool b)
        {
            snap = b;
        }

        /// <summary>
        /// Gets the X coordinate of the camera's destination position
        /// </summary>
        /// <returns>Camera.pos.x</returns>
        public override float getPositionAX()
        {
            return pos.x;
        }
        /// <summary>
        /// Sets the X coordinate of the camera's destination
        /// </summary>
        /// <param name="x">The new value of Camera.pos.x</param>
        public override void setPositionAX(float x)
        {
            pos.x=x;
        }
        /// <summary>
        /// Gets the Y coordinate of the camera's destination position
        /// </summary>
        /// <returns>Camera.pos.y</returns>
        public override float getPositionAY()
        {
            return pos.y;
        }
        /// <summary>
        /// Sets the Y coordinate of the camera's destination
        /// </summary>
        /// <param name="y">The new value of Camera.pos.y</param>
        public override void setPositionAY(float y)
        {
            pos.y = y;
        }
        /// <summary>
        /// Gets the Z coordinate of the camera's destination position
        /// </summary>
        /// <returns>Camera.pos.z</returns>
        public override float getPositionAZ()
        {
            return pos.z;
        }
        /// <summary>
        /// Sets the Z coordinate of the camera's destination
        /// </summary>
        /// <param name="z">The new value of Camera.pos.z</param>
        public override void setPositionAZ(float z)
        {
            pos.z = z;
        }

        /// <summary>
        /// Gets the X euler angle of the camera's destination rotation
        /// </summary>
        /// <returns>Camera.rtp.x</returns>
        public override float getPositionBX()
        {
            return rtp.x;
        }
        /// <summary>
        /// Sets the X euler angle of the camera's destination rotation
        /// </summary>
        /// <param name="x">The new value of Camera.rtp.x</param>
        public override void setPositionBX(float x)
        {
            rtp.x = x;
        }
        /// <summary>
        /// Gets the Y euler angle of the camera's destination rotation
        /// </summary>
        /// <returns>Camera.rtp.y</returns>
        public override float getPositionBY()
        {
            return rtp.y;
        }
        /// <summary>
        /// Sets the Y euler angle of the camera's destination rotation
        /// </summary>
        /// <param name="y">The new value of Camera.rtp.y</param>
        public override void setPositionBY(float y)
        {
            rtp.y = y;
        }
        /// <summary>
        /// Gets the Z euler angle of the camera's destination rotation
        /// </summary>
        /// <returns>Camera.rtp.z</returns>
        public override float getPositionBZ()
        {
            return rtp.z;
        }
        /// <summary>
        /// Sets the Z euler angle of the camera's destination rotation
        /// </summary>
        /// <param name="z">The new value of Camera.rtp.z</param>
        public override void setPositionBZ(float z)
        {
            rtp.z = z;
        }

        /// <summary>
        /// Returns the camera movement's speed
        /// </summary>
        /// <returns>Camera.speed</returns>
        public override float getFloatA()
        {
            return speed;
        }
        /// <summary>
        /// Sets the camera movement's speed
        /// </summary>
        /// <param name="f">The new value for Camera.speed</param>
        public override void setFloatA(float f)
        {
            speed = f;
        }

        /// <summary>
        /// Creates an empty Camera action belonging to a specified Trigger
        /// </summary>
        /// <param name="t">The trigger this action is to belong to</param>
        public Camera(Trigger t)
        {
            save = load = false;
            trig = t;
            pos = new Position();
            rtp = new Position();          
        }
        /// <summary>
        /// Creates a complete Camera action with a specified save, load, position, rotation, and speed value belonging to a given Trigger and snap set to false.
        /// </summary>
        /// <param name="t">The trigger this action is to belong to</param>
        /// <param name="isSave">Sets if this action saves the camera position</param>
        /// <param name="isLoad">Sets if this action loads the last saved camera position</param>
        /// <param name="cameraPosition">Sets the camera movement's destination</param>
        /// <param name="cameraRTP">Sets the camera movement's target rotation</param>
        /// <param name="cameraSpeed">Sets the speed of the camera movement</param>
        public Camera(Trigger t, bool isSave, bool isLoad, Position cameraPosition, Position cameraRTP, float cameraSpeed)
        {
            trig = t;

            save = isSave;
            load = isLoad;

            if (save)
            {
                load = false;
            }

            if (cameraPosition == null)
            {
                pos = new Position();
                posE = false;
            }
            else
            {
                pos = cameraPosition;
                posE = true;
            }

            if (cameraRTP == null)
            {
                rtp = new Position();
                rtpE = false;
            }
            else
            {
                rtp = cameraRTP;
                rtpE = true;
            }

            speed = cameraSpeed;

            
        }
        /// <summary>
        /// Creates a complete Camera action with a specified save, load, snap, position, rotation, and speed value belonging to a given Trigger.
        /// </summary>
        /// <param name="t">The trigger this action is to belong to</param>
        /// <param name="isSave">Sets if this action saves the camera position</param>
        /// <param name="isLoad">Sets if this action loads the last saved camera position</param>
        /// <param name="cameraPosition">Sets the camera movement's destination</param>
        /// <param name="cameraRTP">Sets the camera movement's target rotation</param>
        /// <param name="cameraSpeed">Sets the speed of the camera movement</param>
        /// <param name="s">Sets if the camera movement snaps to the destination or if it pans</param>
        public Camera(Trigger t, bool isSave, bool isLoad, Position cameraPosition, Position cameraRTP, float cameraSpeed, bool s)
        {
            trig = t;
            snap = s;
            save = isSave;
            load = isLoad;

            if (save)
            {
                load = false;
            }

            if (cameraPosition == null)
            {
                pos = new Position();
                posE = false;
            }
            else
            {
                pos = cameraPosition;
                posE = true;
            }

            if (cameraRTP == null)
            {
                rtp = new Position();
                rtpE = false;
            }
            else
            {
                rtp = cameraRTP;
                rtpE = true;
            }

            speed = cameraSpeed;

        }

        /// <summary>
        /// Produces a string containing the data for the Camera Action that is in an XML format readable by the AOTS scenario system
        /// </summary>
        /// <returns>A string containing the data for the Camera Action that is in an XML format readable by the AOTS scenario system</returns>
        public override string toString()
        {
            string output = "<Camera";
            if (snap)
            {
                output += " Snap=\"1\"";
            }
            if (save)
            {
                return output + " Save=\"1\"/>";
            }
            if (load)
            {
                return output + " Load=\"1\"/>";
            }
            if (posE)
            {
                output += " Position=\"" + pos.x + "," + pos.y + "\"";
            }
            if (rtpE)
            {
                output += " RTP=\"" + rtp.x + "," + rtp.y + "," + rtp.z + "\"";
            }

            
            return output + " Speed=\"" + speed + "\"/>";

        }
        /// <summary>
        /// Produces a string stating whether this Camera action saves, loads, or alters the camera position
        /// </summary>
        /// <returns>A string stating whether this Camera action saves, loads, or alters the camera position</returns>
        public override string getSummary()
        {
            if (save)
            {
                return "Camera(Save)";
            }
            else if (load)
            {
                return "Camera(Load)";
            }
            else
            {
                return "Camera(Position Change)";
            }
        }
    }
    /// <summary>
    /// An action that changes the visibility of an area to the player
    /// </summary>
    public class Reveal : Action
    {
        /// <summary>
        /// Arbitrary name for the region you’re revealing
        /// </summary>
        public string name;
        /// <summary>
        /// Position on the map to be revealed
        /// </summary>
        public Position pos;
        /// <summary>
        /// Size of the area to be revealed
        /// </summary>
        public float size, radarSize;
        /// <summary>
        /// Toggles if you're making an area visible or invisible
        /// </summary>
        public bool enable;

        /// <summary>
        /// Gets the arbitrary internal name given to the revealed area
        /// </summary>
        /// <returns>The arbitrary internal name given to the revealed area</returns>
        public override string getStringA()
        {
            return name;
        }
        /// <summary>
        /// Renames the revealed area
        /// </summary>
        /// <param name="s">The new name for the revealed area</param>
        public override void setStringA(string s)
        {
            name=s;
        }

        /// <summary>
        /// Gets the X coordinate of the position of center of the revealed area 
        /// </summary>
        /// <returns>Reveal.pos.x</returns>
        public override float getPositionAX()
        {
            return pos.x;
        }
        /// <summary>
        /// Sets the X coordinate of the position of center of the revealed area 
        /// </summary>
        /// <param name="x">The new value for Reveal.pos.x</param>
        public override void setPositionAX(float x)
        {
            pos.x = x;
        }
        /// <summary>
        /// Gets the Y coordinate of the position of center of the revealed area 
        /// </summary>
        /// <returns>Reveal.pos.y</returns>
        public override float getPositionAY()
        {
            return pos.y;
        }
        /// <summary>
        /// Sets the Y coordinate of the position of center of the revealed area 
        /// </summary>
        /// <param name="y">The new value for Reveal.pos.y</param>
        public override void setPositionAY(float y)
        {
            pos.y = y;
        }
        /// <summary>
        /// Gets the Z coordinate of the position of center of the revealed area 
        /// </summary>
        /// <returns>Reveal.pos.z</returns>
        public override float getPositionAZ()
        {
            return pos.z;
        }
        /// <summary>
        /// Sets the Z coordinate of the position of center of the revealed area 
        /// </summary>
        /// <param name="z">The new value for Reveal.pos.z</param>
        public override void setPositionAZ(float z)
        {
            pos.z = z;
        }

        /// <summary>
        /// Gets the radius of the affected area
        /// </summary>
        /// <returns>The radius of the affected area</returns>
        public override float getFloatA()
        {
            return size;
        }
        /// <summary>
        /// Sets the radius of the affected area
        /// </summary>
        /// <param name="f">The new radius for the affected area</param>
        public override void setFloatA(float f)
        {
            size=f;
        }
        /// <summary>
        /// Gets the radius of the revealed area visible by radar only
        /// </summary>
        /// <returns>The radius of the revealed area visible by radar only</returns>
        public override float getFloatB()
        {
            return radarSize;
        }
        /// <summary>
        /// Sets the radius of the revealed area visible by radar only
        /// </summary>
        /// <param name="f">The new radar radius</param>
        public override void setFloatB(float f)
        {
            radarSize = f;
        }

        /// <summary>
        /// Gets whether the Reveal action reveals or hides an area. True if it reveals, false if it hides.
        /// </summary>
        /// <returns>Reveal.enable</returns>
        public override bool getBoolA()
        {
            return enable;
        }
        /// <summary>
        /// Sets whether the Reveal action reveals or hides an area. True if it reveals, false if it hides.
        /// </summary>
        /// <param name="b">The new value for Reveal.enable</param>
        public override void setBoolA(bool b)
        {
            enable = b;
        }

        /// <summary>
        /// Constructs an empty Reveal action belonging to a specified Trigger
        /// </summary>
        /// <param name="t">The Trigger the action is to belong to</param>
        public Reveal(Trigger t)
        {
            pos = new Position();
            trig = t;
        }
        /// <summary>
        /// Constructs a complete Reveal action with a specified name, position, size, radar radius,
        /// and value determining if it reveals or hides belonging to a given Trigger.
        /// </summary>
        /// <param name="t">The Trigger this Reveal action is to belong to</param>
        /// <param name="revealName">The arbitrary internal name for the revealed position</param>
        /// <param name="revealPosition">The coordinates of the center of the revealed area</param>
        /// <param name="revealSize">The radius of the revealed area</param>
        /// <param name="radarRadius">The radius of the revealed area visible by radar only</param>
        /// <param name="revealNotHide">Sets if the reveal action reveals or hides an area</param>
        public Reveal(Trigger t, string revealName, Position revealPosition, float revealSize, float radarRadius, bool revealNotHide)
        {
            pos = revealPosition;
            name = revealName;
            size = revealSize;
            radarSize = radarRadius;
            enable = revealNotHide;
            trig = t;

        }

        /// <summary>
        /// Prouces a string encoding the Action's data in an XML format readable by the AOTS scenario system
        /// </summary>
        /// <returns>A string encoding the Action's data in an XML format readable by the AOTS scenario system</returns>
        public override string toString()
        {
            int temp = 0;
            if (enable)
            {
                temp = 1;
            }
            return "<Reveal Name=\"" + name + "\" Position=\"" + pos.x + "," + pos.y + "," + "\" Size=\"" + size + "\" RadarSize=\"" + radarSize + "\" Enable=\"" + temp + "\"/>";
        }

        /// <summary>
        /// Produces a string stating the action type and the internal name for the revealed location
        /// </summary>
        /// <returns>String stating the action type and the internal name for the revealed location</returns>
        public override string getSummary()
        {
            return "Reveal(" + name + ")";
        }
    }
    /// <summary>
    /// An action that either creates or removes a circular UI indicator over an area to draw the player's attention
    /// </summary>
    public class AreaIndicator : Action
    {
        /// <summary>
        /// An arbitrary internal name for the area indicator that may be referenced by later actions
        /// </summary>
        public string name;
        /// <summary>
        /// The color of the Area Indicator. Valid colors include: red, yellow, green, cyan, blue
        /// </summary>
        public string color;
        /// <summary>
        /// The coordinate data for the center of the area indicator on the map
        /// </summary>
        public Position pos;
        /// <summary>
        /// The radius of the area indicator
        /// </summary>
        public float size;
        /// <summary>
        /// A value determining if you are activating or deactivating an Area Indicator
        /// EX: You can create an Area Indicator in one action with on=true and then deactivate it with a later action
        /// in which the name is the same and on=false
        /// </summary>
        public bool on;

        /// <summary>
        /// Gets the internal name for the area indicator
        /// </summary>
        /// <returns>The internal name for the area indicator</returns>
        public override string getStringA()
        {
            return name;
        }
        /// <summary>
        /// Sets the internal name for the area indicator
        /// </summary>
        /// <param name="s">The new name for the area indicator</param>
        public override void setStringA(string s)
        {
            name = s;
        }
        /// <summary>
        /// Gets the color of the Area Indicator
        /// </summary>
        /// <returns>The color of the Area Indicator</returns>
        public override string getStringB()
        {
            return color;
        }
        /// <summary>
        /// Sets the color of the Area Indicator
        /// </summary>
        /// <param name="s">The new color of the Area Indicator</param>
        public override void setStringB(string s)
        {
            color = s;
        }

        /// <summary>
        /// Gets the X coordinate of the center of the AreaIndicator 
        /// </summary>
        /// <returns>AreaIndicator.pos.x</returns>
        public override float getPositionAX()
        {
            return pos.x;
        }
        /// <summary>
        /// Sets the X coordinate of the center of the AreaIndicator 
        /// </summary>
        /// <param name="x">The new value for AreaIndicator.pos.x</param>
        public override void setPositionAX(float x)
        {
            pos.x=x;
        }
        /// <summary>
        /// Gets the Y coordinate of the center of the AreaIndicator 
        /// </summary>
        /// <returns>AreaIndicator.pos.y</returns>
        public override float getPositionAY()
        {
            return pos.y;
        }
        /// <summary>
        /// Sets the Y coordinate of the center of the AreaIndicator 
        /// </summary>
        /// <param name="y">The new value for AreaIndicator.pos.y</param>
        public override void setPositionAY(float y)
        {
            pos.y = y;
        }

        /// <summary>
        /// Gets the radius of the Area Indicator
        /// </summary>
        /// <returns>AreaIndicator.size</returns>
        public override float getFloatA()
        {
            return size;
        }
        /// <summary>
        /// Sets the radius of the Area Indicator
        /// </summary>
        /// <param name="f">The new value for AreaIndicator.size</param>
        public override void setFloatA(float f)
        {
            size=f;
        }

        /// <summary>
        /// Gets whether the action creates an Area Indicator or if it deactivates one
        /// </summary>
        /// <returns>True if the action creates an Area Indicator, false if it deactivates one</returns>
        public override bool getBoolA()
        {
            return on;
        }
        /// <summary>
        /// Sets whether the action creates an Area Indicator or if it deactivates one
        /// </summary>
        /// <param name="b">A bool that is true if the action creates an Area Indicator, false if it deactivates one</param>
        public override void setBoolA(bool b)
        {
            on=b;
        }

        /// <summary>
        /// Constructs a blank, ineffective Area Indicator belonging to a specified Trigger
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public AreaIndicator(Trigger t)
        {
            trig = t;
            pos = new Position();
        }
        /// <summary>
        /// Constructs a complete AreaIndicator action containing a specified name, color, size, and position belonging to a given Trigger that may activate or deactivate an Area Indicator.
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="indicatorName">The arbitrary internal name for the area indicator that may be referenced by later actions</param>
        /// <param name="indicatorColor">The color of the Area Indicator</param>
        /// <param name="indicatorSize">The radius of the Area Indicator</param>
        /// <param name="indicatorPosition">The coordinates of the center of the Area Indicator</param>
        /// <param name="duration">If duration is true, an Area Indicator is turned on. Otherwise, one is turned off</param>
        public AreaIndicator(Trigger t, string indicatorName, string indicatorColor, int indicatorSize, Position indicatorPosition, bool duration)
        {
            trig = t;
            name = indicatorName;
            color = indicatorColor;
            pos = indicatorPosition;
            size = indicatorSize;
            on = duration;
        }

        /// <summary>
        /// Produces a string encoding the Area Indicator's data in an XML format that can be read by the AOTS scenario system
        /// </summary>
        /// <returns>A string encoding the Area Indicator's data in an XML format that can be read by the AOTS scenario system</returns>
        public override string toString()
        {
            if (!on)
            {
                return "<AreaIndicator Name=\"" + name + "\" Color=\"" + color + "\" Size=\"" + size + "\" Position=\"" + pos.x + "," + pos.y + "," + "\" Duration=\"0\"/>";
            }
            return "<AreaIndicator Name=\"" + name + "\" Color=\"" + color + "\" Size=\"" + size + "\" Position=\"" + pos.x + "," + pos.y + "," + "\"/>";
        }
        /// <summary>
        /// Returns a string containing the action type and the internal name of the area indicator
        /// </summary>
        /// <returns>A string containing the action type and the internal name of the area indicator</returns>
        public override string getSummary()
        {
            return "AreaIndicator(" + name + ")";
        }
    }
    /// <summary>
    /// An action that adds or checks off an objective to the list of objectives in the UI
    /// </summary>
    public class Objective : Action
    {
        /// <summary>
        /// Arbitrary internal name given to the objective that may be referenced by future actions that can check it
        /// </summary>
        public string name;
        /// <summary>
        /// The key that corresponds with a key in the CSV data table that is paired with the UI text to be displayed in the objectives UI
        /// </summary>
        public string textString;
        /// <summary>
        /// The status of the checkbox in the objectives list(true if checked, false if not)
        /// </summary>
        public bool setCheck;

        /// <summary>
        /// Gets the internal name given to the objective
        /// </summary>
        /// <returns>Objective.name</returns>
        public override string getStringA()
        {
            return name;
        }
        /// <summary>
        /// Renames the objective(internal)
        /// </summary>
        /// <param name="s">The new value for Objective.name</param>
        public override void setStringA(string s)
        {
            name=s;
        }
        /// <summary>
        /// Gets the key that corresponds with a key in the CSV data table that is paired with the UI text to be displayed in the objectives UI
        /// </summary>
        /// <returns>Objective.textString</returns>
        public override string getStringB()
        {
            return textString;
        }
        /// <summary>
        /// Sets the key that corresponds with a key in the CSV data table that is paired with the UI text to be displayed in the objectives UI
        /// </summary>
        /// <param name="s">The new value for Objective.textString</param>
        public override void setStringB(string s)
        {
            textString = s;
        }

        /// <summary>
        /// Gets the status of the checkbox in the objectives list(true if checked, false if not)
        /// </summary>
        /// <returns>Objective.setCheck</returns>
        public override bool getBoolA()
        {
            return setCheck;
        }
        /// <summary>
        /// Sets the status of the checkbox in the objectives list(true if checked, false if not)
        /// </summary>
        /// <param name="b">The new value for Objective.setCheck</param>
        public override void setBoolA(bool b)
        {
            setCheck = b;
        }

        /// <summary>
        /// Constructs an empty Objective action that belongs to a specified Trigger
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public Objective(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs a complete Objective action with a specified internal name, UI text key, and checkbox status;
        /// belonging to a specified Trigger
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="objName">Internal name given to the objective that may be referenced by future actions</param>
        /// <param name="objString">Key that corresponds with a key in the CSV data table that is paired with the UI text to be displayed in the objectives UI</param>
        /// <param name="objCheck">The status of the checkbox in the objectives list(true if checked, false if not)</param>
        public Objective(Trigger t, string objName, string objString, bool objCheck)
        {
            trig = t;
            name = objName;
            textString = objString;
            setCheck = objCheck;
        }

        /// <summary>
        /// Produces a string encoding the objective's data in an XML format that can be read by the AOTS scenario system.
        /// Can either provide a checked status or a String, but never initializes a checked objective
        /// </summary>
        /// <returns>A string encoding the objective's data in an XML format that can be read by the AOTS scenario system</returns>
        public override string toString()
        {
            if (setCheck)
            {
                int temp = 0;
                if (setCheck)
                {
                    temp = 1;
                }
                return "<Objective Name=\"" + name + "\" SetCheck=\"" + temp + "\"/>";
            }
            return "<Objective Name=\"" + name + "\" String=\"" + textString + "\"/>";
        }

        /// <summary>
        /// Produces a string containing the action type and the internal name of the objective
        /// </summary>
        /// <returns>A string containing the action type and the internal name of the objective</returns>
        public override string getSummary()
        {
            return "Objective(" + name + ")";
        }
    }
    /// <summary>
    /// An action that either enables or disables the player's use of a specified unit, building, or ability
    /// </summary>
    public class Restrict : Action
    {
        /// <summary>
        /// The type of resource being restricted (or unrestricted).
        /// Valid values include: Orbital(Orbital Ability), Unit, Building, Research(Quantum Upgrade)
        /// </summary>
        public string type;
        /// <summary>
        /// Internal(in the Ashes code) name of the Orbital Ability, unit, building, or Quantum Upgrade being restricted
        /// </summary>
        public string id;
        /// <summary>
        /// True if unrestricted, false if restricted
        /// </summary>
        public bool enable;

        /// <summary>
        /// Gets the type of resource being restricted
        /// </summary>
        /// <returns>Restrict.type</returns>
        public override string getStringA()
        {
            return type;
        }
        /// <summary>
        /// Sets the type of resource being restricted
        /// </summary>
        /// <param name="s">The new value for Restrict.type</param>
        public override void setStringA(string s)
        {
            type = s;
        }
        
        /// <summary>
        /// Gets the internal(in the Ashes code) name of the Orbital Ability, unit, building, or Quantum Upgrade being restricted
        /// </summary>
        /// <returns>Restrict.id</returns>
        public override string getStringB()
        {
            return id;
        }
        /// <summary>
        /// Sets the internal(in the Ashes code) name of the Orbital Ability, unit, building, or Quantum Upgrade being restricted
        /// </summary>
        /// <param name="s">The new value for Restrict.id</param>
        public override void setStringB(string s)
        {
            id = s;
        }

        /// <summary>
        /// Gets the Restrict action's restriction status(true if unrestricted, false if restricted)
        /// </summary>
        /// <returns>True if unrestricted, false if restricted</returns>
        public override bool getBoolA()
        {
            return enable;
        }
        /// <summary>
        /// Sets the Restrict action's restriction status
        /// </summary>
        /// <param name="b">The new restriction status(true if unrestricted, false if restricted)</param>
        public override void setBoolA(bool b)
        {
            enable = b;
        }

        /// <summary>
        /// Constructs an empty, ineffective Restrict action belonging to a specified Trigger
        /// </summary>
        /// <param name="t">The Trigger the action is to belong to</param>
        public Restrict(Trigger t)
        {
            trig = t;
        }

        /// <summary>
        /// Constructs a complete Restrict action with a specified type, ID, and restriction status, belonging to a given Trigger
        /// </summary>
        /// <param name="t">The Trigger the action is to belong to</param>
        /// <param name="restrictType">The type of resource being restricted (or unrestricted)</param>
        /// <param name="restrictID">Internal name of the Orbital Ability, unit, building, or Quantum Upgrade being restricted</param>
        /// <param name="unrestricted">A bool that is true if the resource is to be unrestricted, false if restricted</param>
        public Restrict(Trigger t, string restrictType, string restrictID, bool unrestricted)
        {
            trig = t;
            type = restrictType;
            id = restrictID;
            enable = unrestricted;
        }

        /// <summary>
        /// Produces a string encoding the Restricts's type, ID, and enable status in an XML format that can be read by the AOTS scenario system
        /// </summary>
        /// <returns>A string encoding the Restrict's data in an XML format that can be read by the AOTS scenario system</returns>
        public override string toString()
        {
            int temp = 0;
            if (enable)
            {
                temp = 1;
            }
            return "<Restrict Type=\"" + type + "\" ID=\"" + id + "\" Enable=\"" + temp + "\"/>";
        }

        /// <summary>
        /// Produces a string containing the action type and the internal name of the resource being restricted.
        /// </summary>
        /// <returns>A string containing the action type and the internal name of the resource being restricted</returns>
        public override string getSummary()
        {
            string displayedName = trig.scen.mainWindow.AAH.findAnyDisplayedName(id);
            if (displayedName != "")
            {
                return "Restrict(" + displayedName + ")";
            }
            else
            return "Restrict(" + id + ")";
        }
    }
    /// <summary>
    /// An action that spawns a unit at a designated position
    /// </summary>
    public class SpawnUnit : Action
    {
        /// <summary>
        /// Arbitrary name given to the unit being spawned to be referenced later by actions that may move the unit, destroy the unit, etc.
        /// </summary>
        public string name;
        /// <summary>
        /// Internal name(referenced by AOTS code) for the unit type to be spawned
        /// </summary>
        public string template;
        /// <summary>
        /// MetaUnit parent spawned previously
        /// </summary>
        public string parent;
        /// <summary>
        /// Number assigned to the player the unit belongs to
        /// </summary>
        public int player;
        /// <summary>
        /// Coordinates of the spawn point on the map
        /// </summary>
        public Position pos;
        /// <summary>
        /// Toggles if the spawned unit is immortal(true if immortal, false if mortal)
        /// </summary>
        public bool noDeath;

        /// <summary>
        /// Gets the arbitrary name given to the unit being spawned to be referenced later by actions
        /// </summary>
        /// <returns>SpawnUnit.name</returns>
        public override string getStringA()
        {
            return name;
        }
        /// <summary>
        /// Sets the arbitrary name given to the unit being spawned to be referenced later by actions
        /// </summary>
        /// <param name="s">The new value for SpawnUnit.name</param>
        public override void setStringA(string s)
        {
            name=s;
        }

        /// <summary>
        /// Gets the internal name(referenced by AOTS code) for the unit type to be spawned
        /// </summary>
        /// <returns>The internal name(referenced by AOTS code) for the unit type to be spawned</returns>
        public override string getStringB()
        {
            return template;
        }
        /// <summary>
        /// Sets the internal name(referenced by AOTS code) for the unit type to be spawned
        /// </summary>
        /// <param name="s">The new value for SpawnUnit.template</param>
        public override void setStringB(string s)
        {
            template=s;
        }

        /// <summary>
        /// Gets the spawned unit's parent metaunit
        /// </summary>
        /// <returns>The spawned unit's parent metaunit</returns>
        public override string getStringC()
        {
            return parent;
        }
        /// <summary>
        /// Sets the spawned unit's parent metaunit
        /// </summary>
        /// <param name="s">The new value of SpawnUnit.parent</param>
        public override void setStringC(string s)
        {
            parent=s;
        }

        /// <summary>
        /// Gets the number assigned to the player the unit belongs to
        /// </summary>
        /// <returns>SpawnUnit.player</returns>
        public override int getIntA()
        {
            return player;
        }
        /// <summary>
        /// Sets the number assigned to the player the unit belongs to
        /// </summary>
        /// <param name="i">The new value for SpawnUnit.player</param>
        public override void setIntA(int i)
        {
            player=i;
        }

        /// <summary>
        /// Gets the X coordinate of the spawn point
        /// </summary>
        /// <returns>SpawnUnit.pos.x</returns>
        public override float getPositionAX()
        {
            return pos.x;
        }
        /// <summary>
        /// Sets the X coordinate of the spawn point
        /// </summary>
        /// <param name="x">The new X coordinate of the spawn point</param>
        public override void setPositionAX(float x)
        {
            pos.x=x;
        }
        /// <summary>
        /// Gets the Y coordinate of the spawn point
        /// </summary>
        /// <returns>SpawnUnit.pos.y</returns>
        public override float getPositionAY()
        {
            return pos.y;
        }
        /// <summary>
        /// Sets the Y coordinate of the spawn point
        /// </summary>
        /// <param name="y">The new Y coordinate of the spawn point</param>
        public override void setPositionAY(float y)
        {
            pos.y=y;
        }

        /// <summary>
        /// Gets the mortality of the spawned unit
        /// </summary>
        /// <returns>True if immortal, 0 if mortal</returns>
        public override bool getBoolA()
        {
            return noDeath;
        }
        /// <summary>
        /// Sets the mortality of the spawned unit
        /// </summary>
        /// <param name="b">True if immortal, 0 if mortal</param>
        public override void setBoolA(bool b)
        {
            noDeath=b;
        }

        /// <summary>
        /// Constructs an empty, ineffective SpawnUnit action belonging to a specified Trigger
        /// </summary>
        /// <param name="t">The trigger this action is to belong to</param>
        public SpawnUnit(Trigger t)
        {
            trig = t;
            pos = new Position();
            name = "new";
        }
        /// <summary>
        /// Constructs a complete SpawnUnit action belonging to a given Trigger with a specified name, template, parent, owner, position, and mortality
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="unitName">Arbitrary name given to the unit being spawned to be referenced later by actions that may move the unit, destroy the unit, etc.</param>
        /// <param name="unitTemplate">Internal name(referenced by AOTS code) for the unit type to be spawned</param>
        /// <param name="unitParent">The parent metaunit of the spawned unit(optional)</param>
        /// <param name="playerOwner">Number assigned to the player the unit belongs to</param>
        /// <param name="spawnPosition">The coordinates of the spawn position on the map</param>
        /// <param name="immortal">True if immortal, false if mortal</param>
        public SpawnUnit(Trigger t, string unitName, string unitTemplate, string unitParent, int playerOwner, Position spawnPosition, bool immortal)
        {
            trig = t;
            name = unitName;
            template = unitTemplate;
            parent = unitParent;
            player = playerOwner;
            pos = spawnPosition;
            noDeath = immortal;
        }

        /// <summary>
        /// Encodes the SpawnUnit action's name, template, player, position, mortality, and parent
        /// into an XML format that can be read by the AOTS scenario system
        /// </summary>
        /// <returns>A string in an XML format that can be read by the AOTS scenario system</returns>
        public override string toString()
        {
            int temp = 0;
            if (noDeath)
            {
                temp = 1;
            }
            if (parent == null || parent.Equals(""))
            {
                return "<SpawnUnit Name=\"" + name + "\" Template=\"" + template + "\" Player=\"" + player + "\" Position=\"" + pos.x + "," + pos.y + "\" NoDeath=\"" + temp + "\"/>";
            }
            return "<SpawnUnit Name=\"" + name + "\" Template=\"" + template + "\" Player=\"" + player + "\" Position=\"" + pos.x + "," + pos.y + "\" NoDeath=\"" + temp + "\" Parent=\"" + parent + "\"/>";
        }

        /// <summary>
        /// Essentially a copy of getStringA()
        /// </summary>
        /// <returns>SpawnUnit.name</returns>
        public override string getVal()
        {
            return name;
        }

        /// <summary>
        /// Uses getChildren() to produce a string with the action type, unit name, parent, and names of its children
        /// </summary>
        /// <returns>A string with the action type, unit name, parent, and names of its children</returns>
        public override string getSummary()
        {
            string output="SpawnUnit(" + name + ")";
            if (parent != null && !parent.Equals(""))
            {
                output += ("  Parent:" + parent);
            }
            bool childFlag=false;
            for (int i = 0;i< trig.actions.Length; i++)
            {
                if (trig.actions[i].GetType() == typeof(SpawnUnit))
                {
                    if (name!=null&&name.Equals(trig.actions[i].getStringC()))
                    {
                        if (!childFlag)
                        {
                            output += " Children:";
                            childFlag = true;
                            output += (" " + trig.actions[i].getStringA());
                        }
                        else
                        {
                            output += (", " + trig.actions[i].getStringA());
                        }
                        
                    }
                }
            }
                return output;
        }

        //TODO: Change scope of search in isParent() to search all SpawnUnits PRIOR to this one
        /// <summary>
        /// Searches the parent Trigger's other SpawnUnit actions to see if any of them has this spawned unit as its parent
        /// </summary>
        /// <returns>True if anything has this unit's name as its parent</returns>
        public override bool isParent()
        {
            for (int i = 0; i < trig.actions.Length; i++)
            {
                if (trig.actions[i].GetType() == typeof(SpawnUnit))
                {
                    if (name.Equals(trig.actions[i].getStringC()))
                    {
                        return true;

                    }
                }
            }
            return false;
        }
        //TODO: Change scope of search in getChildren() to search all SpawnUnits AFTER to this one
        /// <summary>
        /// Searches the parent Trigger for SpawnUnit actions in which the parent value is set to this action's name
        /// </summary>
        /// <returns>An array of Action objects that has this SpawnUnit's name as its parent</returns>
        public override Action[] getChildren()
        {
            if (!isParent())
            {
                return null;
            }
            else
            {
                Action[] output = new Action[0];
                for (int i = 0; i < trig.actions.Length; i++)
                {
                    if (trig.actions[i].GetType() == typeof(SpawnUnit))
                    {
                        if (name.Equals(trig.actions[i].getStringC()))
                        {
                            Action[] temp = new Action[output.Length + 1];
                            for (int q = 0; q < output.Length; q++)
                            {
                                temp[q] = output[q];
                            }
                            temp[output.Length] = trig.actions[i];
                            output = temp;

                        }
                    }
                }
                return output;
            }
        }
        //TODO: change scope of search in hasParent() to search all SpawnUnits PRIOR to this one, perhaps change UI elements to show if the user put in a parent value that doesn't match anything
        /// <summary>
        /// If this SpawnUnit action's parent value is not empty,
        /// this function will search the trigger to see if a unit with that parent value as its name exists within the trigger
        /// </summary>
        /// <returns>True if there is a unit with this SpawnUnit's parent value as its name exists within the trigger</returns>
        public override bool hasParent()
        {
            if (parent == null || parent.Equals(""))
            {
                return false;
            }
            String[] names = trig.getSpawnUnitNames();
            for (int i = 0; i < names.Length; i++)
            {
                if (parent.Equals(names[i]))
                {
                    return true;
                }
            }
            return false;
        }
        //TODO: change scope of search in hasChildren() to search all SpawnUnits AFTER to this one
        /// <summary>
        /// Searches the parent trigger to see if there is at least one SpawnUnit with this action's name as its parent value
        /// </summary>
        /// <returns>True if at least one SpawnUnit in the parent Trigger has this action's name as its parent value</returns>
        public override bool hasChildren()
        {
            for (int i = 0; i < trig.actions.Length; i++)
            {
                if (trig.actions[i].GetType() == typeof(SpawnUnit))
                {
                    if (name!=null&&name.Equals(trig.actions[i].getStringC()))
                    {
                        return true;

                    }
                }
            }
            return false;
        }
    }
    /// <summary>
    /// An action that destroys a unit spawned previously, often for cinematic effect
    /// </summary>
    public class DestroyUnit : Action
    {
        /// <summary>
        /// The arbitrary name given previously to a spawned unit to be destroyed
        /// </summary>
        public string name;
        /// <summary>
        /// Time until the unit is destroyed(like a time bomb)
        /// </summary>
        public int time;

        /// <summary>
        /// Gets the arbitrary name given previously to a spawned unit to be destroyed
        /// </summary>
        /// <returns>DestroyUnit.name</returns>
        public override string getStringA()
        {
            return name;
        }
        /// <summary>
        /// Sets the arbitrary name given previously to a spawned unit to be destroyed
        /// </summary>
        /// <param name="s">The new value for DestroyUnit.name</param>
        public override void setStringA(string s)
        {
            name=s;
        }

        /// <summary>
        /// Gets time until the unit is destroyed
        /// </summary>
        /// <returns>DestroyUnit.time</returns>
        public override int getIntA()
        {
            return time;
        }
        /// <summary>
        /// Sets time until the unit is destroyed
        /// </summary>
        /// <param name="i">New value for DestroyUnit.time</param>
        public override void setIntA(int i)
        {
           time=i;
        }

        /// <summary>
        /// Constructs an empty, ineffective DestroyUnit action belonging to a specified Trigger
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public DestroyUnit(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs a complete DestroyUnit action under a specified Trigger with a given name and time
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="unitName">The arbitrary name given previously to a spawned unit to be destroyed</param>
        /// <param name="deathTime">Time until the unit is destroyed</param>
        public DestroyUnit(Trigger t, string unitName, int deathTime)
        {
            trig = t;
            name = unitName;
            time = deathTime;
        }

        /// <summary>
        /// Constructs a complete DestroyUnit without a Trigger (may be useless)
        /// </summary>
        /// <param name="n">Name</param>
        /// <param name="t">Time</param>
        public DestroyUnit(string n, int t)
        {
            name = n;
            time = t;
        }

        /// <summary>
        /// Produces a string encoding the name and time into an XML format that can be read by the AOTS scenario system
        /// </summary>
        /// <returns>A string in an XML format that can be read by the AOTS scenario system</returns>
        public override string toString()
        {
            return "<DestroyUnit Name=\"" + name + "\" Time=\"" + time + "\"/>";
        }
        /// <summary>
        /// Essentially a copy of getStringA()
        /// </summary>
        /// <returns>DestroyUnit.name</returns>
        public override string getVal()
        {
            return name;
        }
        /// <summary>
        /// Produces a string containing the action type and the name of the unit destroyed
        /// </summary>
        /// <returns>A string containing the action type and the name of the unit destroyed</returns>
        public override string getSummary()
        {
            return "DestroyUnit(" + name + ")";
        }
    }
    /// <summary>
    /// An action that moves a previously spawned AI unit
    /// </summary>
    public class AttackAttackMove : Action
    {
        /// <summary>
        /// The name of the previously spawned unit(the name you assigned to its "name" attribute) to be moved
        /// </summary>
        public string name;
        /// <summary>
        /// The coordinates of the destination point of the unit being moved
        /// </summary>
        public Position pos;

        /// <summary>
        /// Gets the name of the previously spawned unit to be moved
        /// </summary>
        /// <returns>AttackAttackMove.name</returns>
        public override string getStringA()
        {
            return name;
        }
        /// <summary>
        /// Sets the name of the previously spawned unit to be moved
        /// </summary>
        /// <param name="s">The new value for AttackAttackMove.name</param>
        public override void setStringA(string s)
        {
            name = s;
        }

        /// <summary>
        /// Gets the X coordinate of the destination point of the unit being moved
        /// </summary>
        /// <returns>AttackAttackMove.pos.x</returns>
        public override float getPositionAX()
        {
            return pos.x;
        }
        /// <summary>
        /// Sets the X coordinate of the destination point of the unit being moved
        /// </summary>
        /// <param name="x">The new value for AttackAttackMove.pos.x</param>
        public override void setPositionAX(float x)
        {
            pos.x = x;
        }
        /// <summary>
        /// Gets the Y coordinate of the destination point of the unit being moved
        /// </summary>
        /// <returns>AttackAttackMove.pos.y</returns>
        public override float getPositionAY()
        {
            return pos.y;
        }
        /// <summary>
        /// Sets the Y coordinate of the destination point of the unit being moved
        /// </summary>
        /// <param name="y">The new value for AttackAttackMove.pos.y</param>
        public override void setPositionAY(float y)
        {
            pos.y = y;
        }

        /// <summary>
        /// Constructs an empty, ineffective AttackAttackMove action under a specified Trigger
        /// </summary>
        /// <param name="t">The Trigger the action is to belong to</param>
        public AttackAttackMove(Trigger t)
        {
            trig = t;
            pos = new Position();
        }
        /// <summary>
        /// Constructs a complete AttackAttackMove action under a given Trigger with a specified name and position
        /// </summary>
        /// <param name="t">The Trigger the action is to belong to</param>
        /// <param name="unitName">The name of the previously spawned unit(the name you assigned to its "name" attribute) to be moved</param>
        /// <param name="targetPosition">The coordinates of the destination point of the unit being moved</param>
        public AttackAttackMove(Trigger t, string unitName, Position targetPosition)
        {
            trig = t;
            name = unitName;
            pos = targetPosition;
       }
        /// <summary>
        /// Constructs a complete AttackAttackMove without a Trigger(most likely useless)
        /// </summary>
        /// <param name="n">name</param>
        /// <param name="p">position</param>
        public AttackAttackMove(string n, Position p)
        {
            name = n;
            pos = p;
        }

        /// <summary>
        /// Encodes the AttackAttackMove's unit name and destination into a string in an XML format that the AOTS scenario system can read
        /// </summary>
        /// <returns>A string in an XML format that the AOTS scenario system can read</returns>
        public override string toString()
        {
            return "<AttackAttackMove Name=\"" + name + "\" Position=\"" + pos.x + "," + pos.y + "\"/>";
        }
        /// <summary>
        /// Produces a string containing the action type and the name of the affected unit
        /// </summary>
        /// <returns>A string containing the action type and the name of the affected unit</returns>
        public override string getSummary()
        {
            return "AttackAttackMove(" + name + ")";
        }
    }
    /// <summary>
    /// An action that spawns a building at a given point on the map
    /// </summary>
    public class SpawnBuilding : Action
    {
        /// <summary>
        /// Arbitrary name given to the spawned building that can be referenced by the DestroyBuilding action, the Destruction trigger, etc.
        /// </summary>
        public string name;
        /// <summary>
        /// Internal name of the building to be built(the same internal names used by AOTS game code)
        /// </summary>
        public string template;
        /// <summary>
        /// Number assigned to the player the building belongs to
        /// </summary>
        public int player;
        /// <summary>
        /// The spawn point for the building on the map
        /// </summary>
        public Position pos;

        /// <summary>
        /// Gets the Arbitrary name given to the spawned building
        /// </summary>
        /// <returns>SpawnBuilding.name</returns>
        public override string getStringA()
        {
            return name;
        }
        /// <summary>
        /// Sets the Arbitrary name given to the spawned building
        /// </summary>
        /// <param name="s">The new value for SpawnBuilding.name</param>
        public override void setStringA(string s)
        {
            name=s;
        }

        /// <summary>
        /// Gets the internal name of the building to be built(the same internal names used by AOTS game code)
        /// </summary>
        /// <returns>SpawnBuilding.template</returns>
        public override string getStringB()
        {
            return template;
        }
        /// <summary>
        /// Sets the internal name of the building to be built(the same internal names used by AOTS game code)
        /// </summary>
        /// <param name="s">The new value for SpawnBuilding.template</param>
        public override void setStringB(string s)
        {
            template = s;
        }

        /// <summary>
        /// Gets the number assigned to the player the building belongs to
        /// </summary>
        /// <returns>SpawnBuilding.player</returns>
        public override int getIntA()
        {
            return player;
        }
        /// <summary>
        /// Sets the number assigned to the player the building belongs to
        /// </summary>
        /// <param name="i">The new value for SpawnBuilding.player</param>
        public override void setIntA(int i)
        {
            player = i;
        }

        /// <summary>
        /// Gets the X coordinate of the building's spawn point
        /// </summary>
        /// <returns>SpawnBuilding.pos.x</returns>
        public override float getPositionAX()
        {
            return pos.x;
        }
        /// <summary>
        /// Sets the X coordinate of the building's spawn point
        /// </summary>
        /// <param name="x">The new value for SpawnBuilding.pos.x</param>
        public override void setPositionAX(float x)
        {
            pos.x=x;
        }
        /// <summary>
        /// Gets the Y coordinate of the building's spawn point
        /// </summary>
        /// <returns>SpawnBuilding.pos.y</returns>
        public override float getPositionAY()
        {
            return pos.y;
        }
        /// <summary>
        /// Sets the Y coordinate of the building's spawn point
        /// </summary>
        /// <param name="y">The new value for SpawnBuilding.pos.u</param>
        public override void setPositionAY(float y)
        {
            pos.y=y;
        }

        /// <summary>
        /// Constructs an empty, ineffective SpawnBuilding action under a specified Trigger
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public SpawnBuilding(Trigger t)
        {
            trig = t;
            pos = new Position();
        }
        /// <summary>
        /// Constructs a complete SpawnBuilding action under a given Trigger with a specified name, template, player, and spawn point.
        /// </summary>
        /// <param name="t">The trigger this action is to belong to</param>
        /// <param name="buildingName">Arbitrary name given to the spawned building that can be referenced by the DestroyBuilding action, the Destruction trigger, etc.</param>
        /// <param name="buildingTemplate">Internal name of the building to be built(the same internal names used by AOTS game code)</param>
        /// <param name="ownerPlayer">Number assigned to the player the building belongs to</param>
        /// <param name="spawnPosition">The spawn point for the building on the map</param>
        public SpawnBuilding(Trigger t, string buildingName, string buildingTemplate, int ownerPlayer, Position spawnPosition)
        {
            trig = t;
            name = buildingName;
            template = buildingTemplate;
            player = ownerPlayer;
            pos = spawnPosition;
        }
        /// <summary>
        /// Spawns a complete SpawnBuilding action without a trigger(unused)
        /// </summary>
        /// <param name="n">name</param>
        /// <param name="t">template</param>
        /// <param name="pl">player</param>
        /// <param name="po">spawn point</param>
        public SpawnBuilding(string n, string t, int pl, Position po)
        {
            name = n;
            template = t;
            player = pl;
            pos = po;
        }

        /// <summary>
        /// Encodes the action's data into the attributes of an XML element that can be read by the Ashes Scenario system.
        /// </summary>
        /// <returns>An XML element that can be read by the Ashes Scenario system</returns>
        public override string toString()
        {
            return "<SpawnBuilding Name=\"" + name + "\" Template=\"" + template + "\" Player=\"" + player + "\" Position=\"" + pos.x + "," + pos.y + "\"/>";
        }
        /// <summary>
        /// Essentially a copy of getStringA()
        /// </summary>
        /// <returns>SpawnBuilding.name</returns>
        public override string getVal()
        {
            return name;
        }
        /// <summary>
        /// Produces a string containing the action type and the arbitrary name given to the spawned building
        /// to be referenced by other actions and triggers
        /// </summary>
        /// <returns>A string containing the action type and the arbitrary name given to the spawned building</returns>
        public override string getSummary()
        {
            return "SpawnBuilding(" + name + ")";
        }
    }
    /// <summary>
    /// Destroys a building previously spawned by a SpawnBuilding action
    /// </summary>
    public class DestroyBuilding : Action
    {
        /// <summary>
        /// Arbitrary name given to the spawned building to be destroyed
        /// </summary>
        public string name;
        /// <summary>
        /// Time in seconds until the building is destroyed after this action is fired(like the fuse on a bomb)
        /// </summary>
        public int time;

        /// <summary>
        /// Gets the arbitrary name given to the spawned building to be destroyed
        /// </summary>
        /// <returns>DestroyBuilding.name</returns>
        public override string getStringA()
        {
            return name;
        }
        /// <summary>
        /// Sets the arbitrary name given to the spawned building to be destroyed
        /// </summary>
        /// <param name="s">The new value for DestroyBuilding.name</param>
        public override void setStringA(string s)
        {
            name=s;
        }

        /// <summary>
        /// Gets the time in seconds until the building is destroyed after this action is fired
        /// </summary>
        /// <returns>DestroyBuilding.name</returns>
        public override int getIntA()
        {
            return time;
        }
        /// <summary>
        /// Sets the time in seconds until the building is destroyed after this action is fired
        /// </summary>
        /// <param name="i">The new value for DestroyBuilding.name</param>
        public override void setIntA(int i)
        {
            time=i;
        }

        /// <summary>
        /// Constructs an empty, useless DestroyBuilding action under a specified trigger
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public DestroyBuilding(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs a complete DestroyBuilding action with a specified name and timer under a given Trigger
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="buildingName">Arbitrary name given to the spawned building to be destroyed</param>
        /// <param name="deathTime">Time in seconds until the building is destroyed after this action is fired</param>
        public DestroyBuilding(Trigger t, string buildingName, int deathTime)
        {
            trig = t;
            name = buildingName;
            time = deathTime;
        }

        /// <summary>
        /// Produces an XML compliant element containing the DestroyUnit's name and time as attributes that is readable by the AOTS scenario system
        /// </summary>
        /// <returns>An XML compliant element containing the DestroyUnit's name and time as attributes</returns>
        public override string toString()
        {
            return "<DestroyBuilding Name=\"" + name + "\" Time=\"" + time + "\"/>";
        }
        /// <summary>
        /// Essentially a copy of getStringA()
        /// </summary>
        /// <returns>DestroyBuilding.name</returns>
        public override string getVal()
        {
            return name;
        }

        /// <summary>
        /// Produces a string containing the action's type and the name of the building being destroyed
        /// </summary>
        /// <returns>A string containing the action's type and the name of the building being destroyed</returns>
        public override string getSummary()
        {
            return "DestroyBuilding(" + name + ")";
        }
    }
    /// <summary>
    /// An action that either activates or deactivates cinematic letterboxing
    /// </summary>
    public class LetterBox : Action
    {
        /// <summary>
        /// If true, the action is letterboxing the camera; if false, the action is deactivating the previously enable letterboxing
        /// </summary>
        public bool enable;
        /// <summary>
        /// If true, the letterboxing jumpcuts in/out; if false the letterboxing transitions via a fade
        /// </summary>
        public bool snap;

        /// <summary>
        /// Gets whether the letterboxing is being enabled or disabled
        /// </summary>
        /// <returns>True if the action is letterboxing the camera, false if the action is deactivating the previously enable letterboxing</returns>
        public override bool getBoolA()
        {
            return enable;
        }
        /// <summary>
        /// Sets whether the letterbxoing is being enabled or disabled
        /// </summary>
        /// <param name="b">If true, the action is letterboxing the camera; if false, the action is deactivating the previously enable letterboxing</param>
        public override void setBoolA(bool b)
        {
            enable=b;
        }
        /// <summary>
        /// Gets whether the letterboxing snaps or fades
        /// </summary>
        /// <returns>True if the letterboxing jumpcuts in/out, false if the letterboxing transitions via a fade</returns>
        public override bool getBoolB()
        {
            return snap;
        }
        /// <summary>
        /// Sets whether the letterboxing snaps or fades
        /// </summary>
        /// <param name="b">True if the letterboxing jumpcuts in/out, false if the letterboxing transitions via a fade</param>
        public override void setBoolB(bool b)
        {
            snap=b;
        }

        /// <summary>
        /// Constructs an empty, ineffective Letterbox action under a specified Trigger
        /// </summary>
        /// <param name="t">The trigger this action is to belong to</param>
        public LetterBox(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs a complete action under Trigger t that will enable/disable letterboxing on the camera on a snap/fade
        /// </summary>
        /// <param name="t">The trigger this action is to belong to</param>
        /// <param name="letterBoxEnable">If true, the action is letterboxing the camera; if false, the action is deactivating the previously enable letterboxing</param>
        /// <param name="camSnap">If true, the letterboxing jumpcuts in/out; of false the letterboxing transitions via a fade</param>
        public LetterBox(Trigger t, bool letterBoxEnable, bool camSnap)
        {
            trig = t;
            enable = letterBoxEnable;
            snap = camSnap;
        }

        /// <summary>
        /// Produces an XML-compliant element encoding the enable and sap properties as attributes in such a way that can be read by the AOTS scenario system
        /// </summary>
        /// <returns>An XML-compliant element encoding the enable and sap properties as attributes</returns>
        public override string toString()
        {
            int temp1 = 0, temp2 = 0;
            if (enable)
            {
                temp1 = 1;
            }
            if (snap)
            {
                temp2 = 1;
            }
            return "<LetterBox Enable=\"" + temp1 + "\" Snap=\"" + temp2 + "\"/>";
        }
        /// <summary>
        /// Produces a string containing the action's type
        /// </summary>
        /// <returns>"Letterbox"</returns>
        public override string getSummary()
        {
            return "LetterBox";
        }
    }
    /// <summary>
    /// An action that hides aspects of the in-game UI such as the resources, the player's stats, or the rankings
    /// </summary>
    public class HidePanel : Action
    {
        /// <summary>
        /// If true, the action hides the panel displaying username, icon, logistics, and quanta
        /// </summary>
        public bool hidePlayer;
        /// <summary>
        /// If true, the action hides the panel with the rankings of the players and their respective military/economic ratings and turinium levels
        /// </summary>
        public bool hideRank;
        /// <summary>
        /// If true, the action hides the panel that displays metal and radioactive storage and income
        /// </summary>
        public bool hideResource;

        /// <summary>
        /// Gets whether or not the action hides the panel displaying username, icon, logistics, and quanta
        /// </summary>
        /// <returns>True if the action hides the panel displaying username, icon, logistics, and quanta</returns>
        public override bool getBoolA()
        {
            return hidePlayer;
        }
        /// <summary>
        /// Sets whether or not the action hides the panel displaying username, icon, logistics, and quanta
        /// </summary>
        /// <param name="b">True if the action hides the panel displaying username, icon, logistics, and quanta</param>
        public override void setBoolA(bool b)
        {
            hidePlayer=b;
        }

        /// <summary>
        /// Gets whether or not the action hides the panel with the rankings of the players and their respective military/economic ratings and turinium levels
        /// </summary>
        /// <returns>True if the action hides the panel with the rankings of the players and their respective military/economic ratings and turinium levels</returns>
        public override bool getBoolB()
        {
            return hideRank;
        }
        /// <summary>
        /// Sets whether or not the action hides the panel with the rankings of the players and their respective military/economic ratings and turinium levels
        /// </summary>
        /// <param name="b">True if the action hides the panel with the rankings of the players and their respective military/economic ratings and turinium levels</param>
        public override void setBoolB(bool b)
        {
            hideRank=b;
        }

        /// <summary>
        /// Gets whether the action hides the panel that displays metal and radioactive storage and income
        /// </summary>
        /// <returns>True if the action hides the panel that displays metal and radioactive storage and income</returns>
        public override bool getBoolC()
        {
            return hideResource;
        }
        /// <summary>
        /// Sets whether the action hides the panel that displays metal and radioactive storage and income
        /// </summary>
        /// <param name="b">True if the action hides the panel that displays metal and radioactive storage and income</param>
        public override void setBoolC(bool b)
        {
            hideResource=b;
        }

        /// <summary>
        /// Creates a complete HidePanel action without a trigger
        /// </summary>
        /// <param name="player">If true, the action hides the panel displaying username, icon, logistics, and quanta</param>
        /// <param name="rank">If true, the action hides the panel with the rankings of the players and their respective military/economic ratings and turinium levels</param>
        /// <param name="resource">If true, the action hides the panel that displays metal and radioactive storage and income</param>
        public HidePanel(bool player, bool rank, bool resource)
        {
            hidePlayer = player;
            hideRank = rank;
            hideResource = resource;
        }
        /// <summary>
        /// Creates a HidePanel action that doesn't hide any UI elements (or unhides them if they were previously hidden) under a given Trigger
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public HidePanel(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs a complete action under a given Trigger that will hide specified UI elements
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="hide_player">If true, the action hides the panel displaying username, icon, logistics, and quanta</param>
        /// <param name="hide_rank">If true, the action hides the panel with the rankings of the players and their respective military/economic ratings and turinium levels</param>
        /// <param name="hide_resource">If true, the action hides the panel that displays metal and radioactive storage and income</param>
        public HidePanel(Trigger t, bool hide_player, bool hide_rank, bool hide_resource)
        {
            trig = t;
            hidePlayer = hide_player;
            hideRank = hide_rank;
            hideResource = hide_resource;
        }

        /// <summary>
        /// Encodes the HidePanel action into an XML element that can be read by the AOTS scenario system
        /// </summary>
        /// <returns>An XML element encoding the HidePanel's properties as attributes</returns>
        public override string toString()
        {
            int temp1 = 0, temp2 = 0, temp3 = 0;
            if (hidePlayer)
            {
                temp1 = 1;
            }
            if (hideRank)
            {
                temp2 = 1;
            }
            if (hideResource)
            {
                temp3 = 1;
            }
            return "<HidePanel HidePlayer=\"" + temp1 + "\" HideRank=\"" + temp2 + "\" HideResource=\"" + temp3 + "\"/>";
        }
        /// <summary>
        /// Produces a string containing the action's type
        /// </summary>
        /// <returns>"HidePanel"</returns>
        public override string getSummary()
        {
            return "HidePanel";
        }
    }
    /// <summary>
    /// An Action that either grants a player a certain amount of various resources, such as metal or quanta or takes them away
    /// </summary>
    public class GrantStuff : Action
    {
        /// <summary>
        /// The number given to the player that is to have it's resources added to or subtracted from
        /// </summary>
        public int player = 0;
        /// <summary>
        /// The number of quanta being added(value is negative if quanta are to be reduced)
        /// </summary>
        public int quanta = 0;
        /// <summary>
        /// The number of metal being added(value is negative if metal are to be reduced)
        /// </summary>
        public int metal = 0;
        /// <summary>
        /// The number of radioactives being added(value is negative if radioactives are to be reduced)
        /// </summary>
        public int radioactives = 0;
        /// <summary>
        /// The number of logistics being added(value is negative if logistics are to be reduced)
        /// </summary>
        public int logistics = 0;
        //TODO: figure out what tech does
        /// <summary>
        /// Resource from earlier builds of Ashes kept in for legacy support
        /// </summary>
        public int tech=0;

        /// <summary>
        /// True if the Action is to affect the resources of one player, false if it is to affect the resources of all players
        /// </summary>
        public bool hasPlayer;

        /// <summary>
        /// Gets whether or not the Action is to affect the resources of one player
        /// </summary>
        /// <returns>True if the Action is to affect the resources of one player, false if it is to affect the resources of all players</returns>
        public override bool getBoolA()
        {
            return hasPlayer;
        }
        /// <summary>
        /// Sets whether or not the Action is to affect the resources of one player
        /// </summary>
        /// <param name="b">True if the Action is to affect the resources of one player, false if it is to affect the resources of all players</param>
        public override void setBoolA(bool b)
        {
            hasPlayer = b;
        }

        /// <summary>
        /// Gets the number assigned to the affected player
        /// </summary>
        /// <returns>The number given to the player that is to have it's resources added to or subtracted from</returns>
        public override int getIntA()
        {
            return player;
        }
        /// <summary>
        /// Sets the target player
        /// </summary>
        /// <param name="i">The number given to the player that is to have it's resources added to or subtracted from</param>
        public override void setIntA(int i)
        {
            player=i;
        }
        /// <summary>
        /// Gets the number of quanta being added
        /// </summary>
        /// <returns>The number of quanta being added(value is negative if quanta are to be reduced)</returns>
        public override int getIntB()
        {
            return quanta;
        }
        /// <summary>
        /// Sets the number of quanta being added
        /// </summary>
        /// <param name="i">The number of quanta being added(value is negative if quanta are to be reduced)</param>
        public override void setIntB(int i)
        {
            quanta = i;
        }
        /// <summary>
        /// Gets the number of metal being added
        /// </summary>
        /// <returns>The number of metal being added(value is negative if metal are to be reduced)</returns>
        public override int getIntC()
        {
            return metal;
        }
        /// <summary>
        /// Sets the number of metal being added
        /// </summary>
        /// <param name="i">The number of metal being added(value is negative if metal are to be reduced)</param>
        public override void setIntC(int i)
        {
            metal=i;
        }
        /// <summary>
        /// Gets the number of radioactives being added
        /// </summary>
        /// <returns>The number of radioactives being added(value is negative if radioactives are to be reduced)</returns>
        public override int getIntD()
        {
            return radioactives;
        }
        /// <summary>
        /// Sets the number of radioactives being added
        /// </summary>
        /// <param name="i">The number of radioactives being added(value is negative if radioactives are to be reduced)</param>
        public override void setIntD(int i)
        {
            radioactives=i;
        }
        /// <summary>
        /// Gets the number of logistics being added
        /// </summary>
        /// <returns>The number of logistics being added(value is negative if logistics are to be reduced)</returns>
        public override int getIntE()
        {
            return logistics;
        }
        /// <summary>
        /// Sets the number of logistics being added
        /// </summary>
        /// <param name="i">The number of logistics being added(value is negative if logistics are to be reduced)</param>
        public override void setIntE(int i)
        {
            logistics=i;
        }
        /// <summary>
        /// Gets the number of tech being added
        /// </summary>
        /// <returns>The number of tech being added(value is negative if logistics are to be reduced)</returns>
        public override int getIntF()
        {
            return tech;
        }
        /// <summary>
        /// Sets the number of tech being added
        /// </summary>
        /// <param name="i">The number of tech being added(value is negative if logistics are to be reduced)</param>
        public override void setIntF(int i)
        {
            tech=i;
        }

        /// <summary>
        /// Creates a GrantStuff action under a specified Trigger that won't do anything
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public GrantStuff(Trigger t)
        {
            trig = t;
            hasPlayer = false;
        }
        /// <summary>
        /// Creates a GrantStuff action under a given Trigger with a specified player(if any) and amounts for quanta, metal, radioactives, logistics, and tech
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="playerGrantee">The number given to the player that is to have it's resources added to or subtracted from</param>
        /// <param name="playerEnable">True if the Action is to affect the resources of one player, false if it is to affect the resources of all players</param>
        /// <param name="quantaAmt">The number of quanta being added(value is negative if quanta are to be reduced)</param>
        /// <param name="metalAmt">The number of metal being added(value is negative if metal are to be reduced)</param>
        /// <param name="radAmt">The number of radioactives being added(value is negative if radioactives are to be reduced)</param>
        /// <param name="logAmt">The number of logistics being added(value is negative if logistics are to be reduced)</param>
        /// <param name="techAmt">Resource from earlier builds of Ashes kept in for legacy support</param>
        public GrantStuff(Trigger t, int playerGrantee, bool playerEnable, int quantaAmt, int metalAmt, int radAmt, int logAmt,int techAmt)
        {
            trig = t;
            hasPlayer = playerEnable;
            player = playerGrantee;
            quanta = quantaAmt;
            metal = metalAmt;
            radioactives = radAmt;
            logistics = logAmt;
            tech = techAmt;
        }

        /// <summary>
        /// Encodes the information of the GrantStuff action into the attributes of an XML element that can be read by the Ashes scenario system
        /// </summary>
        /// <returns>An XML element that can be read by the Ashes scenario system</returns>
        public override string toString()
        {
            //return "TEST";
            string output = "<GrantStuff";

            if (hasPlayer)
            {
                output += " Player=\"" + player + "\"";
            }
            if (quanta != 0)
            {
                output += (" Quanta=\"" + quanta + "\"");
            }
            if (metal != 0)
            {
                output += (" Metal=\"" + metal + "\"");
            }
            if (radioactives != 0)
            {
                output += (" Radioactives=\"" + radioactives + "\"");
            }
            if (logistics != 0)
            {
                output += (" Logistics=\"" + logistics + "\"");
            }
            if (tech != 0)
            {
                output += (" Tech=\"" + tech + "\"");
            }
            output += "/>";
            return output;

        }
        /// <summary>
        /// Produces a string containing the action type, the player(if any) and affected resources
        /// </summary>
        /// <returns>A string containing the action type, the player(if any) and affected resources</returns>
        public override string getSummary()
        {
            string output = "GrantStuff(";
            if (hasPlayer)
            {
                output += player;
            }
            else
            {
                output += "All";
            }
            if (quanta > 0)
            {
                output += " quanta,";
            }
            if (metal > 0)
            {
                output += " metal,";
            }
            if (radioactives > 0)
            {
                output += " rad,";
            }
            if (logistics > 0)
            {
                output += " log,";
            }
            if (tech > 0)
            {
                output += " tech,";
            }
            return output+=")";
        }
    }
    /// <summary>
    /// An action that upgrades a player's HP or damage, which can be used to adjust difficulty
    /// </summary>
    public class GrantTech : Action
    {
        /// <summary>
        /// The number given to the player that is to have it's resources added to or subtracted from
        /// </summary>
        public int player;
        /// <summary>
        /// The number of times the tech is being upgraded(an alternative to making multiple duplicate GrantTech actions)
        /// </summary>
        public int x;
        /// <summary>
        /// The property of player being upgraded
        /// <para>
        /// Value     | Upgrade
        /// "HP"      | Increased Health
        /// "Weapons" | Increased Damage
        /// </para>
        /// </summary>
        public string tech;

        /// <summary>
        /// Gets the number given to the player that is to have it's resources added to or subtracted from
        /// </summary>
        /// <returns>The number given to the player that is to have it's resources added to or subtracted from</returns>
        public override int getIntA()
        {
            return player;
        }
        /// <summary>
        /// Sets the number given to the player that is to have it's resources added to or subtracted from
        /// </summary>
        /// <param name="i">The number given to the player that is to have it's resources added to or subtracted from</param>
        public override void setIntA(int i)
        {
            player = i;
        }

        /// <summary>
        /// Gets the number of times the tech is being upgraded
        /// </summary>
        /// <returns>The number of times the tech is being upgraded(an alternative to making multiple duplicate GrantTech actions)</returns>
        public override int getIntB()
        {
            return x;
        }
        /// <summary>
        /// Sets the number of times the tech is being upgraded
        /// </summary>
        /// <param name="i">The number of times the tech is being upgraded(an alternative to making multiple duplicate GrantTech actions)</param>
        public override void setIntB(int i)
        {
            x = i;
        }

        /// <summary>
        ///  Gets the property of player being upgraded
        /// </summary>
        /// <returns>The property of player being upgraded</returns>
        public override string getStringA()
        {
            return tech;
        }
        /// <summary>
        /// Sets the property of player being upgraded
        /// </summary>
        /// <param name="s">The property of player being upgraded</param>
        public override void setStringA(string s)
        {
            tech=s;
        }

        /// <summary>
        /// Constructs a GrantTech action under a given Trigger that will not do anything or even be saved to the XML document unless modified
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public GrantTech(Trigger t)
        {
            trig = t;
            x = 1;
        }
        /// <summary>
        /// Constructs an action under a given Trigger that grants a specified upgrade to a specified player a specified number of times
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="playerGrantee">The number given to the player that is to have it's resources added to or subtracted from</param>
        /// <param name="techGranted">The property of player being upgraded
        /// <para>
        /// Value     | Upgrade
        /// "HP"      | Increased Health
        /// "Weapons" | Increased Damage
        /// </para>
        /// </param>
        /// <param name="times">The number of times the tech is being upgraded(an alternative to making multiple duplicate GrantTech actions)</param>
        public GrantTech(Trigger t, int playerGrantee, string techGranted, int times)
        {
            trig = t;

            player = playerGrantee;
            tech = techGranted;
            x = times;
        }

        /// <summary>
        /// Encodes the action's data into teh attributes of an XML element that can be read by the Ashes scenario system
        /// </summary>
        /// <returns>An XML element that can be read by the Ashes scenario system</returns>
        public override string toString()
        {
            if (player < 0 || tech == null)
            {
                return "";
            }
            if (x > 1)
            {
                string output = "<GrantTech Player=\"" + player + "\" Tech=\"" + tech + "\"/>";
                for (int i = 1; i < x; i++)
                {
                    output += "\n\t\t<GrantTech Player=\"" + player + "\" Tech=\"" + tech + "\"/>";
                }
                return output;
            }
            return "<GrantTech Player=\"" + player + "\" Tech=\"" + tech + "\"/>";
        }
        /// <summary>
        /// Produces a string containing the action's type, affected player, upgrade type, and number of repetitions
        /// </summary>
        /// <returns>A string containing the action's type, affected player, upgrade type, and number of repetitions</returns>
        public override string getSummary()
        {
            return "GrantTech("+player+" "+tech+" x"+x+")";
        }
    }
    /// <summary>
    /// An action that instructs a spawned unit to capture the nearest generator
    /// </summary>
    public class CaptureNearest : Action
    {
        /// <summary>
        /// The name given to the selected unit in its SpawnUnit action
        /// </summary>
        public string name;
        /// <summary>
        /// The minimum amount of time the unit will wait before capturing the nearest generator
        /// </summary>
        public int randomTimeMin;
        /// <summary>
        /// The maximum amount of time the unit will wait before capturing the nearest generator
        /// </summary>
        public int randomTimeMax;
        /// <summary>
        /// True if the selected unit will capture the nearest generator until it either captures every generator or it dies, false if it is only to capture one generator
        /// </summary>
        public bool repeat;

        /// <summary>
        /// Gets the name given to the selected unit in its SpawnUnit action
        /// </summary>
        /// <returns>The name given to the selected unit in its SpawnUnit action</returns>
        public override string getStringA()
        {
            return name;
        }
        /// <summary>
        /// Sets the name given to the selected unit in its SpawnUnit action
        /// </summary>
        /// <param name="s">The name given to the selected unit in its SpawnUnit action</param>
        public override void setStringA(string s)
        {
            name=s;
        }

        /// <summary>
        /// Gets the minimum amount of time the unit will wait before capturing the nearest generator
        /// </summary>
        /// <returns>The minimum amount of time the unit will wait before capturing the nearest generator</returns>
        public override int getIntA()
        {
            return randomTimeMin;
        }
        /// <summary>
        /// Sets the minimum amount of time the unit will wait before capturing the nearest generator
        /// </summary>
        /// <param name="i">The minimum amount of time the unit will wait before capturing the nearest generator</param>
        public override void setIntA(int i)
        {
            randomTimeMin = i;
        }

        /// <summary>
        /// Gets the maximum amount of time the unit will wait before capturing the nearest generator
        /// </summary>
        /// <returns>The maximum amount of time the unit will wait before capturing the nearest generator</returns>
        public override int getIntB()
        {
            return randomTimeMax;
        }
        /// <summary>
        /// Sets the maximum amount of time the unit will wait before capturing the nearest generator
        /// </summary>
        /// <param name="i">The maximum amount of time the unit will wait before capturing the nearest generator</param>
        public override void setIntB(int i)
        {
            randomTimeMax = i;
        }

        /// <summary>
        /// Gets whether or not the action will repeat forever
        /// </summary>
        /// <returns>True if the selected unit will capture the nearest generator until it either captures every generator or it dies, false if it is only to capture one generator</returns>
        public override bool getBoolA()
        {
            return repeat;
        }
        /// <summary>
        /// Sets whether or not the action will repeat forever
        /// </summary>
        /// <param name="b">True if the selected unit will capture the nearest generator until it either captures every generator or it dies, false if it is only to capture one generator</param>
        public override void setBoolA(bool b)
        {
            repeat = b;
        }

        /// <summary>
        /// Constructs an empty, ineffective CaptureNearest action under a specified Trigger
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public CaptureNearest(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs a complete CaptureNearest action under a given Trigger with a specified unit and wait time range that may or may not repeat
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="capturerName">The name given to the selected unit in its SpawnUnit action</param>
        /// <param name="minimum">The minimum amount of time the unit will wait before capturing the nearest generator</param>
        /// <param name="maximum">The maximum amount of time the unit will wait before capturing the nearest generator</param>
        /// <param name="repeating">True if the selected unit will capture the nearest generator until it either captures every generator or it dies, false if it is only to capture one generator</param>
        public CaptureNearest(Trigger t, string capturerName, int minimum, int maximum, bool repeating)
        {
            trig = t;
            name = capturerName;
            randomTimeMin = minimum;
            randomTimeMax = maximum;
            repeat = repeating;
        }

        /// <summary>
        /// Encodes the CaptureNearest action's data into the attributes of an XML element that can be read by the Ashes scenario system
        /// </summary>
        /// <returns>An XML element that can be read by the Ashes scenario system containing the action's data</returns>
        public override string toString()
        {
            string temp = "false";
            if (repeat)
                temp = "true";
            return "<CaptureNearest Name=\"" + name + "\" RandomTime=\"" + randomTimeMin + "," + randomTimeMax + "\" Repeat=\"" + temp + "\"/>";
        }
        /// <summary>
        /// Produces a string containing the action's type and the name of the selected unit
        /// </summary>
        /// <returns>A string containing the action's type and the name of the selected unit</returns>
        public override string getSummary()
        {
            return "CaptureNearest(" + name + ")";
        }
    }
    /// <summary>
    /// An action that toggles whether or not the AI is active
    /// </summary>
    public class ToggleAI : Action
    {
        /// <summary>
        /// True if the AI is to be turned off, false if it is to be turned on
        /// </summary>
        public bool disableAI;

        /// <summary>
        /// Gets whether the AI is turned on or off
        /// </summary>
        /// <returns>True if the AI is to be turned off, false if it is to be turned on</returns>
        public override bool getBoolA()
        {
            return disableAI;
        }
        /// <summary>
        /// Sets whether the AI is turned on or off
        /// </summary>
        /// <param name="b">True if the AI is to be turned off, false if it is to be turned on</param>
        public override void setBoolA(bool b)
        {
            disableAI = b;
        }

        /// <summary>
        /// Constructs an action that will turn on the AI
        /// </summary>
        /// <param name="t">The trigger this action is to belong to</param>
        public ToggleAI(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Cobstructs an action that may turn the AI either on or off
        /// </summary>
        /// <param name="t">The trigger this action is to belong to</param>
        /// <param name="dAI">True if the AI is to be turned off, false if it is to be turned on</param>
        public ToggleAI(Trigger t, bool dAI)
        {
            trig = t;
            disableAI = dAI;
        }

        /// <summary>
        /// Encodes the ToggleAI action's data (basically just whether AI is being turned on or off) into an attribute in an XML element that the Ashes Scenario system can read
        /// </summary>
        /// <returns>An attribute in an XML element that the Ashes Scenario system can read</returns>
        public override string toString()
        {
            int temp = 0;
            if (disableAI)
                temp = 1;
            return "<ToggleAI DisableAI=\"" + temp + "\"/>";
        }
        /// <summary>
        /// Returns the action type
        /// </summary>
        /// <returns>"ToggleAI"</returns>
        public override string getSummary()
        {
            return "ToggleAI";
        }
    }
    /// <summary>
    /// An action that ends the scenario in either a victory or a defeat
    /// </summary>
    public class EndMission : Action
    {
        /// <summary>
        /// True if the ending is a victory, false if it is a defeat
        /// </summary>
        public bool victory;
        /// <summary>
        /// A key in the CSV data table corresponding to the text to be displayed in the game's UI
        /// </summary>
        public string text;

        /// <summary>
        /// Gets whether the ending is a victory or a defeat
        /// </summary>
        /// <returns>True if the ending is a victory, false if it is a defeat</returns>
        public override bool getBoolA()
        {
            return victory;
        }
        /// <summary>
        /// Sets whether the ending is a victory or a defeat
        /// </summary>
        /// <param name="b">True if the ending is a victory, false if it is a defeat</param>
        public override void setBoolA(bool b)
        {
            victory=b;
        }

        /// <summary>
        /// Gets the key in the CSV data table corresponding to the text to be displayed in the game's UI
        /// </summary>
        /// <returns>A key in the CSV data table corresponding to the text to be displayed in the game's UI</returns>
        public override string getStringA()
        {
            return text;
        }
        /// <summary>
        /// Sets the key in the CSV data table corresponding to the text to be displayed in the game's UI
        /// </summary>
        /// <param name="s">A key in the CSV data table corresponding to the text to be displayed in the game's UI</param>
        public override void setStringA(string s)
        {
            text=s;
        }

        /// <summary>
        /// Constructs an action under a given Trigger that ends the scenario in a defeat without any UI text
        /// </summary>
        /// <param name="t">The trigger this action is to belong to</param>
        public EndMission(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs an action under a given Trigger that may end the scenario in either a victory or defeat with specified UI text
        /// </summary>
        /// <param name="t">The trigger this action is to belong to</param>
        /// <param name="v">True if the ending is a victory, false if it is a defeat</param>
        /// <param name="txt">A key in the CSV data table corresponding to the text to be displayed in the game's UI</param>
        public EndMission(Trigger t, bool v, string txt)
        {
            trig = t;

            victory = v;
            text = txt;
        }

        /// <summary>
        /// Encodes the action's data into the attributes of an XML element that the Ashes scenario system can read
        /// </summary>
        /// <returns>An XML element that the Ashes scenario system can read</returns>
        public override string toString()
        {
            int temp = 0;
            if (victory)
            {
                temp = 1;
            }
            return "<EndMission Victory=\"" + temp + "\" String=\"" + text + "\"/>";

        }
        /// <summary>
        /// Produces a string that contains the action type and whether the ending is a victory or a defeat
        /// </summary>
        /// <returns>A string that contains the action type and whether the ending is a victory or a defeat</returns>
        public override string getSummary()
        {
            if (victory)
            {
                return "EndMission(Victory)";
            }
            else
            {
                return "EndMission(Defeat)";
            }
        }
    }
    /// <summary>
    /// An action that makes the player select a unit spawned by the scenario
    /// </summary>
    public class Select : Action
    {
        /// <summary>
        /// The unit spawned by the scenario to be selected
        /// </summary>
        public string target;

        /// <summary>
        /// Gets the unit spawned by the scenario to be selected
        /// </summary>
        /// <returns>The unit spawned by the scenario to be selected</returns>
        public override string getStringA()
        {
            return target;
        }
        /// <summary>
        /// Sets the unit spawned by the scenario to be selected
        /// </summary>
        /// <param name="s">The unit spawned by the scenario to be selected</param>
        public override void setStringA(string s)
        {
            target=s;
        }

        /// <summary>
        /// Constructs an ampty action that won't even be saved to the XML document without modification
        /// </summary>
        /// <param name="t">The Trigger this unit is to belong to</param>
        public Select(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs an action under a given Trigger that selects a specified unit
        /// </summary>
        /// <param name="t">The Trigger this unit is to belong to</param>
        /// <param name="selection">The unit spawned by the scenario to be selected</param>
        public Select(Trigger t, string selection)
        {
            trig = t;
            target = selection;
        }

        /// <summary>
        /// Encodes the action into an XML element the Ashes Scenario system can read with its target as an attribute
        /// </summary>
        /// <returns>An XML element the Ashes Scenario system</returns>
        public override string toString()
        {
            if (target == null)
            {
                return "";
            }
            return "<Select Target=\"" + target + "\"/>";
        }
        /// <summary>
        /// Produces a string containing the action's type and target
        /// </summary>
        /// <returns>A string containing the action's type and target</returns>
        public override string getSummary()
        {
            return "Select(" + target + ")";
        }
    }
    /// <summary>
    /// An action that enables or disables the notifications given by the announcer’s voice
    /// </summary>
    public class Notifications : Action
    {
        /// <summary>
        /// True if notifications are to be turned on, false if they are to be turned off
        /// </summary>
        public bool enable;

        /// <summary>
        /// Gets whether the notifications are being turned on or off
        /// </summary>
        /// <returns>True if notifications are to be turned on, false if they are to be turned off</returns>
        public override bool getBoolA()
        {
            return enable;
        }
        /// <summary>
        /// Sets whether the notifications are being turned on or off
        /// </summary>
        /// <param name="b">True if notifications are to be turned on, false if they are to be turned off</param>
        public override void setBoolA(bool b)
        {
            enable=b;
        }

        /// <summary>
        /// Constructs an action that turns off the notifications
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public Notifications(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs an action that turns the notifications either on or off
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="e">True if notifications are to be turned on, false if they are to be turned off</param>
        public Notifications(Trigger t, bool e)
        {
            trig = t;
            enable = e;
        }

        /// <summary>
        /// Encodes the action into an XML element that can be read by the Ashes scenario system
        /// </summary>
        /// <returns>An XML element that can be read by the Ashes scenario system</returns>
        public override string toString()
        {
            int temp = 0;
            if (enable)
                temp = 1;
            return "<Notifications Enable=\"" + temp + "\"/>";
        }
        /// <summary>
        /// Produces a string containing the action's type
        /// </summary>
        /// <returns>"Notifications"</returns>
        public override string getSummary()
        {
            return "Notifications";
        }
    }
    /// <summary>
    /// An action that highlights a part of the UI
    /// </summary>
    public class Highlight : Action
    {
        /// <summary>
        /// Arbitrary name given to the highlight that may be referenced by another Highlight action later
        /// </summary>
        public string name;
        /// <summary>
        /// UI button to be highlighted(Tech, Orbital, Army...)
        /// </summary>
        public string button;
        /// <summary>
        /// True if the button is being highlighted, false if this action is returning a previously highlighted button to normal
        /// </summary>
        public bool enable;
        /// <summary>
        /// True if the action is highlighting a UI button, false if this action is referencing a previous Highlight action to be undone
        /// </summary>
        public bool hasButton;

        /// <summary>
        /// Gets the arbitrary name given to the highlight
        /// </summary>
        /// <returns>Arbitrary name given to the highlight that may be referenced by another Highlight action later</returns>
        public override string getStringA()
        {
            return name;
        }
        /// <summary>
        /// Sets the arbitrary name given to the highlight
        /// </summary>
        /// <param name="s">Arbitrary name given to the highlight that may be referenced by another Highlight action later</param>
        public override void setStringA(string s)
        {
           name=s;
        }

        /// <summary>
        /// Gets the UI button to be highlighted
        /// </summary>
        /// <returns>UI button to be highlighted(Tech, Orbital, Army...)</returns>
        public override string getStringB()
        {
            return button;
        }
        /// <summary>
        /// Sets the UI button to be highlighted
        /// </summary>
        /// <param name="s">UI button to be highlighted(Tech, Orbital, Army...)</param>
        public override void setStringB(string s)
        {
            button=s;
        }

        /// <summary>
        /// Gets whether a button is being highlighted or if this action is returning a previously highlighted button to normal
        /// </summary>
        /// <returns>True if the button is being highlighted, false if this action is returning a previously highlighted button to normal</returns>
        public override bool getBoolA()
        {
            return enable;
        }
        /// <summary>
        /// Sets whether a button is being highlighted or if this action is returning a previously highlighted button to normal
        /// </summary>
        /// <param name="b">True if the button is being highlighted, false if this action is returning a previously highlighted button to normal</param>
        public override void setBoolA(bool b)
        {
            enable=b;
        }

        /// <summary>
        /// Gets whether the action is highlighting a UI button or if this action is referencing a previous Highlight action to be undone
        /// </summary>
        /// <returns>True if the action is highlighting a UI button, false if this action is referencing a previous Highlight action to be undone</returns>
        public override bool getBoolB()
        {
            return hasButton;
        }
        /// <summary>
        /// Sets whether the action is highlighting a UI button or if this action is referencing a previous Highlight action to be undone
        /// </summary>
        /// <param name="b">True if the action is highlighting a UI button, false if this action is referencing a previous Highlight action to be undone</param>
        public override void setBoolB(bool b)
        {
            hasButton=b;
        }

        /// <summary>
        /// Constructs an empty Highlight action under a given Trigger that won't even be saved tot he XML document unless it is modified
        /// </summary>
        /// <param name="t">Trigger this action is to belong to</param>
        public Highlight(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs an action under a given Trigger with a specified name, button to highlight, and value specifying if the action is highlighting a UI button or returning it to normal
        /// </summary>
        /// <param name="t">Trigger this action is to belong to</param>
        /// <param name="highlightName">Arbitrary name given to the highlight that may be referenced by another Highlight action later</param>
        /// <param name="highlightedButton">UI button to be highlighted(Tech, Orbital, Army...)</param>
        /// <param name="isHighlighted">True if the action is highlighting a UI button, false if this action is referencing a previous Highlight action to be undone</param>
        public Highlight(Trigger t, string highlightName, string highlightedButton, bool isHighlighted)
        {
            trig = t;
            name = highlightName;
            button = highlightedButton;
            enable = isHighlighted;
            if (highlightedButton == null || highlightedButton.Equals(""))
            {
                hasButton = false;
                button = "";
            }
        }

        /// <summary>
        /// Encodes the action's data into attributes in an XML element that can be read by the Ashes scenario system
        /// </summary>
        /// <returns>An XML element that can be read by the Ashes scenario system</returns>
        public override string toString()
        {
            if (name == null || name == "")
            {
                return "";
            }
            string temp = "<Highlight Name=\"" + name;
            int temp2 = 0;
            if (enable)
                temp2 = 1;
            if (button != null && hasButton)
            {
                temp += "\" Button=\"" + button;
            }
            temp += "\" Enable=\"" + temp2 + "\" />";
            return temp;
        }
        /// <summary>
        /// Produces a string containing the action type and the highlight's name
        /// </summary>
        /// <returns>A string containing the action type and the highlight's name</returns>
        public override string getSummary()
        {
            return "Highlight(" + name + ")";
        }
    }
    /// <summary>
    /// An action that turns on an AI player’s AI
    /// </summary>
    public class ActivateAI : Action
    {
        //TODO: See if ActivateAI's name has any purpose
        /// <summary>
        /// Seemingly pointless arbitrary name
        /// </summary>
        public string name;
        /// <summary>
        /// Number assigned to the player whose AI is being activated
        /// </summary>
        public int player;

        /// <summary>
        /// Gets the action's name(seemingly pointless)
        /// </summary>
        /// <returns>Seemingly pointless arbitrary name</returns>
        public override string getStringA()
        {
            return name;
        }
        /// <summary>
        /// Sets the action's name(seemingly pointless)
        /// </summary>
        /// <param name="s">Seemingly pointless arbitrary name</param>
        public override void setStringA(string s)
        {
            name=s;
        }

        /// <summary>
        /// Gets the player whose AI is being activated by number
        /// </summary>
        /// <returns>Number assigned to the player whose AI is being activated</returns>
        public override int getIntA()
        {
            return player;
        }
        /// <summary>
        /// Sets the player whose AI is being activated by number
        /// </summary>
        /// <param name="i">Number assigned to the player whose AI is being activated</param>
        public override void setIntA(int i)
        {
            player=i;
        }

        /// <summary>
        /// Constructs an action that activates player 0's AI
        /// </summary>
        /// <param name="t">Trigger this action is to belong to</param>
        public ActivateAI(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs an action under a given Trigger with a specified name that activates a specified player
        /// </summary>
        /// <param name="t">Trigger this action is to belong to</param>
        /// <param name="aIName">Seemingly pointless arbitrary name</param>
        /// <param name="aIPlayer">Number assigned to the player whose AI is being activated</param>
        public ActivateAI(Trigger t, string aIName, int aIPlayer)
        {
            trig = t;
            name = aIName;
            player = aIPlayer;
        }

        /// <summary>
        /// Encodes the ActivateAI action's name and selected player into the attributes of an XML element that the Ashes Scenario system can read
        /// </summary>
        /// <returns>An XML element that the Ashes Scenario system can read</returns>
        public override string toString()
        {
            return "<ActivateAI Name=\"" + name + "\" Player=\"" + player + "\" />";
        }
        /// <summary>
        /// Produces a string containing the action's type and name
        /// </summary>
        /// <returns>A string containing the action's type and name</returns>
        public override string getSummary()
        {
            return "ActivateAI(" + name + ")";
        }
    }
    /// <summary>
    /// An action that plays a sound
    /// </summary>
    public class PlaySound : Action
    {
        
        /// <summary>
        /// Internal name for the desired sound effect
        /// </summary>
        public string sound;

        /// <summary>
        /// Gets the internal name for the desired sound effect
        /// </summary>
        /// <returns>Internal name for the desired sound effect</returns>
        public override string getStringA()
        {
            return sound;
        }
        /// <summary>
        /// Sets the internal name for the desired sound effect
        /// </summary>
        /// <param name="s">Internal name for the desired sound effect</param>
        public override void setStringA(string s)
        {
            sound = s;
        }

        /// <summary>
        /// Constructs a PlaySound action under a given Trigger that doesn't play any sound until modified
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public PlaySound(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs an action under a given Trigger that plays a specified sound
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="soundName">Internal name for the desired sound effect</param>
        public PlaySound(Trigger t, string soundName)
        {
            trig = t;
            sound = soundName;
        }

        /// <summary>
        /// Encodes the internal name of the sound as the attribute of an XML element that can be read by the Ashes Scenario system
        /// </summary>
        /// <returns>An XML element that can be read by the Ashes Scenario system</returns>
        public override string toString()
        {
            return "<PlaySound Sound=\"" + sound + "\"/>";
        }
        /// <summary>
        /// Produces a string containing the action type and the internal name of the sound effect
        /// </summary>
        /// <returns>A string containing the action type and the internal name of the sound effect</returns>
        public override string getSummary()
        {
            return "PlaySound(" + sound + ")";
        }
    }
    /// <summary>
    /// An action that either pauses or unpauses the flow of in-game time
    /// </summary>
    public class Pause : Action
    {
        /// <summary>
        /// True of this action pauses, false if it unpauses
        /// </summary>
        public bool enable;

        /// <summary>
        /// Gets whether this action pauses or unpauses the flow of in-game time
        /// </summary>
        /// <returns>True of this action pauses, false if it unpauses</returns>
        public override bool getBoolA()
        {
            return enable;
        }
        /// <summary>
        /// Sets whether this action pauses or unpauses the flow of in-game time
        /// </summary>
        /// <param name="b">True of this action pauses, false if it unpauses</param>
        public override void setBoolA(bool b)
        {
            enable=b;
        }

        /// <summary>
        /// Constructs an action under the given Trigger that unpauses the in-game time
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public Pause(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs an action under the given Trigger that can either pause or unpause the in-game time
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="pause">True of this action pauses, false if it unpauses</param>
        public Pause(Trigger t, bool pause)
        {
            trig = t;
            enable = pause;
        }

        /// <summary>
        /// Encodes the action into an XML element that the Ashes scenario system can read
        /// </summary>
        /// <returns>An XML element that the Ashes scenario system can read</returns>
        public override string toString()
        {
            int temp = 0;
            if (enable)
                temp = 1;
            return "<Pause Enable=\"" + temp + "\"/>";
        }
        /// <summary>
        /// Produces a string containing the action's type
        /// </summary>
        /// <returns>"Pause"</returns>
        public override string getSummary()
        {
            return "Pause";
        }
    }
    /// <summary>
    /// An action that changes the AI personality of a given player
    /// </summary>
    public class ChangeAIPersonality : Action
    {
        /// <summary>
        /// Internal name given to the AI personality desired
        /// <para>
        /// Valid Values
        /// *******************
        /// MrBland
        /// MsBland
        /// Skirmisher
        /// Defender
        /// MadBomber
        /// AirPower FTW
        /// Even More AirPower
        /// Ranger
        /// Crusher
        /// Stomp
        /// Faux Airpower
        /// Paranoid about Air
        /// Drone Hiver(SS)
        /// Drone Hiver 2(SS)
        /// Drone Hiver 3(SS)
        /// Avatar(SS)
        /// Avatar 2(SS)
        /// Avatar 3(SS)
        /// Incursion Force(PHC)
        /// Incursion Force2(PHC)
        /// Incursion Force3(PHC)
        /// AndrewZ(PHC)
        /// </para>
        /// </summary>
        public string name;
        /// <summary>
        /// Number assigned to the AI player whose personality you’re changing
        /// </summary>
        public int player;

        /// <summary>
        /// Gets the AI personality selected by its internal name
        /// </summary>
        /// <returns>Internal name given to the AI personality desired</returns>
        public override string getStringA()
        {
            return name;
        }
        /// <summary>
        /// Sets the AI personality selected by its internal name
        /// </summary>
        /// <param name="s">Internal name given to the AI personality desired</param>
        public override void setStringA(string s)
        {
            name=s;
        }

        /// <summary>
        /// Gets the AI player whose personality is being altered by number
        /// </summary>
        /// <returns>Number assigned to the AI player whose personality you’re changing</returns>
        public override int getIntA()
        {
            return player;
        }
        /// <summary>
        /// Sets the AI player whose personality is being altered by number
        /// </summary>
        /// <param name="i">Number assigned to the AI player whose personality you’re changing</param>
        public override void setIntA(int i)
        {
            player=i;
        }

        /// <summary>
        /// Constructs an empty ChangeAIPersonality action that won't even save to the XML document without modification
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public ChangeAIPersonality(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs an action under the given Trigger that changes a specified player's AI to a specified personality
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="personalityName">Internal name given to the AI personality desired</param>
        /// <param name="playerNum">Number assigned to the AI player whose personality you’re changing</param>
        public ChangeAIPersonality(Trigger t, string personalityName, int playerNum)
        {
            trig = t;
            name = personalityName;
            player = playerNum;
        }

        /// <summary>
        /// Encodes the action's type, personality name, and player number as attributes in an XML element that can be read by the Ashes scenario system
        /// </summary>
        /// <returns>An XML element that can be read by the Ashes scenario system</returns>
        public override string toString()
        {
            if (name == null || name == "")
            {
                return "";
            }
            return "<ChangeAIPersonality Name=\""+name+"\" Player=\""+player+"\"/>";
        }
        /// <summary>
        /// Produces a string containing the action's type, the player number, and the personality name
        /// </summary>
        /// <returns>A string containing the action's type, the player number, and the personality name</returns>
        public override string getSummary()
        {
            return "ChangeAIPersonality(" + player + " " + name + ")";
        }
    }
    /// <summary>
    /// An action that changes the difficulty of a given AI player
    /// </summary>
    public class ChangeAIDifficulty : Action
    {
        /// <summary>
        /// Number assigned to the player whose difficulty you’re changing
        /// </summary>
        public int player;
        /// <summary>
        /// Internal name for the difficulty desired
        /// <para>
        /// Acceptable Values
        /// ********************
        /// Beginner
        /// Novice
        /// Easy
        /// Intermediate
        /// Normal
        /// Challenging
        /// Tough
        /// Painful
        /// Insane
        /// </para>
        /// </summary>
        public string difficulty;

        /// <summary>
        /// Gets the player whose difficulty is being affected by number
        /// </summary>
        /// <returns>Number assigned to the player whose difficulty you’re changing</returns>
        public override int getIntA()
        {
            return player;
        }
        /// <summary>
        /// Sets the player whose difficulty is being affected by number
        /// </summary>
        /// <param name="i">Number assigned to the player whose difficulty you’re changing</param>
        public override void setIntA(int i)
        {
            player=i;
        }

        /// <summary>
        /// Gets the desired difficulty by its internal name
        /// </summary>
        /// <returns>Internal name for the difficulty desired</returns>
        public override string getStringA()
        {
            return difficulty;
        }
        /// <summary>
        /// Sets the desired difficulty by its internal name
        /// </summary>
        /// <param name="s">Internal name for the difficulty desired
        /// <para>
        /// Acceptable Values
        /// ********************
        /// Beginner
        /// Novice
        /// Easy
        /// Intermediate
        /// Normal
        /// Challenging
        /// Tough
        /// Painful
        /// Insane
        /// </para></param>
        public override void setStringA(string s)
        {
            difficulty=s;
        }

        /// <summary>
        /// Constructs an empty action under the given action that will not save to the XML document until it is modified
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        public ChangeAIDifficulty(Trigger t)
        {
            trig = t;
        }
        /// <summary>
        /// Constructs an action under a given action modifying a specified player's difficulty into a specified value
        /// </summary>
        /// <param name="t">The Trigger this action is to belong to</param>
        /// <param name="aIPlayer">Number assigned to the player whose difficulty you’re changing</param>
        /// <param name="diff">Internal name for the difficulty desired
        /// <para>
        /// Acceptable Values
        /// ********************
        /// Beginner
        /// Novice
        /// Easy
        /// Intermediate
        /// Normal
        /// Challenging
        /// Tough
        /// Painful
        /// Insane
        /// </para></param>
        public ChangeAIDifficulty(Trigger t, int aIPlayer, string diff)
        {
            trig = t;
            player = aIPlayer;
            difficulty = diff;
        }

        /// <summary>
        /// Encodes the action's player number and difficulty as attributes in an XML element that the Ashes scenario system can read
        /// </summary>
        /// <returns>An XML element that the Ashes scenario system can read</returns>
        public override string toString()
        {
            if (difficulty == null)
            {
                return "";
            }
            return "<ChangeAIDifficulty Player=\"" + player + "\" Difficulty=\"" + difficulty + "\" />";
        }
        /// <summary>
        /// Produces a string containing the action's type, selected player, and target difficulty
        /// </summary>
        /// <returns>A string containing the action's type, selected player, and target difficulty</returns>
        public override string getSummary()
        {
            return "ChangeAIDifficulty(" + player + " " + difficulty + ")";
        }
    }

    #endregion
}
