using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class VerticalVisibilityParser : IFieldParser
    {
        private const int VerticalVisibility = 1;

        private static readonly Regex VerticalVisibilityRegex = new Regex(@"^VV(\d{3})$");
        
        public bool Parse(ParserContext context, string field)
        {
            var match = VerticalVisibilityRegex.Match(field);

            if (!match.Success)
                return false;

            context.Metar.VerticalVisibility = int.Parse(match.Groups[VerticalVisibility].Value) * 100;

            return true;
        }
    }
}