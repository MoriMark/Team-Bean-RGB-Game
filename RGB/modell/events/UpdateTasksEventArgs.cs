using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
