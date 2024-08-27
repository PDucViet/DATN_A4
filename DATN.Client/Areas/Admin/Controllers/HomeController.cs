using DATN.Client.Constants;
using DATN.Client.Services;
using DATN.Core.ViewModel.StatisticAdminVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Areas.Admin.Controllers
{
    //[Authorize(Roles ="Admin")]
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ClientService _clientService;
        public HomeController(ClientService clientService)
        {
            _clientService = clientService;
        }
        public async Task<IActionResult> Index(DateTime? date)
        {
            if (date == null)
            {
                date = DateTime.Now;
            }
            var formattedDate = date.Value.ToString("yyyy-MM-ddTHH:mm:ss");
            var encodedDate = Uri.EscapeDataString(formattedDate);
            var data = await _clientService.Get<StatisticAdminDasbroadVM>($"{ApiPaths.Statistic}/GetStatisticAdminDasbroad?data={encodedDate}");
            ViewBag.RevenueByDay = data.RevenueByDay;
            ViewBag.RevenueByMonth = data.RevenueByMonth;
            ViewBag.TopsellingProductInMonth = data.TopsellingProductInMonth;
            ViewBag.TopsellingProductInWeek = data.TopsellingProductInWeek;
            ViewBag.IncomeByMonth = data.IncomeByMonth;
            ViewBag.IncomeByDay = data.IncomeByDay;
            ViewBag.AppGrowthByMonth = data.AppGrowthByMonth;
            ViewBag.AppGrowthByDay = data.AppGrowthByDay;

            return View();
        }
    }
}
