using RGBModell.modell.enums;
using RGBModell.modell.gameobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.structs
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
