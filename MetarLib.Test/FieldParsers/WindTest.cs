using MetarLib.Enums;
using MetarLib.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.FieldParsers
{
    [TestClass]
    public class WindTest : FieldParserTestBase
    {
        public WindTest() : base(new WindParser()) {}

        [TestMethod]
        public void Metar_with_wind()
        {
            var metar = GetMetar("09009KT");

            Assert.AreEqual(90, metar.WindDirection);
            Assert.IsFalse(metar.IsWindVariable);
            Assert.AreEqual(9, metar.WindSpeed);
            Assert.IsNull(metar.WindGustingTo);
            Assert.AreEqual(UnitOfSpeed.Knots, metar.WindSpeedUnit);
        }
        
        [TestMethod]
        public void Metar_with_gusts()
        {
            var metar = GetMetar("18005G09KT");

            Assert.AreEqual(180, metar.WindDirection);
            Assert.IsFalse(metar.IsWindVariable);
            Assert.AreEqual(5, metar.WindSpeed);
            Assert.AreEqual(9, metar.WindGustingTo);
            Assert.AreEqual(UnitOfSpeed.Knots, metar.WindSpeedUnit);
        }
        
        [TestMethod]
        public void Metar_with_variable_wind_direction()
        {
            var metar = GetMetar("VRB03KT");

            Assert.IsNull(metar.WindDirection);
            Assert.IsTrue(metar.IsWindVariable);
            Assert.AreEqual(3, metar.WindSpeed);
            Assert.IsNull(metar.WindGustingTo);
            Assert.AreEqual(UnitOfSpeed.Knots, metar.WindSpeedUnit);
        }
        
        [TestMethod]
        public void Metar_with_variable_wind_direction_and_gusts()
        {
            var metar = GetMetar("VRB03G07KT");

            Assert.IsNull(metar.WindDirection);
            Assert.IsTrue(metar.IsWindVariable);
            Assert.AreEqual(3, metar.WindSpeed);
            Assert.AreEqual(7, metar.WindGustingTo);
            Assert.AreEqual(UnitOfSpeed.Knots, metar.WindSpeedUnit);
        }
        
        [TestMethod]
        public void Metar_with_wind_in_meters_per_second()
        {
            var metar = GetMetar("27015G25MPS");

            Assert.AreEqual(270, metar.WindDirection);
            Assert.IsFalse(metar.IsWindVariable);
            Assert.AreEqual(15, metar.WindSpeed);
            Assert.AreEqual(25, metar.WindGustingTo);
            Assert.AreEqual(UnitOfSpeed.MetersPerSecond, metar.WindSpeedUnit);
        }
        
        [TestMethod]
        public void Metar_with_variable_wind_in_kilometers_per_hour()
        {
            var metar = GetMetar("VRB30G34KPH");

            Assert.IsNull(metar.WindDirection);
            Assert.IsTrue(metar.IsWindVariable);
            Assert.AreEqual(30, metar.WindSpeed);
            Assert.AreEqual(34, metar.WindGustingTo);
            Assert.AreEqual(UnitOfSpeed.KilometersPerHour, metar.WindSpeedUnit);
        }
    }
}