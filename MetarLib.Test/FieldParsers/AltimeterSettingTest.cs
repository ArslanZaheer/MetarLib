using MetarLib.Enums;
using MetarLib.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetarLib.Test.FieldParsers
{
    [TestClass]
    public class AltimeterSettingTest : IFieldParserTestBase
    {
        public AltimeterSettingTest() : base(new AltimeterSettingParser()) {}

        [TestMethod]
        public void Metar_with_altimeter_setting_in_mmHg()
        {
            var metar = GetMetar("A2992");
            
            Assert.AreEqual(29.92m, metar.AltimeterSetting);
            Assert.AreEqual(UnitOfPressure.InchesOfMercury, metar.AltimeterSettingUnit);
        }
        
        [TestMethod]
        public void Metar_with_altimeter_setting_in_hPa()
        {
            var metar = GetMetar("Q1013");
            
            Assert.AreEqual(1013m, metar.AltimeterSetting);
            Assert.AreEqual(UnitOfPressure.Hectopascals, metar.AltimeterSettingUnit);
        }
    }
}