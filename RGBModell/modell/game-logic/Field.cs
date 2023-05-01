using RGBModell.modell.enums;
using RGBModell.modell.gameobjects;
using RGBModell.modell.structs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.game_logic
{
    public class Field 
    {
        private const Int32 BORDER = 5;
        private GameObject[,] field;
        public Int32 TableSize { get; private set; }
        public Int32 MatrixSize { get; private set; }
        private List<Exit> exits;
        private const Int32 NUMBER_OF_EXITS = 5;

        public Field(Int32 tableSize)
        {
            if (tableSize <= 0)
                throw new ArgumentException("table size can not be zero or negative!");

            TableSize = tableSize;
            MatrixSize = tableSize + 2 * BORDER;

            field = new GameObject[MatrixSize, MatrixSize];
        }

        /// <summary>
        /// Check if a given object is on the table meaning that it is between predefined borders.
        /// </summary>
        /// <param name="gameObject">The object which position will be checked.</param>
        /// <returns>Returns true if the given object is between the borders.</returns>
        /// <exception cref="ArgumentNullException">If the given object is null.</exception>
        public Boolean BetweenBorders(GameObject gameObject)
        {
            if (gameObject is null)
                throw new ArgumentNullException("given paramter is null!");

            return BetweenBorders(gameObject.i,gameObject.j);
        }

        /// <summary>
        /// Check if a given i and j coordinates mapping to the predefined borders.
        /// </summary>
        /// <param name="i">The first dimension index.</param>
        /// <param name="j">The second dimension index.</param>
        /// <returns>Returns true if the given coordinates are included between the borders.</returns>
        public Boolean BetweenBorders(Int32 i, Int32 j)
        {
            if (i < BORDER)
                return false;

            if (i >= MatrixSize - BORDER)
                return false;

            if (j < BORDER)
                return false;

            if (j >= MatrixSize - BORDER)
                return false;

            return true;
        }

        public GameObject GetValue(Int32 i, Int32 j)
        {
            if (i < 0 || j < 0) throw new IndexOutOfRangeException($"(i:{i}|j:{j})<0");
            if (i >= MatrixSize || j >= MatrixSize) throw new IndexOutOfRangeException($"(i:{i}|j:{j})>=TableSize{MatrixSize}");
        
            return field[i, j];
        }

        public void SetValue(Int32 i, Int32 j, GameObject value)
        {
            if (i < 0 || j < 0) throw new IndexOutOfRangeException($"(i:{i}|j:{j})<0");
            if (i >= MatrixSize || j >= MatrixSize) throw new IndexOutOfRangeException($"(i:{i}|j:{j})>=TableSize{MatrixSize}");

            field[i, j] = value;
            field[i, j].i = i;
            field[i, j].j = j;
        }

        public List<Exit> GetExits { get { return exits; } }

        public GameObject[,] CalculateVisionOfRobot(Robot robot)
        {
            const Int32 viewDistance = 4;
            
            GameObject[,] calculateMatrix = new GameObject[viewDistance * 2 + 1, viewDistance * 2 + 1];

            Int32 i = 0, j = 0;
            for (Int32 ii = robot.i - viewDistance; ii <= robot.i + viewDistance; ++ii)
            {
                j = 0;
                for (Int32 jj = robot.j - viewDistance; jj <= robot.j + viewDistance; ++jj)
                {
                    if (Math.Abs(ii - robot.i) + Math.Abs(jj - robot.j) <= viewDistance && BetweenBorders(ii, jj))
                        calculateMatrix[i, j] = field[ii, jj];
                    else
                        calculateMatrix[i, j] = new Wall(ii, jj);
                    j++;
                }
                i++;
            }

            /*
            GameObject[,] calculateMatrix = new GameObject[MatrixSize, MatrixSize];
            for (Int32 i = 0; i < MatrixSize; ++i)
            {
                for (Int32 j = 0; j < MatrixSize; ++j)
                {
                    //Manhattan
                    
                    //if (Math.Abs(i - robot.i) + Math.Abs(j - robot.j) <= viewDistance && BetweenBorders(i,j))
                    //    calculateMatrix[i, j] = field[i, j];
                    //else
                    //    calculateMatrix[i, j] = new Wall(i, j);
                    
                    if (BetweenBorders(i, j))
                        calculateMatrix[i, j] = field[i, j];
                    else
                        calculateMatrix[i, j] = new Wall(i, j);
                    
                }
            }
            */

            return calculateMatrix;
        }

        /// <summary>
        /// Generate exits on the edges of the board except for the corners
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the given number of exits is less than four or greater than TableSize * 4 - 4 - 4, which represents 
        /// the maximum number of exits allowed on the board, excluding the corners and duplicated corners calculated by square discrict.
        /// </exception>
        public void GenerateExits()
        {
            //TableSize * edges - duplicateCorners - corners
            if (NUMBER_OF_EXITS < 4 || NUMBER_OF_EXITS > (MatrixSize * 4 - 4 - 4)) throw new ArgumentException($"NumberOfExists must be greater than 4 and less than or equal {MatrixSize * 4 - 4 - 4}");

            Int32 bBorderMin = BORDER;
            Int32 bBorderMax = MatrixSize - BORDER - 1;

            Random rnd = new Random();

            //Make sure to have one 1-1 exits in every edges
            {
                Int32[] rndInd = new int[4];
                for (int i = 0; i < rndInd.Length; ++i)
                    rndInd[i] = rnd.Next(bBorderMin + 1, bBorderMax);

                exits = new List<Exit>(){
                    new Exit(new Coordinate(bBorderMin, rndInd[0]),Direction.Up),
                    new Exit(new Coordinate(rndInd[1], bBorderMax), Direction.Right),
                    new Exit(new Coordinate(bBorderMax, rndInd[2]), Direction.Down),
                    new Exit(new Coordinate(rndInd[3], bBorderMin), Direction.Left)
                };
            }
            int existLeft = NUMBER_OF_EXITS - 4;
            
            while (existLeft > 0)
            {
                Int32 x = 0,y = 0;
                Direction dir = Direction.Down;
                int rndInd = rnd.Next(bBorderMin + 1, bBorderMax);

                switch (rnd.Next(0, 4))
                {
                    case 0:
                        x = bBorderMin;
                        y = rndInd;
                        dir = Direction.Up;
                        break;
                    case 1:
                        x = rndInd;
                        y = bBorderMax;
                        dir = Direction.Right;
                        break;
                    case 2:
                        x = bBorderMax;
                        y = rndInd;
                        dir = Direction.Down;
                        break;
                    case 3:
                        x = rndInd;
                        y = bBorderMin;
                        dir = Direction.Left;
                        break;
                }

                Boolean contains = exits.Exists(e =>
                    e.Coordinate.X == x &&
                    e.Coordinate.Y == y &&
                    //not necessary
                    e.Direction == dir);

                if (!contains)
                {
                    exits.Add(new Exit(new Coordinate(x,y), dir));
                    existLeft--;
                }
            }
        }

        //Field generation for the start of the game
        public List<Robot> GenerateField(Int32 numOfRobots, Int32 numOfTeams)
        {
            List<Robot> robots = new List<Robot>();
            //Initializing the table with empty fields
            for (int i = 0; i < MatrixSize; i++)
                for (int j = 0; j < MatrixSize; j++)
                    field[i, j] = new Empty(i, j);

            Random RNG = new Random();
            int x; int y;
            #region Setting boxes
            Int32 numOfBoxes = TableSize;
            BoxColor[] boxColors = { BoxColor.Red, BoxColor.Green, BoxColor.Yellow, BoxColor.Blue };

            while (numOfBoxes > 0)
            {
                x = RNG.Next(MatrixSize); y = RNG.Next(MatrixSize);
                if (!BetweenBorders(x, y))
                    continue;

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
            #endregion
            #region Setting robots
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
                x = RNG.Next(MatrixSize); y = RNG.Next(MatrixSize);
                if (!BetweenBorders(x, y))
                    continue;

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
                                robotAdd = new Robot(x, y, Direction.Up, teamCol, TileType.RedRobot, playersNeeded[current].ToString());
                                SetValue(x, y, robotAdd);
                                robots.Add(robotAdd);
                                playersNeeded[current]--;
                                numOfPlayers--;
                                break;

                            case Team.Green:
                                robotAdd = new Robot(x, y, Direction.Up, teamCol, TileType.GreenRobot, playersNeeded[current].ToString());
                                SetValue(x, y, robotAdd);
                                robots.Add(robotAdd);
                                playersNeeded[current]--;
                                numOfPlayers--;
                                break;

                            case Team.Yellow:
                                robotAdd = new Robot(x, y, Direction.Up, teamCol, TileType.YellowRobot, playersNeeded[current].ToString());
                                SetValue(x, y, robotAdd);
                                robots.Add(robotAdd);
                                playersNeeded[current]--;
                                numOfPlayers--;
                                break;

                            case Team.Blue:
                                robotAdd = new Robot(x, y, Direction.Up, teamCol, TileType.BlueRobot, playersNeeded[current].ToString());
                                SetValue(x, y, robotAdd);
                                robots.Add(robotAdd);
                                playersNeeded[current]--;
                                numOfPlayers--;
                                break;
                        }
                    }
                }
            }
            #endregion
            GenerateExits();

            return robots;
        }
    }
}