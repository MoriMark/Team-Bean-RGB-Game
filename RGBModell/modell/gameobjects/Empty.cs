using RGBModell.modell.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    /// <summary>
    /// Class <c>Empty</c> is a derived class from <c>GameObject</c> base class, which represents a an empty tile on the field.
    /// </summary>
    public class Empty : GameObject
    {
        /// <summary>
        /// This constructor omly takes the coordinates and automaticly assigns the type.
        /// </summary>
        public Empty(int i, int j) : base(i, j, enums.TileType.Empty)
        {
            
            isempty = true;
        }
    }
}
