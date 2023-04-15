using RGB.modell.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell.structs
{
    public struct Exit
    {
        public Coordinate Coordinate { get; private set; }
        public Direction Direction { get; private set; }

        public Exit(Coordinate coordinate, Direction direction)
        {
            Coordinate = coordinate;
            Direction = direction;
        }
    }

}
