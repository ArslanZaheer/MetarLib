using System;
using System.Text.RegularExpressions;
using MetarLib.Contracts;

namespace MetarLib.Parsers
{
    public class TimeOfObservationParser : IFieldParser
    {
        private static readonly Regex TimeOfObservationRegex = new Regex(@"^(\d{2})(\d{2})(\d{2})Z$", RegexOptions.Compiled);

        private readonly IDateTimeProvider _dateTimeProvider;
        
        public TimeOfObservationParser(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        
        public bool Parse(string field, Metar metar)
        {
            var match = TimeOfObservationRegex.Match(field);
            
            if (!match.Success)
                return false;

            var day = int.Parse(match.Groups[1].Value);
            var hour = int.Parse(match.Groups[2].Value);
            var minutes = int.Parse(match.Groups[3].Value);

            var dateTime = _dateTimeProvider.Now;

            if (dateTime.Day < day)
                dateTime = dateTime.AddMonths(-1);
            
            metar.TimeOfObservation = new DateTimeOffset(dateTime.Year, dateTime.Month, day, hour, minutes, 0, TimeSpan.Zero);
            
            return true;
        }
    }
}