using RGBModell.modell.enums;
using RGBModell.modell.gameobjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBModell.modell.boxlogic
{
    /// <summary>
    /// Class <c>BoxGroup</c> stores and manages a group of boxes and their attachments.
    /// </summary>
    public class BoxGroup
    {
        public int groupid { get; }
        private static int staticgroupid = 0;
        public List<Box> boxes { get; }
        private List<BoxAttachment> boxattachments;

        /// <summary>
        /// This constructor takes two boxes and creates a group with them.
        /// </summary>
        public BoxGroup(Box boxa, Box boxb)
        {
            boxes = new List<Box>();
            boxattachments = new List<BoxAttachment>();
            staticgroupid++;
            groupid = staticgroupid;
            boxes.Add(boxa);
            boxa.ingroup = groupid;
            boxes.Add(boxb);
            boxb.ingroup = groupid;
            boxattachments.Add(new BoxAttachment(boxa, boxb));
        }

        /// <summary>
        /// This constructor takes a list of boxes and attachments and creates a group with them.
        /// </summary>
        public BoxGroup(List<Box> boxes2, List<BoxAttachment> boxattachments2)
        {
            boxes = new List<Box>();
            boxattachments = new List<BoxAttachment>();
            staticgroupid++;
            groupid = staticgroupid;
            boxes = boxes2;
            boxattachments = boxattachments2;
            for(int i=0; i<boxes.Count; i++)
            {
                boxes[i].ingroup = groupid;
            }
        }

        /// <summary>
        /// This constructor takes two groups and combines them based on the originbox and attachbox.
        /// </summary>
        public BoxGroup(Box originbox, Box attachbox, BoxGroup group1, BoxGroup group2)
        {
            boxes = new List<Box>();
            boxattachments = new List<BoxAttachment>();
            staticgroupid++;
            groupid = staticgroupid;
            boxes = group1.boxes;
            for(int i=0; i< boxes.Count; i++)
            {
                boxes[i].ingroup = groupid;
            }
            boxattachments = group1.boxattachments;
            boxattachments.Add(new BoxAttachment(originbox, attachbox));
            for(int i=0;i < group2.boxes.Count; i++)
            {
                boxes.Add(group2.boxes[i]);
                group2.boxes[i].ingroup = groupid;
            }
            for(int i=0;i<group2.boxattachments.Count; i++)
            {
                boxattachments.Add(group2.boxattachments[i]);
            }
        }


        /// <summary>
        /// This method adds the attachbox to the group by creating an attachment with the originbox.
        /// </summary>
        public void AddBox(Box originbox, Box attachbox) 
        {
            if(attachbox.ingroup != groupid)
            {
                attachbox.ingroup = groupid;
                boxes.Add(attachbox);
                
            }
            bool duplicate = false;
            for(int i= 0; i < boxattachments.Count; i++)
            {
                if (boxattachments[i].ContainsBox(originbox) == AttachmentLocation.Second && boxattachments[i].ContainsBox(attachbox) == AttachmentLocation.First)
                {
                    duplicate = true;
                }
                if (boxattachments[i].ContainsBox(attachbox) == AttachmentLocation.Second && boxattachments[i].ContainsBox(originbox) == AttachmentLocation.First)
                {
                    duplicate = true;
                }
            }
            if(!duplicate)
            {
                boxattachments.Add(new BoxAttachment(originbox, attachbox));
            }
            
        }

        /// <summary>
        /// This method deletes an attachment between two boxes.
        /// </summary>
        public void DetachAttachment(Box boxa, Box boxb)
        {
            bool found = false;
            int i = 0;
            while(!found && i < boxattachments.Count)
            {
                if(boxattachments[i].ContainsBox(boxa) != AttachmentLocation.No && boxattachments[i].ContainsBox(boxb) != AttachmentLocation.No)
                {
                    found= true;
                    boxattachments.RemoveAt(i);
                }
                else
                {
                    i++;
                }
                
            }
            bool[] singlebox = new bool[2];
            singlebox[0] = true;
            singlebox[1] = true;
            for(int j=0;j<boxattachments.Count;j++)
            {
                if (boxattachments[j].ContainsBox(boxa) != AttachmentLocation.No)
                {
                    singlebox[0] = false;
                }
                if(boxattachments[j].ContainsBox(boxb) != AttachmentLocation.No)
                {
                    singlebox[1] = false;
                }
            }
            if (singlebox[0])
            {
                boxa.ingroup = 0;
                boxes.Remove(boxa);
            }
            if (singlebox[1])
            {
                boxb.ingroup= 0;
                boxes.Remove(boxb);
            }
        }

        /// <summary>
        /// This method checks wether a group is viable.
        /// </summary>
        /// <returns>Returns a zero if the group is viable, returns its id if it is not viable.</returns>
        public int CheckGroup()
        {
            if(boxes.Count <= 1 || boxattachments.Count < 1)
            {
                for(int i=0;i<boxes.Count;i++)
                {
                    boxes[i].ingroup = 0;
                }
                boxes.Clear();
                boxattachments.Clear();
                return groupid;
            }
            return 0;
        }

        /// <summary>
        /// This method checks wether a group split into two and returns the not attached group.
        /// </summary>
        /// <returns>Returns a null if the group is whole, returns the plit part.</returns>
        public BoxGroup? SplitGroup()
        {
            List<int> boxids = new List<int>();
            List<Box> currentboxes= new List<Box>();
            List<Box> nextboxes = new List<Box>();
            List<BoxAttachment> attachmentscycled = new List<BoxAttachment>();
            currentboxes.Add(boxes[0]);
            for(int i=0;i<currentboxes.Count;i++)
            {
                for(int j=0;j<boxattachments.Count;j++)
                {
                    if (boxattachments[j].ContainsBox(currentboxes[i]) == AttachmentLocation.First)
                    {
                        if (!boxids.Contains(boxattachments[j].GetSecondBox().id))
                        {
                            nextboxes.Add(boxattachments[j].GetSecondBox());
                        }
                        if (!attachmentscycled.Contains(boxattachments[j]))
                        {
                            attachmentscycled.Add(boxattachments[j]);
                        }
                    }
                }
                if(i== currentboxes.Count-1)
                {
                    if (nextboxes.Count > 0)
                    {
                        i -= currentboxes.Count;
                    }
                    for (int j = 0; j < currentboxes.Count; j++)
                        {
                            if (!boxids.Contains(currentboxes[j].id))
                            {
                                boxids.Add(currentboxes[j].id);
                            }

                        }
                        currentboxes.Clear();
                    for(int j = 0; j < nextboxes.Count; j++)
                    {
                        currentboxes.Add(nextboxes[j]);
                    }
                    nextboxes.Clear();                                        
                }
            }

            if(boxids.Count < boxes.Count)
            {
                var newgroupattachments = boxattachments.Except(attachmentscycled);
                List<BoxAttachment> newgroupattachmentslist = newgroupattachments.ToList<BoxAttachment>();
                for(int i=0; i<boxattachments.Count; i++)
                {
                    if (!attachmentscycled.Contains(boxattachments[i]))
                    {
                        
                        boxattachments.Remove(boxattachments[i]);
                        i--;
                    }
                }
                List<Box> newboxes = new List<Box>();
                for (int i = 0; i < boxes.Count; i++)
                {
                    if (!boxids.Contains(boxes[i].id))
                    {
                        newboxes.Add(boxes[i]);
                        boxes.Remove(boxes[i]);
                        i--;
                    }
                }
                BoxGroup newgroup = new BoxGroup(newboxes, newgroupattachmentslist);
                return newgroup;
            }
            else
            {
                return null;
            }

        }

        /// <summary>
        /// This method converts the group into a Boxcolor Matrix.
        /// </summary>
        /// <returns>Returns the groups Boxcolor matrix.</returns>
        public BoxColor[,] ConvertToMatrix()
        {
            int mini = boxes[0].i;
            int minj = boxes[0].j;
            int maxi = boxes[0].i;
            int maxj = boxes[0].j;
            for(int i = 1; i < boxes.Count; i++)
            {
                if(mini > boxes[i].i)
                {
                    mini = boxes[i].i;
                }
                if(minj > boxes[i].j)
                {
                    minj = boxes[i].j;
                }
                if(maxi < boxes[i].i)
                {
                    maxi = boxes[i].i;
                }
                if(maxj < boxes[i].j)
                {
                    maxj = boxes[i].j;
                }
            }

            BoxColor[,] colormatrix = new BoxColor[(maxi - mini) + 1, (maxj - minj) + 1];
            for(int i = 0; i < maxi - mini; i++)
            {
                for(int j=0;j < (maxj - minj); j++)
                {
                    colormatrix[i,j] = BoxColor.NoColor;
                }
            }

            for(int i = 0; i < boxes.Count; i++)
            {
                colormatrix[boxes[i].i - mini, boxes[i].j - minj] = boxes[i].color;
            }

            return colormatrix;
        }
    }
}
