namespace MetarLib.Contracts
{
    public interface IFieldParser
    {
        bool Parse(ParserContext context, string field);
    }
}