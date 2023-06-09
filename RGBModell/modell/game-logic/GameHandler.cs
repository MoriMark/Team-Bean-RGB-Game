﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGBModell.modell.enums;
using RGBModell.modell.game_logic;
using RGBModell.modell.gameobjects;
using RGBModell.modell.structs;

namespace RGBModell.modell
{
    public class GameHandler
    {
        public List<RobotAction> actionsThisTurn;
        public int move { get; set; }
        public int round { get; set; }
        private int numOfPlayers;
        private int numOfTeams;
        private int eventround;

        public event EventHandler robotChanged = null!;

        public GameRule gameRule { get; private set; }

        public MessageHandler messageHandler { get; private set; }

        public GameHandler(Int32 numOfPlayers, Int32 numOfTeams)
        {
            move = 1;
            round = 1;
            eventround= 1;
            this.numOfPlayers = numOfPlayers;
            this.numOfTeams = numOfTeams;
            gameRule = new GameRule(numOfPlayers, numOfTeams);
            actionsThisTurn = new List<RobotAction>();
            messageHandler = new MessageHandler();
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

        public Int32 GetTeamPoints(Team team)
        {
            return gameRule.GetTeamPoints(team);
        }

        public void addAction(Robot robot, List<Coordinate> coords, Actions action)
        {
            Random rnd = new Random();
            actionsThisTurn.Add(new RobotAction(robot, coords, action));
            move++;
            if (move > (numOfTeams * numOfPlayers))
            {
                move = 1;
                round++;
                eventround++;
                resolveActions();
                gameRule.WeldCheck();
                int eventcheck = rnd.Next(10,26);
                if(eventcheck - eventround < 0)
                {
                    eventround = 0;
                    gameRule.SpecialEvent();
                }
            }
            gameRule.numberOfCurrentRound = round;
            gameRule.UpdateMapsOfRobots();
            gameRule.NextRobot();
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
                                gameRule.MakeTurn(Direction.Left, action.robot);
                                break;
                            case Direction.Down:
                                gameRule.MakeTurn(Direction.Right, action.robot);
                                break;
                            case Direction.Left:
                                gameRule.MakeTurn(Direction.Down, action.robot);
                                break;
                            case Direction.Right:
                                gameRule.MakeTurn(Direction.Up, action.robot);
                                break;
                        }
                        break;

                    case Actions.RotateRight:
                        switch (action.robot.facing)
                        {
                            case Direction.Up:
                                gameRule.MakeTurn(Direction.Right, action.robot);
                                break;
                            case Direction.Down:
                                gameRule.MakeTurn(Direction.Left, action.robot);
                                break;
                            case Direction.Left:
                                gameRule.MakeTurn(Direction.Up, action.robot);
                                break;
                            case Direction.Right:
                                gameRule.MakeTurn(Direction.Down, action.robot);
                                break;
                        }
                    break;

                    case Actions.Clean:
                        gameRule.Clean(action.robot);
                        break;

                    case Actions.Wait:
                        gameRule.Pass(action.robot);
                        break;

                    case Actions.Unweld:
                        if (action.coordinates.Count == 2)
                        {
                            gameRule.UnWeld(action.coordinates[0].X, action.coordinates[0].Y,
                                action.coordinates[1].X, action.coordinates[1].Y, action.robot);
                        }
                        break;

                    case Actions.Weld:
                        gameRule.Weld(action.robot);
                        break;

                    case Actions.Connect:
                        gameRule.Lift(action.robot);
                        break;

                    case Actions.Disconnect:
                        gameRule.UnLift(action.robot);
                        break;
                }
            }
            actionsThisTurn.Clear();
        }
    }
}
