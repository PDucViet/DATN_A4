using System.Diagnostics.SymbolStore;
using AutoMapper;
using DATN.Client.Constants;
using DATN.Client.Helper;
using DATN.Client.Models;
using DATN.Client.Services;
using DATN.Core.Infrastructures;
using DATN.Core.Models;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModels.Paging;
using DATN.Core.ViewModels.SendMailVM;
using DATN.Core.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace DATN.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [Route("Admin/[controller]/[action]")]
    //[Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly ClientService _clientService;
        private readonly HttpClient _httpClient;
        private readonly ExcelService _excelService;
        public UserController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, ClientService clientService, HttpClient httpClient, ExcelService excelService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _clientService = clientService;
            _httpClient = httpClient;
            _excelService = excelService;
        }
        public async Task<IActionResult> Index(UserPaging request)
        {
            var userLogin = SessionHelper.GetObject<UserInfo>(HttpContext.Session, "user");
            UserPaging userPaging = new UserPaging();
            try
            {
                userPaging = _unitOfWork.UserRepository.GetUserPaging(request);
                if (userPaging == null)
                {
                    return NotFound();
                }
                foreach (var item in userPaging.Items)
                {
                    var user = await _userManager.FindByIdAsync(item.Id.ToString());
                    if (user != null)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        item.Roles = roles.ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }

            var userPagingStored = JsonConvert.SerializeObject(userPaging);
            HttpContext.Session.SetString("UserPaging",userPagingStored);
            ViewBag.userLogin = userLogin;
            return View(userPaging);
        }

    
        public IActionResult HandleAddVoucher(string UserId,string UserName,bool IsChecked )
        {
            var getUserPagingStored=  HttpContext.Session.GetString("UserPaging");
            var userPagingDeserialized = string.IsNullOrEmpty(getUserPagingStored)==false?JsonConvert.DeserializeObject<UserPaging>(getUserPagingStored): new UserPaging();
            if (IsChecked)
            {
                UserVoucherShowModal userVoucherShowModal = new UserVoucherShowModal();
                userVoucherShowModal.StartDate =DateTime.Now;
                userVoucherShowModal.UserId =Guid.Parse(UserId);
                userVoucherShowModal.UserName =UserName;
                userVoucherShowModal.EndDate = DateTime.Now.AddDays(7);
                userPagingDeserialized?.UserVoucherShowModal.Add(userVoucherShowModal);
            }
            else
            {
                var userVoucher =
                    userPagingDeserialized?.UserVoucherShowModal.FirstOrDefault(c => c.UserId == Guid.Parse(UserId));
                if (userVoucher != null)
                {
                    userPagingDeserialized?.UserVoucherShowModal.Remove(userVoucher);
                }
            }
            var userPagingStored = JsonConvert.SerializeObject(userPagingDeserialized);
            HttpContext.Session.SetString("UserPaging",userPagingStored);   
            return Json(new { data = userPagingDeserialized.UserVoucherShowModal});

        }
        public IActionResult HandleDateChangeVoucher(string UserId,string DateValue,bool IsStartDate)
        {
            var getUserPagingStored=  HttpContext.Session.GetString("UserPaging");
            var userPagingDeserialized = JsonConvert.DeserializeObject<UserPaging>(getUserPagingStored);
            if (IsStartDate)
            {
                var StartDateUpdate =
                    userPagingDeserialized?.UserVoucherShowModal.FirstOrDefault(c => c.UserId == Guid.Parse(UserId));
                if (StartDateUpdate is not null)
                {
                    StartDateUpdate.StartDate = Convert.ToDateTime(DateValue);
                }
            }
            else
            {
                var EndDateUpdate =
                    userPagingDeserialized?.UserVoucherShowModal.FirstOrDefault(c => c.UserId == Guid.Parse(UserId));
                if (EndDateUpdate is not null)
                {
                    EndDateUpdate.EndDate = Convert.ToDateTime(DateValue);
                }
            }
            var userPagingStored = JsonConvert.SerializeObject(userPagingDeserialized);
            HttpContext.Session.SetString("UserPaging",userPagingStored);   
            return Json(new { data = userPagingDeserialized.UserVoucherShowModal});

        }
        public async Task<IActionResult>  HandleChangeStatus(string UserId,bool IsActive)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            user.isActive = IsActive;
          var result=  await _userManager.UpdateAsync(user);
          return Json(new { data = result});
        }
        public async Task<IActionResult>  HandleUpdateRangeStatus(bool isActive)
        {
            var getUserPagingStored=  HttpContext.Session.GetString("UserPaging");
            var userPagingDeserialized = JsonConvert.DeserializeObject<UserPaging>(getUserPagingStored);
            foreach (var x in userPagingDeserialized.UserVoucherShowModal)
            {
                var user = await _userManager.FindByIdAsync(x.UserId.ToString());
                user.isActive = isActive;
               await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index", userPagingDeserialized);
        }
        
        [HttpPost]
        public async Task<IActionResult> SaveVocherForUser(int? VoucherIdSelected)
        {
            var getUserPagingStored=  HttpContext.Session.GetString("UserPaging");
            var userPagingDeserialized = JsonConvert.DeserializeObject<UserPaging>(getUserPagingStored);
            if (userPagingDeserialized.UserVoucherShowModal.Count>0)
            {

                foreach (var item in userPagingDeserialized.UserVoucherShowModal)
                {
                    item.VoucherId = VoucherIdSelected;
                }
               var addedVoucher = await _unitOfWork.UserRepository.AddVoucherToListUser(userPagingDeserialized.UserVoucherShowModal);
            }

            userPagingDeserialized.ListVoucherDropDown = new List<VoucherVM>();
            var userPagingStored = JsonConvert.SerializeObject(userPagingDeserialized);
            HttpContext.Session.SetString("UserPaging",userPagingStored);   
            return RedirectToAction("Index",userPagingDeserialized);
        }
        public async Task<IActionResult> SendMail()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendMail(SendMailVM vm, string listEmail)
        {
            if (ModelState.IsValid)
            {
                var listMail = listEmail.Split("\r\n").ToList();
                foreach (var item in listMail)
                {
                    vm.Email = item.ToString();
                    var response = await _clientService.Post($"{ApiPaths.SendMail}/SendMail", vm);

                }
            }
            return Redirect("~/Admin/Home/Index");
        }
        public IActionResult Create()
        {
            var role = _unitOfWork.RoleRepository.getRolesName();
            ViewData["Roles"] = new MultiSelectList(role, "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserVM userVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingUser = await _userManager.FindByEmailAsync(userVm.Email);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError("", "Email already exists.");
                        var role = _unitOfWork.RoleRepository.getRolesName();
                        ViewData["Roles"] = new MultiSelectList(role, "Name");
                        return View(userVm);
                    }
                    var response = await _clientService.Post<UserVM>($"{ApiPaths.User}/CreateUser", userVm);
                    if (response != null)
                    {
                        ToastHelper.ShowSuccess(TempData, "Thêm  thành công!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var role = _unitOfWork.RoleRepository.getRolesName();
                        ViewData["Roles"] = new MultiSelectList(role, "Name");
                        return View(userVm);
                    }
                }
                else
                {
                    var role = _unitOfWork.RoleRepository.getRolesName();
                    ViewData["Roles"] = new MultiSelectList(role, "Name");
                }
                
                return View(userVm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "There was an error creating the user. Please try again later.");
                ToastHelper.ShowError(TempData, ex.Message);
                return View(userVm);
            }
        }
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var user = await _clientService.Get<AppUser>($"{ApiPaths.User}/GetUserById?id={id}");

                if (user != null)
                {
                    var userVm = _mapper.Map<UserVM>(user);
                    var roles = await _userManager.GetRolesAsync(user);
                    userVm.Roles = roles.ToList();
                    userVm.ListVoucherNameByUser = await _unitOfWork.UserRepository.GetListVoucherByUserId(id);
                    return View(userVm);
                }
                else
                {
                    throw new Exception("Không tìm thấy người dùng");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("Index");
            }

        }
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var user = await _clientService.Get<AppUser>($"{ApiPaths.User}/GetUserById?id={id}");

                if (user == null)
                {
                    throw new Exception("Không tìm thấy người dùng");
                }

                var userVm = _mapper.Map<UserVM>(user);
                var roles = _userManager.GetRolesAsync(user).Result;
                userVm.Roles = roles.ToList();
                var roleNames = _unitOfWork.RoleRepository.getRolesName().ToList();
                ViewData["Roles"] = new MultiSelectList(roleNames, "Name");
                return View(userVm);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserVM userVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _clientService.Post<UserVM>($"{ApiPaths.User}/EditUser", userVm);
                    if (response != null)
                    {
                        ToastHelper.ShowSuccess(TempData, "Sửa người dùng thành công!");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var role = _unitOfWork.RoleRepository.getRolesName();
                        ViewData["Roles"] = new MultiSelectList(role, "Name");
                        return View(userVm);
                    }
                }
                var role2 = _unitOfWork.RoleRepository.getRolesName();
                ViewData["Roles"] = new MultiSelectList(role2, "Name");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "There was an error updating the user. Please try again later.");
                ToastHelper.ShowError(TempData, ex.Message);
                return View(userVm);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Export(string search, string lastLoginTimeFilter)
        {
            try
            {
                var users = await _clientService.Get<List<AppUser>>($"{ApiPaths.User}/GetUserExport?search={search}&lastLoginTimeFilter={lastLoginTimeFilter}");
                if (users == null || users.Count == 0)
                {
                    throw new Exception("Không tìm thấy người dùng");
                }
                var userVM = _mapper.Map<List<UserExportVM>>(users);
                // Step 1: declare file name
                string fileName = "Users List";

                // Step 2: Create titles
                Dictionary<int, string> titles = new Dictionary<int, string>() {
                        {1, "Id" },
                        {2, "Tên đầy đủ" },
                        {3, "Ngày sinh" },
                        {4, "Email" },
                        {5, "Tài khoản" },
                        {6, "SĐT" },
                        {7, "Đại chỉ" },
                        {8, "Còn hoạt động ?" },
                        {9, "Nhật email ?" },
                        {10, "Lần đăng nhập gần nhất" },
                        {11, "Mô tả" },
                    };

                // Step 3: Convert to IEnumerable
                IEnumerable<UserExportVM> accountsEnumerable = userVM;

                // Step 4: Get File stream
                Stream fileStream = await _excelService.ExportExcel(fileName, titles, accountsEnumerable);

                // Step 5: Return file for downloading
                return File(fileStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{fileName}.csv");
            }
            catch (Exception ex)
            {
                ToastHelper.ShowWarning(TempData, ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
