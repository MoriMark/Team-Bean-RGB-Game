using RGBModell.modell.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    /// <summary>
    /// Class <c>Box</c> is a derived class from <c>DeletableObject</c> base class, which represents different colored Boxes on the field.
    /// </summary>
    public class Box : DeletableObject
    {
        public int id { get; }
        
        private static int staticid = 0;
        public BoxColor color { get; }
        public int ingroup { get; set; }
        public Team attaching { get; set; }
        /// <summary>
        /// This cnstructor takes the same parameters as <c>GameObject</c> plus the color of the box.
        /// </summary>
        public Box(int i, int j, BoxColor color, TileType type) : base(i,j, type)
        {
            id = staticid;
            staticid++;
            this.color = color;
            ingroup = 0;
            Random RNG = new Random();
            health = RNG.Next(1,5);
        }

    }
}
