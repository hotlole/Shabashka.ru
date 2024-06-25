using Microsoft.AspNetCore.Mvc;

namespace Шабашка.рф.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
