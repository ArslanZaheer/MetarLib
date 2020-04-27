using MetarLib.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test
{
    [TestClass]
    public class VisibilityTest : MetarParserTestBase
    {
        [TestMethod]
        public void Metar_with_visibility()
        {
            var metar = GetMetar("7000");

            Assert.AreEqual(7000, metar.Visibility);
            Assert.AreEqual(UnitOfLength.Meters, metar.VisibilityUnit);
        }
        
        [TestMethod]
        public void Metar_with_CAVOK()
        {
            var metar = GetMetar("CAVOK");

            Assert.AreEqual(9999, metar.Visibility);
            Assert.AreEqual(UnitOfLength.Meters, metar.VisibilityUnit);
        }
        
        [TestMethod]
        public void Metar_with_low_visibility()
        {
            var metar = GetMetar("0500");

            Assert.AreEqual(500, metar.Visibility);
            Assert.AreEqual(UnitOfLength.Meters, metar.VisibilityUnit);
        }
        
        [TestMethod]
        public void Metar_with_statute_miles()
        {
            var metar = GetMetar("8SM");

            Assert.AreEqual(8, metar.Visibility);
            Assert.AreEqual(UnitOfLength.StatuteMiles, metar.VisibilityUnit);
            Assert.IsFalse(metar.VisibilityLessThan);
        }
        
        [TestMethod]
        public void Metar_with_fractional_statute_miles()
        {
            var metar = GetMetar("1/8SM");

            Assert.AreEqual(0.125m, metar.Visibility);
            Assert.AreEqual(UnitOfLength.StatuteMiles, metar.VisibilityUnit);
            Assert.IsFalse(metar.VisibilityLessThan);
        }
        
        [TestMethod]
        public void Metar_with_less_than_statute_miles()
        {
            var metar = GetMetar("M1/4SM");

            Assert.AreEqual(0.25m, metar.Visibility);
            Assert.AreEqual(UnitOfLength.StatuteMiles, metar.VisibilityUnit);
            Assert.IsTrue(metar.VisibilityLessThan);
        }
    }
}