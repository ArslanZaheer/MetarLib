using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test
{
    [TestClass]
    public class TemperatureDewpointTest : MetarParserTestBase
    {
        [TestMethod]
        public void Metar_with_temperature_and_dewpoint()
        {
            var metar = GetMetar("19/16");

            Assert.AreEqual(19, metar.Temperature);
            Assert.AreEqual(16, metar.Dewpoint);
        }
        
        [TestMethod]
        public void Metar_with_negative_temperature_and_dewpoint()
        {
            var metar = GetMetar("M06/M01");

            Assert.AreEqual(-6, metar.Temperature);
            Assert.AreEqual(-1, metar.Dewpoint);
        }
    }
}