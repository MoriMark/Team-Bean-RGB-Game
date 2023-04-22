using RGB.modell.enums;
using RGB.modell.events;
using Task = RGB.modell.structs.Task;

namespace RGB.modell.game_logic
{
    public partial class TaskHandler
    {
        Dictionary<Team, List<Task>> tasks;

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
        }

        public void OnTasksUpdate(Team team)
        {
            if (UpdateFields != null)
                UpdateFields(this, new UpdateTasksEventArgs(new List<Task>(tasks[team])));
        }
    }
}
