using System;
using System.Linq;
using System.Text.RegularExpressions;
using MetarLib.Contracts;
using MetarLib.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.FieldParsers
{
    [TestClass]
    public class ValidityTest
    {
        private readonly ValidityParser _parser = new ValidityParser();

        [TestMethod]
        public void Metar_has_validity()
        {
            var context = new ParserContext();
            context.ParseTemporary();
            
            var metar = context.GetResult();
            metar.TimeOfObservation = new DateTimeOffset(2020, 1, 1, 0, 0, 0, TimeSpan.Zero);
            
            _parser.Parse(context, "0101/0104");

            var temporaryMetar = metar.Temporary.First();
            
            Assert.AreEqual(temporaryMetar.ValidFrom.Day, 1);
            Assert.AreEqual(temporaryMetar.ValidFrom.Hour, 1);
            Assert.AreEqual(temporaryMetar.ValidTo.Day, 1);
            Assert.AreEqual(temporaryMetar.ValidTo.Hour, 4);
        }
        
        [TestMethod]
        public void Metar_has_validity_next_month()
        {
            var context = new ParserContext();
            context.ParseTemporary();
            
            var metar = context.GetResult();
            metar.TimeOfObservation = new DateTimeOffset(2020, 1, 31, 0, 0, 0, TimeSpan.Zero);
            
            _parser.Parse(context, "0101/0104");

            var temporaryMetar = metar.Temporary.First();
            
            Assert.AreEqual(temporaryMetar.ValidFrom.Month, 2);
            Assert.AreEqual(temporaryMetar.ValidFrom.Day, 1);
            Assert.AreEqual(temporaryMetar.ValidFrom.Hour, 1);
            Assert.AreEqual(temporaryMetar.ValidTo.Month, 2);
            Assert.AreEqual(temporaryMetar.ValidTo.Day, 1);
            Assert.AreEqual(temporaryMetar.ValidTo.Hour, 4);
        }
    }
}