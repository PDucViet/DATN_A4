using AutoMapper;
using Azure.Core;
using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class VoucherController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ClientService _clientService;
        private readonly HttpClient _httpClient;
        private readonly DATNDbContext _dbContext;
        public VoucherController(IUnitOfWork unitOfWork, IMapper mapper, ClientService clientService, HttpClient httpClient, DATNDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clientService = clientService;
            _httpClient = httpClient;
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index(VoucherPaging request)
        {
            VoucherPaging Paging = new VoucherPaging();

            try
            {
                Paging = await _clientService.Post<VoucherPaging>("https://localhost:7095/api/Voucher/GetVoucherPaging", request);

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
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var voucher = await _clientService.Get<VoucherVM>($"https://localhost:7095/api/Voucher/Get/{id}");

                if (voucher == null)
                {
                    throw new Exception("Không tìm thấy");
                }
                return View(voucher);
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
        public async Task<IActionResult> Create(VoucherVM voucher)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(voucher); // Trả về lại view với model và hiển thị lỗi
                }

               var result= await _clientService.Post<VoucherVM>("https://localhost:7095/api/Voucher/Create", voucher);
                if(result != null)
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
              
                var lstVoucher = await _clientService.Get<VoucherVM>($"https://localhost:7095/api/Voucher/Get/{id}");
                return View(lstVoucher);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Update1(VoucherVM voucher)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Update",voucher); // Trả về lại view với model và hiển thị lỗi

                }
                var result = await _clientService.Put<VoucherVM>($"https://localhost:7095/api/Voucher/Update/{voucher.Id}", voucher);
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
                var voucher = await _clientService.Delete<VoucherVM>($"https://localhost:7095/api/Voucher/Delete/{id}");
                if (voucher != null)
                {
                    return RedirectToAction("Index");
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("Index");
            }

        }
    }
}
