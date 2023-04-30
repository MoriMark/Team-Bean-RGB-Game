using RGBModell.modell.enums;
using RGBModell.modell.gameobjects;

namespace RGBModell.modell.exceptions
{
    public class RobotNotInTeam : Exception
    {
        public RobotNotInTeam() { }

        //TODO use robot parameter to identify it
        public RobotNotInTeam(Robot robot) : base($"Robot not in any team!") { }
    }
}
