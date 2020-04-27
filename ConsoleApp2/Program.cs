using System.IO;
using MetarLib;
using MetarLib.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var metarText = File.ReadAllText("/Users/michiel/Desktop/metar.txt");

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddMetarLib();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            var parser = serviceProvider.GetService<IMetarParser>();
            
            var metars = parser.Parse(metarText);
        }
    }
}