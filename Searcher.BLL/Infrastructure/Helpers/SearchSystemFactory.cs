using Microsoft.Extensions.Logging;
using Searcher.BLL.Infrastructure.SearchSystems;
using Searcher.BLL.Interfaces;

namespace Searcher.BLL.Infrastructure.Helpers
{
    public class SearchSystemFactory : ISearchSystemFactory
    {
        public ISearchSystem InitSearchSystemBing(ILogger logger, string apiKey)
        {
            return new BingSearchSystem(logger, apiKey);
        }

        public ISearchSystem InitSearchSystemGoogle(ILogger logger, string apiKey, string cx)
        {
            return new GoogleSearchSystem(logger, apiKey, cx);
        }

        public ISearchSystem InitSearchSystemYandex(ILogger logger, string apiKey, string user)
        {
            return new YandexSearchSystem(logger, apiKey, user);
        }
    }
}
