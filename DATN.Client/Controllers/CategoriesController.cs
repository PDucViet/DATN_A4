using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.CategoryVM;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DATN.Client.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [HttpGet]
        public async Task< IActionResult> Index(int cateId,int?subCateId)
        {
            // Gọi API để lấy danh sách danh mục từ Backend
            var response = await _httpClient.GetAsync($"https://localhost:7095/api/Category/GetAll");
            response.EnsureSuccessStatusCode();

            // Đọc nội dung phản hồi từ API
            var responseContent = await response.Content.ReadAsStringAsync();

            // Truyển danh sách danh mục sang View
            var cates = JsonConvert.DeserializeObject<List<CategoryVM>>(responseContent);
            ViewBag.Categories = cates ?? new List<CategoryVM>();

            // Gán giá trị cateId vào ViewBag nếu có
            ViewBag.cateId = cateId;
            if (subCateId != null)
            {
                ViewBag.subCateId = subCateId;
            }
            else
            {
                ViewBag.subCateId = 0;
            }

            return View();
        }
        
        
    }
}
