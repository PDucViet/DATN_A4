using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Model;
using DATN.Core.ViewModel.NotificationVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.TimeRangeVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class TimeRangeController : Controller
    {
        private readonly ClientService _clientService;
        public TimeRangeController(ClientService clientService)
        {
            _clientService = clientService;
        }
        public async Task<IActionResult> Index(TimeRangePaging request)
        {
            TimeRangePaging partnerPaging = new TimeRangePaging();


            partnerPaging = await _clientService.Post<TimeRangePaging>("https://localhost:7095/api/TimeRange/GetTimeRangePaging", request);

            if (partnerPaging == null)
            {
                return NotFound();
            }

            return View(partnerPaging);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TimeRangeVM timeRange)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(timeRange); // Trả về lại view với model và hiển thị lỗi
                }
                if (timeRange.MinPrice >= timeRange.MaxPrice)
                {
                    ModelState.AddModelError("MaxPrice", "Giá trị bắt đầu nhỏ hơn giá trị kết thúc");
                    return View(timeRange); // Trả về lại view với model và hiển thị lỗi
                }

                var createdTimeRange = await _clientService.Post<TimeRangeVM>("https://localhost:7095/api/TimeRange/Create", timeRange);

                if (createdTimeRange != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Lỗi khi tạo mới khoảng giá.");
                    return View(timeRange); // Trả về lại view với model và hiển thị lỗi
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và hiển thị thông báo lỗi nếu cần
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var timeRange = await _clientService.Get<TimeRangeVM>($"https://localhost:7095/api/TimeRange/GetById/{id}");

                if (timeRange == null)
                {
                    throw new Exception("Không tìm thấy khoảng giá");
                }
                return View(timeRange);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("Index");
            }


        }
        public async Task<IActionResult> Update(int id)
        {
            var timeRanges = await _clientService.Get<TimeRangeVM>($"https://localhost:7095/api/TimeRange/GetById/{id}");
            return View(timeRanges);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TimeRangeVM timeRange)
        {
            var timeRanges = await _clientService.Put<TimeRangeVM>($"https://localhost:7095/api/TimeRange/Update/{timeRange.Id}", timeRange);
            if (timeRanges != null)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
        public async Task<IActionResult> Delete(int id)
        {
            var timeRange = await _clientService.Delete<TimeRangeVM>($"https://localhost:7095/api/TimeRange/Delete/{id}");
            if (timeRange != null)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
    }
}
