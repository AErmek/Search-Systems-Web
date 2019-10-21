using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Searcher.BLL.DTO;
using Searcher.DAL.Entities;
using Searcher.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Searcher.Models
{
    public static class ModelHelper
    {
        public static async Task<SearchResultViewModel> ToPageList(this IQueryable<SearchResult> source, IMapper mapper, int page, int pageSize, string searchString)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            SearchResultViewModel viewModel = new SearchResultViewModel
            {
                PageViewModel = pageViewModel,
                SearchString = searchString,
                SearchResults = mapper.Map<List<SearchResult>, IEnumerable<SearchResultDto>>(items)
            };
            return viewModel;
        }
    }
}
