using Searcher.BLL.DTO;
using Searcher.BLL.DTO.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Searcher.BLL.Interfaces
{
    public interface ISearchService
    {
        Task<List<SearchResultDto>> Find(SearchRequestDto model);
        Task<SearchResultViewModel> GetSearchResults(int page, string searchString, int pageSize);
    }
}
