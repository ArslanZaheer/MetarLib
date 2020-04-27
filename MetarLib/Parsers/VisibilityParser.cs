using System.Text.RegularExpressions;
using MetarLib.Contracts;
using MetarLib.Enums;

namespace MetarLib.Parsers
{
    public class VisibilityParser : IFieldParser
    {
        private const string CloudAndVisibilityOk = "CAVOK";
        private const int VisibilityUnlimited = 9999;
        
        private static readonly Regex VisibilityMetersRegex = new Regex(@"^(\d{4}|CAVOK)$", RegexOptions.Compiled);
        private static readonly Regex VisibilityStatuteMilesRegex = new Regex(@"^(M)?(?:(\d)/)?(\d)SM$", RegexOptions.Compiled);
        
        public bool Parse(string field, Metar metar)
        {
            return TryParseVisibilityMeters(field, metar) || TryParseVisibilityStatuteMiles(field, metar);
        }

        private bool TryParseVisibilityMeters(string metarText, Metar metar)
        {
            var match = VisibilityMetersRegex.Match(metarText);

            if (!match.Success)
                return false;
            
            var visibility = match.Groups[1].Value;

            metar.Visibility = visibility == CloudAndVisibilityOk
                ? VisibilityUnlimited
                : int.Parse(visibility);

            metar.VisibilityUnit = UnitOfLength.Meters;

            return true;
        }

        private bool TryParseVisibilityStatuteMiles(string metarText, Metar metar)
        {
            var match = VisibilityStatuteMilesRegex.Match(metarText);

            if (!match.Success)
                return false;
            
            var numerator = match.Groups[2];
            var value = decimal.Parse(match.Groups[3].Value);

            metar.Visibility = numerator.Success ? decimal.Parse(numerator.Value) / value : value;

            metar.VisibilityLessThan = match.Groups[1].Success;
            metar.VisibilityUnit = UnitOfLength.StatuteMiles;

            return true;
        }
    }
}