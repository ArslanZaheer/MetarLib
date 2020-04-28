using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class AutoParser : IFieldParser
    {
        public bool Parse(string field, Metar metar)
        {
            if (field != "AUTO")
                return false;

            return metar.IsAutomaticObservation = true;
            
        }
    }
}