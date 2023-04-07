using RGB.modell.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell.gameobjects
{
    public class Robot : GameObject
    {
        public Direction facing { get; set; }
        public Box? Attached { get; set; }
        public Team team { get; set; }

        public Robot(int i, int j, Direction facing, Team team) : base(i, j)
        { 
            this.team = team;
            this.facing = facing;
            Attached = null;
        }

        public bool IsAttached()
        {
            return Attached != null;
        }

        public int GetAttachedGroupId()
        {
            if(Attached != null)
            {
                return Attached.id;
            }
            else
            {
                throw new Exception("This function shouldn't have been called while attached is null!");
            }
        }
    }
}
