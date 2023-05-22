using RGBModell.modell.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.View
{
    /// <summary>
    /// Actionbutton stores an action corresponding with the action
    /// defined
    /// </summary>
    /// <returns></returns>
    public class ActionButton : Button
    {
        private Actions action;

        public ActionButton(Actions action)
        {
            this.action = action;
        }

        public Actions getAction()
        {
            return action;
        }
    }
}
