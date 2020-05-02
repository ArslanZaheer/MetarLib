using MetarLib.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.FieldParsers
{
    [TestClass]
    public class ProbabilityTest
    {
        private readonly ProbabilityParser _parser = new ProbabilityParser();

        [TestMethod]
        public void Metar_has_probability()
        {
            var context = new ParserContext();
            _parser.Parse(context, "PROB40");

            Assert.AreEqual(0.4m, context.Probability);
        }
    }
}