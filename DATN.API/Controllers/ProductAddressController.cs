using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.InterestedVM;
using DATN.Core.ViewModel.ProductAtAndressVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAddressController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductAddressController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/productaddresses
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var addresses =  _unitOfWork.productAddressRepository.GetAll();
            if (addresses == null || !addresses.Any())
            {
                return NoContent(); // 204 No Content
            }
            var addressDTOs = _mapper.Map<List<ProductAtAndressVM>>(addresses);
            return Ok(addressDTOs); // 200 OK
        }

        // GET: api/productaddresses/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var address = await _unitOfWork.productAddressRepository.GetById(id);
            if (address == null)
            {
                return NotFound(); // 404 Not Found
            }
            var addressDTO = _mapper.Map<ProductAddress>(address);
            return Ok(addressDTO); // 200 OK
        }

        // POST: api/productaddresses
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductAtAndressVM addressDTO)
        {
            if (addressDTO == null)
            {
                return BadRequest("Address data is null"); // 400 Bad Request
            }
            var address = _mapper.Map<ProductAddress>(addressDTO);
             _unitOfWork.productAddressRepository.Create(address);
            _unitOfWork.SaveChanges();
            return Ok( address); // 201 Created
        }

        // PUT: api/productaddresses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductAtAndressVM addressDTO)
        {
            if (addressDTO == null || id != addressDTO.AndressID)
            {
                return BadRequest("Invalid data"); // 400 Bad Request
            }
            var address = await _unitOfWork.productAddressRepository.GetById(id);
            if (address == null)
            {
                return NotFound(); // 404 Not Found
            }
            _mapper.Map(addressDTO, address);
            _unitOfWork.productAddressRepository.Update(address);
             _unitOfWork.SaveChanges();
            return NoContent(); // 204 No Content
        }

        // DELETE: api/productaddresses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var address = await _unitOfWork.productAddressRepository.GetById(id);
            if (address == null)
            {
                return NotFound(); // 404 Not Found
            }
            _unitOfWork.productAddressRepository.Delete(address);
             _unitOfWork.SaveChanges();
            return NoContent(); // 204 No Content
        }
    }
}
