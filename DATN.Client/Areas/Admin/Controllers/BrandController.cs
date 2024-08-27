using AutoMapper;
using Azure.Core;
using DATN.Client.Constants;
using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.AndressVM;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.MagazineVM;
using DATN.Core.ViewModel.OriginVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;

namespace DATN.Client.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
	public class BrandController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ClientService _clientService;
		private readonly HttpClient _httpClient;


		// private readonly ClientService _clientService;
		public BrandController(ClientService _client, HttpClient httpClient )
		{
			_clientService = _client;
			_httpClient = httpClient;
		}
		public async Task<IActionResult> Index(BrandPaging request)
		{
			BrandPaging partnerPaging = new BrandPaging();


			partnerPaging = await _clientService.Post<BrandPaging>("https://localhost:7095/api/Brand/brandPaging", request);

			if (partnerPaging == null)
			{
				return NotFound();
			}

			return View(partnerPaging);


		}

		public IActionResult Creat() // Tạo view rỗng
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Creat(BrandCreatVM request, [FromForm] IFormFile? Img)
		{
			try
			{
				if (ModelState.IsValid)
				{
                    string requestURL = $"https://localhost:7095/api/Brand/Create";
                    var HttpClient = new HttpClient();
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


                        var result = await HttpClient.PostAsync($"https://api.imgbb.com/1/upload?key=8f7001b162097aed9ebe99a138db1050", formData);
                        var a = await result.Content.ReadAsStringAsync();
                        JObject jsonObject = JObject.Parse(a);
                        var link = jsonObject.SelectToken("data.url").ToString();

                        request.ImageUrl = link;

                    }

                    var response = await HttpClient.PostAsJsonAsync(requestURL, request);
                    ToastHelper.ShowSuccess(TempData, "Thêm  thành thành công!");
                    return RedirectToAction("Index");

                }
				return View(request);

			}
			catch (Exception ex)
			{
                ModelState.AddModelError("", "There was an error creating the user. Please try again later.");
                ToastHelper.ShowError(TempData, ex.Message);

                return View(request);
               
			}

			

		}
		[HttpGet]
		public async Task<IActionResult> Edit(string id) // Tlấy view rỗng từ Sv vừa chọn
		{


			var BrandLst = await _clientService.Get<BrandCreatVM>($"https://localhost:7095/api/Brand/GetById/{id}");
			return View(BrandLst);

		}


		public async Task<IActionResult> EditBrand(BrandCreatVM request, [FromForm] IFormFile? Img)
		{
			// Lấy thông tin bài viết hiện tại từ cơ sở dữ liệu
			var existingBrand = await _clientService.Get<BrandCreatVM>($"https://localhost:7095/api/Brand/GetById/{request.BrandId}");
			if (existingBrand == null)
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
				request.ImageUrl = link;
			}
			else
			{
				// Giữ lại URL ảnh cũ
				request.ImageUrl = existingBrand.ImageUrl;
			}
			var obj = await _clientService.Put<BrandCreatVM>($"https://localhost:7095/api/Brand/Update/{request.BrandId}", request);
			return RedirectToAction("Index");

		}

		public async Task<IActionResult> Details(int id)
		{
			var result = await _clientService.Get<BrandVM>($"https://localhost:7095/api/Brand/GetById/{id}");
			return View(result);

		}

        public async Task<IActionResult> Delete(int id)// Thực hiện lấy data từ view và sửa trong csdl
        {

            var result = await _clientService.Delete<CategoryVM>($"https://localhost:7095/api/Category/Delete?id={id}");
            if (result != null)
            {
                return RedirectToAction("Index");
            }

            else { return BadRequest(); }

        }
    }



}