using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AshesScenarioBuilder1
{
    public partial class DebugForm : Form
    {
        Form1 mainWindow;
        public DebugForm()
        {
            InitializeComponent();
        }

        public DebugForm(Form1 parent)
        {
            InitializeComponent();
            mainWindow = parent;
        }
        public void update()
        {
            Trigger[] inorder = Trigger.sortTriggersChrono(mainWindow.scen1.triggers);
            string output = "";
            int i = 1;
            int j = 1;
            foreach(Trigger curTrig in mainWindow.scen1.triggers)
            {
                output += "\n***" + i + "***\r\n\n";
                output += curTrig.toString();
                output += "\r\n";
                Trigger[] prior = curTrig.getAllPriorTriggers();
                Trigger[] after = curTrig.getTriggersAfter();
                j = 1;
                if(prior!=null)foreach(Trigger curPrior in prior)
                    {
                        output += "\n***" + i + " Prior:"+j+"***\r\n\n";
                        output += curTrig.toString();
                        output += "\r\n";
                        j++;
                    }
                j = 1;
                if (after != null) foreach (Trigger curPrior in after)
                    {
                        output += "\n***" + i + " After:" + j + "***\r\n\n";
                        output += curTrig.toString();
                        output += "\r\n";
                        j++;
                    }
                output += "\r\n";
                i++;
                console.Text = output;
            }
            console.Text = output;
        }

    }
}
