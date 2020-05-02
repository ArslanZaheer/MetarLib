using MetarLib.Contracts;

namespace MetarLib.Test.FieldParsers
{
    public abstract class FieldParserTestBase
    {
        private readonly IFieldParser _fieldParser;

        protected FieldParserTestBase(IFieldParser fieldParser)
        {
            _fieldParser = fieldParser;
        }
        
        protected Metar GetMetar(string field)
        {
            var context = new ParserContext();

            _fieldParser.Parse(context, field);

            return context.GetResult();
        }
    }
}