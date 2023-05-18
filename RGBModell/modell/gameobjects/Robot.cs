using RGBModell.modell.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    /// <summary>
    /// Class <c>Robot</c> is a derived class from <c>GameObject</c> base class, which represents a robot controlled by one of the players on the field.
    /// </summary>
    public class Robot : GameObject
    {
        public Direction facing { get; set; }
        public Box? Attached { get; set; }
        public Team team { get; set; }
        public string name { get; set; }
        public bool actionsucces { get; set; }
        /// <summary>
        /// This constructor takes the same parameters <c>GameObject</c> plus which direction its facing, which team its on and its name.
        /// </summary>
        public Robot(int i, int j, Direction facing, Team team, TileType type, string name) : base(i, j, type)
        { 
            this.facing = facing;
            Attached = null;
            this.team = team;
            this.name = name;
            actionsucces= false;
        }
        /// <summary>
        /// Checks wether the Robot is Attached to a Box.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool IsAttached()
        {
            return Attached != null;
        }
        /// <summary>
        /// Gets the id of the Box it is attached to.
        /// </summary>
        /// <returns>The id of the group which it is attached to or 0 if the box is not in a group.</returns>
        public int GetAttachedGroupId()
        {
            if(Attached != null)
            {
                return Attached.ingroup;
            }
            else
            {
                throw new Exception("This function shouldn't have been called while attached is null!");
            }
        }
    }
}
