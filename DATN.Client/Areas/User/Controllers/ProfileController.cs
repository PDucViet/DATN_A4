using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DATN.Client.Constants;
using DATN.Client.Helper;
using DATN.Client.Models;
using DATN.Client.Services;
using DATN.Core.Infrastructures;
using DATN.Core.Models;
using DATN.Core.ViewModels;
using DATN.Core.ViewModels.AuthenViewModel;
using DATN.Core.ViewModels.UserViewModel;

namespace DATN.Client.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ClientService _clientService;
        public ProfileController(IMapper mapper, ClientService clientService, HttpClient httpClient)
        {
            _mapper = mapper;
            _clientService = clientService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UserProfile profile = new UserProfile();
            try
            {
                var user = SessionHelper.GetObject<UserInfo>(HttpContext.Session, "user");
                profile.UserVM.Id = user.UserId;

                profile = await _clientService.Post<UserProfile>($"{ApiPaths.User}/GetProfile", profile);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return View(profile);
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            try
            {
                var user = SessionHelper.GetObject<UserInfo>(HttpContext.Session, "user");
                if (user == null)
                {
                    throw new Exception("Vui lòng đăng nhập!");
                }
                var userExist = await _clientService.Get<AppUser>($"{ApiPaths.User}/GetUserById?id={user.UserId}");
                if (userExist == null || !userExist.isActive)
                {
                    throw new Exception("Không tìm thấy người dùng!");
                }

                var userVM = _mapper.Map<UserVM>(userExist);

                return View(userVM);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return RedirectToAction("Index", "Profile");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserVM userVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _clientService.Put<ResponseViewModel>($"{ApiPaths.User}/UpdateUser", userVM);
                    if (!result.Status)
                    {
                        throw new Exception(result.Message);
                    }
                    ToastHelper.ShowSuccess(TempData, "Cập nhật thành công!");
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    throw new Exception("Vui lòng điền đầy đủ thông tin!");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return View(userVM);
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM changePasswordVM)
        {
            try
            {
                var user = await _clientService.Get<AppUser>($"{ApiPaths.User}/GetUserByEmail?email={changePasswordVM.Email}");
                if (user == null)
                {
                    throw new Exception($"Không tìm thấy người dùng có email: {changePasswordVM.Email}");
                }
                user.PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, changePasswordVM.Password);
                var userVM = _mapper.Map<UserVM>(user);
                var result = await _clientService.Put<ResponseViewModel>($"{ApiPaths.User}/UpdateUser", userVM);
                if (result.Status)
                {
                    ToastHelper.ShowSuccess(TempData, "Đổi mật khẩu thành công!");
                    return RedirectToAction("LogOut", "Authen");
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return View(changePasswordVM);
        }
    }
}
