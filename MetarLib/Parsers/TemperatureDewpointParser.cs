using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class TemperatureDewpointParser : IFieldParser
    {
        private const int TemperatureNegative = 1;
        private const int Temperature = 2;
        private const int DewpointNegative = 3;
        private const int Dewpoint = 4;
        
        private static readonly Regex TemperatureDewpointRegex = new Regex(@"^(M)?(\d{2})/(M)?(\d{2})$", RegexOptions.Compiled);
        
        public bool Parse(ParserContext context, string field)
        {
            var match = TemperatureDewpointRegex.Match(field);

            if (!match.Success)
                return false;

            var metar = context.Metar;
            
            metar.Temperature = int.Parse(match.Groups[Temperature].Value) * (match.Groups[TemperatureNegative].Success ? -1 : 1);
            metar.Dewpoint = int.Parse(match.Groups[Dewpoint].Value) * (match.Groups[DewpointNegative].Success ? -1 : 1);

            return true;
        }
    }
}