using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.IO;
using System.Drawing;
//using Microsoft.Xna.Framework;
namespace AshesScenarioBuilder1 
{
    public class AshesAssetHandler
    {
        CSVDataSheet unitText;
        CSVDataSheet buildingText;
        CSVDataSheet text;
        CSVDataSheet unitTemplates;
        CSVDataSheet buildingTemplates;
        CSVDataSheet aINames;
        CSVDataSheet researchTypes;
        CSVDataSheet sfx;
        CSVDataSheet orbitals;

        public AshesAssetHandler(UserSettingsManager USM)
        {

            if (USM.hasAssetPath)
            {
                System.Diagnostics.Debug.Assert(true, USM.assetPath + @"\Gamecore\UnitTemplates.csv");
                unitTemplates = new CSVDataSheet(USM.assetPath + @"\Gamecore\UnitTemplates.csv", false);
                buildingTemplates = new CSVDataSheet(USM.assetPath + "\\Gamecore\\BuildingTemplates.csv", false);
                aINames = new CSVDataSheet(USM.assetPath + @"\Gamecore\AINames.csv", true);
                researchTypes = new CSVDataSheet(USM.assetPath +@"\Gamecore\Research.csv", false);

                unitText = new CSVDataSheet(USM.assetPath+@"\UIText\UnitText.csv");
                buildingText = new CSVDataSheet(USM.assetPath + @"\UIText\BuildingText.csv");
                text = new CSVDataSheet(USM.assetPath + @"\UIText\Text.csv");

                sfx = new CSVDataSheet(USM.assetPath + @"\Audio\UI_Game.audiogroup",false);

                //TODO: find if there is an actual in-game asset describing these
                orbitals = new CSVDataSheet(USM.defaultPath+@"\orbitals.csv",false);
            }
            else
            {
                unitTemplates = new CSVDataSheet();
                unitTemplates.entries = new CSVEntry[1];
                unitTemplates.entries[0] = new CSVEntry("STRING NOT FOUND", "STRING NOT FOUND");
                buildingTemplates = new CSVDataSheet();
                buildingTemplates.entries = new CSVEntry[1];
                buildingTemplates.entries[0] = new CSVEntry("STRING NOT FOUND", "STRING NOT FOUND");
                aINames = new CSVDataSheet();
                aINames.entries = new CSVEntry[1];
                aINames.entries[0] = new CSVEntry("STRING NOT FOUND", "STRING NOT FOUND");
                researchTypes = new CSVDataSheet();
                researchTypes.entries = new CSVEntry[1];
                researchTypes.entries[0] = new CSVEntry("STRING NOT FOUND", "STRING NOT FOUND");

                unitText = new CSVDataSheet();
                unitText.entries = new CSVEntry[1];
                unitText.entries[0] = new CSVEntry("STRING NOT FOUND", "STRING NOT FOUND");
                buildingText = new CSVDataSheet();
                buildingText.entries = new CSVEntry[1];
                buildingText.entries[0] = new CSVEntry("STRING NOT FOUND", "STRING NOT FOUND");
                text = new CSVDataSheet();
                text.entries = new CSVEntry[1];
                text.entries[0] = new CSVEntry("STRING NOT FOUND", "STRING NOT FOUND");

                sfx = new CSVDataSheet();
                sfx.entries = new CSVEntry[1];
                sfx.entries[0] = new CSVEntry("STRING NOT FOUND", "STRING NOT FOUND");

                //TODO: find if there is an actual in-game asset describing these
                orbitals = new CSVDataSheet();
                orbitals.entries = new CSVEntry[1];
                orbitals.entries[0]= new CSVEntry("STRING NOT FOUND", "STRING NOT FOUND");
            }
        }
        /*
        static string[] readCSVKeys(UserSettingsManager USM,string relativePath)
        {
            string[] output;
            if (!USM.hasAssetPath)
            {
                output = new string[1];
                output[0] = "ASSETS NOT FOUND";
            }
            else
            {
                string filepath = USM.assetPath + relativePath;
                output = new string[0];

                if (!File.Exists(filepath))
                {
                    output = new string[1];
                    output[0] = "ASSETS NOT FOUND";
                    return output;
                }

                string allText = System.IO.File.ReadAllText(filepath);
                output = allText.Split('\n');
                for (int i = 0; i < output.Length; i++)
                {
                    int index = output[i].IndexOf(",");
                    if (index > 0)
                        output[i] = output[i].Substring(0, index);
                }
            }
            return output;
        }

        static string[] readDisplayedNames(UserSettingsManager USM, string templateRelativePath, string UITextRelativePath)
        {
            string[] output;
            if (!USM.hasAssetPath)
            {
                output = new string[1];
                output[0] = "ASSETS NOT FOUND";
            }
            else
            {
                string templatefilepath = USM.assetPath + templateRelativePath;
                output = new string[0];

                if (!File.Exists(filepath))
                {
                    output = new string[1];
                    output[0] = "ASSETS NOT FOUND";
                    return output;
                }

                string allText = System.IO.File.ReadAllText(filepath);
                output = allText.Split('\n');
                for (int i = 0; i < output.Length; i++)
                {
                    int index = output[i].IndexOf(",");
                    if (index > 0)
                        output[i] = output[i].Substring(0, index);
                }
            }
            return output;
        }
        */

