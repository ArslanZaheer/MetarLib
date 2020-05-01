using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib
{
    public class MetarParser : IMetarParser
    {
        private const string Becoming = "BECMG";
        private const string Temporary = "TEMPO";
        
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
            var metar = new Metar();
            
            var fields = match.Groups[1].Value.Split(' ');
            fields.Aggregate(metar, (current, field) => ParseField(field, current, metar));

            return metar;
        }

        private Metar ParseField(string field, Metar currentMetar, Metar metar)
        {
            if (field == Becoming)
            {
                currentMetar = new TemporaryMetar();

                metar.Becoming.Add((TemporaryMetar)currentMetar);
                return currentMetar;
            }

            if (field == Temporary)
            {
                currentMetar = new TemporaryMetar();

                metar.Temporary.Add((TemporaryMetar)currentMetar);
                return currentMetar;
            }

            foreach (var parser in _fieldParsers)
            {
                if (parser.Parse(field, currentMetar))
                    break;
            }

            return currentMetar;
        }
    }
}