using System.Text.RegularExpressions;
using MetarLib.Contracts;
using MetarLib.Enums;

namespace MetarLib.Parsers
{
    public class WindParser : IFieldParser
    {
        private const string Variable = "VRB";
        private const string KilometersPerHour = "KPH";
        private const string Knots = "KT";
        private const string MetersPerSecond = "MPS";

        private const int Direction = 1;
        private const int Speed = 2;
        private const int Gusting = 3;
        private const int Unit = 4;
        
        private static readonly Regex WindRegex = new Regex($@"^({Variable}|\d{{3}})(\d{{2}})(?:G(\d{{2}}))?({Knots}|{MetersPerSecond}|{KilometersPerHour})$", RegexOptions.Compiled);
        
        public bool Parse(ParserContext context, string field)
        {
            var match = WindRegex.Match(field);

            if (!match.Success)
                return false;

            var metar = context.Metar;
            
            if (match.Groups[Direction].Value == Variable)
                metar.IsWindVariable = true;
            else
                metar.WindDirection = int.Parse(match.Groups[Direction].Value);

            metar.WindSpeed = int.Parse(match.Groups[Speed].Value);

            if (match.Groups[Gusting].Success)
                metar.WindGustingTo = int.Parse(match.Groups[Gusting].Value);

            metar.WindSpeedUnit = match.Groups[Unit].Value switch
            {
                KilometersPerHour => UnitOfSpeed.KilometersPerHour,
                Knots => UnitOfSpeed.Knots,
                MetersPerSecond => UnitOfSpeed.MetersPerSecond,
                _ => UnitOfSpeed.None
            };

            return true;
        }
    }
}