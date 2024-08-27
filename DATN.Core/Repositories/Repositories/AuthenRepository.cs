using DATN.Core.Data;
using DATN.Core.Enum;
using DATN.Core.Models;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.AuthenVM;
using DATN.Core.ViewModels.AuthenViewModel;
using DATN.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class AuthenRepository : IAuthenRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly DATNDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole<Guid>> roleManager, DATNDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _configuration = configuration;
        }

        public async Task<ResponseViewModel> AccessByEmail(EmailLoginViewModel request)
        {
            try
            {
                // Find User by Username in database
                AppUser? user = new AppUser();
                if (request.IsNeedPassword)
                {
                    user = await _context.Users.SingleOrDefaultAsync(a => a.Email == request.Email && a.isActive == true);
                    var check = await _userManager.CheckPasswordAsync(user, request.Password);
                    if (!check)
                    {
                        return new ResponseViewModel() { Status = false, Message = "Sai email hoặc mật khẩu" };
                    }
                }
                user = await _context.Users.SingleOrDefaultAsync(a => a.Email == request.Email && a.isActive == true);
                if (user == null)
                {
                    return new ResponseViewModel() { Status = false, Message = "Không tìm thấy người dùng" };
                }
                //PropertyLogger.LogAllProperties(user);

                //if login success, get roles of this user
                var roles = await _userManager.GetRolesAsync(user);
                foreach (string role in roles)
                {
                    Console.WriteLine("Role: " + role);
                }

                // create info will stored in JWT, these info will be encode and decode by API application use this JWT
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.GivenName, $"{user.FullName}"),
                    new Claim("RoleStr", string.Join(";", roles)),

                };

                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                // create a symmetric key by info configed in appsettings.json
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                // verify the the validation of JWT
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Create a new JWT by some info to spicify contend of JWT 
                var token = new JwtSecurityToken(_configuration["JWT:ValidIssuer"],
                    _configuration["JWT:ValidAudience"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

                var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

                // update login time
                user.LastLoginTime = DateTime.Now;
                _context.Users.Update(user);
                _context.SaveChanges();

                return new ResponseViewModel() { Status = true, Message = accessToken };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new ResponseViewModel() { Status = false, Message = ex.Message };
            }
        }


        /// <summary>
        /// Create account after login by google with valid email
        /// </summary>
        /// <param name="account">Account want to add to database</param>
        /// <returns>new account saved in database</returns>
        public async Task<ResponseViewModel> CreateAccountByGoogle(AppUser account, string Password)
        {
            // insert account to database
            try
            {
                var result = await _userManager.CreateAsync(account, Password);

                // asign to specific row
                if (result.Succeeded)
                {
                    //Debug if user and role can be get
                    var userAfter = await _userManager.FindByEmailAsync(account.Email);

                    //Add User to role
                    List<string> UserRoles = new List<string> { RoleConstants.User };
                    await _userManager.AddToRolesAsync(account, UserRoles);
                    string token = GenerateJWT(userAfter, UserRoles);
                    return new ResponseViewModel() { Status = true, Message = token };
                }
                else
                {
                    throw new Exception("Lỗi đăng nhập");
                }
            }
            catch (Exception ex)
            {
                //Rollback data
                _context.Users.Remove(account);
                await _context.SaveChangesAsync();

                return new ResponseViewModel() { Status = false, Message = ex.Message };
            }
        }

        /// <summary>
        /// Generate JWT
        /// </summary>
        /// <param name="user">Account</param>
        /// <param name="roles">List roles of current account</param>
        /// <returns>JWT string that store account infomation</returns>
        private string GenerateJWT(AppUser user, List<string> roles)
        {
            try
            {
                // create info will stored in JWT, these info will be encode and decode by API application use this JWT
                List<Claim> claims = new List<Claim>
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, $"{user.FullName}"),
                new Claim("RoleStr", string.Join(";", roles)),
                };

                foreach (string role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                // create a symmetric key by info configed in appsettings.json
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                // verify the the validation of JWT
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Create a new JWT by some info to spicify contend of JWT 
                var token = new JwtSecurityToken(_configuration["JWT:ValidIssuer"],
                    _configuration["JWT:ValidAudience"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return String.Empty;
            }
        }
        public async Task<bool> IsAccountExisted(string email)
        {
            try
            {
                var account = await _context.Users
                    .SingleOrDefaultAsync(a => a.Email == email && a.isActive == true);
                return account != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
