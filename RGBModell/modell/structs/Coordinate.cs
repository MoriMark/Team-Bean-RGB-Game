using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.structs
{
    /// <summary>
    /// This struct represents a field in a matrix
    /// </summary>
    public struct Coordinate
    {
        public int X; public int Y;
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
