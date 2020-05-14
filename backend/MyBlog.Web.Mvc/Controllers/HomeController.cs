using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
