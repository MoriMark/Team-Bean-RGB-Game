using RGBModell.modell.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    /// <summary>
    /// Class <c>DeletableObject</c> is a derived class from <c>GameObject</c> base class, which unifies the health for deletable objects on the field.
    /// </summary>
    public class DeletableObject : GameObject
    {
        public int health{get;set;}
        /// <summary>
        /// This constructor takes the same parameters as gameobject plus health.
        /// </summary>
        public DeletableObject(int i, int j, int health, TileType type) : base(i, j, type)
        { 
            this.health = health;
        }
        /// <summary>
        /// Alternative constructor which takes the exact same parameters as gameobject.
        /// </summary>
        public DeletableObject(int i, int j, TileType type) : base(i, j, type)
        {
            
        }
    }
}
