using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.ProductVM;
using DATN.Core.ViewModel.ProductVM.AtributeCreateVM;
using DATN.Core.ViewModel.ProductVM.AttributeUpdateVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Cms;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Attributes = DATN.Core.Model.Product.Attributes;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EAVController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;

        public EAVController(IUnitOfWork unitOfWork, IMapper mapper, DATNDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }

        // GET: api/products/{productId}/attributes
        [HttpGet("id")]
        [SwaggerOperation(Summary = "Get list attribute values")]
        public async Task<IActionResult> GetProductAttributes(int id)
        {
            var productAttributes = await _context.ProductAttributes
                .Include(pa => pa.AttributeValue)
                .ThenInclude(a => a.Attributes)
                .Where(pa => pa.ProductId == id && pa.AttributeValue.Attributes.Type == Core.Enum.AttributeType.variation)
                .ToListAsync();

            if (productAttributes == null || !productAttributes.Any())
            {
                return NotFound();
            }

            var attributeGroups = productAttributes.GroupBy(pa => pa.AttributeValue.Attributes)
                  .Select(g => new ProductAttributeDetailVM
                  {
                      AttributeId = g.Key.Id,
                      AttributeName = g.Key.Name,
                      IsActive = g.Key.IsActive,
                      ValueVms = g.Select(pa => new AttributeValueVM
                      {
                          AtributeValueId = pa.AttributeValue.AtributeValueId,
                          ProductAttributeId = pa.Id,
                          Quantity = pa.Quantity,
                          SalePrice = pa.SalePrice,
                          PuscharPrice = pa.PuscharPrice,
                          AfterDiscountPrice = pa.AfterDiscountPrice,
                          ReleaseYear = pa.ReleaseYear,
                          Tax = pa.Tax,
                          IsActive = pa.AttributeValue.IsActive,
                          IsShow = pa.AttributeValue.IsShow,
                          Value = pa.AttributeValue.Value
                      }).ToList()
                  }).ToList();

            return Ok(attributeGroups);
        }

        [HttpGet("id")]
        [SwaggerOperation(Summary = "Get list attribute values")]
        public async Task<IActionResult> GetListAttributes(int id)
        {
            var listAttributes = await _context.Attributes
                .Include(pa => pa.AttributeValues)
                .Where(pa => pa.ProductId == id && pa.Type == Core.Enum.AttributeType.dynamic)
                .ToListAsync();

            if (listAttributes == null || !listAttributes.Any())
            {
                return BadRequest(new
                {
                    StatusCode = (int)System.Net.HttpStatusCode.BadRequest,
                    Message = "Invalid product ID.",
                    Detailed = "Product ID must be greater than zero."
                });
            }

            var attributeGroups = listAttributes.GroupBy(pa => pa)
          .Select(g => new AttributesVM
          {
              Id = g.Key.Id,
              Name = g.Key.Name,
              IsActive = g.Key.IsActive,
                IsShow = g.Key.IsShow,
              attributeValues = g.SelectMany(a => a.AttributeValues.Select(av => new AttributeValueVM
              {
                  AtributeValueId = av.AtributeValueId,
                  Value = av.Value,
                  IsActive = av.IsActive,
                  IsShow = av.IsShow,
              })).ToList()
          }).ToList();

            return Ok(attributeGroups);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create list attribute and list value")]
        public async Task<IActionResult> AddListAttributesValues(CreateAttributesViewVm createAttributesViewVm)
        {
            if (createAttributesViewVm.productId != null || createAttributesViewVm.attributes != null)
            {
                foreach (var attribute in createAttributesViewVm.attributes)
                {
                    // Thêm mới thuộc tính và giá trị
                    var newAttribute = new Attributes
                    {
                        Name = attribute.Name,
                        IsActive = attribute.IsActive,
                        ProductId = createAttributesViewVm.productId
                    };
                    _unitOfWork.AttributeRepository.Create(newAttribute);
                    _unitOfWork.SaveChanges();
                    foreach (var value in attribute.Values)
                    {
                        var attributeValue = new AttributeValue
                        {
                            Value = value.Value,
                            IsActive = attribute.IsActive,
                            AttributeId = newAttribute.Id,
                            Attributes = newAttribute
                        };
                        await _unitOfWork.AtributeValueRepository.Create(attributeValue);
                        _unitOfWork.SaveChanges();

                        var productAttribute = new ProductAttribute
                        {
                            ProductId = createAttributesViewVm.productId,
                            Quantity = value.Quantity,
                            SalePrice = value.Price,
                            AttributeValueId = attributeValue.AtributeValueId
                        };
                        await _unitOfWork.ProductAtributeRepository.Create(productAttribute);
                        _unitOfWork.SaveChanges();
                    }
                }
                return Ok(createAttributesViewVm);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create list attribute and list value")]
        public async Task<IActionResult> AddListAttributes(CreateAttributesViewVm createAttributesViewVm)
        {
            if (createAttributesViewVm.productId != null || createAttributesViewVm.attributes != null)
            {
                foreach (var attribute in createAttributesViewVm.attributes)
                {
                    // Thêm mới thuộc tính và giá trị
                    var newAttribute = new Attributes
                    {
                        Name = attribute.Name,
                        IsActive = attribute.IsActive,
                        ProductId = createAttributesViewVm.productId
                    };
                    _unitOfWork.AttributeRepository.Create(newAttribute);
                    _unitOfWork.SaveChanges();
                    foreach (var value in attribute.Values)
                    {
                        var attributeValue = new AttributeValue
                        {
                            Value = value.Value,
                            IsActive = attribute.IsActive,
                            AttributeId = newAttribute.Id,
                            Attributes = newAttribute
                        };
                        await _unitOfWork.AtributeValueRepository.Create(attributeValue);
                        _unitOfWork.SaveChanges();
                    }
                }
                return Ok(createAttributesViewVm);
            }
            else
            {
                return BadRequest(new
                {
                    StatusCode = (int)System.Net.HttpStatusCode.BadRequest,
                    Message = "Invalid product ID.",
                    Detailed = "Product ID must be greater than zero."
                });
            }
        }

        //[HttpPut("productId")]
        //public async Task<IActionResult> UpdateListAttributesValues([FromQuery] int productId, [FromBody] AttributeData attributeData)
        //{
        //    try
        //    {
        //        if (attributeData == null || productId == null)
        //        {
        //            return BadRequest("Attributes or product data is missing or empty.");
        //        }

        //        foreach (var attribute in attributeData.attributes)
        //        {
        //            var existingAttribute = await _unitOfWork.AttributeRepository.GetById(attribute.Id);

        //            if (existingAttribute != null)
        //            {
        //                // Update existing attribute
        //                existingAttribute.Name = attribute.AttributeName;
        //                existingAttribute.IsActive = attribute.IsActive;
        //                existingAttribute.Type = Core.Enum.AttributeType.dynamic;

        //                _unitOfWork.AttributeRepository.Update(existingAttribute);

        //                foreach (var value in attribute.ValueVms)
        //                {
        //                    var existingAttributeValue = await _unitOfWork.AtributeValueRepository.GetById(value.Id);

        //                    if (existingAttributeValue != null)
        //                    {
        //                        // Update existing attribute value
        //                        existingAttributeValue.Value = value.Value;
        //                        existingAttributeValue.IsActive = value.IsActive;

        //                        _unitOfWork.AtributeValueRepository.Update(existingAttributeValue);

        //                        // Update or create product attribute
        //                        var productAttribute = await _unitOfWork.ProductAtributeRepository.GetById(value.ProductAttributeId);
        //                        productAttribute.Quantity = value.Quantity;
        //                        productAttribute.SalePrice = value.Price;
        //                        _unitOfWork.ProductAtributeRepository.Update(productAttribute);
        //                    }
        //                    else
        //                    {
        //                        // Create new attribute value
        //                        var newAttributeValue = new AttributeValue
        //                        {
        //                            Value = value.Value,
        //                            IsActive = value.IsActive,
        //                            AttributeId = existingAttribute.Id
        //                        };

        //                        _unitOfWork.AtributeValueRepository.Create(newAttributeValue);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                // Create new attribute
        //                var newAttribute = new Attributes
        //                {
        //                    Name = attribute.AttributeName,
        //                    IsActive = attribute.IsActive,
        //                    Type = Core.Enum.AttributeType.dynamic,
        //                    ProductId = productId
        //                };

        //                _unitOfWork.AttributeRepository.Create(newAttribute);
        //                _unitOfWork.SaveChanges(); // Save to get the new attribute ID

        //                foreach (var value in attribute.ValueVms)
        //                {
        //                    // Create new attribute value
        //                    var newAttributeValue = new AttributeValue
        //                    {
        //                        Value = value.Value,
        //                        IsActive = value.IsActive,
        //                        AttributeId = newAttribute.Id
        //                    };

        //                    _unitOfWork.AtributeValueRepository.Create(newAttributeValue);
        //                    _unitOfWork.SaveChanges(); // Save to get the new attribute value ID

        //                    // Create new product attribute
        //                    var newProductAttribute = new ProductAttribute
        //                    {
        //                        ProductId = productId,
        //                        Quantity = value.Quantity,
        //                        SalePrice = value.Price,
        //                        AttributeValueId = newAttributeValue.AtributeValueId
        //                    };

        //                    _unitOfWork.ProductAtributeRepository.Create(newProductAttribute);
        //                }
        //            }
        //        }
        //        _unitOfWork.SaveChanges();
        //        return Ok(new { Message = "Attributes updated successfully." });
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        [HttpPut("productId")]
        public async Task<IActionResult> UpdateListVariantOfProduct([FromQuery] int productId, [FromBody] List<AttributeValuesUpdateVM> VariantData)
        {
            try
            {
                if (VariantData == null || productId == 0)
                {
                    return BadRequest("Attributes or product data is missing or empty.");
                }

                foreach (var value in VariantData)
                {
                    if (value.AttributeValueId != null && value.ProductAttributeId != null)
                    {
                        // Update existing attribute value
                        var existingAttributeValue = await _unitOfWork.AtributeValueRepository.GetById(value.AttributeValueId);
                        if (existingAttributeValue != null)
                        {
                            existingAttributeValue.Value = value.Value;
                            existingAttributeValue.IsActive = value.IsActive;
                            existingAttributeValue.IsShow = value.IsShow;
                            _unitOfWork.AtributeValueRepository.Update(existingAttributeValue);


                            var productAttribute = await _unitOfWork.ProductAtributeRepository.GetById(value.ProductAttributeId);
                            if (productAttribute != null)
                            {
                                productAttribute.Quantity = value.Quantity;
                                productAttribute.Tax = value.Tax;
                                productAttribute.PuscharPrice = value.PuscharPrice;
                                productAttribute.SalePrice = value.SalePrice;
                                productAttribute.ReleaseYear = value.ReleaseYear;
                                _unitOfWork.ProductAtributeRepository.Update(productAttribute);
                            }
                        }
                        else
                        {
                            return BadRequest("Attribute value not found.");
                        }
                    }
                    else
                    {
                        // Create new Variant Value
                        var newAttributeValue = new AttributeValue
                        {
                            AttributeId = value.AttributeId,
                            Value = value.Value,
                            IsActive = value.IsActive,
                            IsShow = value.IsShow,
                            Type = Core.Enum.ValuesType.variation
                        };

                        var createdAttributeValue = _unitOfWork.AtributeValueRepository.CreateCustom(newAttributeValue);

                        if (createdAttributeValue != null)
                        {
                            var newProductAttribute = new ProductAttribute
                            {
                                ProductId = productId,
                                AttributeValueId = createdAttributeValue.AtributeValueId,
                                Quantity = value.Quantity,
                                Tax = value.Tax,
                                PuscharPrice = value.PuscharPrice,
                                SalePrice = value.SalePrice,
                                ReleaseYear = value.ReleaseYear
                            };
                            _unitOfWork.ProductAtributeRepository.Create(newProductAttribute);
                        }
                        else
                        {
                            return BadRequest("Failed to create new attribute value.");
                        }
                    }
                }

                _unitOfWork.SaveChanges();
                return Ok(new { Message = "Attributes updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("productId")]
        public async Task<IActionResult> UpdateListDynamicOfProduct([FromQuery] int productId, [FromBody] List<AttributeDynamicUpdateVM> DynamicData)
        {
            try
            {
                if (DynamicData == null || productId == 0)
                {
                    return BadRequest("Attributes or product data is missing or empty.");
                }

                foreach (var value in DynamicData)
                {
                    if (value.AttributeValueId != null && value.AttributeId != null)
                    {
                        var existingAttribute = await _unitOfWork.AttributeRepository.GetById(value.AttributeId);
                        if (existingAttribute != null) {
                            existingAttribute.Name = value.Name;
                            existingAttribute.IsActive = true;
                            existingAttribute.IsShow = true;


                            _unitOfWork.AttributeRepository.Update(existingAttribute);
                            // Update existing attribute value
                            var existingAttributeValue = await _unitOfWork.AtributeValueRepository.GetById(value.AttributeValueId);
                            if (existingAttributeValue != null)
                            {
                                existingAttributeValue.Value = value.Value;
                                existingAttributeValue.IsActive = value.IsActive;
                                existingAttributeValue.IsShow = value.IsShow;
                                _unitOfWork.AtributeValueRepository.Update(existingAttributeValue);
                            }
                            else
                            {
                                return BadRequest("Attribute not found.");
                            }
                        }
                        else
                        {
                            return BadRequest("Attribute value not found.");
                        }


                    }
                    else
                    {
                        var newAttribute = new Attributes()
                        {
                            Name = value.Name,
                            IsShow = true,
                            IsActive = true,
                            Type = Core.Enum.AttributeType.dynamic,
                            ProductId = productId
                        };

                        var createdAttribute = _unitOfWork.AttributeRepository.CreateCustom(newAttribute);
                                           
                        if (createdAttribute != null)
                        {
                            // Create new Variant Value
                            var newAttributeValue = new AttributeValue
                            {
                                AttributeId = newAttribute.Id,
                                Value = value.Value,
                                IsActive = value.IsActive,
                                IsShow = value.IsShow,
                                Type = Core.Enum.ValuesType.dynamic
                            };
                            var createdAttributeValue = _unitOfWork.AtributeValueRepository.CreateCustom(newAttributeValue);
                            if (createdAttributeValue != null)
                            {
                                var newProductAttribute = new ProductAttribute
                                {
                                    ProductId = productId,
                                    AttributeValueId = createdAttributeValue.AtributeValueId,
                                };
                                _unitOfWork.ProductAtributeRepository.Create(newProductAttribute);
                            }
                            else
                            {
                                return BadRequest("Failed to create new attribute value.");
                            }
                        }
                        else
                        {
                            return BadRequest("Failed to create new attribute");
                        }
                    }
                }
                _unitOfWork.SaveChanges();
                return Ok(new { Message = "Attributes updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPut("productId")]
        public async Task<IActionResult> UpdateListAttributes([FromQuery] int productId, [FromBody] AttributeData attributeData)
        {
            try
            {
                if (attributeData == null || productId == null)
                {
                    return BadRequest("Attributes or product data is missing or empty.");
                }

                foreach (var attribute in attributeData.attributes)
                {
                    var existingAttribute = await _unitOfWork.AttributeRepository.GetById(attribute.Id);

                    if (existingAttribute != null)
                    {
                        // Update existing attribute
                        existingAttribute.Name = attribute.AttributeName;
                        existingAttribute.IsActive = attribute.IsActive;
                        existingAttribute.Type = Core.Enum.AttributeType.variation;
                        _unitOfWork.AttributeRepository.Update(existingAttribute);

                        foreach (var value in attribute.ValueVms)
                        {
                            var existingAttributeValue = await _unitOfWork.AtributeValueRepository.GetById(value.AttributeValueId);

                            if (existingAttributeValue != null)
                            {
                                // Update existing attribute value
                                existingAttributeValue.Value = value.Value;
                                existingAttributeValue.IsActive = value.IsActive;

                                _unitOfWork.AtributeValueRepository.Update(existingAttributeValue);
                            }
                            else
                            {
                                // Create new attribute value
                                var newAttributeValue = new AttributeValue
                                {
                                    Value = value.Value,
                                    IsActive = value.IsActive,
                                    AttributeId = existingAttribute.Id
                                };

                                _unitOfWork.AtributeValueRepository.Create(newAttributeValue);
                            }
                        }
                    }
                    else
                    {
                        // Create new attribute
                        var newAttribute = new Attributes
                        {
                            Name = attribute.AttributeName,
                            IsActive = attribute.IsActive,
                            Type = Core.Enum.AttributeType.variation,
                            ProductId = productId
                        };

                        _unitOfWork.AttributeRepository.Create(newAttribute);
                        _unitOfWork.SaveChanges(); // Save to get the new attribute ID

                        foreach (var value in attribute.ValueVms)
                        {
                            // Create new attribute value
                            var newAttributeValue = new AttributeValue
                            {
                                Value = value.Value,
                                IsActive = value.IsActive,
                                AttributeId = newAttribute.Id
                            };

                            _unitOfWork.AtributeValueRepository.Create(newAttributeValue);
                        }
                    }
                }
                _unitOfWork.SaveChanges();
                return Ok(new { Message = "Attributes updated successfully." });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}



