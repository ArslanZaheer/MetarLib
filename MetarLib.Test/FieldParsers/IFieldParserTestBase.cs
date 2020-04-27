using MetarLib.Contracts;

namespace MetarLib.Test.FieldParsers
{
    public abstract class IFieldParserTestBase
    {
        private const string MetarTemplate = "METAR {0} {1}=";

        private readonly IFieldParser _fieldParser;

        protected IFieldParserTestBase(IFieldParser fieldParser)
        {
            _fieldParser = fieldParser;
        }
        
        protected IFieldParserTestBase() : this(null) {}
        
        protected Metar GetMetar(string contents, string locationCode = "EHZZ")
        {
            var (metarText, metar) = GetMetarWithText(contents, locationCode);

            _fieldParser?.Parse(metarText, metar);

            return metar;
        }

        protected (string metarText, Metar metar) GetMetarWithText(string contents, string locationCode = "EHZZ")
        {
            var metarText = string.Format(MetarTemplate, locationCode, contents);
            var metar = new Metar(locationCode);

            return (metarText, metar);
        }
    }
}