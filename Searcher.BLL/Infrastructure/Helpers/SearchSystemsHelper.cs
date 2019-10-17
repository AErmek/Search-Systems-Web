using Searcher.BLL.DTO.SearchSystems;
using Searcher.BLL.Enums;
using Searcher.BLL.Infrastructure;
using Searcher.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Searcher.BLL.DTO.Helpers
{
    public class SearchSystemsHelper
    {
        private readonly IFactory<BaseSearchSystem, Options> _searchSystemFactory;

        public SearchSystemsHelper(IFactory<BaseSearchSystem, Options> factory)
        {
            _searchSystemFactory = factory;
        }

        public List<BaseSearchSystem> GetSearchSystems(ApiKeyOptions apiKeyOptions)
        {
            var searchSystemTypes = Enum.GetValues(typeof(SearchSystemType)).Cast<SearchSystemType>();

            var searchSystems = new List<BaseSearchSystem>();
            foreach (var type in searchSystemTypes)
            {
                searchSystems.Add(_searchSystemFactory.Create(new Options(type, apiKeyOptions)));
            }
            return searchSystems;
        }
    }
}
