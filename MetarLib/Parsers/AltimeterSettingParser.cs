using System.Text.RegularExpressions;
using MetarLib.Contracts;
using MetarLib.Enums;

namespace MetarLib.Parsers
{
    public class AltimeterSettingParser : IFieldParser
    {
        private static readonly Regex AltimeterSettingRegex = new Regex(@" (A|Q)(\d{4})", RegexOptions.Compiled);

        public void Parse(string metarText, Metar metar)
        {
            var match = AltimeterSettingRegex.Match(metarText);

            if (!match.Success)
                return;

            var altimeterSetting = decimal.Parse(match.Groups[2].Value);

            if (match.Groups[1].Value == "A")
            {
                metar.AltimeterSetting = altimeterSetting / 100m;
                metar.AltimeterSettingUnit = UnitOfPressure.InchesOfMercury;
            }
            else if (match.Groups[1].Value == "Q")
            {
                metar.AltimeterSetting = altimeterSetting;
                metar.AltimeterSettingUnit = UnitOfPressure.Hectopascals;
            }
        }
    }
}