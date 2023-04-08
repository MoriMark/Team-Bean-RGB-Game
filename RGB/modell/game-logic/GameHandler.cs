﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGB.modell.enums;
using RGB.modell.game_logic;
using RGB.modell.gameobjects;

namespace RGB.modell
{
    public class GameHandler
    {
        public GameRule gameRule { get; private set; }

        public GameHandler(Int32 numOfPlayers, Int32 numOfTeams)
        {
            gameRule = new GameRule(numOfPlayers, numOfTeams);
        }

        public void StartGame()
        {
            gameRule.StartGame();
        }

        public void NextRound()
        {
            gameRule.NextRobot();
        }

        public Robot GetCurrentPlayer()
        {
            return gameRule.CurrentRobot();
        }

        public GameObject GetFieldValue(Int32 x, Int32 y)
        {
            return gameRule.GetFieldValue(x, y);
        }
        public void DoAction(Actions action)
        {
            //TODO
            //does given action in the game
        }
    }
}
