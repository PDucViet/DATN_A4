using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel;
using DATN.Core.ViewModel.InterestedVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInterestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductInterestController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/productinterests
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var interests =  _unitOfWork.interestedRepository.GetAll();
            if (interests == null || !interests.Any())
            {
                return NoContent(); // 204 No Content
            }
            var interestDTOs = _mapper.Map<List<InterestedVM>>(interests);
            return Ok(interestDTOs); // 200 OK
        }

        // GET: api/productinterests/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var interest = await _unitOfWork.interestedRepository.GetById(id);
            if (interest == null)
            {
                return NotFound(); // 404 Not Found
            }
            var interestDTO = _mapper.Map<InterestedVM>(interest);
            return Ok(interestDTO); // 200 OK
        }

        // POST: api/productinterests
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InterestedVM interestDTO)
        {
            if (interestDTO == null)
            {
                return BadRequest("Interest data is null"); // 400 Bad Request
            }
            var interest = _mapper.Map<Interested>(interestDTO);
             _unitOfWork.interestedRepository.Create(interest);
             _unitOfWork.SaveChanges();
            return Ok( interest); // 201 Created
        }

        // DELETE: api/productinterests/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var interest = await _unitOfWork.interestedRepository.GetById(id);
            if (interest == null)
            {
                return NotFound(); // 404 Not Found
            }
            _unitOfWork.interestedRepository.Delete(interest);
            _unitOfWork.SaveChanges();
            return NoContent(); // 204 No Content
        }
    }
}
