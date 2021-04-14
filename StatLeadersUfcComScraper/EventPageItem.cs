using System.Collections.Generic;

namespace StatLeadersUfcComScraper
{
    public class EventPageItem
    {
        public string Header { get; set; }
        public IEnumerable<EventPageRow> Rows { get; set; }
    }
}