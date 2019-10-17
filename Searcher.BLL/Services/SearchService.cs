using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Searcher.BLL.DTO;
using Searcher.BLL.DTO.Helpers;
using Searcher.BLL.DTO.ViewModels;
using Searcher.BLL.Infrastructure;
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

        public SearchService(IUnitOfWork unitOfWork, IMapper mapper, IOptions<ApiKeyOptions> options)
        {
            _unitOfWork = unitOfWork;
            _searchResultRepository = _unitOfWork.GetRepository<SearchResult>();
            _mapper = mapper;
            _apiKeyOptions = options.Value;
        }

        public async Task<List<SearchResultDto>> Find(SearchRequestDto model)
        {

            var searchSystemHelper = new SearchSystemsHelper(new SearchSystemFactory());
            var searchSystems = searchSystemHelper.GetSearchSystems(_apiKeyOptions);

            ThreadHelper threadHelper = new ThreadHelper(searchSystems);
            var searchResults = threadHelper.FindAny(model.KeyWord);

            if (searchResults?.Count > 0)
            {
                await _searchResultRepository.AddRangeAsync(_mapper.Map<IEnumerable<SearchResult>>(searchResults));
                await _unitOfWork.SaveChangesAsync();
            }
            return searchResults;
        }

        public async Task<SearchResultViewModel> GetSearchResults(int page, string searchString, int pageSize)
        {
            #region If Search

            IQueryable<SearchResult> source = _searchResultRepository.Find(x => string.IsNullOrWhiteSpace(searchString) ? true :
            x.Snippet.Contains(searchString)
            || x.Name.Contains(searchString)
            || x.Url.Contains(searchString));

            #endregion

            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            SearchResultViewModel viewModel = new SearchResultViewModel
            {
                PageViewModel = pageViewModel,
                SearchString = searchString,
                SearchResults = _mapper.Map<List<SearchResult>, IEnumerable<SearchResultDto>>(items)
            };
            return viewModel;
        }
    }
}
