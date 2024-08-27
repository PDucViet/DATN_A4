using AutoMapper;
using Azure;
using DATN.Client.Constants;
using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.NotificationVM;
using DATN.Core.ViewModel.OriginVM;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class NotificationController : Controller

    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ClientService _clientService;
        private readonly HttpClient _httpClient;
        public NotificationController(IUnitOfWork unitOfWork, IMapper mapper,ClientService clientService, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clientService = clientService;
            _httpClient = httpClient;
        }
  
        public async Task<IActionResult> Index(NotificationPaging request)
        {
            NotificationPaging paging = new NotificationPaging();
            try
            {
                paging = await _clientService.Post<NotificationPaging>("https://localhost:7095/api/Notification/GetNotificationPaging", request);

                if (paging == null)
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }

            return View(paging);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var result = await _clientService.Get<NotificationVM>($"https://localhost:7095/api/Notification/Get/{id}");

                if (result == null)
                {
                    throw new Exception("Không tìm thấy nhà đồng hành");
                }
                return View(result);
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
        public async Task<IActionResult> Create(NotificationVM notificationVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(notificationVM); // Trả về lại view với model và hiển thị lỗi
                }

                var result = await _clientService.Post<NotificationVM>("https://localhost:7095/api/Notification/Create", notificationVM);
                if (result != null)
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

        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var result = await _clientService.Get<NotificationVM>($"https://localhost:7095/api/Notification/Get/{id}");
                return View(result);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Update1(NotificationVM notificationVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(notificationVM); // Trả về lại view với model và hiển thị lỗi

                }
                var result = await _clientService.Put<NotificationVM>($"https://localhost:7095/api/Notification/Update/{notificationVM.NotificationId}", notificationVM);
                if (result != null)
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
 
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var originlst = await _clientService.Delete<NotificationVM>($"https://localhost:7095/api/Notification/Delete/{id}");
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
