using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;



namespace AshesScenarioBuilder1
{
    /// <summary>
    /// An object representing the CSV data, which contains the dialogue, UI text, objective names, and other displayed text
    /// </summary>
    public class CSVDataSheet
    {
        /// <summary>
        /// An array of individual key-translated string pairings
        /// </summary>
        public CSVEntry[] entries;
        /// <summary>
        /// Creates an empty data table
        /// </summary>
        public CSVDataSheet()
        {
            entries = new CSVEntry[0];
        }
        /// <summary>
        /// Reads a CSV file and stores the table as a set of key-translated string pairings
        /// </summary>
        /// <param name="path">The search path of the CSV file to be loaded</param>
        public CSVDataSheet(string path)
        {
            if (System.IO.Path.GetExtension(path).Equals(".csv") && File.Exists(path))
            {
                string allText = System.IO.File.ReadAllText(path);
                string[] rows = allText.Split('\n');
                string[][] rowsAndCol = new string[rows.Length][];
                entries = new CSVEntry[rows.Length];
                for (int i = 0; i < rows.Length; i++)
                {
                    rowsAndCol[i] = rows[i].Split(',');
                    if (rowsAndCol[i].Length > 2)
                    {
                        for (int q = 2; q < rowsAndCol[i].Length; q++)
                        {
                            rowsAndCol[i][1] +=(","+ rowsAndCol[i][q]);
                        }
                        string[] tempS = new string[2];
                        tempS[0] = rowsAndCol[i][0];                        
                        tempS[1] = rowsAndCol[i][1];
                        rowsAndCol[i] = tempS;                   
                    }
                    if (rowsAndCol[i].Length>1&&rowsAndCol[i][1]!=""&&rowsAndCol[i][1][0] == (char)34)
                    {
                        string x = rowsAndCol[i][1].Remove(0, 1);
                        x = x.Remove(x.Length - 1, 1);
                        rowsAndCol[i][1] = x;
                    }
                }
                for (int i = 0; i < entries.Length; i++)
                {
                    if(rowsAndCol[i].Length>1)
                    entries[i] = new CSVEntry(rowsAndCol[i][0], rowsAndCol[i][1]);
                }
                CSVEntry[] temp = new CSVEntry[entries.Length - 1];
                for (int i = 1; i < entries.Length; i++)
                {
                    temp[i - 1] = entries[i];
                }
                entries = temp;
                triggerEvent();
            }
            else
            {
                entries = new CSVEntry[0];
            }
            
        }

