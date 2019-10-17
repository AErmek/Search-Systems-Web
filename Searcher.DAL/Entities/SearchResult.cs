using System;

namespace Searcher.DAL.Entities
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Snippet { get; set; }
        public int BrowserType { get; set; }
        public DateTime DateTime { get; set; }
    }
}
