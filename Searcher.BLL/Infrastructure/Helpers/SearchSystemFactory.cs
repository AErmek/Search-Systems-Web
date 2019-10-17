using Searcher.BLL.DTO.SearchSystems;
using Searcher.BLL.Enums;
using Searcher.BLL.Infrastructure;
using Searcher.BLL.Interfaces;
using System;

namespace Searcher.BLL.DTO
{
    public class SearchSystemFactory : IFactory<BaseSearchSystem, Options>
    {
        public BaseSearchSystem Create(Options options)
        {
            switch (options.Type)
            {
                case SearchSystemType.Yandex:
                    return new YandexSearchSystem(options.ApiKeyOptions);
                case SearchSystemType.Google:
                    return new GoogleSearchSystem(options.ApiKeyOptions);
                case SearchSystemType.Bing:
                    return new BingSearchSystem(options.ApiKeyOptions);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
