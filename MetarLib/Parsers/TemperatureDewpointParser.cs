using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class TemperatureDewpointParser : IFieldParser
    {
        private static readonly Regex TemperatureDewpointRegex = new Regex(@"(M)?(\d{2})/(M)?(\d{2})", RegexOptions.Compiled);
        
        public bool Parse(string field, Metar metar)
        {
            var match = TemperatureDewpointRegex.Match(field);

            if (!match.Success)
                return false;
            
            metar.Temperature = int.Parse(match.Groups[2].Value) * (match.Groups[1].Success ? -1 : 1);
            metar.Dewpoint = int.Parse(match.Groups[4].Value) * (match.Groups[3].Success ? -1 : 1);

            return true;
        }
    }
}