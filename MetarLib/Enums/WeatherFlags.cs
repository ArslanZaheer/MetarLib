using System;

namespace MetarLib.Enums
{
    [Flags]
    public enum WeatherFlags : long
    {
        None = 0,
        
        Blowing = 1 << 0, // BL
        Drizzle = 1 << 1, // DZ
        DustDevils = 1 << 2, // PO
        DustStorm = 1 << 3, // DS
        Fog = 1 << 4, // FG
        Freezing = 1 << 5, // FZ
        FunnelCloud = 1 << 6, // FC
        Hail = 1 << 7, // GR
        Haze = 1 << 8, // HZ
        Heavy = 1 << 9, // +
        IceCrystals = 1 << 10, // IC
        IcePellets = 1 << 11, // PL
        InTheVicinity = 1 << 12, // VC
        Light = 1 << 13, // -
        LowDrifting = 1 << 14, // DR
        Mist = 1 << 15, // BR
        Patches = 1 << 16, // BC
        Rain = 1 << 17, // RA
        Recent = 1 << 18, // RE
        Sand = 1 << 19, // SA
        Sandstorm = 1 << 20, // SS
        Shallow = 1 << 21, // MI
        Shower = 1 << 22, // SH
        SmallHail = 1 << 23, // GS
        Smoke = 1 << 24, // FU
        Snow = 1 << 25, // SN
        SnowGrains = 1 << 26, // SG
        Squall = 1 << 27, // SQ
        Thunderstorm = 1 << 28, // TS
        UnidentifiedPrecipitation = 1 << 29, // UP
        VolcanicAsh = 1 << 30, // VA
        WidespreadDust = 1 << 31 // DU
    }
}