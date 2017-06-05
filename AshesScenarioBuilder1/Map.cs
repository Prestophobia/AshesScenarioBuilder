using Stardock.NitrousTool.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AshesScenarioBuilder1
{
    public partial class Map : Form
    {
        public int mapWidth;
        public int maplength;
        public Form1 mainMenu;
        //public PictureBox display;
        public PictureBox surface;
        public Trigger trig;
        MapElement root;
        public Bitmap mapRender;

        public Map()
        {
            InitializeComponent();
        }
        public Map(Form1 parent)
        {
            mainMenu = parent;
            InitializeComponent();
            //MapDisplay.Controls.Add(transTest);
            //display = MapDisplay;
            surface = this.MapDisplay;
            mapRender = Properties.Resources.empty;
        }

        public void update()
        {
            string mapPath = mainMenu.USM.assetPath + "/Maps/" + mainMenu.scen1.map;
            displayDDS((mapPath + "/MapPreview.dds"), MapDisplay);
            //transTest.BackColor = Color.Transparent;
            CSVDataSheet mapDataCSV = new CSVDataSheet(mapPath + "/GamePlayMap.csv");
            string e = mapDataCSV.getTranslatedString("MapSize");
            if (!e.Equals(""))
            {
                string[] dims = e.Split(',');
                mapWidth = (int)float.Parse(dims[0]);
                maplength = (int)float.Parse(dims[01]);
            }
           
            if (root != null)
            {
                root.disposeChain();
            }
            if(trig!=null) root = MapElement.processTrigger(trig,this);
            mapRender = renderMap();
            MapDisplay.Image = mapRender;
            //if (trig != null)  root.showPictureChain();
            string b = "beep";
        }
        private void displayDDS(string path, PictureBox pb)
        {
            if (File.Exists(path))
            {
                DdsImage dds = new DdsImage(path);
                pb.Image = (System.Drawing.Image)dds.BitmapImage.Clone();
               // pb.Image = MapElement.rotateBmp((Bitmap)dds.BitmapImage.Clone(), 45);
                //pb.Image = MapElement.changeColor((Bitmap)dds.BitmapImage.Clone(), Color.Green);
            }
            else
            {
                string pathAttempt2 = mainMenu.USM.assetPath + "/" + path;
                if (File.Exists(pathAttempt2))
                {
                    DdsImage dds = new DdsImage(pathAttempt2);
                    pb.Image = (System.Drawing.Image)dds.BitmapImage.Clone();
                    //pb.Image = MapElement.changeColor((Bitmap)dds.BitmapImage.Clone(), Color.Green);
                }
            }
        }

        public Func<Map, float>    getScreenSpaceHSF         = (m)    => m.surface.Width / m.mapWidth;
        public Func<Map, float>    getScreenSpaceVSF         = (m)    => m.surface.Height / m.maplength;
        public Func<int, Map, float> toScreenSpaceHorizontal   = (w, m) => (float)w * ((float)m.surface.Width / m.mapWidth);
        public Func<int, Map, float> toScreenSpaceVertical     = (w, m) => (float)w * ((float)m.surface.Height / m.maplength);
        public Func<int, Map, float> undoCartesianHorizontalWS = (w, m) => (float)(w*-1) + ((float)m.maplength / 2);
        public Func<int, Map, float> undoCartesianVerticalWS   = (w, m) => (float)(w) + ((float)m.maplength / 2);

        public Func<int, Bitmap, Map, float> toImageSpaceH = (w, bmp, m) => (float)w * ((float)bmp.Width / m.mapWidth);
        public Func<int, Bitmap, Map, float> toImageSpaceV = (w, bmp, m) => (float)w * ((float)bmp.Height / m.maplength);

        public Point toScreenSpace(Point w)
        {
            return new Point((int)toScreenSpaceHorizontal(w.X, this), (int)toScreenSpaceVertical(w.Y, this));
        }

        public Point worldSpaceUndoCartesian(Point w)
        {
            return new Point((int)undoCartesianHorizontalWS(w.X, this), (int)undoCartesianVerticalWS(w.Y, this));
        }
        public Point worldSpaceCartesianToScreenSpace(Point w)
        {
            Point temp = worldSpaceUndoCartesian(w);
            return toScreenSpace(worldSpaceUndoCartesian(w));
        }
        public Point worldSpaceCartesianToImageSpace(Point w, Bitmap bmp)
        {
            Point noCart = worldSpaceUndoCartesian(w);
            return new Point((int)toImageSpaceH(noCart.X, bmp, this), (int)toImageSpaceV(noCart.Y, bmp, this));
        }
        public Bitmap renderMap()
        {
            Bitmap output;
            string mapPath = mainMenu.USM.assetPath + "/Maps/" + mainMenu.scen1.map + "/MapPreview.dds";
            if (File.Exists(mapPath))
            {
                DdsImage dds = new DdsImage(mapPath);
                output = (Bitmap)dds.BitmapImage.Clone();
            }
            else
            {
                string pathAttempt2 = mainMenu.USM.assetPath + "/" + mapPath;
                if (File.Exists(pathAttempt2))
                {
                    DdsImage dds = new DdsImage(pathAttempt2);
                    output = (Bitmap)dds.BitmapImage.Clone();
                }
                else output = new Bitmap(MapDisplay.Width, MapDisplay.Height);
            }
            Graphics gfx = Graphics.FromImage(output); 
            if (root != null)
            {
                Bitmap icon=root.getIcon();
                Point location = new Point(0, 0);
                gfx.InterpolationMode = InterpolationMode.NearestNeighbor;
                MapElement curE = root;                         
                if (root.m_coordinates != null)
                    location = worldSpaceCartesianToImageSpace((Point)root.m_coordinates, output);
                else location = new Point(output.Width / 2, output.Height / 2);
                gfx.DrawImage(icon, location);                  
                
                while (curE.next != null)
                {
                    curE = curE.next;

                    icon = curE.getIcon();
                    location = new Point(0, 0);                   
                    if (curE.m_coordinates != null)
                    {
                        Point temp= worldSpaceCartesianToImageSpace((Point)curE.m_coordinates, output);
                        temp = new Point(temp.X - (icon.Width / 2), temp.Y - (icon.Height / 2));
                        location = temp;
                    }
                    else location = new Point(output.Width/2,output.Height/2);
                    
                    gfx.DrawImage(icon, location);
                }
            }            
            gfx.Dispose();
            return output;
        }
    }
}
