using System;

namespace StatLeadersUfcComScraper.ConsoleExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var result = new StatLeadersScraper().ScrapeCareer(
                //fighterStatus: 1,
                //weightClass: WeightClasses.Bantamweight,
                //country: FiltersCountry.Australia
                );
            Console.WriteLine("Hello World!");
        }
    }
}