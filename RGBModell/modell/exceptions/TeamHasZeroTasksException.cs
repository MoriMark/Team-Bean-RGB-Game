using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.exceptions
{
    public class TeamHasZeroTasksException : Exception
    {
        public TeamHasZeroTasksException() : base() { }
    }
}
