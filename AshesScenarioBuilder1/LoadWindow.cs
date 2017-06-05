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
    /// <summary>
    /// Unimplemented loading pop-up
    /// </summary>
    public partial class LoadWindow : Form
    {
        /// <summary>
        /// Opens up the window itself
        /// </summary>
        public LoadWindow()
        {
            InitializeComponent();
            
        }

        private void LoadWindow_Load(object sender, EventArgs e)
        {
            Controls.Add(pictureBox1);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
