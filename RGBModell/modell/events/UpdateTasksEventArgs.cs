using Task = RGBModell.modell.structs.Task;

namespace RGBModell.modell.events
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
