using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class IcaoLocationCodeParser : IFieldParser
    {
        private const int LocationCode = 1;
        
        private static readonly Regex IcaoLocationCodeRegex = new Regex(@"^([A-Z]{4})$", RegexOptions.Compiled);
        
        public bool Parse(ParserContext context, string field)
        {
            var metar = context.Metar;
            
            if (metar.IcaoLocationCode != null)
                return false;
            
            var match = IcaoLocationCodeRegex.Match(field);

            if (!match.Success)
                return false;

            metar.IcaoLocationCode = match.Groups[LocationCode].Value;

            return true;
        }
    }
}