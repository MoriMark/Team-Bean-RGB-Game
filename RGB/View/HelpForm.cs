using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGB
{
    /// <summary>
    /// Help screen
    /// </summary>
    /// <returns></returns>
    public partial class HelpForm : Form
    {
        private const Int32 MAX_PAGE = 17;
        private Int32 currentPage = 0;
        private string[] texts;
        private string[] titles;
        public HelpForm()
        {
            InitializeComponent();
            texts = new string[] {
                "Each robot has its player, team, ID and direction. The team is based on the color of the robot. The ID of the robot is the number it was given on the team. The direction is the facing of the robot. The players are decided beforehand. Players control these robots to complete tasks."
                , "Tasks must be assembled and pushed through the exits in order to earn points. Each has a limited time until it expires. If completed it rewards one point."
                , "After every move there will be a popup message with the next player's team and ID"
                , "1 - Round and Move counter | 2 - last turn success indicator | 3 - Mapmodes (top - normal, bottom - groups) | 4 - Symbol selection buttons | 5 - Send button, sends currently selected symbol to Message board | 6 - View map | 7 - Currently available tasks | 8 - Remaining time | 9 - Exit | 10 - Your robot | 11 - Box | 12 - Message board | 13 - Team scores | 14 - Actions"
                , "The player moves the robot in a chosen direction. The robot also moves the boxes its connected to. This action fails if the robot or the boxes it is carrying don’t have space in the chosen direction to move."
                , "The player rotates the robot in a chosen direction. The robot also rotates any boxes its connected to. This action fails if the the boxes it is carrying don’t have space in the chosen direction to rotate to."
                , "The player orders the robot to weld in front of it. This action fails if there are no boxes in front of it. The welding is only completed if two boxes have been welded by the same team in the same turn."
                , "You will need a teammate for this action. Make sure you both are facing the box you want to weld(1). When successful the 2 boxes will be in the same group (2) and move together (3)\n\n(Note: Neither player needs to be connected to the box they are welding)"
                , "The player chooses two locations next to their robot. If this contains a welded group of boxes it severs the connection between them. This action fails if the selected fields are not seperable."
                , "When clicking the unweld action button you will have to select 2 boxes (1), once the 2 boxes have been selected the prompt will change to: Ready to unweld! (2) click Unweld again and in the following round the boxes will be in seperate groups (4) if they were in the same group to begin with."
                , "The player orders the robot to connect itself to the box in front of it. This fails if there are no boxes in front of it."
                , "The player orders the robot to disconnect from the box it is attached to. This fails if it is not connected to anything."
                , "The player orders the robot to clean the area in front of it. This can remove single boxes and debris from the field. This fails if there are no removable objects in front of it."
                , "Boxes and asteroids can be deleted with the clean action. The boxes have a set amount of hit points before they are deleted, asteroids are removed after just one use of the clean action."
                , "The player orders the robot to wait and not do anything. This always succeeds and passes the turn."
                , "Boxes can be pushed outside the edge of the playzone(1) to complete a task (2) you have to deliver a group of boxes that match the one displayed in the task panel. Each task has a time limit, and which exit they have to be delivered to. To deliver a group just step on the exit and it will automatically disappear and your team will get 1 point."
                , "When viewing a map you can see everything your or any teammates you met have seen. You will see the current state of the map around each teammate and the last seen state of each block."
                };
            titles = new string[] {
                  "Robots"
                , "Tasks"
                , "Next Round"
                , "User Interface"
                , "Actions: Move"
                , "Actions: Rotate"
                , "Actions: Weld"
                , "Actions: Weld"
                , "Actions: Unweld"
                , "Actions: Unweld"
                , "Actions: Connect"
                , "Actions: Disconnect"
                , "Actions: Clean"
                , "Actions: Clean"
                , "Actions: Wait"
                , "Finishing a Task"
                , "Map"
            };
            SetHelp();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            currentPage--;
            if (currentPage < 0)
            {
                currentPage = MAX_PAGE - 1;
            }
            SetHelp();
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            currentPage++;
            if (currentPage >= MAX_PAGE)
            {
                currentPage = 0;
            }
            SetHelp();
        }

        private void SetHelp()
        {
            helpTitle.Text = titles[currentPage];
            labelHelp.Text = texts[currentPage];
            pageCounter.Text = $"{currentPage + 1} / {MAX_PAGE}";
            switch (currentPage)
            {
                case 0:
                    pictureHelp.BackgroundImage = Properties.Resources.tut1;
                    break;
                case 1:
                    pictureHelp.BackgroundImage = Properties.Resources.tut1_1;
                    break;
                case 2:
                    pictureHelp.BackgroundImage = Properties.Resources.tut2;
                    break;
                case 3:
                    pictureHelp.BackgroundImage = Properties.Resources.tut3;
                    break;
                case 4:
                    pictureHelp.BackgroundImage = Properties.Resources.tut4;
                    break;
                case 5:
                    pictureHelp.BackgroundImage = Properties.Resources.tut5;
                    break;
                case 6:
                    pictureHelp.BackgroundImage = Properties.Resources.tut6;
                    break;
                case 7:
                    pictureHelp.BackgroundImage = Properties.Resources.tut7;
                    break;
                case 8:
                    pictureHelp.BackgroundImage = Properties.Resources.tut8;
                    break;
                case 9:
                    pictureHelp.BackgroundImage = Properties.Resources.tut9;
                    break;
                case 10:
                    pictureHelp.BackgroundImage = Properties.Resources.tut10;
                    break;
                case 11:
                    pictureHelp.BackgroundImage = Properties.Resources.tut11;
                    break;
                case 12:
                    pictureHelp.BackgroundImage = Properties.Resources.tut12;
                    break;
                case 13:
                    pictureHelp.BackgroundImage = Properties.Resources.tut13;
                    break;
                case 14:
                    pictureHelp.BackgroundImage = Properties.Resources.tut14;
                    break;
                case 15:
                    pictureHelp.BackgroundImage = Properties.Resources.tut15;
                    break;
                case 16:
                    pictureHelp.BackgroundImage = Properties.Resources.tut16;
                    break;
                default:
                    pictureHelp.BackgroundImage = null!;
                    break;
            }
        }
    }
}
