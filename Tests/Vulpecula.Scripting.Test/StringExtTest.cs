using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vulpecula.Scripting.Test
{
    [TestClass]
    public class StringExtTest
    {
        [TestMethod]
        public void TestCase01()
        {
            var i = "path_to_floor";
            var o = "PathToFloor";

            Assert.AreEqual(o, i.ToUpperCamelcase());
        }

        [TestMethod]
        public void TestCase02()
        {
            var i = "path_To_Floor";
            var o = "PathToFloor";

            Assert.AreEqual(o, i.ToUpperCamelcase());
        }
    }
}