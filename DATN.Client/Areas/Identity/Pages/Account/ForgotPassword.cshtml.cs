// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using DATN.Client.Constants;
using DATN.Client.Helper;
using DATN.Client.Services;
using DATN.Core.Models;
using DATN.Core.ViewModels.SendMailVM;

namespace DATN.Client.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ClientService _clientService;

        public ForgotPasswordModel(UserManager<AppUser> userManager, IEmailSender emailSender, ClientService clientService)
        {
            _userManager = userManager;
            _emailSender = emailSender;
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
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);
                SendMailVM sendMailVM = new SendMailVM()
                {
                    Email = Input.Email,
                    Subject = "Đặt lại mật khẩu",
                    Content = $"Hãy bấm vào đây để thay đổi mật khẩu của bạn <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>."
                };
                await _clientService.Post($"{ApiPaths.SendMail}/SendMail", sendMailVM);

                ToastHelper.ShowSuccess(TempData, "Kiểm tra email để nhận đường dẫn đặt lại mật khẩu!");
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return Page();
        }
    }
}
