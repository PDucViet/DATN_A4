using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DATN.Core.Infrastructures;
using DATN.Core.ViewModels.AuthenViewModel;
using DATN.Core.ViewModels;
using DATN.Core.Models;
using DATN.Api.Helpers;

namespace DATN.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AuthenController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AccessByEmail([FromBody] EmailLoginViewModel request)
        {
            ResponseViewModel response = new ResponseViewModel();
            try
            {
                // Check if model state is valid or not
                if (request == null || request.Email == null)
                {
                    throw new Exception("Không nhận được thông tin người dùng");
                }

                string email = request.Email;

                if (await _unitOfWork.AuthenRepository.IsAccountExisted(email) == false)
                {
                    AppUser account = new AppUser()
                    {
                        UserName = request.Email,
                        Email = request.Email,
                        FullName = request.GivenName + request.Surname,
                        isActive = true
                    };
                    // Insert User to db
                    string Password = PasswordHepler.GenerateAccountPassword(10);
                    response = await _unitOfWork.AuthenRepository.CreateAccountByGoogle(account, email);

                    if (!response.Status)
                    {
                        throw new Exception();
                    }

                    // Return login Result
                    return Ok(new ResponseViewModel { Status = true, Message = response.Message });
                }

                response = await _unitOfWork.AuthenRepository.AccessByEmail(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Ok(new ResponseViewModel() { Status = false, Message = ex.Message });
            }
        }
    }
}
