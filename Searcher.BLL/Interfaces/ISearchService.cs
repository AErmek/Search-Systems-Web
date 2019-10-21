using Searcher.BLL.DTO;
using Searcher.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Searcher.BLL.Interfaces
{
    public interface ISearchService
    {
        Task<List<SearchResultDto>> Find(SearchRequestDto model);
        IQueryable<SearchResult> GetSearchResults(string searchString);
    }
}
