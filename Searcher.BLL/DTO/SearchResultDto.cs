using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Searcher.BLL.Enums;
using System;

namespace Searcher.BLL.DTO
{
    public class SearchResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Snippet { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public SearchSystemType BrowserType { get; set; }
        public DateTime DateTime { get; set; }
    }
}
