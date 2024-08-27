using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Controllers
{
    public class NewFeedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail()
        {
            return View();
        }
    }
}
