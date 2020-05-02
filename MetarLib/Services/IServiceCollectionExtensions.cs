using MetarLib.Contracts;
using MetarLib.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace MetarLib.Services
{
    public static class IServiceCollectionExtensions
    {
        public static void AddMetarLib(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            serviceCollection.AddSingleton<IFieldParser, IcaoLocationCodeParser>();
            serviceCollection.AddSingleton<IFieldParser, TimeOfObservationParser>();
            serviceCollection.AddSingleton<IFieldParser, AutoParser>();
            serviceCollection.AddSingleton<IFieldParser, WindParser>();
            serviceCollection.AddSingleton<IFieldParser, WindVariationParser>();
            serviceCollection.AddSingleton<IFieldParser, VisibilityParser>();
            serviceCollection.AddSingleton<IFieldParser, WeatherParser>();
            serviceCollection.AddSingleton<IFieldParser, CloudParser>();
            serviceCollection.AddSingleton<IFieldParser, TemperatureDewpointParser>();
            serviceCollection.AddSingleton<IFieldParser, AltimeterSettingParser>();
            serviceCollection.AddSingleton<IFieldParser, VerticalVisibilityParser>();
            serviceCollection.AddSingleton<IFieldParser, ProbabilityParser>();
            serviceCollection.AddSingleton<IFieldParser, ValidityParser>();

            serviceCollection.AddSingleton<IMetarParser, MetarParser>();
        }
    }
}