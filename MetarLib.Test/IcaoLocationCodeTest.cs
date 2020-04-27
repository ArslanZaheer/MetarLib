using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test
{
    [TestClass]
    public class IcaoLocationCodeTest : MetarParserTestBase
    {
        [TestMethod]
        public void Metar_has_correct_ICAO_location_code()
        {
            const string locationCode = "EHAM";
            
            var metar = GetMetar("010000Z", locationCode);
            
            Assert.AreEqual(locationCode, metar.IcaoLocationCode);
        }

        [TestMethod]
        public void Metar_string_representation_equals_location_code()
        {
            const string locationCode = "EHLE";
            
            var metar = GetMetar("010000Z", locationCode);
            
            Assert.AreEqual(locationCode, metar.ToString());
        }
    }
}