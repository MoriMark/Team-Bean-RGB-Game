using RGB.modell.enums;
using RGB.modell.exceptions;
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
        private List<Robot> sequence;
        private Robot current;

        Int32 numberOfRounds;
        public Boolean GameIsActive { get; private set; }
        public Boolean GameIsPaused { get; private set; }

        public GameRule()
        {
            GameIsActive = false;
            GameIsPaused = false;
        }

        // TODO doc comment
        public Boolean StartGame()
        {
            numberOfRounds = 1;

            throw new NotImplementedException();
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

            return current;
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

            current.facing = direction;
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

            numberOfRounds++;

            // Additional code here
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
        }
    }
}