        public CSVDataSheet(string path, bool acceptsCommas)
        {
            System.Diagnostics.Debug.Assert(true, path);

            if (File.Exists(path))
            {
                string allText = System.IO.File.ReadAllText(path);
                string[] rows = allText.Split('\n');
                string[][] rowsAndCol = new string[rows.Length][];
                entries = new CSVEntry[rows.Length];
                for (int i = 0; i < rows.Length; i++)
                {
                    rowsAndCol[i] = rows[i].Split(',');
                    if (rowsAndCol[i].Length > 2 && acceptsCommas)
                    {
                        for (int q = 2; q < rowsAndCol[i].Length; q++)
                        {
                            rowsAndCol[i][1] += ("," + rowsAndCol[i][q]);
                        }
                        string[] tempS = new string[2];
                        tempS[0] = rowsAndCol[i][0];
                        tempS[1] = rowsAndCol[i][1];
                        rowsAndCol[i] = tempS;
                    }
                    if (rowsAndCol[i].Length > 1 && rowsAndCol[i][1] != "" && rowsAndCol[i][1][0] == (char)34)
                    {
                        string x = rowsAndCol[i][1].Remove(0, 1);
                        x = x.Remove(x.Length - 1, 1);
                        rowsAndCol[i][1] = x;
                    }
                }
                for (int i = 0; i < entries.Length; i++)
                {
                    if (rowsAndCol[i].Length > 1)
                        entries[i] = new CSVEntry(rowsAndCol[i][0], rowsAndCol[i][1]);
                }
                CSVEntry[] temp = new CSVEntry[entries.Length - 1];
                for (int i = 1; i < entries.Length; i++)
                {
                    temp[i - 1] = entries[i];
                }
                entries = temp;
                //triggerEvent();
            }
            else
            {
                entries = new CSVEntry[0];
            }

        }
        /// <summary>
        /// Retrieves the translated string given its paired key
        /// </summary>
        /// <param name="key">The key to be searched</param>
        /// <returns>The key's translated string</returns>
        public string getTranslatedString(string key)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                if(entries[i]!=null&&entries[i].key!=null&&entries[i].key.Equals(key))
                {
                    return entries[i].translatedString;
                }
            }
            return "";
        }

        public string getKey(string translatedString)
        {
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i] != null && entries[i].translatedString != null && entries[i].translatedString.Equals(translatedString))
                {
                    return entries[i].key;
                }
            }
            return "";
        }
        /// <summary>
        /// Overwrites a key's translated string with a new value
        /// </summary>
        /// <param name="key">The key that belongs to the translated string to be overwitten</param>
        /// <param name="translatedString">The new translated string</param>
        public void overwriteEntry(string key, string translatedString)
        {
            if (key==null||key.Equals(""))
            {
                return;
            }
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i]!=null && entries[i].key != null && entries[i].key.Equals(key))
                {
                    entries[i].translatedString=translatedString;
                    return;
                }
            }
            addEntry(new CSVEntry(key, translatedString));
            return;
        }
        /// <summary>
        /// Adds a blank CSV entry
        /// </summary>
        public void addEntry()
        {
            CSVEntry[] temp = new CSVEntry[entries.Length + 1];
            for (int i = 0; i < entries.Length; i++)
            {
                temp[i] = entries[i];
            }
            temp[entries.Length] = new CSVEntry();
            entries = temp;
            OnDataChanged(EventArgs.Empty);
        }
        /// <summary>
        /// Adds a pre-existing CSV entry to the data table
        /// </summary>
        /// <param name="entry">The CSV entry to be added</param>
        public void addEntry(CSVEntry entry)
        {
            if (entries == null)
            {
                entries = new CSVEntry[1];
                entries[0] = entry;
                return;
            }
            CSVEntry[] temp = new CSVEntry[entries.Length + 1];
            for (int i = 0; i < entries.Length; i++)
            {
                temp[i] = entries[i];
            }
            temp[entries.Length] = entry;
            entries = temp;
            OnDataChanged(EventArgs.Empty);
        }
        /// <summary>
        /// Determines if the data table has an entry with the specified key
        /// </summary>
        /// <param name="key">The key being searched</param>
        /// <returns>Returns true if there exists an entry with the given key, false if not</returns>
        public bool hasEntry(string key)
        {
            if (entries == null)
            {
                return false;
            }
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i] != null && entries[i].key != null && entries[i].key.Equals(key))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Adds an entry tro the data table with the specified key and no translated string
        /// </summary>
        /// <param name="key">The key of the new entry</param>
        public void addEntry(string key)
        {
            CSVEntry[] temp = new CSVEntry[entries.Length + 1];
            for (int i = 0; i < entries.Length; i++)
            {
                temp[i] = entries[i];
            }
            temp[entries.Length] = new CSVEntry(key,"");
            entries = temp;
            OnDataChanged(EventArgs.Empty);
        }
        /// <summary>
        /// Triggers the OnDataChanged event
        /// </summary>
        public void triggerEvent()
        {
            OnDataChanged(EventArgs.Empty);
        }
        /// <summary>
        /// Converts the object back into CSV format
        /// </summary>
        /// <returns>A string of representing the object in CSV format</returns>
        public string toString()
        {
            string output = "Key,Translated String\n";
            if (entries != null &&entries.Length>0)
            {
                output += entries[0].toString();
                if (entries.Length > 1)
                {
                    for (int i=1; i < entries.Length; i++)
                    {
                        if(entries[i]!=null)
                        output += "\n" + entries[i].toString();
                    }
                }
            }
            return output;
        }
        /// <summary>
        /// Event handler interacting with the main GUI (unimplemented)
        /// </summary>
        public event EventHandler DataChanged;
        /// <summary>
        /// Triggers Datachanged
        /// </summary>
        /// <param name="e">Event system *magic*</param>
        protected virtual void OnDataChanged(EventArgs e)
        {
            if (DataChanged != null)
            {
                DataChanged(this, e);
            }
        }
        /// <summary>
        /// Writes the data table to a CSV file
        /// </summary>
        /// <param name="path">The path of the file to write to</param>
        /// <returns>Returns true when complete</returns>
        public bool writeCSVFile(string path)
        {
            string output = toString();
            File.WriteAllText(path, output);
            return true;
        }

    }
    /// <summary>
    /// Simple object representing the pairing of a key and its translated string
    /// </summary>
    public class CSVEntry
    {
        /// <summary>
        /// The strings representing the key and translated string
        /// </summary>
        public string key, translatedString;
        /// <summary>
        /// Creates a key-string pairing
        /// </summary>
        /// <param name="k">The key</param>
        /// <param name="ts">The translated string</param>
        public CSVEntry(string k, string ts)
        {
            key = k.Trim();
            translatedString = ts.Trim();
        }
        /// <summary>
        /// Creates an empty CSV entry
        /// </summary>
        public CSVEntry()
        {
            key = "";
            translatedString = "";
        }
        /// <summary>
        /// Creates a string representing the key and translated string separated by a comma
        /// </summary>
        /// <returns>A string representing the key and translated string separated by a comma</returns>
        public string toString()
        {
            return key + "," + "\""+translatedString+"\"";
        }
    }
}
