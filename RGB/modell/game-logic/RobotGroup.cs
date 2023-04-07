using RGB.modell.enums;
using RGB.modell.exceptions;
using RGB.modell.gameobjects;

namespace RGB.modell
{
    public class RobotGroup
    {
        private Dictionary<Team, List<Robot>> groups;
        private Dictionary<Team, Int32> groupPoints;

        public RobotGroup()
        {
            groups = new Dictionary<Team, List<Robot>>();
            groupPoints = new Dictionary<Team, Int32>();
        }

        /// <summary>
        /// Searches for the team associated with the specified robot.
        /// </summary>
        /// <param name="robot">The robot to search for.</param>
        /// <returns>
        /// The team associated with the specified robot found, or null if the robot is not found in any of the teams.
        /// </returns>
        public Team? TeamOfRobot(Robot robot)
        {
            foreach (KeyValuePair<Team, List<Robot>> kvp in groups)
            {
                if (kvp.Value.Contains(robot))
                    return kvp.Key;
            }

            return null;
        }

        /// <summary>
        /// Adds the given robot to the specified team.
        /// </summary>
        /// <param name="robot">The robot to add to the team.</param>
        /// <param name="team">The team to add the robot to.</param>
        /// <exception cref="RobotAlreadyInATeamException">Thrown when the given robot is already in a team.</exception>
        public void AddRobotToGroup(Robot robot, Team team)
        {
            Team? teamOf = TeamOfRobot(robot);
            if (teamOf.HasValue)
                throw new RobotAlreadyInATeamException(robot, teamOf.Value);

            List<Robot> robots = groups[team];
            if (robots == null)
                robots = new List<Robot>();

            robots.Add(robot);
        }

        /// <summary>
        /// Removes the given robot from its current team.
        /// </summary>
        /// <param name="robot">The robot to remove from the group.</param>
        /// <exception cref="RobotNotInTeamException">Thrown when the given robot is not in a team.</exception>
        public void RemoveRobotFromGroup(Robot robot)
        {
            Team? teamOf = TeamOfRobot(robot);

            if (!teamOf.HasValue)
                throw new RobotNotInTeam(robot);

            List<Robot> robots = groups[teamOf.Value];

            robots.Remove(robot);
        }

        /// <summary>
        /// Adds the points for a given team.
        /// </summary>
        /// <param name="team">The team to add the points for.</param>
        /// <param name="points">The number of points to add for the team.</param>
        public void AddTeamPoints(Team team, int points)
        {
            if (!groupPoints.ContainsKey(team))
            {
                groupPoints.Add(team, points);
                return;
            }

            groupPoints[team] = groupPoints[team] + points;
        }

        /// <summary>
        /// Sets the points for a given team.
        /// </summary>
        /// <param name="team">The team to set the points for.</param>
        /// <param name="points">The number of points to set for the team.</param>
        public void SetTeamPoints(Team team, int points)
        {
            if (!groupPoints.ContainsKey(team))
            {
                groupPoints.Add(team, points);
                return;
            }

            groupPoints[team] = points;
        }

        /// <summary>
        /// Returns the current points of each team.
        /// </summary>
        /// <returns>The teams with their points.</returns>
        public Dictionary<Team, Int32> GetTeamPoints()
        {
            return new Dictionary<Team, Int32>(groupPoints);
        }
    }
}
