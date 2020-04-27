using System.Text.RegularExpressions;
using MetarLib.Contracts;
using MetarLib.Enums;

namespace MetarLib.Parsers
{
    public class WindParser : IFieldParser
    {
        private const string WindVariable = "VRB";
        private const string KilometersPerHour = "KPH";
        private const string Knots = "KT";
        private const string MetersPerSecond = "MPS";
        
        private static readonly Regex WindRegex = new Regex(@"^(VRB|\d{3})(\d{2})(?:G(\d{2}))?(KT|MPS|KPH)$", RegexOptions.Compiled);
        
        public bool Parse(string field, Metar metar)
        {
            var match = WindRegex.Match(field);

            if (!match.Success)
                return false;

            if (match.Groups[1].Value == WindVariable)
                metar.IsWindVariable = true;
            else
                metar.WindDirection = int.Parse(match.Groups[1].Value);

            metar.WindSpeed = int.Parse(match.Groups[2].Value);

            if (match.Groups[3].Success)
                metar.WindGustingTo = int.Parse(match.Groups[3].Value);

            metar.WindSpeedUnit = match.Groups[4].Value switch
            {
                KilometersPerHour => UnitOfSpeed.KilometersPerHour,
                Knots => UnitOfSpeed.Knots,
                MetersPerSecond => UnitOfSpeed.MetersPerSecond
            };

            return true;
        }
    }
}