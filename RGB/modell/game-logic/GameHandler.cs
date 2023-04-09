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
        private List<Actions> actionsThisTurn;
        public int move { get; set; }
        public int round { get; set; }
        private int numOfPlayers;
        private int numOfTeams;

        public event EventHandler? robotChanged;

        public GameRule gameRule { get; private set; }

        public GameHandler(Int32 numOfPlayers, Int32 numOfTeams)
        {
            move = 1;
            round = 1;
            this.numOfPlayers = numOfPlayers;
            this.numOfTeams = numOfTeams;
            gameRule = new GameRule(numOfPlayers, numOfTeams);
            actionsThisTurn = new List<Actions>();
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
        public void AttemptAction(List<Coordinate> coords, Actions action)
        {
            switch (action) 
            {
                case Actions.Wait:
                    gameRule.NextRobot();
                    robotChanged(this, EventArgs.Empty);
                    break;
                default:
                    break;
            }
            move++;
            if (move > (numOfTeams*numOfPlayers))
            {
                move = 1;
                round++;
            }
        }
    }
}
