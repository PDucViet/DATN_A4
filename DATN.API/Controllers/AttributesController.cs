using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.AttributeVM.AttributeDynamic;
using DATN.Core.ViewModel.ProductVM;
using DATN.Core.ViewModel.ProductVM.AtributeCreateVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;

        public AttributesController(IUnitOfWork unitOfWork, IMapper mapper, DATNDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttributeDynamic()
        {
            // Fetch all dynamic attributes
            var listAttributes = await _context.Attributes
               .Include(pa => pa.AttributeValues)
               .Where(pa => pa.Type == Core.Enum.AttributeType.dynamic)
               .ToListAsync();
            // Check if there are any attributes
            if (!listAttributes.Any())
            {
                throw new Exception("Không tồn tại các thuộc tính");
            }

            // Group attributes by their names and map to AttributeVM
            var attributeVms = listAttributes.Select(attribute => new AttributesVM
            {
                Id = attribute.Id,
                Name = attribute.Name,
                Type = attribute.Type,
                IsActive = attribute.IsActive,
                IsShow = attribute.IsShow,
                attributeValues = attribute.AttributeValues.Select(value => new AttributeValueVM
                {
                    Value = value.Value,
                    IsShow = value.IsShow,
                    IsActive = value.IsActive
                }).ToList()
            }).ToList();

            // Return the list of attribute view models
            return Ok(attributeVms);
        }
      
        // GET: api/attributes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var attribute = await _context.Attributes
                 .Include(a => a.AttributeValues)
                 .FirstOrDefaultAsync(a => a.Id == id);

            if (attribute == null)
            {
                return NotFound("Không tìm thấy thuộc tính"); // 404 Not Found nếu attribute không tồn tại
            }


            attribute.AttributeValues?.Select(av => new AttributeValueVM
            {
                AtributeValueId = av.AtributeValueId,
                Value = av.Value,
                IsActive = av.IsActive,
                IsShow = av.IsShow,
            }).ToList();

            if (attribute.AttributeValues == null)
            {
                return NotFound("Không tìm thấy giá trị thuộc tính"); // 404 Not Found nếu không có giá trị nào
            }

            var attributeDTO = _mapper.Map<AttributesVM>(attribute);
            return Ok(attributeDTO); // 200 OK
        }

        // POST: api/attributes
        [HttpPost]
        public async Task<IActionResult> CreateAttributeDynamic([FromBody] CreateAttributeVM attributeVm)
        {
            if (attributeVm != null)
            {
                var existingAttribute = _context.Attributes.Where(c => c.Name == attributeVm.Name);
                if (existingAttribute.Any()) return Conflict("Thuộc tính đã tồn tại");
                // Thêm mới thuộc tính và giá trị
                var newAttribute = new Attributes
                {
                    Type = Core.Enum.AttributeType.dynamic,
                    Name = attributeVm.Name,
                    IsActive = attributeVm.IsActive,
                    IsShow = attributeVm.IsShow
                };
                _unitOfWork.AttributeRepository.Create(newAttribute);
                _unitOfWork.SaveChanges();

                var valueExisting = _context.AttributeValues.FirstOrDefault(c => c.AtributeValueId == attributeVm.Value.AtributeValueId);
                if (valueExisting.AttributeId == null)
                {
                    valueExisting.AttributeId = newAttribute.Id;

                    _unitOfWork.AtributeValueRepository.Update(valueExisting);
                }
                else
                {
                    var attributeValue = new AttributeValue
                    {
                        AttributeId = newAttribute.Id,
                        Value = valueExisting.Value,
                        IsActive = valueExisting.IsActive,
                        IsShow = valueExisting.IsShow,
                    };
                    _unitOfWork.AtributeValueRepository.Create(attributeValue);
                }
                _unitOfWork.SaveChanges();
                return Ok(attributeVm);
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = (int)System.Net.HttpStatusCode.BadRequest,
                    Message = "Tạo thuộc tính động thất bại",
                });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateDynamicProduct([FromBody] List<CreateDynamicProductVM> attributeVm)
        {
            if (!ModelState.IsValid)
            {
                // Trả về các lỗi validation
                return BadRequest(ModelState);
            }

            var errors = new List<string>();

            foreach (var attribute in attributeVm)
            {
                var existingAttribute = _context.Attributes.FirstOrDefault(c => c.Name == attribute.Name);
                if (existingAttribute != null)
                {
                    ModelState.AddModelError("Name", $"Thuộc tính '{attribute.Name}' đã tồn tại.");
                }

                var valueExisting = _context.AttributeValues.FirstOrDefault(c => c.Value == attribute.Value);
                if (valueExisting != null && valueExisting.AttributeId != null)
                {
                    ModelState.AddModelError("Value", $"Giá trị '{attribute.Value}' đã tồn tại.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Thêm mới thuộc tính và giá trị
                var newAttribute = new Attributes
                {
                    Type = Core.Enum.AttributeType.dynamic,
                    Name = attribute.Name,
                    IsActive = true,
                    IsShow = true
                };
                _unitOfWork.AttributeRepository.Create(newAttribute);
                _unitOfWork.SaveChanges();

                var attributeValue = new AttributeValue
                {
                    AttributeId = newAttribute.Id,
                    Value = attribute.Value,
                    IsActive = true,
                    IsShow = true
                };
                _unitOfWork.AtributeValueRepository.Create(attributeValue);
                _unitOfWork.SaveChanges();
            }

            return Ok(attributeVm);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAttributeDynamic([FromBody] UpdateAttributeVM attributeVm)
        {
            if (attributeVm != null)
            {
                var attribute = await _unitOfWork.AttributeRepository.GetById(attributeVm.Id);
                if (attribute == null) return NotFound("Không tìm thấy thuộc tính");
                // Thêm mới thuộc tính và giá trị

                attribute.Name = attributeVm.Name;
                attribute.IsActive = attributeVm.IsActive;
                attribute.IsShow = attributeVm.IsShow;

                _unitOfWork.AttributeRepository.Update(attribute);

                var value = await _unitOfWork.AtributeValueRepository.GetById(attributeVm.Value.AtributeValueId);
                if (value == null) return NotFound("Không tìm thấy giá trị");

                value.Value = attributeVm.Value.Value;
                value.IsActive = attributeVm.Value.IsActive;
                value.IsShow = attributeVm.Value.IsShow;

                _unitOfWork.AtributeValueRepository.Update(value);

                _unitOfWork.SaveChanges();
                return Ok(attributeVm);
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = (int)System.Net.HttpStatusCode.BadRequest,
                    Message = "Cập nhật thuộc tính động thất bại",
                });
            }
        }
      

        //// PUT: api/attributes/{id}
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, [FromBody] AttributesVM attributeVm)
        //{
        //    if (attributeVm == null || id != attributeVm.Id)
        //    {
        //        return BadRequest("Invalid data"); // 400 Bad Request
        //    }
        //    var attribute = await _unitOfWork.AttributeRepository.GetById(id);
        //    if (attribute == null)
        //    {
        //        return NotFound(); // 404 Not Found
        //    }
        //    _mapper.Map(attributeVm, attribute);
        //    _unitOfWork.AttributeRepository.Update(attribute);
        //    _unitOfWork.SaveChanges();
        //    return NoContent(); // 204 No Content
        //}

        //// DELETE: api/attributes/{id}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var attribute = await _unitOfWork.AttributeRepository.GetById(id);
        //    if (attribute == null)
        //    {
        //        return NotFound(); // 404 Not Found
        //    }
        //    _unitOfWork.AttributeRepository.Delete(attribute);
        //    _unitOfWork.SaveChanges();
        //    return NoContent(); // 204 No Content
        //}
    }
}

