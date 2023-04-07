using System;
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

        public GameHandler()
        {
            gameRule = new GameRule();
        }

        public void DoAction(Actions action) 
        { 
            //TODO
            //does given action in the game
        }

        public Robot GetCurrentPlayer()
        {
            return gameRule.CurrentRobot();
        }

        public GameObject GetCoords(Int32 x, Int32 y)
        {
            return gameRule.GetCoords(x, y);
        }
    }
}
