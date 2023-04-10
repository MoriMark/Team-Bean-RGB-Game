using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell.enums
{
    public enum Actions
    {
        MoveUp, MoveDown, MoveLeft, MoveRight, Move,
        RotateLeft, RotateRight, Rotate,
        Weld, Unweld,
        Connect, Disconnect,
        Wait,
        Clean,
        Cancel,
        None
    }
}
