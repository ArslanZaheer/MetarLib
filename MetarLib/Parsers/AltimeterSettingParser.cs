using System.Text.RegularExpressions;
using MetarLib.Contracts;
using MetarLib.Enums;

namespace MetarLib.Parsers
{
    public class AltimeterSettingParser : IFieldParser
    {
        private const string InchesOfMercury = "A";
        private const string Hectopascals = "Q";
        
        private const int Unit = 1;
        private const int Pressure = 2;

        private static readonly Regex AltimeterSettingRegex = new Regex($@"^({InchesOfMercury}|{Hectopascals})(\d{{4}})$", RegexOptions.Compiled);

        public bool Parse(ParserContext context, string field)
        {
            var match = AltimeterSettingRegex.Match(field);

            if (!match.Success)
                return false;

            var metar = context.Metar;
            var unit = match.Groups[Unit].Value;
            var altimeterSetting = decimal.Parse(match.Groups[Pressure].Value);

            if (unit == InchesOfMercury)
            {
                metar.AltimeterSetting = altimeterSetting / 100m;
                metar.AltimeterSettingUnit = UnitOfPressure.InchesOfMercury;
            }
            else if (unit == Hectopascals)
            {
                metar.AltimeterSetting = altimeterSetting;
                metar.AltimeterSettingUnit = UnitOfPressure.Hectopascals;
            }

            return true;
        }
    }
}