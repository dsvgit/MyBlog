using Microsoft.AspNetCore.Mvc;

namespace MyBlog.Web.Mvc.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
