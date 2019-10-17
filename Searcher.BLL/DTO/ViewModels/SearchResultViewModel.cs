using System.Collections.Generic;

namespace Searcher.BLL.DTO.ViewModels
{
    public class SearchResultViewModel
    {
        public IEnumerable<SearchResultDto> SearchResults { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string SearchString { get; set; }
    }
}
