using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGB.modell.enums;

namespace RGB.View
{
    public class SymbolButton : Button
    {
        public Symbol symbol { get; set; }

        public SymbolButton(Symbol symbol) { this.symbol = symbol; }
    }
}
