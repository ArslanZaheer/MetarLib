using MetarLib.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.FieldParsers
{
    [TestClass]
    public class WindVariationTest : FieldParserTestBase
    {
        public WindVariationTest() : base(new WindVariationParser()) {}

        [TestMethod]
        public void Metar_with_wind_variation()
        {
            var metar = GetMetar("270V320");
            
            Assert.AreEqual(270, metar.WindVaryingFrom);
            Assert.AreEqual(320, metar.WindVaryingTo);
        }
    }
}