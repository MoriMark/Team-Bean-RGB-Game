using RGB.modell.game_logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell.events
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
