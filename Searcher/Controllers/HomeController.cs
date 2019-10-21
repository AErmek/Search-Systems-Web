using Microsoft.AspNetCore.Mvc;

namespace Searcher.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}