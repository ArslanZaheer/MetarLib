using MetarLib.Contracts;
using MetarLib.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.FieldParsers
{
    [TestClass]
    public class VerticalVisibilityTest : FieldParserTestBase
    {
        public VerticalVisibilityTest() : base(new VerticalVisibilityParser())
        {
        }

        [TestMethod]
        public void Metar_with_vertical_visibility()
        {
            var metar = GetMetar("VV050");
            
            Assert.AreEqual(5000, metar.VerticalVisibility);
        }
    }
}