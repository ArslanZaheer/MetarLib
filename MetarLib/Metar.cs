using System;
using MetarLib.Enums;

namespace MetarLib
{
    public class Metar
    {
        public Metar(string icaoLocationCode)
        {
            IcaoLocationCode = icaoLocationCode;
        }
        
        public string IcaoLocationCode { get; }
        public DateTimeOffset TimeOfObservation { get; internal set; }
        
        public int? WindDirection { get; internal set; }
        public bool IsWindVariable { get; internal set; }
        public int? WindSpeed { get; internal set; }
        public int? WindGustingTo { get; internal set; }
        public UnitOfSpeed WindSpeedUnit { get; internal set; }
        
        public int? WindVaryingFrom { get; internal set; }
        public int? WindVaryingTo { get; internal set; }
        
        public bool VisibilityLessThan { get; internal set; }
        public decimal? Visibility { get; internal set; }
        public UnitOfLength VisibilityUnit { get; internal set; }
        
        public int? Temperature { get; internal set; }
        public int? Dewpoint { get; internal set; }
        
        public Cloud[] Clouds { get; internal set; }
        
        public decimal? AltimeterSetting { get; internal set; }
        public UnitOfPressure AltimeterSettingUnit { get; internal set; }

        public override string ToString() => IcaoLocationCode;
    }
}