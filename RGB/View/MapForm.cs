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
            SetMapLabel();
        }

        private int SizeOfMap(List<Mapfield> map)
        {
            maxX = map[0].coords.X;
            maxY = map[0].coords.Y;
            minX = map[0].coords.X;
            minY = map[0].coords.Y;

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
                if (map[i].coords.Y < minY)
                {
                    minY = map[i].coords.Y;
                }
            }
            sizeX = maxX - minX;
            sizeY = maxY - minY;

            if (sizeX > sizeY)
            {
                return sizeX + 1;
            }
            else
            {
                return sizeY + 1;
            }
        }

        private void SetUpMapView()
        {
            int viewDist = 4;
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
                pb.Dock = DockStyle.Fill;
                switch (field.type)
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
                        pb.BackColor = Color.Violet;
                        break;
                    case TileType.Obstacle:
                        pb.BackColor = Color.DarkGray;
                        break;
                }
                bool darken = true;
                //check if none of the synced robots see a tile
                foreach (Robot r in robot.seenRobots)
                {
                    if (Math.Abs(field.coords.X - r.i) + Math.Abs(field.coords.Y - r.j) <= viewDist)
                        darken = false;
                }
                //check if the current robot sees the tile
                if (Math.Abs(field.coords.X - robot.i) + Math.Abs(field.coords.Y - robot.j) <= viewDist)
                    darken = false;

                if (darken)
                {
                    int red = (int)Math.Floor(pb.BackColor.R * 0.8);
                    int green = (int)Math.Floor(pb.BackColor.G * 0.8);
                    int blue = (int)Math.Floor(pb.BackColor.B * 0.8);
                    Color darker = Color.FromArgb(255, red, green, blue);
                    pb.BackColor = darker;
                }

                Coordinate shift;
                if (sizeX > sizeY)
                {
                    shift = new Coordinate(0, (sizeX - sizeY) / 2);
                    mapTable.Controls.Add(pb, field.coords.Y - minY + shift.Y, field.coords.X - minX + shift.X);
                }
                else    //if sizeY >= sizeX
                {
                    shift = new Coordinate((sizeY - sizeX) / 2, 0);
                    mapTable.Controls.Add(pb, field.coords.Y - minY + shift.Y, field.coords.X - minX + shift.X);
                }
            }
        }

        private void SetMapLabel()
        {
            mapLabel.Text = $"{robot.name}, {robot.team}'s map\nSynchronized with: ";
            foreach (Robot r in robot.seenRobots)
            {
                mapLabel.Text += $"{r.name}, {r.team}   ";
            }
        }
    }
}
