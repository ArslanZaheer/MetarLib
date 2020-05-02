using System.Linq;
using MetarLib.Enums;
using MetarLib.Parsers;
using MetarLib.Test.AssertHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.FieldParsers
{
    [TestClass]
    public class WeatherTest : FieldParserTestBase
    {
        public WeatherTest() : base(new WeatherParser()) {}

        [TestMethod]
        public void Metar_with_weather()
        {
            var metar = GetMetar("RA");
            
            Assert.AreEqual(1, metar.Weather.Count);

            var weather = metar.Weather.Single();
            
            Assert.That.EnumHasFlag(WeatherCodes.Rain, weather);
        }
        
        [TestMethod]
        public void Metar_with_weather_with_modifier()
        {
            var metar = GetMetar("+SN");
            
            Assert.AreEqual(1, metar.Weather.Count);

            var weather = metar.Weather.Single();
            
            Assert.That.EnumHasFlag(WeatherCodes.Heavy, weather);
            Assert.That.EnumHasFlag(WeatherCodes.Snow, weather);
        }
        
        [TestMethod]
        public void Metar_with_multiple_weathers_with_modifier()
        {
            var metar = GetMetar("-TSGR");
            
            Assert.AreEqual(1, metar.Weather.Count);

            var weather = metar.Weather.Single();
            
            Assert.That.EnumHasFlag(WeatherCodes.Light, weather);
            Assert.That.EnumHasFlag(WeatherCodes.Thunderstorm, weather);
            Assert.That.EnumHasFlag(WeatherCodes.Hail, weather);
        }
    }
}