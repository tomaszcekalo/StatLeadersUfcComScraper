using Newtonsoft.Json;
using System;

namespace StatLeadersUfcComScraper.ConsoleExample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IStatLeadersScraper scraper = new StatLeadersScraper();
            var roundComb = scraper.ScrapeRoundComb();
            Console.WriteLine(JsonConvert.SerializeObject(roundComb, Formatting.Indented));
            var round = scraper.ScrapeRound();
            Console.WriteLine(JsonConvert.SerializeObject(round, Formatting.Indented));

            var fightComb = scraper.ScrapeFightComb();
            Console.WriteLine(JsonConvert.SerializeObject(fightComb, Formatting.Indented));
            var fight = scraper.ScrapeFight();
            Console.WriteLine(JsonConvert.SerializeObject(fight, Formatting.Indented));
            var career = scraper.ScrapeCareer(
                //fighterStatus: 1,
                //weightClass: WeightClasses.Bantamweight,
                //country: FiltersCountry.Australia
                );
            Console.WriteLine(JsonConvert.SerializeObject(career, Formatting.Indented));
            Console.WriteLine("Hello World!");
        }
    }
}