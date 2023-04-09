using RGB.modell.enums;
using RGB.modell.gameobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell.game_logic
{
    public class Field 
    {
        private const Int32 border = 5;
        private GameObject[,] field;
        public Int32 TableSize { get; private set; }

        public Field(Int32 tableSize)
        {
            if (tableSize <= 0)
                throw new ArgumentException("table size can not be zero or negative!");

            TableSize = tableSize;

            field = new GameObject[TableSize + 2*border, TableSize + 2*border];
        }

        public GameObject[,] GetField()
        {
            return field;
        }

        public GameObject GetValue(Int32 i, Int32 j)
        {
            if (i < 0 || j < 0) throw new IndexOutOfRangeException($"(i:{i}|j:{j})<0");
            if (i >= TableSize || j >= TableSize) throw new IndexOutOfRangeException($"(i:{i}|j:{j})>=TableSize{TableSize}");
        
            return field[i + border, j + border];
        }

        public void SetValue(Int32 i, Int32 j, GameObject value)
        {
            if (i < 0 || j < 0) throw new IndexOutOfRangeException($"(i:{i}|j:{j})<0");
            if (i >= TableSize || j >= TableSize) throw new IndexOutOfRangeException($"(i:{i}|j:{j})>=TableSize{TableSize}");

            field[i + border, j + border] = value;
        }

        public Field CalculateVisionOfRobot(Robot robot)
        {
            Field calculatedField = (Field)field.Clone();

            //TODO calcualate

            return calculatedField;
        }
        //Field generation for the start of the game
        public List<Robot> GenerateField(Int32 numOfRobots, Int32 numOfTeams)
        {
            List<Robot> robots = new List<Robot>();
            //Initializing the table with empty fields
            for (int i = 0; i < TableSize + (border * 2); i++)
            {
                for (int j = 0; j < TableSize + (border * 2); j++)
                {
                    field[i, j] = new Empty(i, j);
                }
            }
            //Setting borders
            for (int i = 0; i < TableSize+(border*2); i++)
            {
                for (int j = 0; j < TableSize + (border * 2); j++)
                {
                    if (i < 5 || j < 5)
                    {
                        field[i, j] = new Wall(i, j);
                    }
                    else if (i > TableSize + border || j > TableSize + border)
                    {
                        field[i, j] = new Wall(i, j);
                    }
                }
            }
            //Setting boxes
            Int32 numOfBoxes = TableSize;
            Random RNG = new Random();
            BoxColor[] boxColors = { BoxColor.Red, BoxColor.Green, BoxColor.Yellow, BoxColor.Blue };
            int x; int y;

            while (numOfBoxes > 0)
            {
                x = RNG.Next(TableSize); y = RNG.Next(TableSize);   
                if (RNG.Next(100) > 90 && GetValue(x, y).IsEmpty())
                {
                    BoxColor boxCol = boxColors[RNG.Next(0,3)];
                    switch (boxCol)
                    { 
                        case BoxColor.Red:
                            SetValue(x, y, new Box(x, y, boxCol, TileType.RedBox));
                            numOfBoxes--;
                            break;

                        case BoxColor.Green:
                            SetValue(x, y, new Box(x, y, boxCol, TileType.GreenBox));
                            numOfBoxes--;
                            break;

                        case BoxColor.Yellow:
                            SetValue(x, y, new Box(x, y, boxCol, TileType.YellowBox));
                            numOfBoxes--;
                            break;

                        case BoxColor.Blue:
                            SetValue(x, y, new Box(x, y, boxCol, TileType.BlueBox));
                            numOfBoxes--;
                            break;
                    }
                }
            }
            //Setting robots
            Int32 numOfPlayers = numOfRobots * numOfTeams;
            Team[] teamColors = { Team.Red, Team.Blue, Team.Green, Team.Yellow };
            //Each team to have equal amount of robots
            int[] playersNeeded = new int[numOfTeams];
            for (int i = 0; i < numOfTeams; i++)
            {
                playersNeeded[i] = numOfRobots;
            }
            //Place robots until each of the are placed
            while (numOfPlayers > 0)
            {
                x = RNG.Next(TableSize); y = RNG.Next(TableSize);
                if (RNG.Next(100) > 96 && GetValue(x, y).IsEmpty())
                {
                    int current = RNG.Next(0, numOfTeams);
                    Team teamCol = teamColors[current];
                    if (playersNeeded[current] > 0)
                    {
                        Robot robotAdd;
                        switch (teamCol)
                        {
                            case Team.Red:
                                robotAdd = new Robot(x, y, Direction.Up, teamCol, TileType.RedRobot);
                                SetValue(x, y, robotAdd);
                                robots.Add(robotAdd);
                                playersNeeded[current]--;
                                numOfPlayers--;
                                break;

                            case Team.Green:
                                robotAdd = new Robot(x, y, Direction.Up, teamCol, TileType.GreenRobot);
                                SetValue(x, y, robotAdd);
                                robots.Add(robotAdd);
                                playersNeeded[current]--;
                                numOfPlayers--;
                                break;

                            case Team.Yellow:
                                robotAdd = new Robot(x, y, Direction.Up, teamCol, TileType.YellowRobot);
                                SetValue(x, y, robotAdd);
                                robots.Add(robotAdd);
                                playersNeeded[current]--;
                                numOfPlayers--;
                                break;

                            case Team.Blue:
                                robotAdd = new Robot(x, y, Direction.Up, teamCol, TileType.BlueRobot);
                                SetValue(x, y, robotAdd);
                                robots.Add(robotAdd);
                                playersNeeded[current]--;
                                numOfPlayers--;
                                break;
                        }
                    }
                }
            }
            return robots;
        }
    }
}