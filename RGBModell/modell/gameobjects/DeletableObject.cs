using RGBModell.modell.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    public class DeletableObject : GameObject
    {
        public int health{get;set;}
        public DeletableObject(int i, int j, int health, TileType type) : base(i, j, type)
        { 
            this.health = health;
        }

        public DeletableObject(int i, int j, TileType type) : base(i, j, type)
        {
            
        }
    }
}
