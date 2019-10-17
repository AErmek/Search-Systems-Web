using Microsoft.AspNetCore.Mvc;
using Searcher.BLL.DTO;
using Searcher.BLL.Interfaces;
using System.Threading.Tasks;

namespace Searcher.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        public SearchController(ISearchService service)
        {
            _searchService = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List(string searchString, int page = 1)
        {
            int elementsCountInPage = 10;
            return View(await _searchService.GetSearchResults(page, searchString, elementsCountInPage));
        }

        [HttpPost]
        public async Task<ActionResult> ShowResults(SearchRequestDto searchRequest)
        {
            return View(await _searchService.Find(searchRequest));
        }
    }
}