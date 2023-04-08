using RGB.modell.boxlogic;
using RGB.modell.enums;
using RGB.modell.events;
using RGB.modell.exceptions;
using RGB.modell.game_logic;
using RGB.modell.gameobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell
{
    public class GameRule
    {
        private Field field;

        private List<Robot> robots;
        private Robot currentRobot;

        private Dictionary<int, BoxGroup> boxgroups;

        Int32 numberOfCurrentRound;
        public Boolean GameIsActive { get; private set; }
        public Boolean GameIsPaused { get; private set; }


        public event EventHandler<UpdateFieldsEventArgs> UpdateFields;

        public GameRule()
        {
            GameIsActive = false;
            GameIsPaused = false;
        }

        public GameRule(Int32 numOfRobots, Int32 numOfTeams)
        {
            GameIsActive = false;
            GameIsPaused = false;
            int totalRobots = numOfRobots * numOfTeams;
            int tableSize = totalRobots*4;
            field = new Field(tableSize);
            robots = field.GenerateField(numOfRobots, numOfTeams);
        }

        public GameObject GetFieldValue(Int32 x, Int32 y)
        {
            return field.GetValue(x-5, y-5);    //-5 for border size
        }

        // TODO doc comment
        public Boolean StartGame()
        {
            if (field is null) throw new TableIsMissingException();

            if (robots is null | robots.Count == 0) throw new RobotsReuiredToPlayException();

            numberOfCurrentRound = 1;

            GameIsActive = true;
            GameIsPaused = false;

            currentRobot = robots[0];
            OnFieldsUpdate();

            return true;
            //throw new NotImplementedException();
        }

        // TODO doc comment
        public void SetTable(Field field)
        {
            if (GameIsActive) throw new ActiveGameException();

            this.field = field;
        }

        //Temporary
        public void SetRobots(List<Robot> robots)
        {
            if (GameIsActive) throw new ActiveGameException();

            this.robots = robots;
        } 

        /// <summary>
        /// Pauses or resumes the game.
        /// </summary>
        /// <returns>The new state of the game.</returns>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        public Boolean PauseGame()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();

            GameIsPaused = !GameIsPaused;

            // Additional code here

            return GameIsPaused;
        }

        /// <summary>
        /// Returns the current robot.
        /// </summary>
        /// <returns>The current robot.</returns>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        public Robot CurrentRobot()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();

            return currentRobot;
        }

        /// <summary>
        /// Advances the current robot to the next one to make it's turn
        /// </summary>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void NextRobot()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            Int32 i = robots.IndexOf(currentRobot);
            if (i == robots.Count - 1)
            {
                currentRobot = robots[0];
                numberOfCurrentRound++;
            }
            else
            {
                currentRobot = robots[i + 1];
            }
        }

        /// <summary>
        /// Makes a turn, changing the current direction of the player.
        /// </summary>
        /// <param name="direction">The direction to turn to.</param>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void MakeTurn(Direction direction)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            currentRobot.facing = direction;

            NextRobot();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <exception cref="NoActiveGameException"></exception>
        /// <exception cref="GameIsPausedException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public void MakeStep(Int32 i, Int32 j)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            throw new NotImplementedException();

            NextRobot();
        }

        /// <summary>
        /// Proceeds to the next round in the game.
        /// </summary>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public void Pass()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            // Additional code here

            NextRobot();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public Boolean Clean()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            throw new NotImplementedException();

            NextRobot();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NoActiveGameException">Thrown when there is no active game.</exception>
        /// <exception cref="GameIsPausedException">Thrown when the active game is paused.</exception>
        public Boolean Lift()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            throw new NotImplementedException();

            NextRobot();
        }

        public Boolean Weld()
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            throw new NotImplementedException();

            NextRobot();
        }

        public Boolean UnWeld(Int32 i1, Int32 j1, Int32 i2, Int32 j2)
        {
            if (!GameIsActive)
                throw new NoActiveGameException();
            if (GameIsPaused)
                throw new GameIsPausedException();

            throw new NotImplementedException();

            NextRobot();
        }

        private void OnFieldsUpdate()
        {
            if (UpdateFields != null)
                UpdateFields(this, new UpdateFieldsEventArgs(field.CalculateVisionOfRobot(currentRobot)));
        }
    }
}
