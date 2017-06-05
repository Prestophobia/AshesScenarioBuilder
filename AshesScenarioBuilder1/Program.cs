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
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 appInst = new Form1();
            Application.Run(appInst);
        }
    }
    /// <summary>
    ///   <para>
    ///     The Ashes of the Singularity Scenario Builder is an open-source project meant to make creating scenarios in the game easier for devs and modders alike
    ///   </para>
    /// </summary>
    internal class NamespaceDoc
    {
    }
    
}
