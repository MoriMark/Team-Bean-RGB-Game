using RGBModell.modell.enums;
using RGBModell.modell.gameobjects;

namespace RGBModell.modell.exceptions
{
    public class RobotAlreadyInATeamException : Exception
    {
        public RobotAlreadyInATeamException() { }

        //TODO use robot parameter to identify it
        public RobotAlreadyInATeamException(Robot robot, Team team) : base($"Robot already in team ${team.ToString()}!") { }
    }
}
