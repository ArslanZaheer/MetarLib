using System.Linq;
using MetarLib.Enums;
using MetarLib.Parsers;
using MetarLib.Test.AssertHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.FieldParsers
{
    [TestClass]
    public class WeatherTest : IFieldParserTestBase
    {
        public WeatherTest() : base(new WeatherParser()) {}

        [TestMethod]
        public void Metar_with_weather()
        {
            var metar = GetMetar("RA");
            
            Assert.AreEqual(1, metar.Weather.Count);

            var weather = metar.Weather.Single();
            
            Assert.That.EnumHasFlag(WeatherFlags.Rain, weather);
        }
        
        [TestMethod]
        public void Metar_with_weather_with_modifier()
        {
            var metar = GetMetar("+SN");
            
            Assert.AreEqual(1, metar.Weather.Count);

            var weather = metar.Weather.Single();
            
            Assert.That.EnumHasFlag(WeatherFlags.Heavy, weather);
            Assert.That.EnumHasFlag(WeatherFlags.Snow, weather);
        }
        
        [TestMethod]
        public void Metar_with_multiple_weathers_with_modifier()
        {
            var metar = GetMetar("-TSGR");
            
            Assert.AreEqual(1, metar.Weather.Count);

            var weather = metar.Weather.Single();
            
            Assert.That.EnumHasFlag(WeatherFlags.Light, weather);
            Assert.That.EnumHasFlag(WeatherFlags.Thunderstorm, weather);
            Assert.That.EnumHasFlag(WeatherFlags.Hail, weather);
        }
    }
}