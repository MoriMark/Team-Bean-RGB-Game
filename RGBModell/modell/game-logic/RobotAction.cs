using RGBModell.modell.structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGBModell.modell.enums;
using RGBModell.modell.gameobjects;

namespace RGBModell.modell.game_logic
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
