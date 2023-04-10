using RGB.modell.structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGB.modell.enums;

namespace RGB.modell.game_logic
{
    public class RobotAction
    {
        public List<Coordinate> coordinates;
        public Actions action;

        public RobotAction(List<Coordinate> coordinates, Actions action) 
        {
            this.action = action;
            this.coordinates = coordinates;
        }
    }
}
