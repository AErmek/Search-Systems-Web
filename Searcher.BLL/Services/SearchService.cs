using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Searcher.BLL.DTO;
using Searcher.BLL.Infrastructure;
using Searcher.BLL.Infrastructure.Helpers;
using Searcher.BLL.Interfaces;
using Searcher.DAL.Entities;
using Searcher.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Searcher.BLL.Services
{
    public class SearchService : ISearchService
    {
        private readonly IGenericRepository<SearchResult> _searchResultRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApiKeyOptions _apiKeyOptions;
        private readonly ILogger<SearchService> _logger;

        public SearchService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<ApiKeyOptions> options, ILogger<SearchService> logger)
        {
            _unitOfWork = unitOfWork;
            _searchResultRepository = _unitOfWork.GetRepository<SearchResult>();
            _mapper = mapper;
            _apiKeyOptions = options.Value;
            _logger = logger;
        }

        public async Task<List<SearchResultDto>> Find(SearchRequestDto model)
        {
            var searchSystems = GetSearchSystems(_apiKeyOptions);

            SearchExecuter searchExecuter = new SearchExecuter(searchSystems);
            var searchResults = searchExecuter.ExecuteSearch(model.KeyWord);

            if (searchResults?.Count > 0)
            {
                await _searchResultRepository.AddRangeAsync(_mapper.Map<IEnumerable<SearchResult>>(searchResults));
                await _unitOfWork.SaveChangesAsync();
            }

            return searchResults;
        }

        public IQueryable<SearchResult> GetSearchResults(string searchString)
        {
            IQueryable<SearchResult> source = _searchResultRepository.Find(x => string.IsNullOrWhiteSpace(searchString) ? true :
            x.Snippet.Contains(searchString)
            || x.Name.Contains(searchString)
            || x.Url.Contains(searchString));

            return source;
        }

        private List<ISearchSystem> GetSearchSystems(ApiKeyOptions apiKeyOptions)
        {
            ISearchSystemFactory searchSystemFactory = new SearchSystemFactory();
            var searchSystems = new List<ISearchSystem> {
                searchSystemFactory.InitSearchSystemYandex(_logger,apiKeyOptions.YandexKey,apiKeyOptions.YandexUser),
                searchSystemFactory.InitSearchSystemGoogle(_logger,apiKeyOptions.GoogleKey,apiKeyOptions.GoogleCx),
                searchSystemFactory.InitSearchSystemBing(_logger,apiKeyOptions.BingKey)
            };
            return searchSystems;
        }
    }
}
