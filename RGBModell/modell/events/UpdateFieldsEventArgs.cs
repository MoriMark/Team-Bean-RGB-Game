using RGBModell.modell.game_logic;
using RGBModell.modell.gameobjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.events
{
    public class UpdateFieldsEventArgs : EventArgs
    {
        public GameObject[,] gameObjects;

        public UpdateFieldsEventArgs(GameObject[,] gameObjects)
        {
            this.gameObjects = gameObjects;
        }
    }
}
