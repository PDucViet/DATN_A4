using AutoMapper;
using Azure.Core;
using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Infrastructures;
using DATN.Core.ViewModel.MagazineVM;
using DATN.Core.ViewModel.NotificationVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class MagazineController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ClientService _clientService;
        private readonly HttpClient _httpClient;
        public MagazineController(IUnitOfWork unitOfWork, IMapper mapper, ClientService clientService, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clientService = clientService;
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index(MagazinePaging request)
        {
            MagazinePaging Paging = new MagazinePaging();
            try
            {
                Paging = await _clientService.Post<MagazinePaging>("https://localhost:7095/api/Magazine/GetMagazinePaging", request);

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
                var result = await _clientService.Get<MagazineVM>($"https://localhost:7095/api/Magazine/Get/{id}");

                if (result == null)
                {
                    throw new Exception("Không tìm thấy");
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
        public async Task<IActionResult> Create(MagazineVM magazineVM, [FromForm] IFormFile? Img)
        {
            try
            {
                if (Img != null)
                {
                    string data;
                    using (var memoryStream = new MemoryStream())
                    {
                        await Img.CopyToAsync(memoryStream);
                        var arr = memoryStream.ToArray();
                        data = Convert.ToBase64String(arr);
                    }
                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(data), "image");


                    var result = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?key=8f7001b162097aed9ebe99a138db1050", formData);
                    var a = await result.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(a);
                    var link = jsonObject.SelectToken("data.url").ToString();

                    magazineVM.Image = link;
                }
                if (!ModelState.IsValid)
                {
                    return View(magazineVM); // trả về lại view với model và hiển thị lỗi
                }
                var results = await _clientService.Post<MagazineVM>("https://localhost:7095/api/Magazine/Create", magazineVM);
                if (results != null)
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
                var originlst = await _clientService.Get<MagazineVM>($"https://localhost:7095/api/Magazine/Get/{id}");
                return View(originlst);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("Index");
            }
        }
        public async Task<IActionResult> Update1(MagazineVM magazine, [FromForm] IFormFile? Img)
        {
            try
            {
                // Lấy thông tin bài viết hiện tại từ cơ sở dữ liệu
                var existingMagazine = await _clientService.Get<MagazineVM>($"https://localhost:7095/api/Magazine/Get/{magazine.MagazineId}");

                if (existingMagazine == null)
                {
                    return NotFound();
                }
                if (Img != null)
                {
                    string data;
                    using (var memoryStream = new MemoryStream())
                    {
                        await Img.CopyToAsync(memoryStream);
                        var arr = memoryStream.ToArray();
                        data = Convert.ToBase64String(arr);
                    }
                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(data), "image");


                    var result = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?key=8f7001b162097aed9ebe99a138db1050", formData);
                    var a = await result.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(a);
                    var link = jsonObject.SelectToken("data.url").ToString();

                    magazine.Image = link;
                }
                else
                {
                    // Giữ lại URL ảnh cũ
                    magazine.Image = existingMagazine.Image;
                }

                var originlst = await _clientService.Put<MagazineVM>($"https://localhost:7095/api/Magazine/Update/{magazine.MagazineId}", magazine);

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

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _clientService.Delete<MagazineVM>($"https://localhost:7095/api/Magazine/Delete/{id}");
                if (result != null)
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
