using DATN.Client.Constants;
using DATN.Client.Services;
using DATN.Core.ViewModel.MagazineVM;
using DATN.Core.ViewModel.PromotionVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DATN.Client.Controllers
{
    public class MagazineController : Controller
    {
        private readonly ClientService _clientService;

        public MagazineController(ClientService clientService)
        {
            _clientService = clientService;
        }
        public async Task<IActionResult> Index()
        {
            var result = new List<MagazineVM>();
            var data = await _clientService.GetList<MagazineVM>($"{ApiPaths.Magazine}/GetAllActive");

            if ( data != null )
            {
                result = data.OrderByDescending(p=>p.CreateAt).ToList();
            }
            return View(result);
        }
        //[HttpGet]
        //public async Task<IActionResult> GetPromotions()
        //{
        //    try
        //    {
        //        var promotions = await _clientService.GetList<PromotionVM>($"{ApiPaths.Promotion}/GetAllActive");
        //        if (promotions == null)
        //        {
        //            ViewBag.ErrorMessage = "Hiện tại chưa có trương trình khuyến mãi nào!";
        //            return View(new List<PromotionVM>());
        //        }
        //        return View(promotions);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.ErrorMessage = $"Lỗi khi tải danh sách khuyến mãi: {ex.Message}";
        //        return View(new List<PromotionVM>());
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> Detail(int magazineId)
        {
            try
            {
                // Lấy chi tiết của chương trình khuyến mãi hiện tại
                var magazine = await _clientService.Get<MagazineVM>($"{ApiPaths.Magazine}/GetById?magazineId={magazineId}");

                if (magazine == null)
                {
                    return NotFound("Không tìm thấy chương trình.");
                }

                // Lấy danh sách các khuyến mãi trừ khuyến mãi hiện tại
                var magazines = await _clientService.GetList<MagazineVM>($"{ApiPaths.Magazine}/GetAllActive");
                var otherMagazines = magazines.Where(p => p.MagazineId != magazineId).OrderByDescending(p=>p.CreateAt).ToList();

                // Lưu danh sách này vào ViewBag để sử dụng trong View
                ViewBag.OtherMagazines = otherMagazines;

                return View(magazine);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Lỗi khi tải chi tiết chương trình: {ex.Message}";
                return View(new MagazineVM());
            }
        }
    }
}
