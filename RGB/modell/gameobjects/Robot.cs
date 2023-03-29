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

        public Robot(int i, int j, Direction facing) : base(i, j)
        { 
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
                return -1;
            }
        }
    }
}
