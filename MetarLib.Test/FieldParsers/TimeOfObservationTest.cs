using System;
using MetarLib.Contracts;
using MetarLib.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.FieldParsers
{
    [TestClass]
    public class TimeOfObservationTest : IFieldParserTestBase
    {
        private class FakeDateTimeProvider : IDateTimeProvider
        {
            public DateTimeOffset Now { get; set; }

            public FakeDateTimeProvider()
            {
                Now = DateTimeOffset.Now;
            }
        }

        [TestMethod]
        public void Metar_has_correct_time_of_observation()
        {
            var (metarText, metar) = GetMetarWithText("170845Z");
            var parser = new TimeOfObservationParser(new FakeDateTimeProvider());
            
            parser.Parse(metarText, metar);
            
            Assert.AreEqual(17, metar.TimeOfObservation.Day);
            Assert.AreEqual(8, metar.TimeOfObservation.Hour);
            Assert.AreEqual(45, metar.TimeOfObservation.Minute);
        }
        
        [TestMethod]
        public void Metar_day_is_greater_than_current_day()
        {
            var dateTimeProvider = new FakeDateTimeProvider();
            var parser = new TimeOfObservationParser(dateTimeProvider);
            
            var (metarText, metar) = GetMetarWithText("020000Z");
            var currentMonth = 4;
            dateTimeProvider.Now = new DateTimeOffset(2020, currentMonth, 1, 0, 0, 0, TimeSpan.Zero);
            
            parser.Parse(metarText, metar);
            
            Assert.AreEqual(currentMonth - 1, metar.TimeOfObservation.Month);
            Assert.AreEqual(2, metar.TimeOfObservation.Day);
            Assert.AreEqual(0, metar.TimeOfObservation.Hour);
            Assert.AreEqual(0, metar.TimeOfObservation.Minute);
        }
    }
}