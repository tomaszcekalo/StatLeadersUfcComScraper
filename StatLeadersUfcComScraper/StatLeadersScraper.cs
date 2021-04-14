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
    }

    public class StatLeadersScraper : IStatLeadersScraper
    {
        private ScrapingBrowser _browser;

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
    }
}