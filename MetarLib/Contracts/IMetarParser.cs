using System.Collections.Generic;

namespace MetarLib.Contracts
{
    public interface IMetarParser
    {
        IEnumerable<Metar> Parse(string text);
    }
}