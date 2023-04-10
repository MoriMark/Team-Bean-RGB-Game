using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGB.modell.enums;
using RGB.modell.game_logic;
using RGB.modell.gameobjects;
using RGB.modell.structs;

namespace RGB.modell
{
    public class GameHandler
    {
        public List<RobotAction> actionsThisTurn;
        public int move { get; set; }
        public int round { get; set; }
        private int numOfPlayers;
        private int numOfTeams;

        public event EventHandler robotChanged = null!;

        public GameRule gameRule { get; private set; }

        public GameHandler(Int32 numOfPlayers, Int32 numOfTeams)
        {
            move = 1;
            round = 1;
            this.numOfPlayers = numOfPlayers;
            this.numOfTeams = numOfTeams;
            gameRule = new GameRule(numOfPlayers, numOfTeams);
            actionsThisTurn = new List<RobotAction>();
        }

        public void StartGame()
        {
            gameRule.StartGame();
        }

        public Robot GetCurrentPlayer()
        {
            return gameRule.CurrentRobot();
        }

        public GameObject GetFieldValue(Int32 x, Int32 y)
        {
            return gameRule.GetFieldValue(x, y);
        }
        public void addAction(Robot robot, List<Coordinate> coords, Actions action)
        {
            actionsThisTurn.Add(new RobotAction(robot, coords, action));
            gameRule.NextRobot();
            move++;
            if (move > (numOfTeams*numOfPlayers))
            {
                move = 1;
                round++;
                resolveActions();
            }
            robotChanged(this, EventArgs.Empty);
        }
        private void resolveActions()
        {
            foreach(RobotAction action in actionsThisTurn) 
            {
                Coordinate destination;
                Direction direction;
                switch(action.action) 
                {
                    case Actions.MoveUp:
                        destination = new Coordinate(action.robot.i - 1, action.robot.j);
                        gameRule.MakeStep(destination.X,destination.Y, action.robot);
                        break;

                    case Actions.MoveDown:
                        destination = new Coordinate(action.robot.i + 1, action.robot.j);
                        gameRule.MakeStep(destination.X, destination.Y, action.robot);
                        break;

                    case Actions.MoveLeft:
                        destination = new Coordinate(action.robot.i, action.robot.j - 1);
                        gameRule.MakeStep(destination.X, destination.Y, action.robot);
                        break;

                    case Actions.MoveRight:
                        destination = new Coordinate(action.robot.i, action.robot.j + 1);
                        gameRule.MakeStep(destination.X, destination.Y, action.robot);
                        break;

                    case Actions.RotateLeft:
                        switch (action.robot.facing)
                        {
                            case Direction.Up:
                                gameRule.MakeTurn(Direction.Left);
                                break;
                            case Direction.Down:
                                gameRule.MakeTurn(Direction.Right);
                                break;
                            case Direction.Left:
                                gameRule.MakeTurn(Direction.Down);
                                break;
                            case Direction.Right:
                                gameRule.MakeTurn(Direction.Up);
                                break;
                        }
                        break;

                    case Actions.RotateRight:
                        switch (action.robot.facing)
                        {
                            case Direction.Up:
                                gameRule.MakeTurn(Direction.Right);
                                break;
                            case Direction.Down:
                                gameRule.MakeTurn(Direction.Left);
                                break;
                            case Direction.Left:
                                gameRule.MakeTurn(Direction.Up);
                                break;
                            case Direction.Right:
                                gameRule.MakeTurn(Direction.Down);
                                break;
                        }
                    break;
                }
            }
            actionsThisTurn.Clear();
        }
    }
}
