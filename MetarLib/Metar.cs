using System;
using System.Collections.Generic;
using MetarLib.Enums;

namespace MetarLib
{
    public class Metar
    {
        public Metar()
        {
            Clouds = new List<Cloud>();
            Weather = new List<WeatherCodes>();
        }
        
        public string IcaoLocationCode { get; set; }
        public DateTimeOffset TimeOfObservation { get; set; }
        
        public bool IsAutomaticObservation { get; set; }
        
        public int? WindDirection { get; set; }
        public bool IsWindVariable { get; set; }
        public int? WindSpeed { get; set; }
        public int? WindGustingTo { get; set; }
        public UnitOfSpeed WindSpeedUnit { get; set; }
        
        public int? WindVaryingFrom { get; set; }
        public int? WindVaryingTo { get; set; }
        
        public bool VisibilityLessThan { get; set; }
        public decimal? Visibility { get; set; }
        public UnitOfLength VisibilityUnit { get; set; }
        
        public int? Temperature { get; set; }
        public int? Dewpoint { get; set; }
        
        public ICollection<WeatherCodes> Weather { get; }
        
        public ICollection<Cloud> Clouds { get; }
        
        public decimal? AltimeterSetting { get; set; }
        public UnitOfPressure AltimeterSettingUnit { get; set; }

        public override string ToString() => IcaoLocationCode;
    }
}