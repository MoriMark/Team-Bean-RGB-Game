using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    /// <summary>
    /// Class <c>Obstacle</c> is a derived class from <c>DeletableObject</c> base class, which represents different colored Boxes on the field.
    /// </summary>
    public class Obstacle : DeletableObject
    {
        /// <summary>
        /// This constructor takes the same parameters as gameobject plus health.
        /// </summary>
        public Obstacle(int i, int j, int health) : base(i, j , health, enums.TileType.Obstacle)
        { 
            this.health = health;
        }

        
    }
}
