﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell.gameobjects
{
    public class Wall : GameObject
    {
        public Wall(int i, int j) : base(i, j) 
        {
            iswall= true;
        }
    }
}
