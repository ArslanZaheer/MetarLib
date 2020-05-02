using System;

namespace MetarLib
{
    public class TemporaryMetar : Metar
    {
        public decimal? Probability { get; set; }
        
        public DateTimeOffset ValidFrom { get; set; }
        public DateTimeOffset ValidTo { get; set; }
    }
}