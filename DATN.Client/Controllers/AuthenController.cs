using DATN.Client.Constants;
using DATN.Client.Helper;
using DATN.Client.Models;
using DATN.Client.Services;
using DATN.Core.Enum;
using DATN.Core.Models;
using DATN.Core.ViewModel.AuthenVM;
using DATN.Core.ViewModels;
using DATN.Core.ViewModels.AuthenViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;

namespace DATN.Client.Controllers
{
    public class AuthenController : Controller
    {
        private readonly ClientService _clientService; // Service for making API requests
        private readonly IConfiguration _configuration;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AuthenController(
            IConfiguration configuration,
            ClientService clientService,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager
        )
        {
            _configuration = configuration;
            _clientService = clientService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn()
        {
            EmailLoginViewModel loginRequest = new EmailLoginViewModel();
            try
            {
                UserInfo userInfo = SessionHelper.GetObject<UserInfo>(HttpContext.Session, "user");

                if (userInfo != null)
                {
                    Role role = (Role)userInfo.RoleNumber;

                    switch (role)
                    {
                        case Role.Admin:
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        default:
                            return View("Views/Home/Index.cshtml");
                    }
                }
                return View("Views/SignIn.cshtml", loginRequest);
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return View("Views/SignIn.cshtml");
            }
        }
        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(EmailLoginViewModel loginRequest)
        {
            try
            {
                //PropertyLogger.LogAllProperties(loginRequest);
                // Send a login request to the API
                var response = await _clientService.Post<ResponseViewModel>(ApiPaths.Authen + "/AccessByEmail", loginRequest);
                if (response == null)
                {
                    throw new Exception("Đăng nhập thất bại");
                }

                if (response.Status && response.Message != null)
                {
                    // If login is successful, extract and process the access token
                    string accessToken = response.Message; // Extract the access token

                    SessionHelper.SetObject(HttpContext.Session, "AccessToken", accessToken);

                    TokenHelper tokenHelper = new TokenHelper();
                    // Get all claims from the access token
                    var allClaims = tokenHelper.GetAllClaims(accessToken);

                    // Extract the "role" claim
                    string roleStr = tokenHelper.FindClaimValueByKey(allClaims, "RoleStr");

                    List<string> roles = roleStr?.Split(";")?.ToList();

                    if (roles?.Count > 1)
                    {
                        bool IsUser = roles.Any(r => r == RoleConstants.User);

                        if (IsUser == true)
                        {
                            roleStr = RoleConstants.User;
                        }
                    }

                    Role role;
                    if (Enum.TryParse(roleStr, true, out role))
                    {
                        // Process the user's role if needed
                    }

                    // Show a success message
                    ToastHelper.ShowSuccess(TempData, "Đăng nhập thành công");

                    //Console.WriteLine("role: " + roleStr);

                    UserInfo userInfo = new UserInfo();
                    // Store user information in the UserService
                    userInfo.Role = roleStr; // Store the user's role
                    userInfo.RoleNumber = (int)role; // Store the user's role integer
                    userInfo.Fullname = tokenHelper.FindClaimValueByKey(allClaims, "givenname");
                    userInfo.Email = tokenHelper.FindClaimValueByKey(allClaims, "email");
                    userInfo.UserId = Guid.Parse(tokenHelper.FindClaimValueByKey(allClaims, "nameidentifier"));


                    var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Name, userInfo.Email),
                                    new Claim(ClaimTypes.Role, roleStr) ,
                                };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    // Cập nhật HttpContext.User
                    HttpContext.User = claimsPrincipal;

                    SessionHelper.SetObject<UserInfo>(HttpContext.Session, "user", userInfo);
                    bool isInRole = claimsPrincipal.IsInRole(roleStr);
                    // Redirect the user based on their role
                    switch (role)
                    {
                        case Role.Admin:
                            return RedirectToAction("Index", "Home", new { area = "Admin" });
                        case Role.User:
                            return RedirectToAction("Index", "Home", new { area = "" });
                        default:
                            return View("Views/Home/Index.cshtml");
                    }
                }
                else
                {
                    throw new Exception(response.Message);
                }
            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
                return Redirect("~/Identity/Account/Login");
            }
        }
        [HttpGet("login-google")]
        public async Task<IActionResult> LoginGoogle()
        {
            var redirectUrl = Url.Action("FacebookResponse", "Authen");


            // Kiểm tra yêu cầu dịch vụ provider tồn tại
            var listprovider = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var provider_process = listprovider.Find((m) => m.Name == "Google");
            if (provider_process == null)
            {
                return NotFound("Dịch vụ không chính xác: " + "Google");
            }

            // Cấu hình 
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);

