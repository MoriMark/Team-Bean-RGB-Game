﻿using RGBModell.modell.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    public class Empty : GameObject
    {
        public Empty(int i, int j) : base(i, j, enums.TileType.Empty)
        {
            
            isempty = true;
        }
    }
}