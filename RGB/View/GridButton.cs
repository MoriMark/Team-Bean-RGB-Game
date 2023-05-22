using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.View
{
    /// <summary>
    /// Playfield buttons, a gridbutton stores its coordinates
    /// </summary>
    /// <returns></returns>
    public class GridButton : Button
    {
        private Int32 _x;
        private Int32 _y;

        public bool seen { get; set; }
        public Int32 GridX { get { return _x; } }
        public Int32 GridY { get { return _y; } }

        public GridButton(Int32 x, Int32 y) { _x = x; _y = y; seen = true; }
    }
}
