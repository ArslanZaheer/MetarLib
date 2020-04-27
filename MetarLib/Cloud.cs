using MetarLib.Enums;

namespace MetarLib
{
    public class Cloud
    {
        public CloudCoverage Coverage { get; }
        public int Altitude { get; }
        public CloudConvectivity Convectivity { get; }

        internal Cloud(CloudCoverage coverage, int altitude, CloudConvectivity convectivity)
        {
            Coverage = coverage;
            Altitude = altitude;
            Convectivity = convectivity;
        }
    }
}