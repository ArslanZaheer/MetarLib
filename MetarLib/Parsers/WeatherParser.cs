using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MetarLib.Contracts;
using MetarLib.Enums;

namespace MetarLib.Parsers
{
    public class WeatherParser : IFieldParser
    {
        private static readonly Regex WeatherRegex = new Regex(@"^([-+])?((?:[A-Z]{2})+)$", RegexOptions.Compiled);
        private static readonly Regex WeatherCodesRegex = new Regex(@"[A-Z]{2}");

        public bool Parse(string field, Metar metar)
        {
            var match = WeatherRegex.Match(field);

            if (!match.Success)
                return false;

            var codes = new List<string>();
            
            if (match.Groups[1].Success)
                codes.Add(match.Groups[1].Value);
            
            var weatherCodeMatches = WeatherCodesRegex.Matches(match.Groups[2].Value);
            codes.AddRange(weatherCodeMatches.Select(f => f.Value));
            
            var weather = codes.Aggregate(WeatherCodes.None, (codes, code) => codes | code switch
            {
                "-" => WeatherCodes.Light,
                "+" => WeatherCodes.Heavy,
                "BC" => WeatherCodes.Patches,
                "BL" => WeatherCodes.Blowing,
                "BR" => WeatherCodes.Mist,
                "DR" => WeatherCodes.LowDrifting,
                "DS" => WeatherCodes.DustStorm,
                "DU" => WeatherCodes.WidespreadDust,
                "DZ" => WeatherCodes.Drizzle,
                "FC" => WeatherCodes.FunnelCloud,
                "FG" => WeatherCodes.Fog,
                "FU" => WeatherCodes.Smoke,
                "FZ" => WeatherCodes.Freezing,
                "GR" => WeatherCodes.Hail,
                "GS" => WeatherCodes.SmallHail,
                "HZ" => WeatherCodes.Haze,
                "IC" => WeatherCodes.IceCrystals,
                "MI" => WeatherCodes.Shallow,
                "PL" => WeatherCodes.IcePellets,
                "PO" => WeatherCodes.DustDevils,
                "RA" => WeatherCodes.Rain,
                "RE" => WeatherCodes.Recent,
                "SA" => WeatherCodes.Sand,
                "SG" => WeatherCodes.SnowGrains,
                "SH" => WeatherCodes.Shower,
                "SN" => WeatherCodes.Snow,
                "SQ" => WeatherCodes.Squall,
                "SS" => WeatherCodes.Sandstorm,
                "TS" => WeatherCodes.Thunderstorm,
                "UP" => WeatherCodes.UnidentifiedPrecipitation,
                "VA" => WeatherCodes.VolcanicAsh,
                "VC" => WeatherCodes.InTheVicinity
            });

            metar.Weather.Add(weather);
            
            return true;
        }
    }
}