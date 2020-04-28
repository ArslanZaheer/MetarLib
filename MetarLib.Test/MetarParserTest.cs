using System.Linq;
using MetarLib.Contracts;
using MetarLib.Enums;
using MetarLib.Services;
using MetarLib.Test.AssertHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test
{
    [TestClass]
    public class MetarParserTest
    {
        private readonly IMetarParser _metarParser;

        public MetarParserTest()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMetarLib();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            _metarParser = serviceProvider.GetService<IMetarParser>();
        }

        [TestMethod]
        public void Full_METAR_message_1()
        {
            var metarText = "METAR EHBK 271025Z AUTO 23007KT 160V300 9999 NSC 18/04 Q1008 NOSIG=";

            var metars = _metarParser.Parse(metarText).ToArray();
            var metar = metars.Single();
            
            Assert.AreEqual("EHBK", metar.IcaoLocationCode);
            
            Assert.AreEqual(27, metar.TimeOfObservation.Day);
            Assert.AreEqual(10, metar.TimeOfObservation.Hour);
            Assert.AreEqual(25, metar.TimeOfObservation.Minute);
            
            Assert.AreEqual(230, metar.WindDirection);
            Assert.AreEqual(7, metar.WindSpeed);
            Assert.AreEqual(UnitOfSpeed.Knots, metar.WindSpeedUnit);
            
            Assert.AreEqual(160, metar.WindVaryingFrom);
            Assert.AreEqual(300, metar.WindVaryingTo);
            
            Assert.AreEqual(9999, metar.Visibility);
            Assert.AreEqual(UnitOfLength.Meters, metar.VisibilityUnit);
            
            Assert.AreEqual(CloudCoverage.NoSignificantCloud, metar.Clouds.First().Coverage);
            
            Assert.AreEqual(18, metar.Temperature);
            Assert.AreEqual(4, metar.Dewpoint);
            
            Assert.AreEqual(1008, metar.AltimeterSetting);
            Assert.AreEqual(UnitOfPressure.Hectopascals, metar.AltimeterSettingUnit);
        }

        [TestMethod]
        public void Full_METAR_message_2()
        {
            var metarText = "METAR LBBG 041600Z 12012MPS 090V150 1400 R04/P1500N R22/P1500U +SN BKN022 OVC050 M04/M07 Q1020 NOSIG 8849//91=";

            var metars = _metarParser.Parse(metarText).ToArray();
            var metar = metars.Single();
            
            Assert.AreEqual("LBBG", metar.IcaoLocationCode);
            
            Assert.AreEqual(4, metar.TimeOfObservation.Day);
            Assert.AreEqual(16, metar.TimeOfObservation.Hour);
            Assert.AreEqual(0, metar.TimeOfObservation.Minute);
            
            Assert.AreEqual(120, metar.WindDirection);
            Assert.AreEqual(12, metar.WindSpeed);
            Assert.AreEqual(UnitOfSpeed.MetersPerSecond, metar.WindSpeedUnit);
            
            Assert.AreEqual(90, metar.WindVaryingFrom);
            Assert.AreEqual(150, metar.WindVaryingTo);
            
            Assert.AreEqual(1400, metar.Visibility);
            Assert.AreEqual(UnitOfLength.Meters, metar.VisibilityUnit);

            var weather = metar.Weather.Single();
            
            Assert.That.EnumHasFlag(WeatherCodes.Heavy, weather);
            Assert.That.EnumHasFlag(WeatherCodes.Snow, weather);

            var clouds = metar.Clouds.ToArray();
            
            Assert.AreEqual(CloudCoverage.Broken, clouds[0].Coverage);
            Assert.AreEqual(2200, clouds[0].Altitude);
            
            Assert.AreEqual(CloudCoverage.Overcast, clouds[1].Coverage);
            Assert.AreEqual(5000, clouds[1].Altitude);
            
            Assert.AreEqual(-4, metar.Temperature);
            Assert.AreEqual(-7, metar.Dewpoint);
            
            Assert.AreEqual(1020, metar.AltimeterSetting);
            Assert.AreEqual(UnitOfPressure.Hectopascals, metar.AltimeterSettingUnit);
        }
    }
}