using Microsoft.AspNetCore.Mvc;

namespace Searcher.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}