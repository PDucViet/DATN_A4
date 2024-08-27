using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.AttributeVM.AttributeDynamic;
using DATN.Core.ViewModel.AttributeVM.AtttributeVariation;
using DATN.Core.ViewModel.ProductVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttributeValuesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;

        public AttributeValuesController(IUnitOfWork unitOfWork, IMapper mapper, DATNDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
           _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllValueDynamic()
        {
            var listValues = await _context.AttributeValues
               .Where(pa => pa.Type == Core.Enum.ValuesType.dynamic)
               .ToListAsync();
            if (!listValues.Any())
            {
                throw new Exception("Không tồn tại thuộc tính");
            }

            var attributeVlVms = _mapper.Map<IEnumerable<AttributeValueVM>>(listValues);

            return Ok(attributeVlVms);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllValueVariant()
        {
            var listValues = await _context.AttributeValues
               .Where(pa => pa.Type == Core.Enum.ValuesType.variation)
               .ToListAsync();
            if (!listValues.Any())
            {
                throw new Exception("Không tồn tại thuộc tính");
            }

            var attributeVlVms = _mapper.Map<IEnumerable<AttributeValueVM>>(listValues);

            return Ok(attributeVlVms);
        }

        [HttpGet]
        public async Task<IActionResult> GetValueIsActive()
        {
            var atttributeVl = _unitOfWork.AtributeValueRepository.GetAll().Where(c => c.IsActive);
            if (atttributeVl == null)
            {
                throw new Exception("Không tồn tại thuộc tính");
            }

            var attributeVlVms = _mapper.Map<IEnumerable<AttributeValueVM>>(atttributeVl);

            return Ok(attributeVlVms);
        }

        // GET: api/attributes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var attributeVl = await _unitOfWork.AtributeValueRepository.GetById(id);
            if (attributeVl == null)
            {
                return NotFound(); // 404 Not Found
            }
            var attributeVlVm = _mapper.Map<AttributeValueVM>(attributeVl);
            return Ok(attributeVlVm); // 200 OK
        }

        [HttpGet]
        public async Task<IActionResult> CheckExistingValue(string value)
        {
            var existingValue = _context.AttributeValues.FirstOrDefault(x => x.Value.ToLower().Trim() == value.ToLower().Trim());
            if (existingValue !=null)
            {
                return Conflict("Giá trị đã tồn tại"); 
            }
            return Ok("Bạn có thể thêm mới"); // 200 OK
        }

        //[HttpGet]
        //public async Task<IActionResult> CheckExistingListValue(List<string> values)
        //{
        //    // Chuyển tất cả các giá trị trong danh sách thành chữ thường và loại bỏ khoảng trắng
        //    var normalizedValues = values.Select(val => val.ToLower().Trim()).ToList();

        //    // Kiểm tra xem có giá trị nào trong danh sách đã tồn tại trong cơ sở dữ liệu không
        //    var existingValues = context.AttributeValues
        //                                .Where(x => normalizedValues.Contains(x.Value.ToLower().Trim()))
        //                                .Select(x => x.Value)
        //                                .ToList();

        //    if (existingValues.Any())
        //    {
        //        return Conflict("Giá trị đã tồn tại");
        //    }

        //    return Ok(); // 200 OK
        //}

        // POST: api/attributes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttributeValueVM attributeVm)
        {
            if (attributeVm == null)
            {
                return BadRequest("Attribute data is null"); // 400 Bad Request
            }
            var attribute = _mapper.Map<AttributeValue>(attributeVm);
            _unitOfWork.AtributeValueRepository.Create(attribute);
            _unitOfWork.SaveChanges();
            return Ok(attribute); // 201 Created
        }

        [HttpPost]
        public async Task<IActionResult> CreateMultipleValueWhenUpdate([FromBody] List<CreateVLVariantVM> listVl)
        {
            if (listVl == null)
            {
                return BadRequest("Phải tồn tại ít nhất 1 giá trị"); // 400 Bad Request
            }

            foreach (var value in listVl)
            {
                var existingValue = _context.AttributeValues.Where(c => c.Value.ToLower() == value.Value.Trim().ToLower());
                if (existingValue.Any())
                {
                    return Conflict("Giá trị đã tồn tại"); // 409 Conflict
                }

                var valueMapper = _mapper.Map<AttributeValue>(value);

                valueMapper.Type = Core.Enum.ValuesType.variation;
                _unitOfWork.AtributeValueRepository.Create(valueMapper);

            }
            _unitOfWork.SaveChanges();
            return Ok(listVl); // 201 Created
        }

        [HttpPost]
        public async Task<IActionResult> CreateValueDynamic([FromBody] CreateValueVM ValueVM)
        {
            if (ValueVM == null)
            {
                return BadRequest("Giá trị rỗng"); // 400 Bad Request
            }

            var existingValue = _context.AttributeValues.Where(c => c.Value.ToLower() == ValueVM.Value.Trim().ToLower());
            if (existingValue.Any())
            {
                return Conflict("Giá trị đã tồn tại"); // 409 Conflict
            }

            var valueMapper = _mapper.Map<AttributeValue>(ValueVM);
            _unitOfWork.AtributeValueRepository.Create(valueMapper);
            _unitOfWork.SaveChanges();
            return Ok(valueMapper); // 201 Created
        }


        [HttpPost]
        public async Task<IActionResult> CreateMultipleValue([FromBody] List<CreateVLVariantVM> listVl)
        {
            if (listVl == null)
            {
                return BadRequest("Phải tồn tại ít nhất 1 giá trị"); // 400 Bad Request
            }

            foreach (var value in listVl)
            {
                var existingValue = _context.AttributeValues.Where(c => c.Value.ToLower() == value.Value.Trim().ToLower());
                if (existingValue.Any())
                {
                    return Conflict("Giá trị đã tồn tại"); // 409 Conflict
                }

                var valueMapper = _mapper.Map<AttributeValue>(value);
                valueMapper.Type = Core.Enum.ValuesType.variation;

                _unitOfWork.AtributeValueRepository.Create(valueMapper);

            }
             _unitOfWork.SaveChanges();
             return Ok(listVl); // 201 Created
        }

        // PUT: api/attributes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AttributeValueVM attributeVm)
        {
            if (attributeVm == null || id != attributeVm.AtributeValueId)
            {
                return BadRequest("Invalid data"); // 400 Bad Request
            }
            var attribute = await _unitOfWork.AtributeValueRepository.GetById(id);
            if (attribute == null)
            {
                return NotFound(); // 404 Not Found
            }
            _mapper.Map(attributeVm, attribute);
            _unitOfWork.AtributeValueRepository.Update(attribute);
            _unitOfWork.SaveChanges();
            return NoContent(); // 204 No Content
        }

        // DELETE: api/attributes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var attribute = await _unitOfWork.AtributeValueRepository.GetById(id);
            if (attribute == null)
            {
                return NotFound(); // 404 Not Found
            }
            _unitOfWork.AtributeValueRepository.Delete(attribute);
            _unitOfWork.SaveChanges();
            return NoContent(); // 204 No Content
        }
    }
}
