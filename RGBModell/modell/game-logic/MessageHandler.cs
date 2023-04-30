using RGBModell.modell.enums;
using RGBModell.modell.gameobjects;
using RGBModell.modell.structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Message = RGBModell.modell.structs.Message;

namespace RGBModell.modell
{
    public class MessageHandler
    {
        private List<Message> messages;
        
        public MessageHandler()
        {
            messages = new List<Message>();
        }

        public List<Message> GetTeamMessages(Team team)
        {
            return messages.Where(m => m.robot.team == team).ToList();
        }

        public List<Message> GetMessagesOfRobot(Robot robot)
        {
            return messages.Where(m => m.robot == robot).ToList();
        }

        public void CreateMessage(Robot robot, Symbol symbol)
        {
            messages.Add(new Message(robot, symbol));
        }

        public void DeleteMessagesOfRobot(Robot robot)
        {
            messages.RemoveAll(m => m.robot == robot);
        }

        /// <summary>
        /// Deletes the n oldest messages of a robot.
        /// </summary>
        /// <param name="robot">The robot whose messages will be deleted.</param>
        /// <param name="n">Maximum number of messages to be deleted.</param>
        public void DeleteMessagesOfRobot(Robot robot, Int32 n)
        {
            messages.Reverse();
            messages.RemoveAll(m => m.robot == robot && --n >= 0);
            messages.Reverse();
        }
    }
}
