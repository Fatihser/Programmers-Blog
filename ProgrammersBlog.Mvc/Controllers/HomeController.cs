using Microsoft.AspNetCore.Mvc;

namespace ProgrammersBlog.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
