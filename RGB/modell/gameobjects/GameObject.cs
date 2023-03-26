using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell.gameobjects
{
    public class GameObject
    {
        private int i { get; set; }
        private int j { get; set; }
        private bool isempty;
        private bool iswall;

        public GameObject(int i, int j)
        {
            this.i = i;
            this.j = j;
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

        public int GetI()
        {
            return i;
        }

        public int GetJ()
        {
            return j;
        }
    }
}
