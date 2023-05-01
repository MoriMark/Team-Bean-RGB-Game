using Task = RGBModell.modell.structs.Task;

namespace RGBModell.modell.events
{
    public class UpdateTasksEventArgs : EventArgs
    {
        public List<Task> tasks;
        public Int32 points;

        public UpdateTasksEventArgs(List<Task> tasks, Int32 points)
        {
            this.tasks = tasks;
            this.points = points;
        }
    }
}
