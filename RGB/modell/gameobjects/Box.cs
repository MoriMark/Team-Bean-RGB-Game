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
        private int id { get; set; }
        private static int staticid = 0;
        private BoxColor color { get; }
        private int intgroup { get; set; }
        
        public Box(int i, int j, BoxColor color) : base(i,j)
        {
            id = staticid;
            staticid++;
            this.color = color;
            intgroup = 0;
        }
    }
}
