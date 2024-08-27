using AutoMapper;
using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.NotificationVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class LanguageController : Controller
    {
      
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ClientService _clientService;
        private readonly HttpClient _httpClient;
        private readonly DATNDbContext _dbContext;
        public LanguageController(IUnitOfWork unitOfWork, IMapper mapper, ClientService clientService, HttpClient httpClient,DATNDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clientService = clientService;
            _httpClient = httpClient;
            _dbContext = dbContext; 
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
           var partnerPaging = await _clientService.GetList<LanguageVM>("https://localhost:7095/api/Language/GetAll");

            if (partnerPaging == null)
            {
                return NotFound();
            }
            return View(partnerPaging);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var partner = await _clientService.Get<LanguageVM>($"https://localhost:7095/api/Language/Get/{id}");

                if (partner == null)
                {
                    throw new Exception("Không tìm thấy nhà đồng hành");
                }
                return View(partner);
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
        public async Task<IActionResult> Create(LanguageVM origin)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(origin); // Trả về lại view với model và hiển thị lỗi
                }
               
                 await _clientService.Post<LanguageVM>("https://localhost:7095/api/Language/Create", origin);



            }
            catch (Exception ex)
            {

                // Xử lý lỗi và hiển thị thông báo lỗi nếu cần
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            ToastHelper.ShowSuccess(TempData, "Thêm nhà đồng hành thành công!");
            return RedirectToAction("Index");




        }

        public async Task<IActionResult> Update(int id)
        {
            var originlst = await _clientService.Get<LanguageVM>($"https://localhost:7095/api/Language/Get/{id}");
            return View(originlst);
        }
        [HttpPost]
        public async Task<IActionResult> Update1(LanguageVM origin)
        {
            var originlst = await _clientService.Put<LanguageVM>($"https://localhost:7095/api/Language/Update/{origin.Id}", origin);
            if (originlst != null)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var originlst = await _clientService.Delete<LanguageVM>($"https://localhost:7095/api/Language/Delete/{id}");
            if (originlst != null)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
    }
}
