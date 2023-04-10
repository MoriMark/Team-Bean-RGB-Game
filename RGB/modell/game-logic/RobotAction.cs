using RGB.modell.structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGB.modell.enums;
using RGB.modell.gameobjects;

namespace RGB.modell.game_logic
{
    public class RobotAction
    {
        public List<Coordinate> coordinates;
        public Actions action;
        public Robot robot;

        public RobotAction(Robot robot, List<Coordinate> coordinates, Actions action) 
        {
            this.action = action;
            this.coordinates = coordinates;
            this.robot = robot;
        }
    }
}
