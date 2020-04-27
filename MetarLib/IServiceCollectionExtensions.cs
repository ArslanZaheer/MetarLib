using MetarLib.Contracts;
using MetarLib.Parsers;
using Microsoft.Extensions.DependencyInjection;

namespace MetarLib
{
    public static class IServiceCollectionExtensions
    {
        public static void AddMetarLib(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            serviceCollection.AddSingleton<IFieldParser, AltimeterSettingParser>();
            serviceCollection.AddSingleton<IFieldParser, CloudParser>();
            serviceCollection.AddSingleton<IFieldParser, TemperatureDewpointParser>();
            serviceCollection.AddSingleton<IFieldParser, TimeOfObservationParser>();
            serviceCollection.AddSingleton<IFieldParser, VisibilityParser>();
            serviceCollection.AddSingleton<IFieldParser, WindParser>();
            serviceCollection.AddSingleton<IFieldParser, WindVariationParser>();

            serviceCollection.AddSingleton<IMetarParser, MetarParser>();
        }
    }
}