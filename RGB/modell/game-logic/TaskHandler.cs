using RGB.modell.enums;
using RGB.modell.events;
using RGB.modell.exceptions;
using RGB.modell.gameobjects;
using System.Collections.Generic;
using Task = RGB.modell.structs.Task;

namespace RGB.modell.game_logic
{
    public class TaskHandler
    {
        private Dictionary<Team, List<Task>> tasks;
        private Dictionary<Team, Int32> teamPoints;

        //Means the number of rounds a given task can be fulfilled
        private const Int32 taskTimeLimit = 25;
        private Random rnd;

        private static readonly Byte[][,] availableShapes = new Byte[][,]
        {
            // 1x1
            new Byte[,]
            {
                { 1 }
            },

            // 1x2 & 2x1
            new Byte[,]
            {
                { 1,1 },
            },
            new Byte[,]
            {
                { 1 },
                { 1 }
            },

            // 2x2
            new Byte[,]
            {
                { 1,1 },
                { 1,1 }
            },

            // 3x2 & 2x3
            new Byte[,]{
                { 1,0 },
                { 1,0 },
                { 1,1 }
            },
            new Byte[,]{
                { 1,1 },
                { 0,1 },
                { 0,1 }
            },
            new Byte[,]{
                { 1,0,0 },
                { 1,1,1 },
            },
            new Byte[,]{
                { 1,1,1 },
                { 0,0,1 },
            },

            // 3x3
            new Byte[,]{
                { 0,1,0 },
                { 1,1,1 },
                { 0,1,0 }
            },
        };

        public event EventHandler<UpdateTasksEventArgs> UpdateFields;

        public TaskHandler()
        {
            tasks = new Dictionary<Team, List<Task>>();
            rnd = new Random();
        }

        private Int32 BoxesInTask(Task task)
        {
            return task.task.Cast<BoxColor>().Count(x => x != BoxColor.NoColor);
        }

        public List<Task> GetTeamTasks(Team team)
        {
            if(!tasks.ContainsKey(team))
                tasks.Add(team, new List<Task>());

            return tasks[team];
        }

        public Int32 GetTeamPoints(Team team)
        {
            if (!teamPoints.ContainsKey(team))
                teamPoints.Add(team, 0);
                
            return teamPoints[team];
        }

        public void GenerateRandomTaskForTeam(Team team)
        {
            if (!tasks.ContainsKey(team))
                tasks.Add(team, new List<Task>());

            Task task = new Task()
            {
                expiration = taskTimeLimit,
                direction = (Direction)rnd.Next(0, Enum.GetValues(typeof(Direction)).Length),
                task = GetRandomBoxColorMatrix()
            };

            tasks[team].Add(task);
        }

        private BoxColor[,] GetRandomBoxColorMatrix()
        {
            Int32 ind = rnd.Next(0,availableShapes.GetLength(0));

            BoxColor[,] retVal = new BoxColor[availableShapes[ind].GetLength(0),availableShapes[ind].GetLength(1)];

            BoxColor[] values = Enum.GetValues(typeof(BoxColor))
                     .Cast<BoxColor>()
                     .Where(e => e != BoxColor.NoColor)
                     .ToArray();

            for (Int32 i = 0;i < retVal.GetLength(0); ++i)
            {
                for (Int32 j = 0; j < retVal.GetLength(1); ++j)
                {
                    if (availableShapes[ind][i,j] == 1)
                    {
                        retVal[i,j] = values[rnd.Next(0, values.Length)];
                    }
                    else
                    {
                        retVal[i,j] = BoxColor.NoColor;
                    }
                }
            }

            return retVal;
        }

        public void FinishTask(Task task)
        {
            ICollection<Team> teams = tasks.Keys;

            foreach (Team team in teams)
            {
                if (tasks[team].Contains(task))
                {
                    tasks[team].Remove(task);
                    
                    if (!teamPoints.ContainsKey(team))
                        teamPoints.Add(team, 0);

                    teamPoints[team] = ++teamPoints[team];
                }
            }
        }

        public void ReduceTeamTasksTime(Team team)
        {
            List<Task> teamTasks = GetTeamTasks(team);

            teamTasks.ForEach(task => --task.expiration);
        }

        public Task? CheckIfTeamTaskIsDone(Robot robot, List<Box> boxes)
        {
            if (!robot.IsAttached())
                return null;

            Task? retVal = null;

            List<Task> teamTasks = GetTeamTasks(robot.team);

            for (Int32 i = 0;i < teamTasks.Count;i++)
            {
                if (BoxesInTask(teamTasks[i]) != boxes.Count)
                    continue;

                // TODO check pattern
            }

            return retVal;
        }

        public void OnTasksUpdate(Team team)
        {
            if (!tasks.ContainsKey(team))
                throw new TeamHasZeroTasksException();
            if (!teamPoints.ContainsKey(team))
                teamPoints.Add(team, 0);

            if (UpdateFields != null)
                UpdateFields(this, new UpdateTasksEventArgs(new List<Task>(tasks[team])));
        }
    }
}
