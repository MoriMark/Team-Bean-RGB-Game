using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    /// <summary>
    /// Class <c>Wall</c> is a derived class from <c>GameObject</c> base class, which represents a wall on the field.
    /// </summary>
    public class Wall : GameObject
    {
        public Wall(int i, int j) : base(i, j, enums.TileType.Wall) 
        {
            iswall= true;
        }
    }
}
