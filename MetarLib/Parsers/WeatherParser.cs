using System.Linq;
using System.Text.RegularExpressions;
using MetarLib.Contracts;
using MetarLib.Enums;

namespace MetarLib.Parsers
{
    public class WeatherParser : IFieldParser
    {
        private static readonly Regex WeatherRegex = new Regex(@"-|\+|BC|BL|BR|DR|DS|DU|DZ|FC|FG|FU|FZ|GR|GS|HZ|IC|MI|PL|PO|RA|RE|SA|SG|SH|SN|SQ|SS|TS|UP|VA|VC", RegexOptions.Compiled);

        public bool Parse(string field, Metar metar)
        {
            var matches = WeatherRegex.Matches(field);

            if (!matches.Any())
                return false;

            var weather = WeatherFlags.None;
            
            foreach (var value in matches.Select(m => m.Value))
            {
                weather |= value switch
                {
                    "-" => WeatherFlags.Light,
                    "+" => WeatherFlags.Heavy,
                    "BC" => WeatherFlags.Patches,
                    "BL" => WeatherFlags.Blowing,
                    "BR" => WeatherFlags.Mist,
                    "DR" => WeatherFlags.LowDrifting,
                    "DS" => WeatherFlags.DustStorm,
                    "DU" => WeatherFlags.WidespreadDust,
                    "DZ" => WeatherFlags.Drizzle,
                    "FC" => WeatherFlags.FunnelCloud,
                    "FG" => WeatherFlags.Fog,
                    "FU" => WeatherFlags.Smoke,
                    "FZ" => WeatherFlags.Freezing,
                    "GR" => WeatherFlags.Hail,
                    "GS" => WeatherFlags.SmallHail,
                    "HZ" => WeatherFlags.Haze,
                    "IC" => WeatherFlags.IceCrystals,
                    "MI" => WeatherFlags.Shallow,
                    "PL" => WeatherFlags.IcePellets,
                    "PO" => WeatherFlags.DustDevils,
                    "RA" => WeatherFlags.Rain,
                    "RE" => WeatherFlags.Recent,
                    "SA" => WeatherFlags.Sand,
                    "SG" => WeatherFlags.SnowGrains,
                    "SH" => WeatherFlags.Shower,
                    "SN" => WeatherFlags.Snow,
                    "SQ" => WeatherFlags.Squall,
                    "SS" => WeatherFlags.Sandstorm,
                    "TS" => WeatherFlags.Thunderstorm,
                    "UP" => WeatherFlags.UnidentifiedPrecipitation,
                    "VA" => WeatherFlags.VolcanicAsh,
                    "VC" => WeatherFlags.InTheVicinity
                };
            }
            
            metar.Weather.Add(weather);
            
            return true;
        }
    }
}