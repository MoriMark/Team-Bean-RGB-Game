using RGBModell.modell.enums;
using RGBModell.modell.structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.gameobjects
{
    public class Robot : GameObject
    {
        public Direction facing { get; set; }
        public Box? Attached { get; set; }
        public Team team { get; set; }
        public string name { get; set; }
        public List<Mapfield> map;

        public Robot(int i, int j, Direction facing, Team team, TileType type, string name) : base(i, j, type)
        { 
            this.facing = facing;
            Attached = null;
            this.team = team;
            this.name = name;
            map = new List<Mapfield>();
        }

        public bool IsAttached()
        {
            return Attached != null;
        }

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
            foreach (GameObject obj in vision) 
            {
                bool coordFound = false;
                Coordinate c = new Coordinate(obj.i, obj.j);
                for (int i = 0; i < map.Count; i++)
                {
                    if (map[i].coords.X == obj.i && map[i].coords.Y == obj.j)
                    {
                        coordFound = true;
                        map[i] = new Mapfield(round,c,obj.TileType());
                    }
                }
                if (!coordFound)
                {
                    map.Add(new Mapfield(round, c, obj.TileType()));
                }
            }
        }
    }
}
