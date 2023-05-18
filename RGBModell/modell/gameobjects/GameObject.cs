using RGBModell.modell.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    /// <summary>
    /// Class <c>GameObject</c> is the base class for all derived classes on the field.
    /// </summary>
    public class GameObject
    {
        public int i { get; set; }
        public int j { get; set; }
        protected TileType type;
        protected bool isempty;
        protected bool iswall;

        /// <summary>
        /// This constructor takes the coordinates of where the object is on the field and the type it will be.
        /// </summary>
        public GameObject(int i, int j, TileType type)
        {
            this.i = i;
            this.j = j;
            this.type = type;
            isempty = false;
            iswall = false;
        }

        /// <summary>
        /// Checks wether the object is an Empty <c>GameObject</c>.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool IsEmpty()
        {
            return isempty;
        }

        /// <summary>
        /// Checks wether the object is a Wall <c>GameObject</c>.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool IsWall()
        {
            return iswall;
        }

        /// <summary>
        /// TileType getter.
        /// </summary>
        /// <returns>The TileType of this object.</returns>
        public TileType TileType()
        {
            return type;
        }

        
    }
}
