using System.Text.RegularExpressions;
using MetarLib.Contracts;
using MetarLib.Enums;

namespace MetarLib.Parsers
{
    public class VisibilityParser : IFieldParser
    {
        private const string CloudAndVisibilityOk = "CAVOK";
        private const int VisibilityUnlimited = 9999;
        
        private const int Visibility = 1;
        private const int LessThan = 1;
        private const int Numerator = 2;
        private const int Value = 3;
        
        private static readonly Regex VisibilityMetersRegex = new Regex($@"^(\d{{4}}|{CloudAndVisibilityOk})$", RegexOptions.Compiled);
        private static readonly Regex VisibilityStatuteMilesRegex = new Regex(@"^(M)?(?:(\d)/)?(\d)SM$", RegexOptions.Compiled);
        
        public bool Parse(ParserContext context, string field)
        {
            return TryParseVisibilityMeters(field, context.Metar) || TryParseVisibilityStatuteMiles(field, context.Metar);
        }

        private bool TryParseVisibilityMeters(string metarText, Metar metar)
        {
            var match = VisibilityMetersRegex.Match(metarText);

            if (!match.Success)
                return false;
            
            var visibility = match.Groups[Visibility].Value;

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
            
            var numerator = match.Groups[Numerator];
            var value = decimal.Parse(match.Groups[Value].Value);

            metar.Visibility = numerator.Success ? decimal.Parse(numerator.Value) / value : value;

            metar.VisibilityLessThan = match.Groups[LessThan].Success;
            metar.VisibilityUnit = UnitOfLength.StatuteMiles;

            return true;
        }
    }
}