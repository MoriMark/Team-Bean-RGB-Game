using RGBModell.modell.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.structs
{
    public struct Mapfield
    {
        public int lastSeen;
        public Coordinate coords;
        public TileType type;

        public Mapfield(int lastSeen, Coordinate coords, TileType type)
        {
            this.lastSeen = lastSeen;
            this.coords = coords;
            this.type = type;
        }
    }
}
