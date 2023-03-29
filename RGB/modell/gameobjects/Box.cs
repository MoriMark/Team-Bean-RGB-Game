using RGB.modell.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell.gameobjects
{
    public class Box : GameObject
    {
        public int id { get; }
        private static int staticid = 0;
        public BoxColor color { get; }
        public int intgroup { get; set; }
        public Team attaching { get; set; }
        public Box(int i, int j, BoxColor color) : base(i,j)
        {
            id = staticid;
            staticid++;
            this.color = color;
            intgroup = 0;
        }

    }
}
