using Azure;
using Azure.Core;
using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Model;
using DATN.Core.ViewModel.OriginVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class OriginController : Controller
    {
        private readonly ClientService _clientService;
        public OriginController(ClientService clientService)
        {
            _clientService = clientService;
        }
        public async Task<IActionResult> Index(OriginPaging request)
        {
            OriginPaging Paging = new OriginPaging();

            try
            {
                Paging = await _clientService.Post<OriginPaging>("https://localhost:7095/api/Origin/GetOriginPaging", request);

                if (Paging == null)
                {
                    return NotFound();
                }            
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return View(Paging);

        }
        [HttpGet]
        public async Task<IActionResult> Detail(OriginVM originVM)
        {
            try
            {
                var originlst = await _clientService.Get<OriginVM>($"https://localhost:7095/api/Origin/Get/{originVM.Id}");
                if (originlst == null)
                {
                    throw new Exception("Không tìm thấy");
                }
                return View(originlst);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("Index");
            }

          
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(OriginVM originVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(originVM); // Trả về lại view với model và hiển thị lỗi
                }

                var originlst = await _clientService.Post<OriginVM>("https://localhost:7095/api/Origin/Create", originVM);
                if (originlst != null)
                {
                    ToastHelper.ShowSuccess(TempData, "Thêm thành công!");
                }
            }
            catch (Exception ex)
            {

                // Xử lý lỗi và hiển thị thông báo lỗi nếu cần
                TempData["Error"] = ex.Message;

            }
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Update(OriginVM originVM)
        {
            try
            {
                var originlst = await _clientService.Get<OriginVM>($"https://localhost:7095/api/Origin/Get/{originVM.Id}");
                return View(originlst);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Update1(OriginVM originVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(originVM); // Trả về lại view với model và hiển thị lỗi
                }
                var originlst = await _clientService.Put<OriginVM>($"https://localhost:7095/api/Origin/Update/{originVM.Id}", originVM);
                if (originlst != null)
                {
                    ToastHelper.ShowSuccess(TempData, "Sửa thành công!");
                }
            }
            catch (Exception ex)
            {

                // Xử lý lỗi và hiển thị thông báo lỗi nếu cần
                TempData["Error"] = ex.Message;

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(OriginVM origin)
        {
            try
            {
                var originlst = await _clientService.Delete<OriginVM>($"https://localhost:7095/api/Origin/Delete/{origin.Id}");
                if (originlst != null)
                {
                    ToastHelper.ShowSuccess(TempData, "Xóa thành công!");
                }
            }

            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
              
            }
            return RedirectToAction("Index");

}

    }
}
