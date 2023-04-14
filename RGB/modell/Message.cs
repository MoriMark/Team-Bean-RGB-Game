using RGB.modell.enums;
using RGB.modell.gameobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell
{
    public struct Message
    {
        public readonly Robot robot;
        public readonly Symbol symbol;

        public Message(Robot robot, Symbol symbol)
        {
            this.robot = robot;
            this.symbol = symbol;
        }
    }
}
