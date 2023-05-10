using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    public class Obstacle : DeletableObject
    {
        public Obstacle(int i, int j, int health) : base(i, j , health, enums.TileType.Obstacle)
        { 
            this.health = health;
        }

        
    }
}
