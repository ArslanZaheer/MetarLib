using System;
using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class TimeOfObservationParser : IFieldParser
    {
        private const int Day = 1;
        private const int Hour = 2;
        private const int Minutes = 3;
        
        private static readonly Regex TimeOfObservationRegex = new Regex(@"^(\d{2})(\d{2})(\d{2})Z$", RegexOptions.Compiled);

        private readonly IDateTimeProvider _dateTimeProvider;
        
        public TimeOfObservationParser(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        
        public bool Parse(ParserContext context, string field)
        {
            var match = TimeOfObservationRegex.Match(field);
            
            if (!match.Success)
                return false;

            var day = int.Parse(match.Groups[Day].Value);
            var hour = int.Parse(match.Groups[Hour].Value);
            var minutes = int.Parse(match.Groups[Minutes].Value);

            var now = _dateTimeProvider.Now;
            var timeOfObservation = new DateTimeOffset(now.Year, now.Month, day, hour, minutes, 0, TimeSpan.Zero);

            if (timeOfObservation > now)
                timeOfObservation = timeOfObservation.AddMonths(-1);

            context.Metar.TimeOfObservation = timeOfObservation;
            
            return true;
        }
    }
}