using RGBModell.modell.game_logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.events
{
    public class UpdateFieldsEventArgs : EventArgs
    {
        public Field field;

        public UpdateFieldsEventArgs(Field field)
        {
        this.field = field;
        }
    }
}
