using Azure.Core;
using DATN.Client.Constants;
using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Model;
using DATN.Core.ViewModel.AndressVM;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.TimeRangeVM;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;

namespace DATN.Client.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly ClientService _clientService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ClientService _client, ILogger<CategoryController> logger)
        {
            _clientService = _client;
            _logger = logger;
        }
        //public async Task<IActionResult> Index()
        //{
        //    var result = await _clientService.GetList<CategoryVM>("https://localhost:7095/api/Category/GetAll_Admin");
        //    return View(result);
        //}

        public async Task<IActionResult> Index(CategoryPaging request)
        {
            CategoryPaging partnerPaging = new CategoryPaging();


            partnerPaging = await _clientService.Post<CategoryPaging>("https://localhost:7095/api/Category/GetCategoryPaging", request);

            if (partnerPaging == null)
            {
                return NotFound();
            }

            return View(partnerPaging);
        }
        public async Task<IActionResult> CategoryLv1(CategoryPagingLv1 request)
        {
            CategoryPagingLv1 partnerPaging = new CategoryPagingLv1();
            partnerPaging = await _clientService.Post<CategoryPagingLv1>("https://localhost:7095/api/Category/GetCategoryPagingLv1", request);

            if (partnerPaging == null)
            {
                return NotFound();
            }

            return View(partnerPaging);
        }

        public async Task<IActionResult> CategoryLv2(CategoryPagingLv2 request)
        {
            CategoryPagingLv2 partnerPaging = new CategoryPagingLv2();
            partnerPaging = await _clientService.Post<CategoryPagingLv2>("https://localhost:7095/api/Category/GetCategoryPagingLv2", request);

            if (partnerPaging == null)
            {
                return NotFound();
            }

            return View(partnerPaging);
        }
        public async Task<IActionResult> Details(int id)
        {
            var result = await _clientService.Get<CategoryVM>($"https://localhost:7095/api/Category/GetById/get-by-id?id={id}");
            return View(result);

        }
        public async Task<IActionResult> DetailsLv1(int id)
        {
            var result = await _clientService.Get<CategoryVM>($"https://localhost:7095/api/Category/GetById/get-by-id?id={id}");
            return View(result);

        }
        public async Task<IActionResult> DetailsLv2(int id)
        {
            var result = await _clientService.Get<CategoryVM>($"https://localhost:7095/api/Category/GetById/get-by-id?id={id}");
            return View(result);

        }

        public IActionResult Creat() // Tạo view rỗng
        {
            return View();
        }

        // thêm mới Lv0
        [HttpPost]
        public async Task<IActionResult> Creat(CategoryAdminVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    obj.Level = 0;
                    var result = await _clientService.Post<CategoryAdminVM>("https://localhost:7095/api/Category/CreateLv0", obj);
                    if (result != null)
                    {
                        ToastHelper.ShowSuccess(TempData, "Thêm  thành thành công!");
                    }
                    return RedirectToAction("Index");

                }
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                
            }
            return RedirectToAction("Index");
        }

        // thêm mới Lv1
        public async Task<IActionResult> CreateLv1() // Tạo view rỗng
        {
            var ListParentIdLv0 = await _clientService.Get<List<Category>>($"{ApiPaths.Category}/GetAll_Admin");
            ViewBag.CategoryLv0 = ListParentIdLv0;

            var ListRanger = await _clientService.Get<List<TimeRangeVM>>($"{ApiPaths.TimeRange}/GetAll");
            ViewBag.ListRangerName = ListRanger;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLv1(CategoryAdminCreatLv obj)
        {           
            try
            {
                obj.Level = 1;
                if (ModelState.IsValid)
                {                   
                    string requestURL = $"https://localhost:7095/api/Category/CreateLv1";
                    var HttpClient = new HttpClient();
                    var response = await HttpClient.PostAsJsonAsync(requestURL, obj);
                    ToastHelper.ShowSuccess(TempData, "Thêm  thành công!");                
                }
                else
                {
                    return RedirectToAction("CreateLv1");
                }
				
			}
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
               

            }
			return RedirectToAction("CategoryLv1");

		}

        // thêm mới Lv2
        public async Task<IActionResult> CreateLv2() // Tạo view rỗng
        {
            var ListParentIdLv1 = await _clientService.Get<List<Category>>($"{ApiPaths.Category}/GetAllCategoryLevel1");
            ViewBag.CategoryLv1 = ListParentIdLv1;
            var ListParentIdLv0 = await _clientService.Get<List<Category>>($"{ApiPaths.Category}/GetAll_Admin");
            ViewBag.CategoryLv0 = ListParentIdLv0;
            var ListRanger = await _clientService.Get<List<TimeRangeVM>>($"{ApiPaths.TimeRange}/GetAll");
            ViewBag.ListRangerName = ListRanger;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> CreateLv2(CategoryAdminCreatLv obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    obj.Level = 2;
                    string requestURL = $"https://localhost:7095/api/Category/CreateLv2";
                    var HttpClient = new HttpClient();
                    var response = await HttpClient.PostAsJsonAsync(requestURL, obj);
                    ToastHelper.ShowSuccess(TempData, "Thêm  thành công!");
                  

                }
                else
                {
					return RedirectToAction("CreateLv2");
				}

               

            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
              
            }
			return RedirectToAction("CategoryLv2");

		}
        public async Task<IActionResult> Edit(string id) // Tlấy view rỗng từ Sv vừa chọn
        {
            //// vìa là sửa nên chúng ta cần truyền được dữ liệu của sv sang
            string requestURL = $"https://localhost:7095/api/Category/GetById/get-by-id?id={id}";
            var HttpClient = new HttpClient();

            var response = await HttpClient.GetAsync(requestURL); // Truyền cả obj theo requets url
            string apiData = await response.Content.ReadAsStringAsync();
            var sp = JsonConvert.DeserializeObject<CategoryVM>(apiData);
            return View(sp); // truyền list vừa nhận được sang view
        }
        public async Task<IActionResult> EditCategory(CategoryVM category)// Thực hiện lấy data từ view và sửa trong csdl
        {
            category.Level = 0;
            var result = await _clientService.Put<CategoryVM>($"https://localhost:7095/api/Category/Update/edit-category", category);
            if (result != null)
            {
                return RedirectToAction("Index");
            }

            else { return BadRequest(); }
        }

        public async Task<IActionResult> EditLv1(string id) // Tlấy view rỗng từ Sv vừa chọn
        {
            try
            {
                //// vìa là sửa nên chúng ta cần truyền được dữ liệu của sv sang
                string requestURL = $"https://localhost:7095/api/Category/GetById/get-by-id?id={id}";
                var HttpClient = new HttpClient();

                var response = await HttpClient.GetAsync(requestURL); // Truyền cả obj theo requets url
                string apiData = await response.Content.ReadAsStringAsync();
                var sp = JsonConvert.DeserializeObject<CategoryEditAdminLv>(apiData);
                var ListRanger = await _clientService.Get<List<TimeRangeVM>>($"{ApiPaths.TimeRange}/GetAll");
                ViewBag.ListRangerName = ListRanger;
                return View(sp); // truyền list vừa nhận được sang view
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return BadRequest();
            }


        }
        public async Task<IActionResult> EditCategoryLv1(CategoryEditAdminLv category)// Thực hiện lấy data từ view và sửa trong csdl
        {
            try
            {
                category.Level = 1;
                var result = await _clientService.Put<CategoryEditAdminLv>($"https://localhost:7095/api/Category/UpdateLv/edit-category-lv", category);
                if (result != null)
                {
                    return RedirectToAction("CategoryLv1");
                }
                return RedirectToAction("CategoryLv1");

            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("CategoryLv1");

            }


        }

        public async Task<IActionResult> EditLv2(string id) // Tlấy view rỗng từ Sv vừa chọn
        {

            //// vìa là sửa nên chúng ta cần truyền được dữ liệu của sv sang
            string requestURL = $"https://localhost:7095/api/Category/GetById/get-by-id?id={id}";
            var HttpClient = new HttpClient();

            var response = await HttpClient.GetAsync(requestURL); // Truyền cả obj theo requets url
            string apiData = await response.Content.ReadAsStringAsync();
            var ListRanger = await _clientService.Get<List<TimeRangeVM>>($"{ApiPaths.TimeRange}/GetAll");
            ViewBag.ListRangerName = ListRanger;
            var sp = JsonConvert.DeserializeObject<CategoryEditAdminLv>(apiData);
            return View(sp); // truyền list vừa nhận được sang view
        }
        public async Task<IActionResult> EditCategoryLv2(CategoryEditAdminLv category)// Thực hiện lấy data từ view và sửa trong csdl
        {
            try
            {
                category.Level = 2;
                var result = await _clientService.Put<CategoryEditAdminLv>($"https://localhost:7095/api/Category/UpdateLv/edit-category-lv", category);
                if (result != null)
                {
                    return RedirectToAction("CategoryLv2");
                }
                else
                {
                    return RedirectToAction("CategoryLv2");
                }


            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("CategoryLv2");

            }


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

        public async Task<IActionResult> DeleteLv1(int id)// Thực hiện lấy data từ view và sửa trong csdl
        {

            var result = await _clientService.Delete<CategoryVM>($"https://localhost:7095/api/Category/Delete?id={id}");
            if (result != null)
            {
                return RedirectToAction("CategoryLv1");
            }

            else { return BadRequest(); }

        }
        public async Task<IActionResult> DeleteLv2(int id)// Thực hiện lấy data từ view và sửa trong csdl
        {

            var result = await _clientService.Delete<CategoryVM>($"https://localhost:7095/api/Category/Delete?id={id}");
            if (result != null)
            {
                return RedirectToAction("CategoryLv2");
            }

            else { return BadRequest(); }

        }

    }
}
