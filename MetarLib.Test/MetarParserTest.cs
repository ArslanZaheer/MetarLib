using System.Linq;
using MetarLib.Contracts;
using MetarLib.Enums;
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
        public void Full_metar_message_1()
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
            Assert.AreEqual(CloudConvectivity.None, metar.Clouds.First().Convectivity);
            
            Assert.AreEqual(18, metar.Temperature);
            Assert.AreEqual(4, metar.Dewpoint);
            
            Assert.AreEqual(1008, metar.AltimeterSetting);
            Assert.AreEqual(UnitOfPressure.Hectopascals, metar.AltimeterSettingUnit);
        }
    }
}