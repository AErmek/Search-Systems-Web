using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Searcher.BLL.DTO;
using Searcher.BLL.Interfaces;
using Searcher.Models;
using System.Threading.Tasks;

namespace Searcher.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IMapper _mapper;
        public SearchController(ISearchService service, IMapper mappper)
        {
            _mapper = mappper;
            _searchService = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List(string searchString, int page = 1)
        {
            int pageSize = 10;
            return View(await _searchService.GetSearchResults(searchString).ToPageList(_mapper, page, pageSize, searchString));
        }

        [HttpPost]
        public async Task<ActionResult> ShowResults(SearchRequestDto searchRequest)
        {
            return View(await _searchService.Find(searchRequest));
        }
    }
}