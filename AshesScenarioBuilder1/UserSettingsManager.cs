using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.IO;
using System.Drawing;
namespace AshesScenarioBuilder1
{
     public class UserSettingsManager
    {
        public string defaultPath;
        public string assetPath;
        public bool hasAssetPath=false;
        public UserSettingsManager()
        {
            defaultPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            if (Directory.Exists("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Ashes of the Singularity Escalation\\Assets"))
            {
                assetPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Ashes of the Singularity Escalation\\Assets";
                hasAssetPath = true;
            }
            else if (Directory.Exists("C:\\Program Files (x86)\\Steam\\steamapps\\common\\Ashes of the Singularity\\Assets"))
            {
                assetPath = "C:\\Program Files (x86)\\Steam\\steamapps\\common\\Ashes of the Singularity\\Assets";
                hasAssetPath = true;
            }
            if (hasAssetPath)
            {
                writeXML(assetPath);
            }
        }
        public UserSettingsManager(string Asset_Path)
        {
            assetPath = Asset_Path;
            hasAssetPath = true;
            defaultPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        }
        public static UserSettingsManager readXML()
        {
            string defPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            
            //defPath = System.IO.Path.GetDirectoryName(defPath);
            if (!File.Exists(defPath+"\\ASBSettings.xml"))
            {
                return new UserSettingsManager();
            }
            string settingsFile = defPath + "\\ASBSettings.xml";
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            settings.IgnoreProcessingInstructions = true;
            XmlReader reader = XmlReader.Create(settingsFile, settings);

            reader.Read();
            if (!reader.Name.Equals("Settings"))
            {
                return new UserSettingsManager();
            }
            reader.MoveToFirstAttribute();
            string assetPath = reader.Value;
            reader.Close();
            return new UserSettingsManager(assetPath);
            /*while (reader.MoveToNextAttribute())
            {
                if (reader.Name.Equals("Description"))
                {
                    output.description = reader.Value;
                }
            }*/

        }
        public bool writeXML(string path)
        {
            if (path == null | path == "")
            {
                return false;
            }
            File.WriteAllText(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\ASBSettings.xml", "<Settings AssetPath=\"" + path + "\"/>");
            
            return true;
        }
    }
}
