using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell
{
    public class GameHandler
    {
        public GameRule gameRule { get; private set; }

        public GameHandler()
        {
            gameRule = new GameRule();
        }
    }
}
