using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class WindVariationParser : IFieldParser
    {
        private static readonly Regex WindVariationRegex = new Regex(@" (\d{3})V(\d{3})", RegexOptions.Compiled);
        
        public void Parse(string metarText, Metar metar)
        {
            var match = WindVariationRegex.Match(metarText);

            if (!match.Success)
                return;

            metar.WindVaryingFrom = int.Parse(match.Groups[1].Value);
            metar.WindVaryingTo = int.Parse(match.Groups[2].Value);
        }
    }
}