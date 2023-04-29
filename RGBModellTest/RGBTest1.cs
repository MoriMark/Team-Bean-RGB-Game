using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Bson;
using RGB.modell;

namespace RGBModellTest
{
    [TestClass]
    public class RGBTest1
    {
        private GameHandler _handler = null!;
        [TestInitialize]
        public void Initialize()
        {
            _handler = new GameHandler(2, 2);
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1,1);
        }

        
    }
}