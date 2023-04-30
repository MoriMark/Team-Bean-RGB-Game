using RGBModell.modell.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    public class Robot : GameObject
    {
        public Direction facing { get; set; }
        public Box? Attached { get; set; }
        public Team team { get; set; }
        public string name { get; set; }

        public Robot(int i, int j, Direction facing, Team team, TileType type, string name) : base(i, j, type)
        { 
            this.facing = facing;
            Attached = null;
            this.team = team;
            this.name = name;
        }

        public bool IsAttached()
        {
            return Attached != null;
        }

        public int GetAttachedGroupId()
        {
            if(Attached != null)
            {
                return Attached.ingroup;
            }
            else
            {
                throw new Exception("This function shouldn't have been called while attached is null!");
            }
        }
    }
}
