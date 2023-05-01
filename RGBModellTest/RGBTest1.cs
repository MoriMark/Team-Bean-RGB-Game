using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Bson;
using RGBModell.modell.gameobjects;
using RGBModell.modell.testmodell;
using RGBModell.modell.enums;

namespace RGBModellTest
{
    [TestClass]
    public class RGBTest1
    {
        private GameRuleTest ruletest = null!;
        private GameRuleTest ruletest2 = null!;
        [TestInitialize]
        public void Initialize()
        {
            ruletest = new GameRuleTest(1,1);
            ruletest.StartGame();
           
        }

        [TestMethod]
        public void TestTest()
        {
            ruletest2 = new GameRuleTest(2, 1);
            ruletest2.StartGame();
            Assert.IsTrue(ruletest.field.GetValue(6, 6).GetType() == typeof(Robot));
            Assert.IsTrue(ruletest2.field.GetValue(6, 6).GetType() == typeof(Robot));
            Assert.IsTrue(ruletest2.field.GetValue(6, 7).GetType() == typeof(Robot));
        }

        [TestMethod]
        public void MoveTest()
        {
            Assert.IsTrue(ruletest.field.GetValue(6, 6).GetType() == typeof(Robot));
            ruletest.MakeStep(7, 6, (Robot)ruletest.field.GetValue(6, 6));
            Assert.IsTrue(ruletest.field.GetValue(6, 6).IsEmpty());
            Assert.IsTrue(ruletest.field.GetValue(7, 6).GetType() == typeof(Robot));
        }

        [TestMethod]
        public void MoveTest2()
        {
            Assert.IsTrue(ruletest.field.GetValue(6, 6).GetType() == typeof(Robot));
            ruletest.MakeStep(7, 6, (Robot)ruletest.field.GetValue(6, 6));
            Assert.IsTrue(ruletest.field.GetValue(6, 6).IsEmpty());
            Assert.IsTrue(ruletest.field.GetValue(7, 6).GetType() == typeof(Robot));
        }


        [TestMethod]
        public void Rotatetest1()
        {
            ruletest.MakeTurn(Direction.Right,(Robot)ruletest.field.GetValue(6, 6));
            Assert.IsTrue(((Robot)ruletest.field.GetValue(6, 6)).facing == Direction.Right);
        }

        [TestMethod]
        public void ConnectTest()
        {
            ruletest.MakeTurn(Direction.Right, (Robot)ruletest.field.GetValue(6, 6));
            ruletest.field.SetValue(6, 7, new Box(6, 7, BoxColor.Red, TileType.RedBox));
            ruletest.Lift((Robot)ruletest.field.GetValue(6, 6));
            Assert.IsTrue(((Robot)ruletest.field.GetValue(6, 6)).IsAttached());
            Assert.IsTrue(((Robot)ruletest.field.GetValue(6, 6)).GetAttachedGroupId() == 0);
        }

        public void DisConnectTest()
        {
            ruletest.MakeTurn(Direction.Right, (Robot)ruletest.field.GetValue(6, 6));
            ruletest.field.SetValue(6, 7, new Box(6, 7, BoxColor.Red, TileType.RedBox));
            ruletest.Lift((Robot)ruletest.field.GetValue(6, 6));
            Assert.IsTrue(((Robot)ruletest.field.GetValue(6, 6)).IsAttached());
            Assert.IsTrue(((Robot)ruletest.field.GetValue(6, 6)).GetAttachedGroupId() == 0);
            ruletest.Lift((Robot)ruletest.field.GetValue(6, 6));
            Assert.IsFalse(((Robot)ruletest.field.GetValue(6, 6)).IsAttached());
        }

        [TestMethod]
        public void Rotatetest2()
        {
            ruletest.MakeTurn(Direction.Right, (Robot)ruletest.field.GetValue(6, 6));
            ruletest.field.SetValue(6, 7, new Box(6, 7, BoxColor.Red, TileType.RedBox));
            ruletest.Lift((Robot)ruletest.field.GetValue(6, 6));
            ruletest.MakeTurn(Direction.Down, (Robot)ruletest.field.GetValue(6, 6));
            Assert.IsTrue(((Robot)ruletest.field.GetValue(6, 6)).facing == Direction.Down);
            Assert.IsTrue(ruletest.field.GetValue(6, 7).IsEmpty());
            Assert.IsTrue(ruletest.GetFieldValue(7, 6).GetType() == typeof(Box));
        }


        [TestMethod]
        public void WeldTest1()
        {
            ruletest2 = new GameRuleTest(2, 1);
            ruletest2.StartGame();
            ruletest2.MakeTurn(Direction.Right, (Robot)ruletest2.field.GetValue(6, 6));
            ruletest2.MakeTurn(Direction.Down, (Robot)ruletest2.field.GetValue(6, 6));
            ruletest2.MakeTurn(Direction.Right, (Robot)ruletest2.field.GetValue(6, 7));
            ruletest2.MakeTurn(Direction.Down, (Robot)ruletest2.field.GetValue(6, 7));
            ruletest2.field.SetValue(7, 6, new Box(7, 6, BoxColor.Red, TileType.RedBox));
            ruletest2.field.SetValue(7, 7, new Box(7, 7, BoxColor.Red, TileType.RedBox));
            ruletest2.Weld(ruletest2.CurrentRobot());
            ruletest2.NextRobot();
            ruletest2.Weld(ruletest2.CurrentRobot());
            ruletest2.NextRobot();
            Assert.AreEqual(((Box)ruletest2.field.GetValue(7, 7)).ingroup, 1);
        }

        [TestMethod]
        public void UnWeldTest1()
        {
            ruletest2 = new GameRuleTest(2, 1);
            ruletest2.StartGame();
            ruletest2.MakeTurn(Direction.Right, (Robot)ruletest2.field.GetValue(6, 6));
            ruletest2.MakeTurn(Direction.Down, (Robot)ruletest2.field.GetValue(6, 6));
            ruletest2.MakeTurn(Direction.Right, (Robot)ruletest2.field.GetValue(6, 7));
            ruletest2.MakeTurn(Direction.Down, (Robot)ruletest2.field.GetValue(6, 7));
            ruletest2.field.SetValue(7, 6, new Box(7, 6, BoxColor.Red, TileType.RedBox));
            ruletest2.field.SetValue(7, 7, new Box(7, 7, BoxColor.Red, TileType.RedBox));
            ruletest2.Weld(ruletest2.CurrentRobot());
            ruletest2.NextRobot();
            ruletest2.Weld(ruletest2.CurrentRobot());
            ruletest2.NextRobot();
            ruletest2.UnWeld(7, 6, 7, 7, ruletest2.CurrentRobot());
            Assert.AreEqual(((Box)ruletest2.field.GetValue(7, 6)).ingroup, 0);
            Assert.AreEqual(((Box)ruletest2.field.GetValue(7, 7)).ingroup, 0);
        }

        [TestMethod]
        public void WaitTest1()
        {
            ruletest2 = new GameRuleTest(2, 1);
            ruletest2.StartGame(); ruletest2 = new GameRuleTest(2, 1);
            ruletest2.StartGame();
            string id1 = ((Robot)ruletest2.field.GetValue(6, 6)).name;
            string id2 = ((Robot)ruletest2.field.GetValue(6, 7)).name;
            ruletest2.Pass();
            ruletest2.NextRobot();
            Assert.AreEqual(ruletest2.CurrentRobot().name, id2);
        }
    }
}