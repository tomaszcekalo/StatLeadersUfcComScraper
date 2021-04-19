using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;
using System;
using System.Linq;
using System.Web;

namespace StatLeadersUfcComScraper
{
    public interface IStatLeadersScraper
    {
        Career ScrapeCareer(
            int? fighterStatus = null,
            string weightClass = null,
            string country = null);

        Fight ScrapeFight(
            string weightClass = null,
            string country = null);

        FightComb ScrapeFightComb(
            string weightClass = null);

        Round ScrapeRound(
            string weightClass = null,
            string country = null);

        Round ScrapeRoundComb(
            string weightClass = null);
    }

    public class StatLeadersScraper : IStatLeadersScraper
    {
        private readonly ScrapingBrowser _browser;

        public StatLeadersScraper()
        {
            _browser = new ScrapingBrowser();
        }

        public StatLeadersScraper(ScrapingBrowser browser)
        {
            _browser = browser;
        }

        public Career ScrapeCareer(
            int? fighterStatus = null,
            string weightClass = null,
            string country = null)
        {
            string url = Pages.StatLeadersCareer;
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            if (fighterStatus != null)
                query["fighter_status"] = fighterStatus.ToString();
            if (weightClass != null)
                query["weight_class"] = weightClass;
            if (country != null)
                query["country"] = country;

            uriBuilder.Query = query.ToString();
            var uri = uriBuilder.Uri;

            WebPage homePage = _browser.NavigateToPage(uri);

            var result = new Career
            {
                TotalFights = homePage.Html.CssSelect("#TotalFights-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                Wins = homePage.Html.CssSelect("#Wins-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                Finishes = homePage.Html.CssSelect("#Finishes-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                KOTKOWins = homePage.Html.CssSelect("#KOTKOWins-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                SubmissionWins = homePage.Html.CssSelect("#SubmissionWins-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                DecisionWins = homePage.Html.CssSelect("#DecisionWins-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                WinStreak = homePage.Html.CssSelect("#WinStreak-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                TitleFightWins = homePage.Html.CssSelect("#TitleFightWins-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                TotalAwards = homePage.Html.CssSelect("#TotalAwards-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                AverageFightTimeShort = homePage.Html.CssSelect("#AverageFightTimeShort-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                AverageFightTimeLong = homePage.Html.CssSelect("#AverageFightTimeLong-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                TotalFightTime = homePage.Html.CssSelect("#TotalFightTime-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                ControlTime = homePage.Html.CssSelect("#ControlTime-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                ControlTimePercentage = homePage.Html.CssSelect("#ControlTimePercentage-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                TopPositionTime = homePage.Html.CssSelect("#TopPositionTime-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                TopPositionTimePercentage = homePage.Html.CssSelect("#TopPositionTimePercentage-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                BottomPositionTime = homePage.Html.CssSelect("#BottomPositionTime-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                BottomPositionTimePercentage = homePage.Html.CssSelect("#BottomPositionTimePercentage-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                Knockdowns = homePage.Html.CssSelect("#Knockdowns-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                KnockdownsAverage = homePage.Html.CssSelect("#KnockdownsAverage-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                SigStrikesLanded = homePage.Html.CssSelect("#SigStrikesLanded-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                SigStrikesAccuracy = homePage.Html.CssSelect("#SigStrikesAccuracy-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                SLpM = homePage.Html.CssSelect("#SLpM-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                SignificantStrikesDifferential = homePage.Html.CssSelect("#SignificantStrikesDifferential-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                SignificantStrikesDefense = homePage.Html.CssSelect("#SignificantStrikesDefense-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                SApM = homePage.Html.CssSelect("#SApM-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                TotalStrikesLanded = homePage.Html.CssSelect("#TotalStrikesLanded-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                TakedownsLanded = homePage.Html.CssSelect("#TakedownsLanded-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                TakedownsAccuracy = homePage.Html.CssSelect("#TakedownsAccuracy-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                TakedownsDefense = homePage.Html.CssSelect("#TakedownsDefense-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                SubmissionsAttempted = homePage.Html.CssSelect("#SubmissionsAttempted-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault(),
                SubmissionsAverage = homePage.Html.CssSelect("#SubmissionsAverage-group")
                    .Select(ParseCareerItem)
                    .FirstOrDefault()
            };

            return result;

            //return ParseCareerItem(homePage.Html);
        }

        private CareerItem ParseCareerItem(HtmlNode node)
        {
            var result = new CareerItem
            {
                Rows = node
                    .CssSelect(".results-table--tr")
                    //.CssSelect(".results-table--tr :not(.results-table--row-1)")
                    .Select(ParseCareerRow)
                    .Where(x => x.Fighter != null),
                Header = node.CssSelect("header h3")
                    .FirstOrDefault()
                    ?.InnerText,
                Subheader = node.CssSelect("header p")
                    .FirstOrDefault()
                    ?.InnerText
                    .Trim(),
                TableTies = node.CssSelect(".results-table--ties")
                    .FirstOrDefault()
                    ?.InnerText
                    .Trim()
            };
            return result;
        }

        private CareerRow ParseCareerRow(HtmlNode node)
        {
            var result = new CareerRow
            {
                Rank = node.CssSelect("span")
                    .FirstOrDefault()
                    ?.InnerText,
                Total = node.CssSelect("span")
                    .LastOrDefault()
                    ?.InnerText,
                Fighter = node.CssSelect("span a")
                    .Select(x => new Fighter() { Href = x.Attributes["href"].Value, Name = x.InnerText })
                    .FirstOrDefault()
            };
            return result;
        }

        public Fight ScrapeFight(
            string weightClass = null,
            string country = null)
        {
            string url = Pages.StatLeadersFight;
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            if (weightClass != null)
                query["weight_class"] = weightClass;
            if (country != null)
                query["country"] = country;

            uriBuilder.Query = query.ToString();
            var uri = uriBuilder.Uri;

            WebPage homePage = _browser.NavigateToPage(uri);

            return ParseFight(homePage.Html);
        }

        public Fight ParseFight(HtmlNode node)
        {
            var result = new Fight()
            {
                FastestFinish = node.CssSelect("#FastestFinish-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                SigStrikesAttempted = node.CssSelect("#SigStrikesAttempted-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                FastestKOTKO = node.CssSelect("#FastestKOTKO-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                FastestSubmission = node.CssSelect("#FastestSubmission-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                Knockdowns = node.CssSelect("#Knockdowns-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                LargestComebackFinish = node.CssSelect("#LargestComebackFinish-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                LatestFinish = node.CssSelect("#LatestFinish-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                LatestKOTKO = node.CssSelect("#LatestKOTKO-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                LatestSubmission = node.CssSelect("#LatestSubmission-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                LegKicksLanded = node.CssSelect("#LegKicksLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                SigBodyStrikesLanded = node.CssSelect("#SigBodyStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                SigClinchStrikesLanded = node.CssSelect("#SigClinchStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                SigDistanceStrikesLanded = node.CssSelect("#SigDistanceStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                SigGroundStrikesLanded = node.CssSelect("#SigGroundStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                SigHeadStrikesLanded = node.CssSelect("#SigHeadStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                SigStrikesAccuracy = node.CssSelect("#SigStrikesAccuracy-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                SigStrikesLanded = node.CssSelect("#SigStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                SignificantStrikesDifferential = node.CssSelect("#SignificantStrikesDifferential-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                SubmissionsAttempted = node.CssSelect("#SubmissionsAttempted-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TakedownsAccuracy = node.CssSelect("#TakedownsAccuracy-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TakedownsAttempted = node.CssSelect("#TakedownsAttempted-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TakedownsLanded = node.CssSelect("#TakedownsLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TotalBodyStrikesLanded = node.CssSelect("#TotalBodyStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TotalClinchStrikesLanded = node.CssSelect("#TotalClinchStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TotalGroundStrikesLanded = node.CssSelect("#TotalGroundStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TotalHeadStrikesLanded = node.CssSelect("#TotalHeadStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TotalLegStrikesLanded = node.CssSelect("#TotalLegStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TotalStrikesAttempted = node.CssSelect("#TotalStrikesAttempted-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TotalStrikesLanded = node.CssSelect("#TotalStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
            };

            return result;
        }

        public FightItem ParseFightItem(HtmlNode node)
        {
            var result = new FightItem()
            {
                Header = node.CssSelect("header h3")
                    .FirstOrDefault()
                    ?.InnerText,
                SubHeader = node.CssSelect("header p")
                    .FirstOrDefault()
                    ?.InnerText
                    .Trim(),
                Rows = node.CssSelect(".results-table--tr")
                    .Select(ParseFightRow)
            };
            return result;
        }

        public FightRow ParseFightRow(HtmlNode node)
        {
            var spans = node.CssSelect("span")
                .ToList();
            var result = new FightRow
            {
                Fighters = node.CssSelect("span a")
                    .Select(x => new Fighter()
                    {
                        Href = x.Attributes["href"].Value,
                        Name = x.InnerText
                    })
                    .ToArray(),
                DateEvent = spans
                    .LastOrDefault()
                    ?.InnerText,
                Rank = spans
                    .FirstOrDefault()
                    ?.InnerText,
                Total = spans[3]
                    .InnerText,
            };
            return result;
        }

        public FightComb ScrapeFightComb(
            string weightClass = null)
        {
            string url = Pages.StatLeadersFightComb;
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            if (weightClass != null)
                query["weight_class"] = weightClass;

            uriBuilder.Query = query.ToString();
            var uri = uriBuilder.Uri;

            WebPage homePage = _browser.NavigateToPage(uri);

            return ParseFightComb(homePage.Html);
        }

        public FightComb ParseFightComb(HtmlNode node)
        {
            var result = new FightComb()
            {
                SigStrikesAttempted = node.CssSelect("#SigStrikesAttempted-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TakedownsAttempted = node.CssSelect("#TakedownsAttempted-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TakedownsLanded = node.CssSelect("#TakedownsLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TotalStrikesAttempted = node.CssSelect("#TotalStrikesAttempted-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                TotalStrikesLanded = node.CssSelect("#TotalStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                SigStrikesLanded = node.CssSelect("#SigStrikesLanded-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault(),
                SubmissionsAttempted = node.CssSelect("#SubmissionsAttempted-group")
                    .Select(ParseFightItem)
                    .FirstOrDefault()
            };
            return result;
        }

        public Round ScrapeRound(
            string weightClass = null,
            string country = null)
        {
            string url = Pages.StatLeadersRound;
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            if (weightClass != null)
                query["weight_class"] = weightClass;
            if (country != null)
                query["country"] = country;

            uriBuilder.Query = query.ToString();
            var uri = uriBuilder.Uri;

            WebPage homePage = _browser.NavigateToPage(uri);

            return ParseRound(homePage.Html);
        }

        public Round ScrapeRoundComb(
            string weightClass = null)
        {
            string url = Pages.StatLeadersRoundComb;
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            if (weightClass != null)
                query["weight_class"] = weightClass;

            uriBuilder.Query = query.ToString();
            var uri = uriBuilder.Uri;

            WebPage homePage = _browser.NavigateToPage(uri);

            return ParseRound(homePage.Html);
        }

        public Round ParseRound(HtmlNode node)
        {
            var result = new Round()
            {
                SigStrikesAttempted = node.CssSelect("#SigStrikesAttempted-group")
                    .Select(ParseRoundItem)
                    .FirstOrDefault(),
                TakedownsAttempted = node.CssSelect("#TakedownsAttempted-group")
                    .Select(ParseRoundItem)
                    .FirstOrDefault(),
                TakedownsLanded = node.CssSelect("#TakedownsLanded-group")
                    .Select(ParseRoundItem)
                    .FirstOrDefault(),
                TotalStrikesAttempted = node.CssSelect("#TotalStrikesAttempted-group")
                    .Select(ParseRoundItem)
                    .FirstOrDefault(),
                TotalStrikesLanded = node.CssSelect("#TotalStrikesLanded-group")
                    .Select(ParseRoundItem)
                    .FirstOrDefault(),
                SigStrikesLanded = node.CssSelect("#SigStrikesLanded-group")
                    .Select(ParseRoundItem)
                    .FirstOrDefault(),
                SubmissionsAttempted = node.CssSelect("#SubmissionsAttempted-group")
                    .Select(ParseRoundItem)
                    .FirstOrDefault()
            };

            return result;
        }

        public RoundItem ParseRoundItem(HtmlNode node)
        {
            var result = new RoundItem()
            {
                Header = node.CssSelect("header h3")
                    .FirstOrDefault()
                    ?.InnerText,
                SubHeader = node.CssSelect("header p")
                    .FirstOrDefault()
                    ?.InnerText
                    .Trim(),
                Rows = node.CssSelect(".results-table--tr")
                    .Select(ParseRoundRow)
                    .Where(x => x.Fighters.Any())
            };
            return result;
        }

        public RoundRow ParseRoundRow(HtmlNode node)
        {
            var spans = node.CssSelect("span")
                .Select(x => x.InnerText)
                .ToList();
            var round = spans.FirstOrDefault(x =>
                x.Contains("Round "));

            var result = new RoundRow()
            {
                Fighters = node.CssSelect("span a")
                    .Select(x => new Fighter()
                    {
                        Href = x.Attributes["href"].Value,
                        Name = x.InnerText
                    })
                    .ToArray(),
                DateEvent = spans
                    .LastOrDefault(),
                Rank = spans
                    .FirstOrDefault(),
                Total = round != null
                    ? spans[3]
                    : spans[4],
                Round = round
                    ?? spans[3],
            };
            return result;
        }
    }
}