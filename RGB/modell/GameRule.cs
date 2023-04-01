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
    }
}
