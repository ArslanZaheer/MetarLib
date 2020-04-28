using MetarLib.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.FieldParsers
{
    [TestClass]
    public class AutoTest : IFieldParserTestBase
    {
        public AutoTest() : base(new AutoParser()) {}

        [TestMethod]
        public void Metar_is_automatic_observation()
        {
            var metar = GetMetar("AUTO");
            
            Assert.AreEqual(true, metar.IsAutomaticObservation);
        }
    }
}