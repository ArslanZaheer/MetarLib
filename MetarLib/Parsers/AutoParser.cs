using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class AutoParser : IFieldParser
    {
        private const string Auto = "AUTO";
        
        public bool Parse(ParserContext context, string field)
        {
            if (field != Auto)
                return false;

            return context.Metar.IsAutomaticObservation = true;
        }
    }
}