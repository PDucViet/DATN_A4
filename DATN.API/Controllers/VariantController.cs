using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.AttributeVM.AtttributeVariation;
using DATN.Core.ViewModel.ProductVM;
using DATN.Core.ViewModel.ProductVM.AtributeCreateVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Reflection.Metadata.Ecma335;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VariantController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;

        public VariantController(IUnitOfWork unitOfWork, IMapper mapper, DATNDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttributeVariation()
        {
            var listAttributes = await _context.Attributes
               .Include(pa => pa.AttributeValues)
               .Where(pa => pa.Type == Core.Enum.AttributeType.variation)
               .ToListAsync();

            if (!listAttributes.Any())
            {
                throw new Exception("Không tồn tại các thuộc tính");
            }

            // Group attributes by their names and map to AttributeVM
            var attributeVms = listAttributes.Select(attribute => new AttributesVM
            {
                Id = attribute.Id,
                Name = attribute.Name,
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
                 .FirstOrDefaultAsync(a => a.Id == id && a.Type == Core.Enum.AttributeType.variation);

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

        [HttpPost]
        public async Task<IActionResult> CreateAttributeVariation([FromBody] CreateVariantVM attributeVm)
        {
            if (attributeVm != null)
            {  // Thêm mới thuộc tính và giá trị
                var newAttribute = new Attributes
                {
                    Name = attributeVm.Name,
                    IsActive = attributeVm.IsActive,
                    IsShow = attributeVm.IsShow,
                    Type = Core.Enum.AttributeType.variation
                };
                _unitOfWork.AttributeRepository.Create(newAttribute);
                _unitOfWork.SaveChanges();

                foreach (var valueId in attributeVm.AttributeValueId)
                {
                    var existingValue = await _unitOfWork.AtributeValueRepository.GetById(valueId);
                    if (existingValue.AttributeId == null)
                    {
                        existingValue.AttributeId = newAttribute.Id;

                        _unitOfWork.AtributeValueRepository.Update(existingValue);
                    }
                    else
                    {
                        var attributeValue = new AttributeValue
                        {
                            AttributeId = newAttribute.Id,
                            Value = existingValue.Value,
                            IsActive = existingValue.IsActive,
                            IsShow = existingValue.IsShow,
                            Type = Core.Enum.ValuesType.variation
                        };
                        await _unitOfWork.AtributeValueRepository.Create(attributeValue);
                    }

                }
                _unitOfWork.SaveChanges();
                return Ok(attributeVm);
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = (int)System.Net.HttpStatusCode.BadRequest,
                    Message = "Tạo biến thất bại",
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVariationProduct([FromBody] CreateVariantProductVM attributeVm)
        {
            if (attributeVm == null)
            {
                return BadRequest("Tạo biến thất bại, dữ liệu không hợp lệ.");
            }

            var existingVariant = await _context.Attributes.FirstOrDefaultAsync(c => c.Name == attributeVm.Name);
            if (existingVariant != null)
            {
                return Conflict("Thuộc tính đã tồn tại");
            }

            var newAttribute = new Attributes
            {
                Name = attributeVm.Name,
                IsActive = true,
                IsShow = true,
                Type = Core.Enum.AttributeType.variation
            };
            _unitOfWork.AttributeRepository.Create(newAttribute);
            _unitOfWork.SaveChanges();

            foreach (var value in attributeVm.Value)
            {
                var existingValue = await _context.AttributeValues.FirstOrDefaultAsync(c => c.Value == value.Value);
                if (existingValue != null)
                {
                    return Conflict("Giá trị đã tồn tại");
                }
                var attributeValue = new AttributeValue
                {
                    AttributeId = newAttribute.Id,
                    Value = value.Value,
                    IsActive = true,
                    IsShow = true,
                    Type = Core.Enum.ValuesType.variation
                };
                await _unitOfWork.AtributeValueRepository.Create(attributeValue);
            }
            _unitOfWork.SaveChanges();
            return Ok(attributeVm);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateAttributeDynamic([FromBody] UpdateVariantVM VariantVm)
        {
            if (VariantVm != null)
            {
                var attribute = await _unitOfWork.AttributeRepository.GetById(VariantVm.Id);
                if (attribute == null) return NotFound("Không tìm thấy thuộc tính");
                // Thêm mới thuộc tính và giá trị

                attribute.Name = VariantVm.Name;
                attribute.IsActive = VariantVm.IsActive;
                attribute.IsShow = VariantVm.IsShow;

                _unitOfWork.AttributeRepository.Update(attribute);

                foreach (var Variant in VariantVm.attributeValues)
                {
                    var value = await _unitOfWork.AtributeValueRepository.GetById(Variant.AtributeValueId);
                    if (value == null) return NotFound("Không tìm thấy giá trị");

                    value.Value = Variant.Value;
                    value.IsActive = Variant.IsActive;
                    value.IsShow = Variant.IsShow;

                    _unitOfWork.AtributeValueRepository.Update(value);
                }
                _unitOfWork.SaveChanges();
                return Ok(VariantVm);
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

        [HttpPut]
        public async Task<IActionResult> UpdateAttributeVariant([FromBody] UpdateVariantVM VariantVm)
        {
            if (VariantVm != null)
            {
                var attribute = await _unitOfWork.AttributeRepository.GetById(VariantVm.Id);
                if (attribute == null) return NotFound("Không tìm thấy thuộc tính");
                // Thêm mới thuộc tính và giá trị

                attribute.Name = VariantVm.Name;
                attribute.IsActive = VariantVm.IsActive;
                attribute.IsShow = VariantVm.IsShow;

                _unitOfWork.AttributeRepository.Update(attribute);

                foreach (var Variant in VariantVm.attributeValues)
                {
                    var value = await _unitOfWork.AtributeValueRepository.GetById(Variant.AtributeValueId);
                    if (value == null) return NotFound("Không tìm thấy giá trị");

                    value.Value = Variant.Value;
                    value.IsActive = Variant.IsActive;
                    value.IsShow = Variant.IsShow;

                    _unitOfWork.AtributeValueRepository.Update(value);
                }
                _unitOfWork.SaveChanges();
                return Ok(VariantVm);
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
    }
}
