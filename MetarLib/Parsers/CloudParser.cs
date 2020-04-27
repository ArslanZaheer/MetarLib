using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using MetarLib.Contracts;
using MetarLib.Enums;

namespace MetarLib.Parsers
{
    public class CloudParser : IFieldParser
    {
        private static readonly Regex CloudRegex = new Regex(@" (SKC|FEW|SCT|BKN|OVC|NCD|NSC)(\d{3})?(CB|TCU)?", RegexOptions.Compiled);
        
        public void Parse(string metarText, Metar metar)
        {
            var matches = CloudRegex.Matches(metarText);

            if (!matches.Any())
                return;

            var clouds = new List<Cloud>();

            foreach (Match match in matches)
            {
                var coverage = match.Groups[1].Value switch
                {
                    "SKC" => CloudCoverage.SkyClear,
                    
                    "FEW" => CloudCoverage.Few,
                    "SCT" => CloudCoverage.Scattered,
                    "BKN" => CloudCoverage.Broken,
                    "OVC" => CloudCoverage.Overcast,
                    
                    "NCD" => CloudCoverage.NoCloudDetected,
                    "NSC" => CloudCoverage.NoSignificantCloud
                };

                var convectivity = match.Groups[3].Value switch
                {
                    "CB" => CloudConvectivity.Cumulonimbus,
                    "TCU" => CloudConvectivity.ToweringCumulus,
                    _ => CloudConvectivity.None
                };
                
                var altitude = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) * 100 : 0;

                var cloud = new Cloud(coverage, altitude, convectivity);

                clouds.Add(cloud);
            }

            metar.Clouds = clouds.ToArray();
        }
    }
}