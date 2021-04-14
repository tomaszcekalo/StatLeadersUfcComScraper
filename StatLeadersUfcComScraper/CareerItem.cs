using System.Collections.Generic;

namespace StatLeadersUfcComScraper
{
    public class CareerItem
    {
        public string Header { get; set; }
        public string Subheader { get; set; }
        public IEnumerable<CareerRow> Rows { get; set; }
        public string TableTies { get; set; }
    }
}