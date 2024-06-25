using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Шабашка.рф.Models;

namespace Шабашка.рф.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["ImageUrl1"] = "/images/1.jpg";
            ViewData["ImageUrl2"] = "/images/рефералка.jpg";
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["ImageUrl"] = "/images/чмоня.jpg";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}