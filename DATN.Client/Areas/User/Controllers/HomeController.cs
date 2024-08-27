using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
