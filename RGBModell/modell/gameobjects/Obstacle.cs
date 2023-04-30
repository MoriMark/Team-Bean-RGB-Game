﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    public class Obstacle : GameObject
    {
        private int obstaclelife;
        public Obstacle(int i, int j, int obstaclelife) : base(i, j, enums.TileType.Obstacle)
        { 
            this.obstaclelife = obstaclelife;
        }

        public void lowerlife()
        {
            obstaclelife--;
            if(obstaclelife<= 0 )
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