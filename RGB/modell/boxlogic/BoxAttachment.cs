using RGB.modell.enums;
using RGB.modell.gameobjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGB.modell.boxlogic
{
    public class BoxAttachment
    {
        private Box[] twoboxes;
        public Direction direction { get; }

        public BoxAttachment(Box boxa , Box boxb)
        {
            twoboxes = new Box[2];
            twoboxes[0] = boxa;
            twoboxes[1] = boxb;
            direction = DirectionDecide(boxa, boxb);

        }

        private Direction DirectionDecide(Box boxa, Box boxb)
        {
            if(boxa.i != boxb.i)
            {
                if (boxa.i < boxb.i)
                {
                    return Direction.Down;
                }
                else
                {
                    return Direction.Up;
                }
            } else if(boxa.j != boxb.j)
            {
                if (boxa.j < boxb.j)
                {
                    return Direction.Right;
                }
                else
                {
                    return Direction.Left;
                }
            }
            throw new InvalidEnumArgumentException("The Boxes are not next to each other!");
        }

        public Box GetFirstBox()
        {
            return twoboxes[0];
        }

        public Box GetSecondBox()
        {
            return twoboxes[1];
        }

        public AttachmentLocation ContainsBox(Box box)
        {
            if (twoboxes[0].id == box.id)
            {
                return AttachmentLocation.First;
            } else if (twoboxes[1].id == box.id)
            {
                return AttachmentLocation.Second;
            }
            else
            {
                return AttachmentLocation.No;
            }
        }
    }
}
