using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    public class Obstacle : GameObject
    {
        public int health{ get; set; }
        public Obstacle(int i, int j, int health) : base(i, j, enums.TileType.Obstacle)
        { 
            this.health = health;
        }

        public void lowerlife()
        {
            health--;
            if(health <= 0 )
            {
                deletegroup();
            }
        }

        private void deletegroup()
        {
            isempty = true;
        }
    }
}
