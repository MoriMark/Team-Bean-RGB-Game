using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.events
{
    public class SpecialEventEventArgs : EventArgs
    {
        public string eventname;
        public int gameobjectnumber;
        public SpecialEventEventArgs(string eventname, int gameobjectnumber)
        {
            this.eventname = eventname;
            this.gameobjectnumber = gameobjectnumber;
        }
    }
}