        public void reload(UserSettingsManager USM)
        {
            if (USM.hasAssetPath)
            {
                System.Diagnostics.Debug.Assert(true, USM.assetPath + @"\Gamecore\UnitTemplates.csv");
                unitTemplates = new CSVDataSheet(USM.assetPath + @"\Gamecore\UnitTemplates.csv", false);
                buildingTemplates = new CSVDataSheet(USM.assetPath + "\\Gamecore\\BuildingTemplates.csv", false);
                aINames = new CSVDataSheet(USM.assetPath + @"\Gamecore\AINames.csv", false);
                researchTypes = new CSVDataSheet(USM.assetPath + @"\Gamecore\Research.csv", false);

                unitText = new CSVDataSheet(USM.assetPath + @"\UIText\UnitText.csv");
                buildingText = new CSVDataSheet(USM.assetPath + @"\UIText\BuildingText.csv");
                text = new CSVDataSheet(USM.assetPath + @"\UIText\Text.csv");
            }
        }
        public string getUnitTemplateName(string displayedName)
        {
            return unitTemplates.getKey(unitText.getKey(displayedName));
        }
        public string getUnitDisplayedName(string templateName)
        {
            return unitText.getTranslatedString(unitTemplates.getTranslatedString(templateName));
        }
        public string getBuildingTemplateName(string displayedName)
        {
            return buildingTemplates.getKey(buildingText.getKey(displayedName));
        }
        public string getBuildingDisplayedName(string templateName)
        {
            return buildingText.getTranslatedString(buildingTemplates.getTranslatedString(templateName));
        }
        public string getResearchInternalName(string displayedName)
        {
            return text.getKey(displayedName);
        }
        public string getResearchDisplayedName(string internalName)
        {
            return text.getTranslatedString(internalName);
        }
        public string[] getAINames()
        {
            string[] keys = getNames(aINames, true);
            string[] tS = getNames(aINames, false);
            string[] details;
            string[] output = new string[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] == null )
                {
                    output[i] = "";
                }
                else
                {
                    output[i]=keys[i];
                    if (tS[i] != null)
                    {
                        output[i] += "(";
                        details = tS[i].Split(',');
                        if(details.Length>1){
                            output[i]+=details[1];
                        }
                        if (details.Length > 2)
                        {
                            output[i] += ", "+details[2];
                        }
                        output[i] += ")";
                    }
                }
            }
            return output;
        }
        public string[] getUnitNames()
        {
            string[]templates= getNames(unitTemplates, true);
            string[] output = new string[templates.Length];
            for (int i = 0; i < templates.Length; i++)
            {
                if (templates[i] == null || getUnitDisplayedName(templates[i]) == null)
                {
                    output[i] = "";
                }
                else
                    output[i] = getUnitDisplayedName(templates[i]);
            }
            return output;
        }
        public string[] getBuildingNames()
        {
            string[] templates = getNames(buildingTemplates, true);
            string[] output = new string[templates.Length];
            for (int i = 0; i < templates.Length; i++)
            {
                if (templates[i] == null || getBuildingDisplayedName(templates[i]) == null)
                {
                    output[i] = "";
                }
                else
                    output[i] = getBuildingDisplayedName(templates[i]);
            }
            return output;
        }
        public string[] getResearchNames()
        {
            string[] keys = getNames(text, true);
            string[] output = new string[0];
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] != null && keys[i].Contains("PHC_Tech_") && !(keys[i].Contains("Desc"))) 
                {
                    string[] temp = new string[output.Length + 1];
                    for (int j = 0; j < output.Length; j++)
                    {
                        temp[j] = output[j];
                    }
                    temp[temp.Length - 1] = keys[i];
                    output = temp;
                }
            }
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = getResearchDisplayedName(output[i]);
            }
                return output;
        }

        public string[] getSFXNames()
        {
            string[] keys = getNames(sfx, true);
            string[] output = new string[0];
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i] != null&&keys[i]!="")
                {
                    string[] temp = new string[output.Length + 1];
                    for (int j = 0; j < output.Length; j++)
                    {
                        temp[j] = output[j];
                    }
                    temp[temp.Length - 1] = keys[i];
                    output = temp;
                }
            }
            return output;
        }
        public string[] getOrbitalNames()
        {
            string[] output = getNames(orbitals, true);
            for (int i = 0; i < output.Length; i++)
            {
                if(output[i]!=null&&output[i]!="")
                output[i]=output[i].Trim();
            }
                return output;
        }
        public string getOrbitalInternalName(string displayedName)
        {
           return  orbitals.getTranslatedString(displayedName).Trim();
        }
        public string getOrbitalDisplayedName(string internalName)
        {
            return orbitals.getKey(internalName).Trim();
        }
        public string findAnyInternalName(string displayedName)
        {
            string output = getUnitTemplateName(displayedName);
            if (output != "")
                return output;
            output = getBuildingTemplateName(displayedName);
            if (output != "")
                return output;
            output = getResearchInternalName(displayedName);
            if (output != "")
                return output;
            output = getOrbitalInternalName(displayedName);
            return output;
        }
        public string findAnyDisplayedName(string internalName)
        {
            string output = getUnitDisplayedName(internalName);
            if (output != "")
                return output;
            output = getBuildingDisplayedName(internalName);
            if (output != "")
                return output;
            output = getResearchDisplayedName(internalName);
            if (output != "")
                return output;
            output = getOrbitalDisplayedName(internalName);
            return output;
        }
        public string[] getBuildingAndUnitNames()
        {
            string[] units = getUnitNames();
            string[] buildings = getBuildingNames();
            string[] output = new string[units.Length+buildings.Length];
            for (int i = 0; i < output.Length; i++)
            {
                if (i < units.Length)
                {
                    output[i] = units[i];
                }
                else
                {
                    output[i] = buildings[i - units.Length];
                }
            }
            return output;
        }
        public string[] getRestrictValues()
        {
            string[] uAB = getBuildingAndUnitNames();
            string[] orb = getOrbitalNames();
            string[] output = new string[uAB.Length + orb.Length];
            for (int i = 0; i < output.Length; i++)
            {
                if (i < uAB.Length)
                {
                    output[i] = uAB[i];
                }
                else
                {
                    output[i] = orb[i - uAB.Length];
                }
            }
            return output;
        }
        string[] getNames(CSVDataSheet csv,bool isKey)
        {
            string[] output;
            if (csv==null||csv.entries==null||csv.entries.Length <= 1)
            {
                output = new string[1];
                output[0] = "NAMES NOT FOUND";
            }
            else
            {
                output = new string[csv.entries.Length];
                for (int i = 1; i < csv.entries.Length; i++)
                {
                    if (isKey)
                    {
                        if (csv.entries[i] == null || csv.entries[i].key == null)
                        {
                            output[i] = "";
                        }
                        else
                            output[i] = csv.entries[i].key;
                    }
                    else{
                        if (csv.entries[i]==null||csv.entries[i].translatedString == null)
                        {
                            output[i] = "";
                        }
                        else
                        output[i] = csv.entries[i].translatedString;
                    }                    
                }
            }
            return output;
        }
        public bool verify(UserSettingsManager USM)
        {
            if (USM.hasAssetPath)
            {
                System.Diagnostics.Debug.Assert(true, USM.assetPath + @"\Gamecore\UnitTemplates.csv");
                unitTemplates = new CSVDataSheet(USM.assetPath + @"\Gamecore\UnitTemplates.csv", false);
                buildingTemplates = new CSVDataSheet(USM.assetPath + "\\Gamecore\\BuildingTemplates.csv", false);
                aINames = new CSVDataSheet(USM.assetPath + @"\Gamecore\AINames.csv", false);
                researchTypes = new CSVDataSheet(USM.assetPath + @"\Gamecore\Research.csv", false);

                unitText = new CSVDataSheet(USM.assetPath + @"\UIText\UnitText.csv");
                buildingText = new CSVDataSheet(USM.assetPath + @"\UIText\BuildingText.csv");
                text = new CSVDataSheet(USM.assetPath + @"\UIText\Text.csv");
                return true;
            }
            return false;
        }
       
    }
}
