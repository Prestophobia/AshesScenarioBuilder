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
    /// An unimplimented winforms GUI element that would allow actions to be displayed together as a tree structure
    /// </summary>
    class ActionMicro
    {
        public Action selectedAction;
        public TriggerWindow trigWin;
        public Button edit;
        public Panel pan;
        public ActionMicro[] ams;
        int widthAdjust;
        public ActionMini root;
        public ActionMicro parent;

        public ActionMicro(Action act, TriggerWindow tW, int i, ActionMini r, ActionMicro p)
        {
            widthAdjust = i;
            trigWin = tW;
            selectedAction = act;
            root = r;
            parent = p;

            pan = new Panel();
            pan.Size = new Size(100, 20);
            pan.BackColor = Color.AliceBlue;
            edit = new Button();
            edit.Size = new Size(100, 20 - widthAdjust);
            if (selectedAction.getStringA()!=null)
            edit.Text = selectedAction.getStringA();
            edit.Click += edit_Click;
            pan.Controls.Add(edit);
            edit.Location = new Point(0, widthAdjust);
        }

        void edit_Click(object sender, EventArgs e)
        {
            trigWin.setSelectedAction(selectedAction);
        }

        public void addChildren()
        {

            if (selectedAction.hasChildren())
            {
                int y = 25;
                Action[] actions = selectedAction.getChildren();
                ams = new ActionMicro[actions.Length];
                for (int i = 0; i < actions.Length; i++)
                {
                    if (actions[i] != null)
                    {
                        ams[i] = new ActionMicro(actions[i], trigWin, widthAdjust+5,root,this);
                        ams[i].pan.Location = new Point(0, y);
                        pan.Controls.Add(ams[i].pan);
                        root.pan.Height += (ams[i].pan.Height + 5);
                        grow(ams[i].pan.Height + 5);
                        y += (ams[i].pan.Height + 5);
                    }

                }
            }
        }
        public void grow(int i)
        {
            pan.Height += i;
            if (parent != null)
            {
                parent.grow(i);
            }
        }
    }
}
