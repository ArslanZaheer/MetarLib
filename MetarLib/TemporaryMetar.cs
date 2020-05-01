using System;

namespace MetarLib
{
    public class TemporaryMetar : Metar
    {
        public DateTimeOffset ValidFrom { get; set; }
        public DateTimeOffset ValidTo { get; set; }
    }
}