using Searcher.BLL.Enums;

namespace Searcher.BLL.Infrastructure
{
    public class Options
    {
        public Options(SearchSystemType type, ApiKeyOptions apiKeyOptions)
        {
            Type = type;
            ApiKeyOptions = apiKeyOptions;
        }

        public SearchSystemType Type { get; set; }
        public ApiKeyOptions ApiKeyOptions { get; set; }
    }
}
