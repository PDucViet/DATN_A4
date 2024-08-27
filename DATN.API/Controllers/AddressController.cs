using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.ViewModel.AndressVM;
using DATN.Core.ViewModel.OriginVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.ProductVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        public AddressController(IUnitOfWork unitOfWork, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _unitOfWork.AddressRepository.GetAll();
            if (result != null)
            {
                var addressVms = _mapper.Map<List<AddressVM>>(result);
              
                //var productVms = products;
                return Ok(addressVms);
            }
            return NoContent(); // Trả về 204 nếu không có sản phẩm nào được tìm thấy
        }

        [HttpGet("get-by-id")]
		public async Task<IActionResult> GetById( int id)
		{
			var address = await _unitOfWork.AddressRepository.GetById(id);
			if (address == null)
			{
				return NotFound(); // 404 Not Found
			}
			var addressDTO = _mapper.Map<AddressVM>(address);
			return Ok(addressDTO); // 200 OK
		}

      
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddressVM address)
        {
            if (address == null)
            {
                return BadRequest("Notificaton data is null"); // 400 Bad Request
            }
            else
            {
                var result = _mapper.Map<Address>(address);
                _unitOfWork.AddressRepository.Create(result);
                _unitOfWork.SaveChanges();

                return Ok(result); // 201 Created
            }

           
        }

        [HttpPut("edit-address")]
        public async Task<IActionResult> Update( [FromBody] AddressVM address)
        {
            var result = await _unitOfWork.AddressRepository.GetById(address.AddressID);
            if (result == null)
            {
                return NotFound();
            }
            _mapper.Map(address, result);
            _unitOfWork.AddressRepository.Update(result);
            _unitOfWork.SaveChanges();
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.AddressRepository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
           // _mapper.Map(address, result);
            _unitOfWork.AddressRepository.Delete(result);
            _unitOfWork.SaveChanges();
            return Ok(result);
        }

        // phân trang
        [HttpPost]
        public IActionResult GetAddressPaging([FromBody] AddressPaging request)
        {
            AddressPaging partnerPaging = _unitOfWork.AddressRepository.GetAddressPaging(request);
            return Ok(partnerPaging);
        }


    }
}
