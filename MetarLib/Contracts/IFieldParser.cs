namespace MetarLib.Contracts
{
    public interface IFieldParser
    {
        void Parse(string metarText, Metar metar);
    }
}