using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using DATN.Client.Constants;
using DATN.Client.Models;
using DATN.Client.Services;
using DATN.Core.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace DATN.Client
{
    public class OAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public OAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context,IHttpContextAccessor httpContext, ClientService _clientService, SignInManager<AppUser> _signInManager)
        {
            // Kiểm tra xem mã truy cập OAuth còn hợp lệ hay không
            if (httpContext.HttpContext.Session.Get("user")==null)
            {
                // Nếu còn hợp lệ, tạo phiên người dùng
                //var userInfo = GetUserInfoFromOAuth(context.Request.Headers["Authorization"], context);
                //var info = await _signInManager.GetExternalLoginInfoAsync();

                await _signInManager.SignOutAsync();
            }

            await _next(context);
        }

        private bool IsAccessTokenValid(HttpContext context)
        {
            // Thực hiện kiểm tra mã truy cập OAuth ở đây
            // Trong ví dụ này, giả sử mã truy cập luôn hợp lệ
            return true;
        }

        //private JObject GetUserInfoFromOAuth(string accessToken, HttpContext context)
        //{
        //    // Lấy thông tin người dùng từ máy chủ OAuth
        //    var client = new RestClient("https://graph.facebook.com/me");
        //    var request = new RestRequest();
        //    request.AddQueryParameter("access_token", accessToken);
        //    var id = _c

        //    var fb = new FacebookClient("360176879889477", "7b726ab06dd5a4c86f93278f7926a451");

        //    if (response.IsSuccessful)
        //    {
        //        return JObject.Parse(response.Content);
        //    }
        //    else
        //    {
        //        // Xử lý lỗi khi gọi API Graph của Facebook
        //        throw new HttpRequestException("Failed to retrieve user data from Facebook API.");
        //    }
        //}
    }
    public static class OAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseOAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OAuthMiddleware>();
        }
    }

}
