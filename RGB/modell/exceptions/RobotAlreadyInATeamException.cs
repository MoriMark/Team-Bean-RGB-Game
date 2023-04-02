using RGB.modell.enums;
using RGB.modell.gameobjects;

namespace RGB.modell.exceptions
{
    public class RobotAlreadyInATeamException : Exception
    {
        public RobotAlreadyInATeamException() { }

        //TODO use robot parameter to identify it
        public RobotAlreadyInATeamException(Robot robot, Team team) : base($"Robot already in team ${team.ToString()}!") { }
    }
}
