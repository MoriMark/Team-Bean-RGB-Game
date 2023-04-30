using RGBModell.modell.enums;
using RGBModell.modell.events;
using RGBModell.modell.exceptions;
using RGBModell.modell.gameobjects;
using Task = RGBModell.modell.structs.Task;

namespace RGBModell.modell.game_logic
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
            teamPoints = new Dictionary<Team, int>();
            rnd = new Random();
        }

        /// <summary>
        /// Returns the number of boxes of a given task.
        /// </summary>
        /// <param name="task">The task to count the boxes in.</param>
        /// <returns>The number of boxes of a given task.</returns>
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

        /// <summary>
        /// Returns a two-dimensional array of random box colors based on the available shapes.
        /// </summary>
        /// <returns>A two-dimensional array of randomly assigned box colors.</returns>
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

        public void ReduceTasksTime()
        {
            foreach (Team team in tasks.Keys)
            {
                List<Task> teamTasks = GetTeamTasks(team);

                teamTasks.ForEach(task => --task.expiration);
            }
        }

        /// <summary>
        /// Checks if the given list of boxes represents a task for the specified team and returns the corresponding task if found.
        /// </summary>
        /// <param name="team">The team for which to check if the boxes represent a task.</param>
        /// <param name="boxes">The list of boxes to check if they represent a task.</param>
        /// <returns>The task that matches the given list of boxes first in timeline, if found; otherwise, returns null.</returns>
        public Task? GivenPatternIsATaskOfGivenTeam(Team team, List<Box> boxes)
        {
            Task? retVal = null;

            List<Task> teamTasks = GetTeamTasks(team);

            BoxColor[,] boxesMatrix = GenerateMatrixFromAttachedBoxes(boxes);

            for (Int32 i = 0;i < teamTasks.Count;i++)
            {
                if (BoxesInTask(teamTasks[i]) != boxes.Count)
                    continue;

                if (AreBoxColorMatrixesEqual(boxesMatrix, teamTasks[i].task))
                {
                    retVal = teamTasks[i];
                    break;
                }
            }

            return retVal;
        }

        /// <summary>
        /// Generates a matrix of BoxColors from the given list of boxes.
        /// </summary>
        /// <param name="boxes">The list of boxes to generate the matrix from.</param>
        /// <returns>A matrix of BoxColors with the same size and shape as the area covered by the boxes.</returns>
        /// <exception cref="ArgumentException">Thrown when the input list is empty.</exception>
        private BoxColor[,] GenerateMatrixFromAttachedBoxes(List<Box> boxes)
        {
            if (!boxes.Any())
                throw new ArgumentException("boxes must contain atleast one element!");

            Int32 minI = boxes[0].i, minJ = boxes[0].j, maxI = boxes[0].i, maxJ = boxes[0].j;

            for (Int32 i = 1;i < boxes.Count;i++)
            {
                if (boxes[i].i < minI)
                    minI = boxes[i].i;
                if (boxes[i].j < minJ)
                    minJ = boxes[i].j;
                if (boxes[i].i > maxI)
                    maxI = boxes[i].i;
                if (boxes[i].j > maxJ)
                    maxJ = boxes[i].j;
            }

            Int32 sizeI = maxI - minI + 1;
            Int32 sizeJ = maxJ - minJ + 1;

            BoxColor[,] retVal = new BoxColor[sizeI, sizeJ];

            for(Int32 i = 0; i < boxes.Count; i++)
            {
                Int32 setI = boxes[i].i;
                Int32 setJ = boxes[i].j;

                retVal[setI - minI, setJ - minJ] = boxes[i].color;
            }

            return retVal;
        }

        private bool AreBoxColorMatrixesEqual(BoxColor[,] array1, BoxColor[,] array2)
        {
            if (array1 == null || array2 == null ||
                array1.GetLength(0) != array2.GetLength(0) ||
                array1.GetLength(1) != array2.GetLength(1))
            {
                return false;
            }

            // return array1.Cast<BoxColor>().SequenceEqual(array2.Cast<BoxColor>());
            for (int i = 0; i < array1.GetLength(0); i++)
                for (int j = 0; j < array1.GetLength(1); j++)
                    if (array1[i, j] != array2[i, j])
                        return false;

            return true;
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
