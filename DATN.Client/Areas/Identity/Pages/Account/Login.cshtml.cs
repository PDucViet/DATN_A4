// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using DATN.Core.Models;
using DATN.Client.Helper;
using DATN.Client.Services;
using Newtonsoft.Json;
using DATN.Client.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DATN.Core.Enum;
using DATN.Client.Constants;
using DATN.Core.ViewModels;
using NuGet.Protocol.Plugins;
using DATN.Core.ViewModels.AuthenViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using DATN.Core.ViewModels.AuthenViewModel;

namespace DATN.Client.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly ClientService _clientService;

        public LoginModel(SignInManager<AppUser> signInManager, ILogger<LoginModel> logger, UserManager<AppUser> userManager, ClientService clientService)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userManager = userManager;
            _clientService = clientService;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(Input.Email);
                    if (user != null)
                    {
                        // login to get token
                        EmailLoginViewModel loginRequest = new EmailLoginViewModel();
                        loginRequest.Email = Input.Email;
                        loginRequest.Password = Input.Password;
                        loginRequest.IsNeedPassword = true;

                        var response = await _clientService.Post<ResponseViewModel>(ApiPaths.Authen + "/AccessByEmail", loginRequest);
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
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                            SessionHelper.SetObject<UserInfo>(HttpContext.Session, "user", userInfo);

                            // Redirect the user based on their role
                            switch (role)
                            {
                                case Role.Admin:
                                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                                case Role.User:
                                    return RedirectToAction("Index", "Home", new { area = "" });
                                default:
                                    return RedirectToAction("Index", "Home", new { area = "" });
                            }
                        }
                    }

                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
