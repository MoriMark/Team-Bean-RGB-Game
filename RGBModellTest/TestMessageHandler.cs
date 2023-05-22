using RGBModell.modell;
using RGBModell.modell.enums;
using RGBModell.modell.gameobjects;

namespace RGBModellTest
{
    [TestClass]
    public class MessageHandlerTest
    {
        [TestMethod]
        public void AddMessageCheckExistance()
        {
            MessageHandler handler = new MessageHandler();
            Robot robot = new Robot(0, 0, Direction.Up, Team.Green, TileType.GreenRobot, "");

            handler.CreateMessage(robot, Symbol.Smile);

            Assert.AreEqual(1, handler.GetTeamMessages(Team.Green).Count);
        }

        [TestMethod]
        public void AddNMessageCheckExistance()
        {
            Random rnd = new Random();
            MessageHandler handler = new MessageHandler();
            Robot robot = new Robot(0, 0, Direction.Up, Team.Green, TileType.GreenRobot, "");

            int n = rnd.Next(1, 25);
            for (int i = 0; i < n; i++)
                handler.CreateMessage(robot, Symbol.Smile);

            Assert.AreEqual(n, handler.GetTeamMessages(Team.Green).Count);
        }

        [TestMethod]
        public void AddMessageCheckTeamMessage()
        {
            MessageHandler handler = new MessageHandler();
            Robot robot = new Robot(0, 0, Direction.Up, Team.Green, TileType.GreenRobot, "");

            handler.CreateMessage(robot, Symbol.Smile);

            Assert.AreEqual(Symbol.Smile, handler.GetTeamMessages(Team.Green)[0].symbol);
        }

        [TestMethod]
        public void AddMessageCheckTeamMessageInGoodSequence()
        {
            MessageHandler handler = new MessageHandler();
            Robot robot = new Robot(0, 0, Direction.Up, Team.Green, TileType.GreenRobot, "");

            handler.CreateMessage(robot, Symbol.Smile);

            Assert.AreEqual(Symbol.Smile, handler.GetTeamMessages(Team.Green)[0].symbol);
        }

        [TestMethod]
        public void AddMessageCheckRobotMessage()
        {
            MessageHandler handler = new MessageHandler();
            Robot robot = new Robot(0, 0, Direction.Up, Team.Green, TileType.GreenRobot, "");

            handler.CreateMessage(robot, Symbol.Smile);
            handler.CreateMessage(robot, Symbol.Sad);

            Assert.AreEqual(Symbol.Smile, handler.GetMessagesOfRobot(robot)[0].symbol);
            Assert.AreEqual(Symbol.Sad, handler.GetMessagesOfRobot(robot)[1].symbol);
        }

        [TestMethod]
        public void AddMessageCheckRobotMessageSequence()
        {
            MessageHandler handler = new MessageHandler();
            Robot robot = new Robot(0, 0, Direction.Up, Team.Green, TileType.GreenRobot, "");

            handler.CreateMessage(robot, Symbol.Smile);
            handler.CreateMessage(robot, Symbol.Sad);

            Assert.AreEqual(Symbol.Smile, handler.GetMessagesOfRobot(robot)[0].symbol);
            Assert.AreEqual(Symbol.Sad, handler.GetMessagesOfRobot(robot)[1].symbol);
        }

        [TestMethod]
        public void DeleteMessageOfRobot()
        {
            MessageHandler handler = new MessageHandler();
            Robot robot = new Robot(0, 0, Direction.Up, Team.Green, TileType.GreenRobot, "");

            handler.CreateMessage(robot, Symbol.Smile);
            handler.DeleteMessagesOfRobot(robot);

            Assert.AreEqual(0, handler.GetMessagesOfRobot(robot).Count);
        }

        [TestMethod]
        public void DeleteNMessageOfRobot()
        {
            Random rnd = new Random();
            MessageHandler handler = new MessageHandler();
            Robot robot = new Robot(0, 0, Direction.Up, Team.Green, TileType.GreenRobot, "");

            int n = rnd.Next(1, 25);
            for (int i = 0; i < n; i++)
                handler.CreateMessage(robot, Symbol.Smile);

            int m = rnd.Next(0, n);

            int k = n - m;

            handler.DeleteMessagesOfRobot(robot, m);

            Assert.AreEqual(k, handler.GetMessagesOfRobot(robot).Count);
        }
    }
}
