using System.Collections.Generic;
using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib
{
    public class MetarParser : IMetarParser
    {
        private static readonly Regex MetarRegex = new Regex(@"METAR ([A-Z]{4}) (.+?)=", RegexOptions.Compiled | RegexOptions.Singleline);
        
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
            var metar = new Metar(match.Groups[1].Value);

            foreach (var parser in _fieldParsers)
                parser.Parse(match.Value, metar);
            
            return metar;
        }
    }
}