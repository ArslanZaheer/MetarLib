using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test
{
    [TestClass]
    public class TimeOfObservationTest : MetarParserTestBase
    {
        [TestMethod]
        public void Metar_has_correct_time_of_observation()
        {
            var metar = GetMetar("170845Z");
            
            Assert.AreEqual(17, metar.TimeOfObservation.Day);
            Assert.AreEqual(8, metar.TimeOfObservation.Hour);
            Assert.AreEqual(45, metar.TimeOfObservation.Minute);
        }
    }
}