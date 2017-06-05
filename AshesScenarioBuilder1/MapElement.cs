using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AshesScenarioBuilder1
{
    class MapElement
    {
       
        #region id values
        public const UInt16
            FACTION_PHC = 0x1000,
            FACTION_SS = 0x2000,
            FACTION_CREEP = 0x3000,

            TEAM_0 = 0x0100,
            TEAM_1 = 0x0200,
            TEAM_2 = 0x0300,
            TEAM_3 = 0x0400,
            TEAM_4 = 0x0500,
            TEAM_5 = 0x0600,
            TEAM_6 = 0x0700,
            TEAM_7 = 0x0800,

            TYPE_T1 = 0x0010,
            TYPE_T2 = 0x0020,
            TYPE_T3 = 0x0030,
            TYPE_TA = 0x0040,
            TYPE_RADIOACTIVE = 0x0050,
            TYPE_METAL = 0x0060,
            TYPE_FACTORY = 0x0070,
            TYPE_DEFENSE = 0x0080,
            TYPE_NEXUS = 0x0090,
            TYPE_GENERATOR = 0x00A0,
            TYPE_SCRIPTING = 0x00B0
        ;
        #endregion

        #region variables
        PictureBox m_pictureBox;
        public Bitmap m_icon;
        string m_name;
        string m_template;
        string m_parent;
        string m_type;
        bool m_immortality;
        bool m_isPrior;
        bool m_camSave;
        bool m_camLoad;
        int m_player = -1;
        public MapElement next;
        Map map;
        ushort m_ID;
        public readonly Position m_coordinates;
        Position m_rotation;
        int m_radius=0;
        #endregion

        #region constructors
        public MapElement()
        {
            
        }
        
        public MapElement(SpawnUnit sU, Map m, bool isPrior)
        {
            m_name = sU.name;
            m_template = sU.template;
            m_immortality = sU.noDeath;
            m_player = sU.player;
            m_parent = sU.parent;
            m_coordinates = sU.pos;
            m_isPrior = isPrior;
            m_type = "su";
            map = m;
            //initPictureBox();
        }
        
        public MapElement(SpawnBuilding sB, Map m,bool isPrior)
        {
            m_name = sB.name;
            m_template = sB.template;
            m_player = sB.player;
            m_coordinates = sB.pos;
            m_isPrior = isPrior;
            m_type = "sb";
            map = m;
           // initPictureBox();
        }

        public MapElement(AttackAttackMove aAM, Map m, bool isPrior)
        {
            m_name = aAM.name;
            m_coordinates = aAM.pos;
            m_isPrior = isPrior;
            m_type = "aam";
            map = m;
        }
        
        public MapElement(AreaIndicator aI, Map m, bool isPrior)
        {
            m_name = aI.name;
            m_coordinates = aI.pos;
            if(aI.color!=null)m_template = aI.color.ToLower();
            m_isPrior = isPrior;
            m_type = "ai";
            m_radius = (int)aI.size;
            map = m;
           // initPictureBox();
        }
        
        public MapElement(Reveal r, Map m, bool isPrior)
        {
            m_name = r.name;
            m_coordinates = r.pos;
            m_isPrior = isPrior;
            m_type = "r";
            m_radius = (int)r.size;
            map = m;
           // initPictureBox();
        }

        public MapElement(DestroyUnit dU, Map m, bool isPrior)
        {
            m_name = dU.name;
            map = m;
            m_isPrior = isPrior;
            m_coordinates = Trigger.getCurrentPosition(dU.name, dU.trig);
            m_type = "du";
            //initPictureBox();
        }

        public MapElement(Camera c, Map m, bool isPrior)
        {
            m_coordinates = c.pos;
            m_rotation = c.rtp;
            m_camSave = c.save;
            m_camLoad = c.load;
            m_isPrior = isPrior;
            m_type = "c";
            map = m;
            //initPictureBox();
        }

        public MapElement(DestroyBuilding dB, Map m, bool isPrior)
        {
            m_name = dB.name;
            m_type = "db";
            map = m;
            m_isPrior = isPrior;
            Position temp = dB.trig.scen.findSpawnBuilding(dB.name).pos;
            if (temp != null) m_coordinates = temp;
            //initPictureBox();
        }

        public MapElement(CaptureNearest cN, Map m, bool isPrior)
        {
            m_name = cN.name;
            m_type = "cn";
            map = m;
            m_isPrior = isPrior;
        }

        public MapElement(Trigger t, Map m)
        {
            m_name = t.id;
            m_type = t.type.ToLower();
            map = m;
            if (m_type.Equals("area") || m_type.Equals("build"))
            {
                m_coordinates = t.center;
                m_radius = t.size;
                //initPictureBox();
            }
            else if (m_type.Equals("destruction"))
            {
                if (!t.isBuilding) m_coordinates = Trigger.getCurrentPosition(t.target, t);
                else
                {
                    SpawnBuilding temp = t.scen.findSpawnBuilding(t.target);
                    if (temp != null) m_coordinates = temp.pos;
                    else m_coordinates = new Position();
                }
                //initPictureBox();

            }
            else if (m_type.Equals("zonecapture"))
            {
                m_coordinates = t.position;
                m_player = t.owner;

                //initPictureBox();
            }           
        }
        #endregion

        #region private methods
        void initPictureBox()
        {
            m_pictureBox = new PictureBox();
            m_pictureBox.BackColor = Color.Transparent;
            m_pictureBox.Image = getIcon();
            m_pictureBox.Size = new Size(20, 20);
            m_pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            m_pictureBox.Visible = false;
            if (m_radius > 0)
            {
                m_pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                Point temp = map.toScreenSpace(new Point(m_radius,m_radius));
                m_pictureBox.Size = new Size(temp.X, temp.Y);
            }
            m_pictureBox.Location = new Point(map.surface.Left, map.surface.Top);
            if (m_coordinates != null)
                m_pictureBox.Location= map.worldSpaceCartesianToScreenSpace((Point)m_coordinates);       
            map.surface.Controls.Add(m_pictureBox);
            string useless="funny scream";         
        }
        #endregion

        #region public methods
        public void showPictureChain()
        {
            if (m_pictureBox != null)
            {
                m_pictureBox.BringToFront();
                m_pictureBox.Show();
            }
            if (next != null) next.showPictureChain();
        }
        public void updatePositionChain()
        {
            Point newC = map.worldSpaceCartesianToScreenSpace((Point)m_coordinates);
            m_pictureBox.Location = newC;
            if (next!=null) next.updatePositionChain();            
        }
        public void updateSize()
        {
            if (m_radius > 0)
            {
                int w = m_radius * 2;
                int l = w;
                float vsf = map.getScreenSpaceVSF(map);
                float hsf = map.getScreenSpaceHSF(map);
                w = (int)(w*hsf);
                l = (int)(l*vsf);
                m_pictureBox.Size = new System.Drawing.Size(w, l);
            }
            if (next != null) next.updateSize();           
        }

        public void disposeChain()
        {
            if (m_pictureBox!=null) m_pictureBox.Dispose();
            if (next!= null) next.disposeChain();          
        }

        public MapElement search(UInt16 searchID, Position searchCoordinates)
        {
            if (m_ID == searchID && m_coordinates.x==searchCoordinates.x && searchCoordinates.y == m_coordinates.y)
                return this;
            else if (next != null) return next.search(searchID,searchCoordinates);
            return null;        
        }
        
        public Bitmap getIcon()
        {
            Bitmap output = Properties.Resources.empty;
            if (m_type.Equals("su"))
            {
                if (m_template != null && m_template.Length>4)
                {
                    string tempLower;
                    if (m_template != null) tempLower = m_template.ToLower();
                    else tempLower = "pppppppp";//safe and won't break
                    if (tempLower[0] == 'p')
                    {
                        switch (tempLower[4])
                        {
                            case '2':

                                //if (m_isPrior) output = Properties.Resources.t2;
                                //else output = Properties.Resources.t2s;
                                output = Properties.Resources.t2Tiny;
                                //phc t2
                                break;
                            case '3':
                                //if (m_isPrior) output = Properties.Resources.t3;
                                //else output = Properties.Resources.t3s;
                                output = Properties.Resources.t3Tiny;
                                //phc t3
                                break;
                            case 'a':
                                //if (m_isPrior) output = Properties.Resources.ta;
                                //else output = Properties.Resources.tas;
                                output = Properties.Resources.taTiny;
                                //phc ta
                                break;
                            default:
                                //if (m_isPrior) output = Properties.Resources.t1;
                                //else output = Properties.Resources.t1s;
                                output = Properties.Resources.t1Tiny;
                                //phc t1
                                break;
                        }
                        if (tempLower.Equals("phc_2_engineer"))
                            output = Properties.Resources.engiTiny;
                        
                    }
                    else if (tempLower[0] == 's')
                    {
                        switch (tempLower[3])
                        {
                            case '2':

                                //if (m_isPrior) output = Properties.Resources.t2;
                                //else output = Properties.Resources.t2s;
                                output = Properties.Resources.t2Tiny;
                                //phc t2
                                break;
                            case '3':
                                //if (m_isPrior) output = Properties.Resources.t3;
                                //else output = Properties.Resources.t3s;
                                output = Properties.Resources.t3Tiny;
                                //phc t3
                                break;
                            case 'a':
                                //if (m_isPrior) output = Properties.Resources.ta;
                                //else output = Properties.Resources.tas;
                                output = Properties.Resources.taTiny;
                                //phc ta
                                break;
                            default:
                                //if (m_isPrior) output = Properties.Resources.t1;
                                //else output = Properties.Resources.t1s;
                                output = Properties.Resources.t1Tiny;
                                //phc t1
                                break;
                        }
                        if (tempLower.Equals("ss_2_construction"))
                            output = Properties.Resources.engiTiny;
                    }
                    else
                    {
                        output = Properties.Resources.t1Tiny;
                        //creeps
                    }
                    
                }
                else
                {
                    output = Properties.Resources.t1Tiny;
                }
            }
            else if (m_type.Equals("sb"))
            {
                string tempLower;
                if (m_template != null) tempLower = m_template.ToLower();
                else tempLower = "pppppppp";//safe and won't break

                if (tempLower[0] == 'p')
                {
                    //phc
                    //if (m_isPrior) output = Properties.Resources.turrets;
                    //else output = Properties.Resources.turret;
                    output = Properties.Resources.turretTiny;
                    if (tempLower.Equals("phc_seed"))
                        output = Properties.Resources.nexusTiny;
                    else if (tempLower.Equals("permanent_metalextractor"))
                        output = Properties.Resources.nexusTiny;
                    else if (tempLower.Equals("permanent_radioactivesextractor"))
                        output = Properties.Resources.nexusTiny;
                }
                else if (tempLower[0] == 's')
                {
                    //ss
                    //if (m_isPrior) output = Properties.Resources.turrets;
                    //else output = Properties.Resources.turret;
                    output = Properties.Resources.turretTiny;
                    if (tempLower.Equals("ss_seed"))
                        output = Properties.Resources.nexusTiny;
                }
                else if (tempLower[0] == 'm')
                {
                    //metal
                    output = Properties.Resources.metalTiny;
                }
                else if (tempLower[0] == 'r')
                {
                    //radioactives
                    output = Properties.Resources.radTiny;
                }
                else
                {
                    //misc
                    output = Properties.Resources.generatorTiny;
                }
                
            }
            else if (m_type.Equals("du"))
            {
                /*SpawnUnit sU = map.mainMenu.scen1.findSpawnUnit(m_name);
                if (sU != null)
                {
                    string sUTemp = sU.template.ToLower();
                    if (sUTemp[0] == 'p')
                    {
                        switch (sUTemp[4])
                        {
                            case '2':
                                output = Properties.Resources.t2d;
                                //phc t2
                                break;
                            case '3':
                                output = Properties.Resources.t3d;
                                //phc t3
                                break;
                            case 'a':
                                output = Properties.Resources.tad;
                                //phc ta
                                break;
                            default:
                                output = Properties.Resources.t1d;
                                //phc t1
                                break;
                        }
                    }
                    else if (sUTemp[0] == 's')
                    {
                        switch (sUTemp[3])
                        {
                            case '2':
                                output = Properties.Resources.t2d;
                                //phc t2
                                break;
                            case '3':
                                output = Properties.Resources.t3d;
                                //phc t3
                                break;
                            case 'a':
                                output = Properties.Resources.tad;
                                //phc ta
                                break;
                            default:
                                output = Properties.Resources.t1d;
                                //phc t1
                                break;
                        }
                    }
                    else
                    {
                        output = Properties.Resources.t1d;
                        //creeps
                    }
                }
                else
                {
                    output = Properties.Resources.t1d;
                    //error
                }*/
                output = Properties.Resources.destroyTiny;
            }
            else if (m_type.Equals("db"))
            {
                /*
                SpawnBuilding sB = map.mainMenu.scen1.findSpawnBuilding(m_name);
                string tempLower = sB.template.ToLower();
                if (tempLower[0] == 'p')
                {
                    output = Properties.Resources.turretd;
                    //phc
                }
                else if (tempLower[0] == 's')
                {
                    output = Properties.Resources.turretd;
                    //ss
                }
                else
                {
                    output = Properties.Resources.cross;
                    //misc
                }
                */
                output = Properties.Resources.destroyTiny;
            }
            else if (m_type.Equals("ai"))
            {
                output = Properties.Resources.ai;
                int h = (int)map.toImageSpaceH(m_radius, map.mapRender, map) * 2;
                int v = (int)map.toImageSpaceV(m_radius, map.mapRender, map) * 2;
                if (h > 0 && v > 0)
                    output = new Bitmap(output, new Size(h, v));
                else
                    output = new Bitmap(output, new Size(5, 5));

                if (m_template != null)
                {
                    if (m_template.Equals("blue"))
                        output = changeColor(output, Color.Blue);
                    else if (m_template.Equals("red"))
                        output = changeColor(output, Color.Red);
                    else if (m_template.Equals("yellow"))
                        output = changeColor(output, Color.Yellow);
                    else if (m_template.Equals("orange"))
                        output = changeColor(output, Color.Orange);
                    else if (m_template.Equals("purple"))
                        output = changeColor(output, Color.Purple);
                    else if (m_template.Equals("green"))
                        output = changeColor(output, Color.Green);
                    else
                        output = changeColor(output, Color.Cyan);
                }
                else
                    output = changeColor(output, Color.Cyan);

            }
            else if (m_type.Equals("c"))
            {

                if (! (m_camSave||m_camLoad)) output = rotateBmp(Properties.Resources.camRect2, m_rotation.z);
                int h = 140 + ((int)map.toImageSpaceH((int)(m_coordinates.z), map.mapRender, map) * 2);
                int v = 200 + ((int)map.toImageSpaceV((int)(m_coordinates.z), map.mapRender, map) * 2);
                if (h > 0 && v > 0)
                    output = new Bitmap(output, new Size(h, v));
                else
                    output = new Bitmap(output, new Size(5, 5));
            }
            else if (m_type.Equals("r"))
            {
                output = Properties.Resources.highlight1;
                int h = (int)map.toImageSpaceH(m_radius, map.mapRender, map)*2;
                int v = (int)map.toImageSpaceV(m_radius, map.mapRender, map)*2;
                if (h>0&&v>0 )
                    output = new Bitmap(output, new Size(h,v));
                else
                    output = new Bitmap(output, new Size(5, 5));
            }
            else if (m_type.Equals("area"))
            {
                output = Properties.Resources.highlight1;
                int h = (int)map.toImageSpaceH(m_radius, map.mapRender, map) * 2;
                int v = (int)map.toImageSpaceV(m_radius, map.mapRender, map) * 2;
                if (h > 0 && v > 0)
                    output = new Bitmap(output, new Size(h, v));
                else
                    output = new Bitmap(output, new Size(5, 5));
                output = changeColor(output, Color.Cyan);
            }
            else if (m_type.Equals("aam"))
            {
                if(m_name!=null&&m_name.Length>=3) output = changeColor(Properties.Resources.xTiny,Color.FromArgb(255,m_name[0],m_name[1],m_name[2]));
                else output = changeColor(Properties.Resources.xTiny, Color.Red);
            }
            else if (m_type.Equals("destruction"))
            {
                //output = Properties.Resources.destroyTiny;
            }
            //error
            switch (map.trig.scen.getPlayerColor(m_player))
            {
                case 0:
                    output = changeColor(output, Color.Gray);
                    break;
                case 1:
                    output = changeColor(output, Color.FromArgb(255, 188, 32, 46));//red
                    break;
                case 2:
                    output = changeColor(output, Color.FromArgb(255, 0, 94, 207));//blue
                    break;
                case 3:
                    output = changeColor(output, Color.FromArgb(255, 131, 120, 97));//tan
                    break;
                case 4:
                    output = changeColor(output, Color.FromArgb(255, 230, 94, 15));//orange
                    break;
                case 5:
                    output = changeColor(output, Color.FromArgb(255, 218, 150, 15));//yellow
                    break;
                case 6:
                    output = changeColor(output, Color.FromArgb(255, 78, 175, 216));//cyan
                    break;
                case 7:
                    output = changeColor(output, Color.FromArgb(255, 67, 146, 38));//green
                    break;
                case 8:
                    output = changeColor(output, Color.FromArgb(255, 99, 88, 124));//lavender
                    break;
                case 9:
                    output = changeColor(output, Color.FromArgb(255, 44, 114, 103));//turqoise
                    break;
                case 10:
                    output = changeColor(output, Color.FromArgb(255, 155, 161, 49));//lime
                    break;
                case 11:
                    output = changeColor(output, Color.FromArgb(255, 94, 49, 15));//brown
                    break;
                case 12:
                    output = changeColor(output, Color.FromArgb(255, 139, 35, 92));//pink
                    break;
                case 13:
                    output = changeColor(output, Color.FromArgb(255, 58, 54, 126));//purple
                    break;
                case 14:
                    output = changeColor(output, Color.FromArgb(255,188,183,159));//sandy
                    break;
                case 15:
                    output = changeColor(output, Color.FromArgb(255, 131, 89, 71));//light brown
                    break;
            }
            if (m_isPrior)
            {
                output = darkenFade(output);
            }
            return output;
        }
        /*
        public Bitmap getIcon()
        {
            Bitmap output = Properties.Resources.whiteSquare;
            if (m_type.Equals("su"))
            {
                string tempLower = m_template.ToLower();
                if (tempLower[0] == 'p')
                {
                    switch (tempLower[4])
                    {
                        case '2':

                            if (m_isPrior) output = Properties.Resources.t2;
                            else output = Properties.Resources.t2s;
                            //phc t2
                            break;
                        case '3':
                            if (m_isPrior) output = Properties.Resources.t3;
                            else output = Properties.Resources.t3s;
                            //phc t3
                            break;
                        case 'a':
                            if (m_isPrior) output = Properties.Resources.ta;
                            else output = Properties.Resources.tas;
                            //phc ta
                            break;
                        default:
                            if (m_isPrior) output = Properties.Resources.t1;
                            else output = Properties.Resources.t1s;
                            //phc t1
                            break;
                    }
                }
                else if (tempLower[0] == 's')
                {
                    switch (tempLower[3])
                    {
                        case '2':
                            if (m_isPrior) output = Properties.Resources.t2;
                            else output = Properties.Resources.t2s;
                            //phc t2
                            break;
                        case '3':
                            if (m_isPrior) output = Properties.Resources.t3;
                            else output = Properties.Resources.t3s;
                            //phc t3
                            break;
                        case 'a':
                            if (m_isPrior) output = Properties.Resources.ta;
                            else output = Properties.Resources.tas;
                            //phc ta
                            break;
                        default:
                            if (m_isPrior) output = Properties.Resources.t1;
                            else output = Properties.Resources.t1s;
                            //phc t1
                            break;
                    }
                }
                else
                {
                    output = Properties.Resources.t1;
                    //creeps
                }
            }
            if (m_type.Equals("ai"))
            {
                output = Properties.Resources.ai;
            }
            switch (m_player)
            {
                case 0:
                    output = changeColor(output, Color.Gray);
                    break;
                case 1:
                    output = changeColor(output, Color.Red);
                    break;
                case 2:
                    output = changeColor(output, Color.Blue);
                    break;
                case 3:
                    output = changeColor(output, Color.Tan);
                    break;
                case 4:
                    output = changeColor(output, Color.Orange);
                    break;
                case 5:
                    output = changeColor(output, Color.Yellow);
                    break;
                case 6:
                    output = changeColor(output, Color.Cyan);
                    break;
                case 7:
                    output = changeColor(output, Color.Green);
                    break;
            }
            return output;
        }
        */
        #endregion

        #region static methods
        public static Position gameToDisplay(Map mapScreen, Position coordinates)
        {
            if (mapScreen.Width == 0 || mapScreen.Height == 0 || mapScreen.mapWidth==0||mapScreen.maplength==0) return coordinates;
            Position output = new Position();
            if (coordinates.x < 0)
            {
                output.x = (mapScreen.mapWidth / 2) - coordinates.x;
            }
            else
            {
                output.x = coordinates.x + (mapScreen.mapWidth / 2);
            }
            if (coordinates.y < 0)
            {
                output.x = (mapScreen.maplength / 2) - coordinates.x;
            }
            else
            {
                output.y = coordinates.y + (mapScreen.maplength / 2);
            }
            int vsf = mapScreen.Height / mapScreen.maplength;
            int hsf = mapScreen.Width / mapScreen.mapWidth;
            output.x *= hsf;
            output.y *= vsf;
            return output;
        }
       
        public static MapElement processTrigger(Trigger t,Map m)
        {
            Trigger[] prior = t.getPriorTriggers();
            MapElement head=new MapElement(t,m);
            MapElement curElement=head;
            Type curActionType;

            if(t.actions!=null)foreach (Action curAction in t.actions)
            {
                curActionType = curAction.GetType();
                if (curActionType.Equals(typeof(Camera)))
                {
                    curElement.next = new MapElement((Camera)curAction, m, false);
                    curElement = curElement.next;
                }
                else if (curActionType.Equals(typeof(Reveal)))
                {
                    curElement.next = new MapElement((Reveal)curAction, m, false);
                    curElement = curElement.next;
                }
                else if (curActionType.Equals(typeof(AreaIndicator)))
                {
                    curElement.next = new MapElement((AreaIndicator)curAction, m, false);
                    curElement = curElement.next;
                }
                else if (curActionType.Equals(typeof(SpawnUnit)))
                {
                    curElement.next = new MapElement((SpawnUnit)curAction, m, false);
                    curElement = curElement.next;
                }
                else if (curActionType.Equals(typeof(SpawnBuilding)))
                {
                    curElement.next = new MapElement((SpawnBuilding)curAction, m, false);
                    curElement = curElement.next;
                }
                else if (curActionType.Equals(typeof(DestroyUnit)))
                {
                    curElement.next = new MapElement((DestroyUnit)curAction, m, false);
                    curElement = curElement.next;
                }
                else if (curActionType.Equals(typeof(DestroyBuilding)))
                {
                    curElement.next = new MapElement((DestroyBuilding)curAction, m, false);
                    curElement = curElement.next;
                }
                else if (curActionType.Equals(typeof(CaptureNearest)))
                {
                    curElement.next = new MapElement((CaptureNearest)curAction, m, false);
                    curElement = curElement.next;
                }
                else if (curActionType.Equals(typeof(AttackAttackMove)))
                {
                    curElement.next = new MapElement(curAction.toAttackAttackMove(), m, false);
                    curElement = curElement.next;
                }
            }
            if(prior!=null)foreach(Trigger curTrigger in prior)
            {
                if (curTrigger.actions != null) foreach (Action curAction in curTrigger.actions)
                {
                    curActionType = curAction.GetType();
                    if (curActionType.Equals(typeof(AreaIndicator))&&((AreaIndicator)curAction).isStillActive(t))
                    {
                       curElement.next = new MapElement((AreaIndicator)curAction, m, true);
                       curElement = curElement.next;                                       
                    }
                    else if (curActionType.Equals(typeof(SpawnUnit))&& Action.isStillAlive(curAction.getStringA(), t, false))
                    {
                       curElement.next = new MapElement((SpawnUnit)curAction, m, true);
                       curElement = curElement.next;                                             
                    }
                    else if (curActionType.Equals(typeof(SpawnBuilding))&& Action.isStillAlive(curAction.getStringA(), t, true))
                    {                       
                       curElement.next = new MapElement((SpawnBuilding)curAction, m, true);
                       curElement = curElement.next;                                              
                    }
                    else if (curActionType.Equals(typeof(CaptureNearest))&& curAction.getBoolA())
                    {
                       curElement.next = new MapElement((CaptureNearest)curAction, m, true);
                       curElement = curElement.next;                                              
                    }
                    
                }
            }
            return head;
        }
       
        #region drawing
        public static Bitmap changeColor(Bitmap bmp, Color newColor)
        {
            Bitmap output = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), bmp.PixelFormat);
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    output.SetPixel(j, i, Color.FromArgb(bmp.GetPixel(j, i).A, newColor));
                }
            }
            return output;
        }

        public static Bitmap darkenFade(Bitmap bmp)
        {
            Bitmap output = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height), bmp.PixelFormat);
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    output.SetPixel(j, i, Color.FromArgb((int)(bmp.GetPixel(j,i).A*(0.75)),(int)( bmp.GetPixel(j, i).R *0.9), (int)(bmp.GetPixel(j, i).G*0.9), (int)(bmp.GetPixel(j, i).B*0.9)));
                }
            }
            return output;
        }

        public static Bitmap rotateBmp(Bitmap bmp, float angle)
        {
            Bitmap output = new Bitmap(bmp.Width, bmp.Height);
            Graphics gfx = Graphics.FromImage(output);
            gfx.TranslateTransform((float)output.Width / 2, (float)output.Height / 2);
            gfx.RotateTransform(angle);
            gfx.TranslateTransform(-(float)output.Width / 2, -(float)output.Height / 2);
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.DrawImage(bmp, new Point(0, 0));
            gfx.Dispose();
            return output;
        }

        
        #endregion
        #endregion



    }

}
