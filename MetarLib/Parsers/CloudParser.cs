using System.Text.RegularExpressions;
using MetarLib.Contracts;
using MetarLib.Enums;

namespace MetarLib.Parsers
{
    public class CloudParser : IFieldParser
    {
        private const string SkyClear = "SKC";
        private const string Few = "FEW";
        private const string Scattered = "SCT";
        private const string Broken = "BKN";
        private const string Overcast = "OVC";
        private const string NoCloudDetected = "NCD";
        private const string NoSignificantCloud = "NSC";
        private const string Cumulonimbus = "CB";
        private const string ToweringCumulus = "TCU";

        private const int Coverage = 1;
        private const int Altitude = 2;
        private const int Convectivity = 3;

        private static readonly Regex CloudRegex =
            new Regex(
                $@"^({SkyClear}|{Few}|{Scattered}|{Broken}|{Overcast}|{NoCloudDetected}|{NoSignificantCloud})(\d{{3}})?({Cumulonimbus}|{ToweringCumulus})?",
                RegexOptions.Compiled);
        
        public bool Parse(ParserContext context, string field)
        {
            var match = CloudRegex.Match(field);

            if (!match.Success)
                return false;

            var coverage = match.Groups[Coverage].Value switch
            {
                SkyClear => CloudCoverage.SkyClear,
                Few => CloudCoverage.Few,
                Scattered => CloudCoverage.Scattered,
                Broken => CloudCoverage.Broken,
                Overcast => CloudCoverage.Overcast,
                NoCloudDetected => CloudCoverage.NoCloudDetected,
                NoSignificantCloud => CloudCoverage.NoSignificantCloud,
                _ => CloudCoverage.Unknown
            };

            var convectivity = match.Groups[Convectivity].Value switch
            {
                Cumulonimbus => CloudConvectivity.Cumulonimbus,
                ToweringCumulus => CloudConvectivity.ToweringCumulus,
                _ => CloudConvectivity.None
            };
                
            var altitude = match.Groups[Altitude].Success
                ? int.Parse(match.Groups[Altitude].Value) * 100
                : 0;

            var cloud = new Cloud(coverage, altitude, convectivity);
            context.Metar.Clouds.Add(cloud);

            return true;
        }
    }
}