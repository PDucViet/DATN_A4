using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.AttributeVM.ProductAttribute;
using DATN.Core.ViewModel.ProductVM;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductAtributeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;

        public ProductAtributeController(IUnitOfWork unitOfWork, IMapper mapper, DATNDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductAttributeVM createPA)
        {
            if (createPA == null)
            {
                return BadRequest("Dữ liệu không được để trống"); // 400 Bad Request
            }

            var attribute = _mapper.Map<ProductAttribute>(createPA);
            _unitOfWork.ProductAtributeRepository.Create(attribute);
            _unitOfWork.SaveChanges();
            return Ok(attribute); // 201 Created
        }
        //Minh
        [HttpGet]
        public async Task<IActionResult> GetDataCustomById(int productAttributeId)
        {
            var productAttribute = _unitOfWork.ProductAtributeRepository.GetCustom(productAttributeId);
            if (productAttribute == null)
            {
                return NotFound();
            }
            var a = _mapper.Map<ProductAttributeVM>(productAttribute);
            return Ok(_mapper.Map<ProductAttributeVM>(productAttribute));
        }
    }
}
