using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class WindVariationParser : IFieldParser
    {
        private const int From = 1;
        private const int To = 2;
        
        private static readonly Regex WindVariationRegex = new Regex(@"^(\d{3})V(\d{3})$", RegexOptions.Compiled);
        
        public bool Parse(ParserContext context, string field)
        {
            var match = WindVariationRegex.Match(field);

            if (!match.Success)
                return false;

            var metar = context.Metar;
            
            metar.WindVaryingFrom = int.Parse(match.Groups[From].Value);
            metar.WindVaryingTo = int.Parse(match.Groups[To].Value);

            return true;
        }
    }
}