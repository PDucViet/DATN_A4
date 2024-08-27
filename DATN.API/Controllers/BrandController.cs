using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BrandController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var brands = _unitOfWork.brandRepository.GetAll();

            if (brands != null)
            {
                var brandVMs = _mapper.Map<List<BrandVM>>(brands);
                return Ok(brandVMs);
            }
            return BadRequest(new { Message = "Get dữ liệu không thành công"  }); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _unitOfWork.brandRepository.GetById(id);
            if (brand == null)
            {
                return NotFound(); // 404 Not Found
            }

            var brandVM = _mapper.Map<BrandVM>(brand);
            return Ok(brandVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BrandCreatVM brandVM)
        {
            if (brandVM == null)
            {
                return BadRequest("Brand data is null"); // 400 Bad Request
            }

            var brand = _mapper.Map<Brand>(brandVM);


            await _unitOfWork.brandRepository.Create(brand);
            _unitOfWork.SaveChanges();

            return Ok(brand); // 201 Created
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] BrandCreatVM brandVM)
        {
            var result = await _unitOfWork.brandRepository.GetById(brandVM.BrandId);
            if (result == null)
            {
                return NotFound();
            }
            _mapper.Map(brandVM, result);
            _unitOfWork.brandRepository.Update(result);
            _unitOfWork.SaveChanges();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitOfWork.brandRepository.GetById(id);
            if (brand == null)
            {
                return NotFound(); // 404 Not Found
            }
            brand.Status = false;
          _unitOfWork.brandRepository.Update(brand);
            int reusult = _unitOfWork.SaveChanges();


            return Ok(reusult) ; // 204 No Content
        }
		[HttpPost]
		public IActionResult brandPaging([FromBody] BrandPaging request)
		{
			BrandPaging partnerPaging = _unitOfWork.brandRepository.brandPaging(request);
			return Ok(partnerPaging);
		}
	}
}
