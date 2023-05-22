using RGBModell.modell.enums;
using RGBModell.modell.structs;
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
        public List<Mapfield> map;
        public List<Robot> seenRobots { get; set; }

        public Robot(int i, int j, Direction facing, Team team, TileType type, string name) : base(i, j, type)
        { 
            this.facing = facing;
            Attached = null;
            this.team = team;
            this.name = name;
            actionsucces= false;
            map = new List<Mapfield>();
            seenRobots = new List<Robot>();
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

        public void UpdateMap(GameObject[,] vision, int round)
        {
            int viewDist = 4;
            //updating own map
            foreach (GameObject obj in vision) 
            {
                if (Math.Abs(obj.i - this.i) + Math.Abs(obj.j - this.j) <= viewDist)
                {
                    bool coordFound = false;
                    Coordinate c = new Coordinate(obj.i, obj.j);
                    for (int i = 0; i < map.Count; i++)
                    {
                        if (map[i].coords.X == obj.i && map[i].coords.Y == obj.j)
                        {
                            coordFound = true;
                            map[i] = new Mapfield(round, c, obj.TileType());
                        }
                    }
                    if (obj is Robot)
                    {
                        Robot seenRobot = (Robot) obj;
                        bool thisRobotSeen = false;
                        foreach (Robot r in seenRobots)
                        {
                            if (r.name == seenRobot.name)
                            {
                                thisRobotSeen = true;
                            }
                        }
                        if (seenRobot.team == this.team && !thisRobotSeen)
                        {
                            seenRobots.Add(seenRobot);
                        }
                    }
                    if (!coordFound)
                    {
                        map.Add(new Mapfield(round, c, obj.TileType()));
                    }
                }
            }
            //updating map with other seen robots map
            foreach (Robot r in seenRobots)
            {
                foreach (Mapfield field in r.map)
                {
                    bool coordFound = false;
                    Coordinate c = field.coords;
                    for (int i = 0; i < map.Count; i++)
                    {
                        if (map[i].coords.X == c.X && map[i].coords.Y == c.Y)
                        {
                            coordFound = true;
                            if (map[i].lastSeen < field.lastSeen)
                            {
                                map[i] = field;
                            }
                        }
                    }
                    if (!coordFound)
                    {
                        map.Add(field);
                    }
                }
            }
        }
    }
}
