using RGBModell.modell.gameobjects;
using RGBModell.modell.structs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RGBModell.modell.enums;

namespace RGB.View
{
    public partial class MapForm : Form
    {
        private List<Mapfield> map;
        private Robot robot;
        private int size;
        private int sizeX;
        private int sizeY;
        private int maxX;
        private int maxY;
        private int minX;
        private int minY;
        public MapForm(Robot robot)
        {
            InitializeComponent();
            this.robot = robot;
            this.map = robot.map;
            size = SizeOfMap(this.map);
            SetUpMapView();
        }

        private int SizeOfMap(List<Mapfield> map)
        {
            maxX = 0;
            maxY = 0;
            minX = 0;
            minY = 0;

            for (int i = 0; i < map.Count; i++)
            {
                if (map[i].coords.X > maxX)
                {
                    maxX = map[i].coords.X;
                }
                if (map[i].coords.X < minX)
                {
                    minX = map[i].coords.X;
                }
                if (map[i].coords.Y > maxY)
                {
                    maxY = map[i].coords.Y;
                }
                if (map[i].coords.X < minY)
                {
                    minY = map[i].coords.Y;
                }
            }
            sizeX = maxX - minX;
            sizeY = maxY - minY;

            if (sizeX > sizeY)
            {
                return sizeX;
            }
            else
            {
                return sizeY;
            }
        }

        private void SetUpMapView()
        {
            mapTable.RowCount = size;
            mapTable.ColumnCount = size;
            mapTable.Margin = new Padding(0);
            mapTable.Padding = new Padding(0);
            mapTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            mapTable.RowStyles.Clear();
            mapTable.ColumnStyles.Clear();

            for (int i = 0; i < size; i++)
            {
                mapTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / size));
            }
            for (int i = 0; i < size; i++)
            {
                mapTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / size));
            }

            foreach (Mapfield field in map)
            {
                PictureBox pb = new PictureBox();
                pb.Margin = new Padding(0);
                pb.Padding = new Padding(0);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                switch(field.type)
                {
                    case TileType.RedBox:
                        pb.BackColor = Color.Red;
                        break;
                    case TileType.YellowBox:
                        pb.BackColor = Color.Yellow;
                        break;
                    case TileType.BlueBox:
                        pb.BackColor = Color.Blue;
                        break;
                    case TileType.GreenBox:
                        pb.BackColor = Color.Green;
                        break;
                    case TileType.RedRobot:
                        pb.Image = Properties.Resources.red_down;
                        break;
                    case TileType.YellowRobot:
                        pb.Image = Properties.Resources.yellow_down;
                        break;
                    case TileType.GreenRobot:
                        pb.Image = Properties.Resources.green_down;
                        break;
                    case TileType.BlueRobot:
                        pb.Image = Properties.Resources.blue_down;
                        break;
                    case TileType.Wall:
                        pb.BackColor = Color.Black;
                        break;
                    case TileType.Obstacle:
                        pb.BackColor = Color.DarkGray;
                        break;
                    case TileType.Empty:
                        pb.BackColor = Color.White;
                        break;
                }
                mapTable.Controls.Add(pb,field.coords.Y-minY,field.coords.X-minX);
            }
        }
    }
}
