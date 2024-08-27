using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DATN.Core.Infrastructures;
using DATN.Core.Models;
using DATN.Core.ViewModels;
using DATN.Core.ViewModels.UserViewModel;

namespace DATN.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        public UserController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }
        [HttpGet]
        public IActionResult GetUserByEmail(string email)
        {
            var user = _unitOfWork.UserRepository.GetUserByEmail(email);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateUser(UserVM userVM)
        {
            var user = _mapper.Map<AppUser>(userVM);
            var result = _unitOfWork.UserRepository.UpdateUser(user);
            if (result > 0)
            {
                return Ok(new ResponseViewModel() { Status = true, Message = "Update Success" });
            }
            return BadRequest(new ResponseViewModel() { Status = false, Message = "Update Fail" });
        }
        [HttpGet]
        public IActionResult GetUserExport(string? search, string? lastLoginTimeFilter)
        {
            var list = _unitOfWork.UserRepository.GetUsersExport(search, lastLoginTimeFilter);
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Delete([FromForm] Guid userId)
        {
            var success = _unitOfWork.UserRepository.DeleteUser(userId);
            return Ok(new ResponseViewModel { Status = success, Message = "Cập nhật thành công!" });
        }

        [HttpPost]
        public IActionResult UpdateUserList([FromBody] List<UserUpdateVM> users)
        { 
            try
            {
                if (users == null || users.Count == 0)
                {
                    return BadRequest(new ResponseViewModel { Status = false, Message = "Danh sách người dùng trống!" });
                }
                // Lấy danh sách Id của các người dùng trong danh sách
                var userIds = users.Select(u => u.Id).ToList();

                // Lấy danh sách người dùng cần cập nhật từ cơ sở dữ liệu
                var existingUsers = _unitOfWork.UserRepository.GetUsersByIds(userIds).ToList();

                // Lặp qua danh sách người dùng đã có và cập nhật trạng thái
                foreach (var user in existingUsers)
                {
                    var updateUser = users.FirstOrDefault(u => u.Id == user.Id);
                    if (updateUser != null)
                    {
                        user.isActive = !user.isActive;
                    }
                }
                Console.WriteLine(existingUsers);
                // Cập nhật danh sách người dùng vào cơ sở dữ liệu
                _unitOfWork.UserRepository.UpdateRange(existingUsers);
                _unitOfWork.SaveChanges();

                return Ok(new ResponseViewModel { Status = true, Message = "Cập nhật danh sách người dùng thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseViewModel { Status = false, Message = "Lỗi trong quá trình cập nhật danh sách người dùng!"});
            }
        }
        [HttpPost]
        public IActionResult GetProfile([FromBody] UserProfile userProfile)
        {
            userProfile = _unitOfWork.UserRepository.GetUserProfile(userProfile);
            return Ok(userProfile);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserVM userVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userVm.Id = Guid.NewGuid();
                    userVm.UserName = userVm.Email.ToUpper();
                    userVm.NormalizedUserName = userVm.Email.ToUpper();
                    userVm.NormalizedEmail = userVm.Email.ToUpper();
                    userVm.SecurityStamp = Guid.NewGuid().ToString();
                    userVm.PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, userVm.PasswordHash);
                    userVm.EmailConfirmed = true;
                    userVm.isActive = true;
                    userVm.LastLoginTime = DateTime.Now;

                    var user = _mapper.Map<AppUser>(userVm);
                    var result = await _userManager.CreateAsync(user);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRolesAsync(user, userVm.Roles);
                        return Ok(new { Message = "User created successfully." });
                    }
                    else
                    {
                        return BadRequest(new { Errors = result.Errors });
                    }
                }
                return BadRequest(ModelState);
            }
            catch
            {
                return StatusCode(500, new { Message = "There was an error creating the user. Please try again later." });
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditUser([FromBody] UserVM userVm)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userVm.Id.ToString());

                if (user != null)
                {
                    user.Email = userVm.Email;
                    user.UserName = userVm.Email.ToUpper();
                    user.NormalizedUserName = userVm.Email.ToUpper();
                    user.NormalizedEmail = userVm.Email.ToUpper();
                    user.PhoneNumber = userVm.PhoneNumber;
                    user.Address = userVm.Address;
                    user.Description = userVm.Description;
                    user.FullName = userVm.FullName;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        var existingRoles = await _userManager.GetRolesAsync(user);
                        await _userManager.RemoveFromRolesAsync(user, existingRoles);
                        await _userManager.AddToRolesAsync(user, userVm.Roles);
                        return Ok(new { Message = "User updated successfully." });
                    }
                    else
                    {
                        return BadRequest(new { Errors = result.Errors });
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest(ModelState);
        }
    }
}
