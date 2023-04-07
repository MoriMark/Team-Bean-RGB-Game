using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGB.modell.enums;

namespace RGB.modell
{
    public class GameHandler
    {
        public GameRule gameRule { get; private set; }

        public GameHandler()
        {
            gameRule = new GameRule();
        }

        public void doAction(Actions action) 
        { 
            //TODO
            //does given action in the game
        }
    }
}
