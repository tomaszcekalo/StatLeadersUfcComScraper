using System.Collections.Generic;

namespace StatLeadersUfcComScraper
{
    public class RoundItem
    {
        public string Header { get; set; }
        public string SubHeader { get; set; }
        public IEnumerable<RoundRow> Rows { get; set; }
    }
}