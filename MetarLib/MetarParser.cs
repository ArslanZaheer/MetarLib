using System.Collections.Generic;
using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib
{
    public class MetarParser : IMetarParser
    {
        private const string Becoming = "BECMG";
        private const string Temporary = "TEMPO";

        private const int Metar = 1;
        
        private static readonly Regex MetarRegex = new Regex(@"METAR (.+?)=", RegexOptions.Compiled | RegexOptions.Singleline);
        
        private readonly IEnumerable<IFieldParser> _fieldParsers;

        public MetarParser(IEnumerable<IFieldParser> fieldParsers)
        {
            _fieldParsers = fieldParsers;
        }

        public IEnumerable<Metar> Parse(string text)
        {
            var metarMatches = MetarRegex.Matches(text);
            
            var metars = new List<Metar>();

            foreach (Match match in metarMatches)
                metars.Add(ParseMetar(match));
            
            return metars.ToArray();
        }

        private Metar ParseMetar(Match match)
        {
            var context = new ParserContext();
            var fields = match.Groups[Metar].Value.Split(' ');

            foreach (var field in fields)
                ParseField(context, field);

            return context.GetResult();
        }

        private void ParseField(ParserContext context, string field)
        {
            if (field == Becoming)
            {
                context.ParseBecoming();
                return;
            }

            if (field == Temporary)
            {
                context.ParseTemporary();
                return;
            }

            foreach (var parser in _fieldParsers)
            {
                if (parser.Parse(context, field))
                    break;
            }
        }
    }
}