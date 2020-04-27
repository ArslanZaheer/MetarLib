using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class WindVariationParser : IFieldParser
    {
        private static readonly Regex WindVariationRegex = new Regex(@"^(\d{3})V(\d{3})$", RegexOptions.Compiled);
        
        public bool Parse(string field, Metar metar)
        {
            var match = WindVariationRegex.Match(field);

            if (!match.Success)
                return false;

            metar.WindVaryingFrom = int.Parse(match.Groups[1].Value);
            metar.WindVaryingTo = int.Parse(match.Groups[2].Value);

            return true;
        }
    }
}