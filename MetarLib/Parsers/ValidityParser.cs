using System;
using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class ValidityParser : IFieldParser
    {
        private const int FromDay = 1;
        private const int FromHour = 2;
        private const int ToDay = 3;
        private const int ToHour = 4;

        private static readonly Regex ValidityRegex = new Regex(@"^(\d{2})(\d{2})/(\d{2})(\d{2})$"); 
        
        public bool Parse(ParserContext context, string field)
        {
            var match = ValidityRegex.Match(field);

            if (!match.Success)
                return false;

            if (!(context.Metar is TemporaryMetar temporaryMetar))
                return false;
            
            var fromDay = int.Parse(match.Groups[FromDay].Value);
            var fromHour = int.Parse(match.Groups[FromHour].Value);
            var toDay = int.Parse(match.Groups[ToDay].Value);
            var toHour = int.Parse(match.Groups[ToHour].Value);
            
            var observationDate = temporaryMetar.Metar.TimeOfObservation;
            
            var validFrom = new DateTimeOffset(observationDate.Year, observationDate.Month, fromDay, fromHour, 0, 0, TimeSpan.Zero);

            if (validFrom < observationDate)
                validFrom = validFrom.AddMonths(1);
            
            var validTo = new DateTimeOffset(observationDate.Year, observationDate.Month, toDay, toHour, 0, 0, TimeSpan.Zero);
            
            if (validTo < observationDate)
                validTo = validTo.AddMonths(1);

            temporaryMetar.ValidFrom = validFrom;
            temporaryMetar.ValidTo = validTo;
            
            return true;
        }
    }
}