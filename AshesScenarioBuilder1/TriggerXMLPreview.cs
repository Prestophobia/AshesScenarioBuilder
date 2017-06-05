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
    public partial class TriggerXMLPreview : Form
    {
        public TriggerXMLPreview()
        {
            InitializeComponent();
        }
        public void update(string newText)
        {
            previewText.Text = newText.Replace("\n", Environment.NewLine);
        }
    }
}
