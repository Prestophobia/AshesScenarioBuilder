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
    /// An actor in the scenario, be it controlled by the player, AI, or without a brain at all.
    /// </summary>
    public class Player : Action
    {
        /// <summary>
        /// Name given to the player referenced in the UI
        /// </summary>
        public string name;
        /// <summary>
        /// Faction the player is a part of (PHC, SS)
        /// </summary>
        public string faction;
        /// <summary>
        /// Whether the player is controlled by the player, an AI, or nothing (On, Off or Player)
        /// </summary>
        public string aiType;
        //TODO: Provide corresponding difficulty names
        /// <summary>
        /// The difficulty of the AI controlling the player if an AI does control it at all
        /// </summary>
        public string aiDiff;
        /// <summary>
        /// Internal number referenced by both the GUI(The Scenario object's Player array) and the game(Triggers and actions use this rather than a name)
        /// </summary>
        public int id;
        /// <summary>
        /// Number designating which team the player is on
        /// </summary>
        public int team;
        //TODO: Verify color accuracy
        /// <summary>
        /// Number that determines the color of the player's icon, units, buildings, etc.
        /// <para>
        /// Colors:
        /// 0| Reserved for creeps
        /// 1| Red
        /// 2| Blue
        /// 3| Brown
        /// 4| Orange
        /// 5| Yellow
        /// 6| Cyan
        /// 7| Green
        /// </para>
        /// </summary>
        public int color;
        /// <summary>
        /// Starting location on the map (valid values vary by map)
        /// </summary>
        public int startLoc;
        /// <summary>
        /// If true, the player will start without a Nexus
        /// </summary>
        public bool noSeed;
        /// <summary>
        /// If true, the player will start without an engineer
        /// </summary>
        public bool noEngineer;

        /// <summary>
        /// Constructs a normal, red, PHC AI player called "Splinter" with a specified number 
        /// </summary>
        /// <param name="num">Internal number referenced by both the GUI(The Scenario object's Player array) and the game(Triggers and actions use this rather than a name)</param>
        public Player(int num)
        {
            id = num;
            name = "Splinter";
            faction = "PHC";
            aiType = "Off";
            aiDiff = "Normal";
            color = 1;
        }
        /// <summary>
        /// Constructs a player with the specified number, name, faction, team, color, starting location, AI type, and difficulty with options for if it starts with engineers or even has a Nexus(seed)
        /// </summary>
        /// <param name="PlayerNumber">Internal number referenced by both the GUI(The Scenario object's Player array) and the game(Triggers and actions use this rather than a name)</param>
        /// <param name="Name">Name given to the player referenced in the UI</param>
        /// <param name="PlayerFaction">Faction the player is a part of (PHC, SS)</param>
        /// <param name="TeamNumber">Number designating which team the player is on</param>
        /// <param name="PlayerColor">Number that determines the color of the player's icon, units, buildings, etc.</param>
        /// <param name="StartingLocation">Starting location on the map (valid values vary by map)</param>
        /// <param name="AIType">Whether the player is controlled by the player, an AI, or nothing (On, Off or Player)</param>
        /// <param name="AIDifficulty">The difficulty of the AI controlling the player if an AI does control it at all</param>
        /// <param name="NoSeed">If true, the player will start without a Nexus</param>
        /// <param name="NoEngineer">If true, the player will start without an engineer</param>
        public Player(int PlayerNumber, string Name, string PlayerFaction, int TeamNumber, int PlayerColor, int StartingLocation, string AIType, string AIDifficulty, bool NoSeed, bool NoEngineer)
        {
            id = PlayerNumber;
            name = Name;
            faction = PlayerFaction;
            team = TeamNumber;
            color = PlayerColor;
            startLoc = StartingLocation;
            aiType = AIType;
            aiDiff = AIDifficulty;
            noSeed = NoSeed;
            noEngineer = NoEngineer;         
        }
        /// <summary>
        /// Constructs a new empty player with an id of 0
        /// </summary>
        public Player()
        {
            id = 0;
        }
       
        /// <summary>
        /// Produces a string encoding all of the player's data in an XML format that can be read by the Ashes scenario system
        /// </summary>
        /// <returns>A string encoding all of the player's data in an XML format that can be read by the Ashes scenario system</returns>
        public override string toString()
        {
            string output = "<Player Name=\"" + name + "\" Faction=\"" + faction + "\" Team=\"" + team + "\" Color=\"" + color + "\" StartLocation=\"" + startLoc + "\" AIType=\"" + aiType + "\"";
            if (aiDiff != null && !aiDiff.Equals("Player"))
            {
                output += " AIDifficulty=\"" + aiDiff + "\"";
            }
            if (noSeed)
            {
                output += " NoSeed=\"1\"";
            }
            if (noEngineer)
            {
                output += " NoEngineer=\"1\"";
            }
            return output += " />";
        }
    }
}
