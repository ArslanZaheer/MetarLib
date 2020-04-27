namespace MetarLib.Contracts
{
    public interface IFieldParser
    {
        bool Parse(string field, Metar metar);
    }
}