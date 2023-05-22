using RGBModell.modell.enums;
using RGBModell.modell.gameobjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.boxlogic
{
    /// <summary>
    /// Class <c>BoxAttachment</c> represents a connection between two boxes.
    /// </summary>
    public class BoxAttachment
    {
        private Box[] twoboxes;
        public Direction direction { get; }

        /// <summary>
        /// Constructor connects two Boxes.
        /// </summary>
        public BoxAttachment(Box boxa , Box boxb)
        {
            twoboxes = new Box[2];
            twoboxes[0] = boxa;
            twoboxes[1] = boxb;
            direction = DirectionDecide(boxa, boxb);

        }

        /// <summary>
        /// This method decides which way the second Box is compared to the first box.
        /// </summary>
        /// <returns>Returns which way the second Box is compared to the first box.</returns>
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
            throw new InvalidEnumArgumentException("The Boxes are the same!");
        }

        /// <summary>
        /// This method gets the first box in the attachment.
        /// </summary>
        /// <returns>Returns the first box in the attachment</returns>
        public Box GetFirstBox()
        {
            return twoboxes[0];
        }

        /// <summary>
        /// This method gets the second box in the attachment.
        /// </summary>
        /// <returns>Returns the second box in the attachment</returns>
        public Box GetSecondBox()
        {
            return twoboxes[1];
        }

        /// <summary>
        /// This method decides wether the specified box is in the attachment and where.
        /// </summary>
        /// <returns>Returns Attachmentlocation enum which can be the firs, second or its not in the attachment</returns>
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
