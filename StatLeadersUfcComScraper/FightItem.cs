using System.Collections.Generic;

namespace StatLeadersUfcComScraper
{
    public class FightItem
    {
        public string Header { get; set; }
        public IEnumerable<FightRow> Rows { get; set; }
    }
}