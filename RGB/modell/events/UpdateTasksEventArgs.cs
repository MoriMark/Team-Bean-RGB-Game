using Task = RGB.modell.structs.Task;

namespace RGB.modell.events
{
    public class UpdateTasksEventArgs : EventArgs
    {
        public List<Task> tasks;

        public UpdateTasksEventArgs(List<Task> tasks)
        {
            this.tasks = tasks;
        }
    }
}
