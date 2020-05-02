using System;

namespace MetarLib
{
    public class TemporaryMetar : Metar
    {
        public TemporaryMetar(Metar metar, decimal? probability)
        {
            Metar = metar;
            Probability = probability;
        }
        
        public Metar Metar { get; }
        public decimal? Probability { get; }
        
        public DateTimeOffset ValidFrom { get; set; }
        public DateTimeOffset ValidTo { get; set; }
    }
}