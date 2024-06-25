using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Шабашка.рф.Controllers
{
    public class RefferalController : Controller
    {
        // GET: RefferalController
        public ActionResult Index()
        {
            ViewData["ImageUrl"] = "/images/обезьяна.jpg";
            return View();
        }

    }
}
