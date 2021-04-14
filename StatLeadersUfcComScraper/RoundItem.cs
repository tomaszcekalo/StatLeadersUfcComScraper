using System.Collections.Generic;

namespace StatLeadersUfcComScraper
{
    public class RoundItem
    {
        public string Header { get; set; }
        public string Subheader { get; set; }
        public IEnumerable<RoundRow> Rows { get; set; }
    }
}