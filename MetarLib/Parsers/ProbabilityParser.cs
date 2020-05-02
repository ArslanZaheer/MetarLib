using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class ProbabilityParser : IFieldParser
    {
        private static readonly Regex ProbabilityRegex = new Regex(@"^PROB(?<probability>\d{2})$", RegexOptions.Compiled | RegexOptions.ExplicitCapture);

        public bool Parse(ParserContext context, string field)
        {
            var match = ProbabilityRegex.Match(field);

            if (!match.Success)
                return false;

            context.Probability = decimal.Parse(match.Groups["probability"].Value) / 100m;
            return true;
        }
    }
}