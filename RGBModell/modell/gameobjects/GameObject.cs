using RGBModell.modell.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    public class GameObject
    {
        public int i { get; set; }
        public int j { get; set; }
        protected TileType type;
        protected bool isempty;
        protected bool iswall;

        public GameObject(int i, int j, TileType type)
        {
            this.i = i;
            this.j = j;
            this.type = type;
            isempty = false;
            iswall = false;
        }

        public bool IsEmpty()
        {
            return isempty;
        }

        public bool IsWall()
        {
            return iswall;
        }

        public TileType TileType()
        {
            return type;
        }

        
    }
}
