using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGBModell.modell.enums;

namespace RGB.View
{
    /// <summary>
    /// A symbolbutton stores the assigned symbol
    /// </summary>
    /// <returns></returns>
    public class SymbolButton : Button
    {
        public Symbol symbol { get; set; }

        public SymbolButton(Symbol symbol) { this.symbol = symbol; }
    }
}
