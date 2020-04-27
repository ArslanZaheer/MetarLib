using System.Linq;
using MetarLib.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace MetarLib.Test
{
    public abstract class MetarParserTestBase
    {
        private const string MetarTemplate = "METAR {0} {1}=";

        private readonly IMetarParser _parser;

        protected MetarParserTestBase()
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddMetarLib();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            _parser = serviceProvider.GetService<IMetarParser>();
        }

        protected Metar GetMetar(string contents, string locationCode = "EHZZ") =>
            _parser.Parse(string.Format(MetarTemplate, locationCode, contents)).Single();
    }
}