using MetarLib.Contracts;

namespace MetarLib.Test.FieldParsers
{
    public abstract class IFieldParserTestBase
    {
        private readonly IFieldParser _fieldParser;

        protected IFieldParserTestBase(IFieldParser fieldParser)
        {
            _fieldParser = fieldParser;
        }
        
        protected Metar GetMetar(string field)
        {
            var metar = new Metar();

            _fieldParser.Parse(field, metar);

            return metar;
        }
    }
}