using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib
{
    public class MetarParser : IMetarParser
    {
        private static readonly Regex MetarRegex = new Regex(@"METAR (.+?)(?:\sBECMG (.+?))?=", RegexOptions.Compiled | RegexOptions.Singleline);
        
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
            var metar = new Metar();

            var fields = match.Groups[1].Value.Split(' ');

            foreach (var field in fields)
            {
                foreach (var parser in _fieldParsers)
                {
                    if (parser.Parse(field, metar))
                        break;
                }
            }
            
            return metar;
        }
    }
}