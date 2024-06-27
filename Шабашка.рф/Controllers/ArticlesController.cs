using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Шабашка.рф.Controllers
{
    public class ArticlesController : Controller
    {
        public ActionResult Main()
        {
            ViewData["ImageUrl"] = "/images/Обезьяна_думает.jpg";
            ViewData["ImageUrl2"] = "/images/Art2.jpg";
			ViewData["ImageUrl3"] = "/images/Art3.jpg";
			return View();
        }

        public ActionResult Art1()
        {
            return View();
        }

        public ActionResult Art2()
        {
            return View();
        }
        public ActionResult Art3()
        {
            return View();
        }

    }
}
