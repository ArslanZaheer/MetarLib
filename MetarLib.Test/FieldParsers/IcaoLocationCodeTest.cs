using MetarLib.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.FieldParsers
{
    [TestClass]
    public class IcaoLocationCodeTest : IFieldParserTestBase
    {
        public IcaoLocationCodeTest() : base(new IcaoLocationCodeParser()) {}

        [TestMethod]
        public void Metar_has_correct_ICAO_location_code()
        {
            const string locationCode = "EHAM";
            
            var metar = GetMetar(locationCode);
            
            Assert.AreEqual(locationCode, metar.IcaoLocationCode);
        }

        [TestMethod]
        public void Metar_string_representation_equals_location_code()
        {
            const string locationCode = "EHLE";
            
            var metar = GetMetar(locationCode);
            
            Assert.AreEqual(locationCode, metar.ToString());
        }
    }
}