            // Chuyển hướng đến dịch vụ ngoài (Googe, Facebook)
            return new ChallengeResult("Google", properties);
        }
        [HttpGet("login-facebook")]
        public async Task<IActionResult> LoginFacebook()
        {
            var redirectUrl = Url.Action("FacebookResponse", "Authen");

            // Kiểm tra yêu cầu dịch vụ provider tồn tại
            var listprovider = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var provider_process = listprovider.Find((m) => m.Name == "Facebook");
            if (provider_process == null)
            {
                return NotFound("Dịch vụ không chính xác: " + "Facebook");
            }
            // Cấu hình 
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);

            // Chuyển hướng đến dịch vụ ngoài (Googe, Facebook)
            return new ChallengeResult("Facebook", properties);
        }
        [HttpGet("facebook-response")]
        public async Task<IActionResult> FacebookResponse()
        {
            try
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return RedirectToPage("/Identity/Account/Login");
                }


                string? id = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier);
                string? Name = info.Principal.FindFirstValue(ClaimTypes.Name);
                string? GivenName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
                string? Surname = info.Principal.FindFirstValue(ClaimTypes.Surname);
                string? Email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var userExist = await _userManager.FindByEmailAsync(Email);

                var loginResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
                if (loginResult.Succeeded)
                {
                    ToastHelper.ShowSuccess(TempData, "Đăng nhập thành công");

                    var userData = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                    var roles = await _userManager.GetRolesAsync(userData);

                    var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, Email),
                                new Claim(ClaimTypes.Role, String.Join(";",roles))
                            };
                    // Thêm các claims vào đối tượng ClaimsPrincipal
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    // Cập nhật thông tin đăng nhập của người dùng với ClaimsPrincipal mới
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    Role role;
                    if (Enum.TryParse(String.Join(";", roles), true, out role))
                    {
                        // Process the user's role if needed
                    }
                    UserInfo userInfo = new UserInfo();
                    // Store user information in the UserService
                    userInfo.Role = String.Join(";", roles); // Store the user's role
                    userInfo.RoleNumber = (int)role; // Store the user's role integer
                    userInfo.Fullname = userExist.FullName;
                    userInfo.Email = Email;
                    userInfo.UserId = userExist.Id;
                    SessionHelper.SetObject<UserInfo>(HttpContext.Session, "user", userInfo);

                    userData.LastLoginTime = DateTime.Now;
                    await _userManager.UpdateAsync(userData);

                    if (roles.Contains("Admin"))
                    {
                        return Redirect("~/Admin/Home/Index");

                    }
                    else
                    {
                        return Redirect("~/Home/Index");

                    }

                }
                else
                {
                    if (userExist == null)
                    {
                        var userCreate = new AppUser() { UserName = Email, Email = Email, FullName = Name, isActive = true };
                        var createResult = await _userManager.CreateAsync(userCreate);
                        var a = await _userManager.AddPasswordAsync(userCreate, "Abc@123");
                        var b = a.Succeeded;
                        if (createResult.Succeeded)
                        {
                            var linkResult = await _userManager.AddLoginAsync(userCreate, info);
                            var codeactive = await _userManager.GenerateEmailConfirmationTokenAsync(userCreate);
                            await _userManager.ConfirmEmailAsync(userCreate, codeactive);
                            await _userManager.AddToRoleAsync(userCreate, "User");
                            await _signInManager.SignInAsync(userCreate, isPersistent: false, info.LoginProvider);
                            var user = await _userManager.FindByEmailAsync(userCreate.Email);

                            // Tạo một danh sách claims mới
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, Email),
                                new Claim(ClaimTypes.Role, "User") ,
                            };

                            // Thêm các claims vào đối tượng ClaimsPrincipal
                            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var principal = new ClaimsPrincipal(identity);

                            // Cập nhật thông tin đăng nhập của người dùng với ClaimsPrincipal mới
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


                            UserInfo userInfo = new UserInfo();
                            // Store user information in the UserService
                            userInfo.Role = "User"; // Store the user's role
                            userInfo.RoleNumber = 2; // Store the user's role integer
                            userInfo.Fullname = Name;
                            userInfo.Email = Email;
                            userInfo.UserId = user.Id;
                            SessionHelper.SetObject<UserInfo>(HttpContext.Session, "user", userInfo);

                            user.LastLoginTime = DateTime.Now;
                            await _userManager.UpdateAsync(user);
                            ToastHelper.ShowSuccess(TempData, "Đăng nhập thành công");

                            return Redirect("~/Home/Index");
                        }
                    }
                    else
                    {
                        var linkResult = await _userManager.AddLoginAsync(userExist, info);
                        var codeactive = await _userManager.GenerateEmailConfirmationTokenAsync(userExist);
                        await _userManager.ConfirmEmailAsync(userExist, codeactive);
                        await _signInManager.SignInAsync(userExist, isPersistent: false, info.LoginProvider);
                        var user = await _userManager.FindByNameAsync(Email);
                        var roles = await _userManager.GetRolesAsync(user);

                        var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Name, Email),
                                new Claim(ClaimTypes.Role, String.Join(";",roles))
                            };
                        // Thêm các claims vào đối tượng ClaimsPrincipal
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        // Cập nhật thông tin đăng nhập của người dùng với ClaimsPrincipal mới
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        Role role;
                        if (Enum.TryParse(String.Join(";", roles), true, out role))
                        {
                            // Process the user's role if needed
                        }
                        UserInfo userInfo = new UserInfo();
                        // Store user information in the UserService
                        userInfo.Role = String.Join(";", roles); // Store the user's role
                        userInfo.RoleNumber = (int)role; // Store the user's role integer
                        userInfo.Fullname = user.FullName;
                        userInfo.Email = Email;
                        userInfo.UserId = user.Id;
                        userInfo.PasswordHash = user.PasswordHash;
                        SessionHelper.SetObject<UserInfo>(HttpContext.Session, "user", userInfo);

                        user.LastLoginTime = DateTime.Now;
                        await _userManager.UpdateAsync(user);
                        ToastHelper.ShowSuccess(TempData, "Đăng nhập thành công");

                        if (roles.Contains("Admin"))
                        {
                            return Redirect("~/Admin/Home/Index");

                        }
                        else
                        {
                            return Redirect("~/Home/Index");

                        }
                    }

                }
                return RedirectToAction(nameof(SignIn));

            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message); // Display an error message
                return RedirectToAction(nameof(SignIn));
            }
        }
        [HttpGet]
        [Route("SignOut")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                SessionHelper.Remove(HttpContext.Session, "user");
                SessionHelper.Remove(HttpContext.Session, "AccessToken");
                await _signInManager.SignOutAsync();

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            }
            catch (Exception ex)
            {
                ToastHelper.ShowError(TempData, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
