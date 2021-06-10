using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StatLeadersUfcComScraper.Tests
{
    [TestClass]
    public class LiveDataTests
    {
        [TestMethod]
        public void TestScrapeRoundComb()
        {
            IStatLeadersScraper scraper = new StatLeadersScraper();
            var roundComb = scraper.ScrapeRoundComb();
        }
        [TestMethod]
        public void TestScrapeCareer()
        {
            IStatLeadersScraper scraper = new StatLeadersScraper();
            var roundComb = scraper.ScrapeCareer();
        }
        [TestMethod]
        public void TestScrapeFight()
        {
            IStatLeadersScraper scraper = new StatLeadersScraper();
            var roundComb = scraper.ScrapeFight();
        }
        [TestMethod]
        public void TestScrapeFightComb()
        {
            IStatLeadersScraper scraper = new StatLeadersScraper();
            var roundComb = scraper.ScrapeFightComb();
        }
        [TestMethod]
        public void TestScrapeRound()
        {
            IStatLeadersScraper scraper = new StatLeadersScraper();
            var roundComb = scraper.ScrapeRound();
        }
    }
}