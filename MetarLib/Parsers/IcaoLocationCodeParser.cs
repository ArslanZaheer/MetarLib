using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class IcaoLocationCodeParser : IFieldParser
    {
        private static readonly Regex IcaoLocationCodeRegex = new Regex(@"^([A-Z]{4})$", RegexOptions.Compiled);
        
        public bool Parse(string field, Metar metar)
        {
            if (metar.IcaoLocationCode != null)
                return false;
            
            var match = IcaoLocationCodeRegex.Match(field);

            if (!match.Success)
                return false;

            metar.IcaoLocationCode = match.Groups[1].Value;

            return true;
        }
    }
}