using RGB.modell.enums;
using RGB.modell.events;
using RGB.modell.exceptions;
using Task = RGB.modell.structs.Task;

namespace RGB.modell.game_logic
{
    public class TaskHandler
    {
        Dictionary<Team, List<Task>> tasks;
        //Means the number of rounds a given task can be fulfilled
        private const Int32 taskTimeLimit = 25;
        Random rnd;

        private static readonly Byte[][,] availableShapes = new Byte[][,]
        {
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

        public void OnTasksUpdate(Team team)
        {
            if (!tasks.ContainsKey(team))
                throw new TeamHasZeroTasksException();

            if (UpdateFields != null)
                UpdateFields(this, new UpdateTasksEventArgs(new List<Task>(tasks[team])));
        }
    }
}
