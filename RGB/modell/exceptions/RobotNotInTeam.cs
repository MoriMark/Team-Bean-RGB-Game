using RGB.modell.enums;
using RGB.modell.gameobjects;

namespace RGB.modell.exceptions
{
    public class RobotNotInTeam : Exception
    {
        public RobotNotInTeam() { }

        //TODO use robot parameter to identify it
        public RobotNotInTeam(Robot robot) : base($"Robot not in any team!") { }
    }
}
