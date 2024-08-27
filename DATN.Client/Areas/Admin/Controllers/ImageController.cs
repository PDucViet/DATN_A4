using AutoMapper;
using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Infrastructures;
using DATN.Core.ViewModel.ImagePath;
using DATN.Core.ViewModel.MagazineVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ImageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ClientService _clientService;
        private readonly HttpClient _httpClient;
        public ImageController(IUnitOfWork unitOfWork, IMapper mapper, ClientService clientService, HttpClient httpClient)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _clientService = clientService;
            _httpClient = httpClient;
        }
        public async Task<IActionResult> Index(ImagePaging request)
        {
            ImagePaging Paging = new ImagePaging();


            Paging = await _clientService.Post<ImagePaging>("https://localhost:7095/api/Images/GetImagePaging", request);

            if (Paging == null)
            {
                return NotFound();
            }

            return View(Paging);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var result = await _clientService.Get<ImageVM>($"https://localhost:7095/api/Images/Get/{id}");

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
        public async Task<IActionResult> Create(ImageVM image, [FromForm] List<IFormFile>? Img)
        {
            if (Img != null)
            {
                foreach(var files in Img)
                {
                    string data;
                    using (var memoryStream = new MemoryStream())
                    {
                        await files.CopyToAsync(memoryStream);
                        var arr = memoryStream.ToArray();
                        data = Convert.ToBase64String(arr);
                    }
                    var formData = new MultipartFormDataContent();
                    formData.Add(new StringContent(data), "image");


                    var result = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?key=8f7001b162097aed9ebe99a138db1050", formData);
                    var a = await result.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(a);
                    var link = jsonObject.SelectToken("data.url").ToString();
                    image.TypeId = 2;

                    image.ImagePath = link;
                    var request = await _clientService.Post<ImageVM>("https://localhost:7095/api/Images/Create", image);
                }
                
            }

                   
                return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            var request = await _clientService.Get<ImageVM>($"https://localhost:7095/api/Images/Get/{id}");
            return View(request);
        }
        public async Task<IActionResult> Update(ImageVM image, [FromForm] IFormFile? Img)
        {
            // Lấy thông tin bài viết hiện tại từ cơ sở dữ liệu
            var existingImage = await _clientService.Get<ImageVM>($"https://localhost:7095/api/Images/Get/{image.ImageId}");

            if (existingImage == null)
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

                image.ImagePath = link;
            }
            else
            {
                // Giữ lại URL ảnh cũ
                image.ImagePath = existingImage.ImagePath;
            }

            var request = await _clientService.Put<MagazineVM>($"https://localhost:7095/api/Magazine/Update/{image.ImageId}", image);

            if (request != null)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var result = await _clientService.Delete<ImageVM>($"https://localhost:7095/api/Images/Delete/{id}");
            if (result != null)
            {
                return RedirectToAction("Index");
            }
            return BadRequest();
        }
    }
}
