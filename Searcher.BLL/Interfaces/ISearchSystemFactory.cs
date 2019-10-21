using Microsoft.Extensions.Logging;

namespace Searcher.BLL.Interfaces
{
    public interface ISearchSystemFactory
    {
        ISearchSystem InitSearchSystemYandex(ILogger logger, string apiKey, string user);
        ISearchSystem InitSearchSystemGoogle(ILogger logger, string apiKey, string cx);
        ISearchSystem InitSearchSystemBing(ILogger logger, string apiKey);
    }
}
