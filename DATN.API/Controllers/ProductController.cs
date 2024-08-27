using AutoMapper;
using DATN.API.Data.Filter;
using DATN.Core.Data;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.ProductVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Linq;
using System.Security.AccessControl;
using System.Net;
using System.Reflection;
using DATN.Core.ViewModel.ProductVM.CreateProduct2;
using AutoMapper.Configuration.Annotations;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModels.Paging;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepo;

        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, DATNDbContext context, IProductRepository productRepository, IProductRepository productRepo)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _productRepository = productRepository;
            _productRepo = productRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productRepo.GetAll();

            if (products != null)
            {
                var productsVM = _mapper.Map<List<ProductVM>>(products);
                
                return Ok(productsVM);
            }
            return NoContent();
        }

        [HttpPost]
        public IActionResult GetProductPaging([FromBody] ProductPaging request)
        {
            ProductPaging partnerPaging = _unitOfWork.ProductRepository.ProductPaging(request);
            return Ok(partnerPaging);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var product = _unitOfWork.ProductRepository.GetAllCustom().Where(p => p.Id == id).First();
            if (product == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(product);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = _unitOfWork.ProductRepository.GetByIdCustom(id);

            var brand = await _unitOfWork.brandRepository.GetById(products.BrandId);

            products.Brand.Name = brand.Name;
            if (products != null)
            {
                var productsVM = _mapper.Map<ProductVM>(products);
                return Ok(productsVM);
            }
            else
            {
                return NotFound();
            }
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById2(int? id)
        //{
        //    try
        //    {
        //        var product =  _productRepository.GetById(id);
        //        var productsVM = _mapper.Map<ProductVM>(product);
        //        return Ok(productsVM);
        //    }
        //    catch (Exception ex)
        //    {
        //         await Console.Out.WriteLineAsync(ex.Message);
        //        return null;
        //    }
        //}

        // PUT: api/Product/Update/{id}
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductApi productVm)
        {
            var exsistingProduct = await _unitOfWork.ProductRepository.GetById(productVm.Id);
            if (exsistingProduct == null)
            {
                return NotFound();
            }

            exsistingProduct.Name = productVm.Name;
            exsistingProduct.Description = productVm.Description;
            exsistingProduct.BrandId = productVm.BrandId;
            exsistingProduct.Status = productVm.Status;
            exsistingProduct.OriginId = productVm.OriginId;
            exsistingProduct.CreateBy = productVm.CreateBy;

            _unitOfWork.ProductRepository.Update(exsistingProduct);
            var result =  _unitOfWork.SaveChanges();
            if (result >0 )
            {
                return Ok();
            }
            else
            {
                return NotFound("Update không thành công");
            }
           
        }

        // POST: api/Product/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductApi productVm)
        {
            if (productVm == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid product data");
            }

            var product = new Product
            {
                Name = productVm.Name,
                Description = productVm.Description,
                CreateBy = productVm.CreateBy,
                OriginId = productVm.OriginId,
                BrandId = productVm.BrandId,
            };

            _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.SaveChanges();

            // Handle image uploads and variants processing here

            return Ok(product);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct2([FromBody] CreateProduct2VM productVm)
        {
            if (productVm == null)
            {
                return BadRequest("Product data is null");
            }

            //if (productVm.ImageSave != null)
            //{
            //    var fileName = Path.GetFileNameWithoutExtension(productVm.ImageSave.FileName);
            //    var extension = Path.GetExtension(productVm.ImageSave.FileName);
            //    var newFileName = $"{productVm.Name}_{DateTime.Now:yyyyMMddHHmmss}{extension}";
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Products", newFileName);

            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await productVm.ImageSave.CopyToAsync(stream);
            //    }

            //    // Lưu đường dẫn vào thuộc tính imagePath
            //    productVm.imagePath = $"/Images/Products/{newFileName}";
            //}
            //var product = new Product

            var product = _mapper.Map<Product>(productVm);
            _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.SaveChanges();

            return Ok(product); // 201 Created
        }
        // DELETE: api/Product/Delete/{id}
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.SaveChanges();

            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductVM>>> GetProductByCategory(int categoryId)
        {
            // Lấy danh mục với categoryId truyền vào
            var category = await _unitOfWork.CategoryRepository.GetAll().AsQueryable()
                .FirstOrDefaultAsync(c => c.Id == categoryId);

            if (category == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy danh mục
            }

            List<int> categoryIds = new List<int>();

            // Nếu danh mục là cấp 1, lấy thêm các danh mục con cấp 2
            if (category.Level == 1)
            {
                var subCategories = await _unitOfWork.CategoryRepository.GetAll().AsQueryable()
                    .Where(c => c.ParentCategoryId == categoryId && c.Level == 2)
                    .ToListAsync();

                categoryIds.AddRange(subCategories.Select(c => c.Id));
            }

            // Thêm danh mục truyền vào vào danh sách Id
            categoryIds.Add(categoryId);

            // Lấy danh sách sản phẩm theo danh mục cấp 1 và cấp 2
            var products = _unitOfWork.ProductRepository.GetAllCustom()
                .Where(p => p.CategoryProducts.Any(cp => categoryIds.Contains(cp.CategoryId)))
                .ToList();
            if (products != null && products.Any())
            {
                var productVms = _mapper.Map<List<ProductVM>>(products);
                return Ok(productVms);
            }

            return NoContent();
        }

		[HttpGet]
		public async Task<ActionResult<List<Product>>> GetProductByBrand(string brandId)
		{
			var products =  _unitOfWork.ProductRepository.GetAll()
				.Where(p=>p.BrandId == int.Parse(brandId))
				.ToList();


			if (products != null && products.Any())
			{
				var productVms = _mapper.Map<List<ProductVM>>(products);
				return Ok(productVms);
			}

			return NoContent(); // Trả về 204 nếu không tìm thấy sản phẩm nào
		}

		private IEnumerable<Product> FilterProduct(ProductFilter filter)
		{
			var query = _unitOfWork.ProductRepository.GetAll().Where(x => x.Status != ProductStatus.stop&&x.CategoryProducts.Any(p=>p.Category.ParentCategoryId==filter.Cate));
			//if (filter.BrandId != null && filter.BrandId.Any() && filter.CateId != null && filter.CateId.Any())
			//{
			//	query = query.Where(x => filter.BrandId.Contains(x.BrandId.Value)&& x.CategoryProducts.Any(p=>filter.CateId.Contains(p.CategoryId)));
			//}else if (filter.BrandId != null && filter.BrandId.Any())
   //         {
   //             query = query.Where(x => filter.BrandId.Contains(x.BrandId.Value));
   //         }else if(filter.CateId != null && filter.CateId.Any())
   //         {
   //             query = query.Where(x => x.CategoryProducts.Any(p => filter.CateId.Contains(p.CategoryId)));
   //         }else if (filter.MinPrice != null && filter.MaxPrice != null)
   //         {
   //             var productVM = _mapper.Map<List<ProductVM>>(query);
   //             var result = productVM.Where(p=>p.DefaultPrice>=filter.MinPrice&& p.DefaultPrice<=filter.MaxPrice).ToList();
   //             query = _mapper.Map<List<Product>>(result);
   //         }
            if (filter.BrandId != null && filter.BrandId.Any())
            {
                query = query.Where(x => filter.BrandId.Contains(x.BrandId.Value));
            }
            if (filter.CateId != null && filter.CateId.Any())
            {
                query = query.Where(x => x.CategoryProducts.Any(p => filter.CateId.Contains(p.CategoryId)));
            }
            if (filter.MinPrice != null && filter.MaxPrice != null)
            {
                var productVM = _mapper.Map<List<ProductVM>>(query);
                var result = productVM.Where(p => p.DefaultPrice >= filter.MinPrice && p.DefaultPrice <= filter.MaxPrice).ToList();
                query = _mapper.Map<List<Product>>(result);
            }
            return query;
		}
		[HttpPost]
        public ActionResult GetProductByFilter([FromBody] ProductFilter productFilter)
        {
            var products = FilterProduct(productFilter).ToList();
            if (products != null && products.Any())
            {
                var productVms = _mapper.Map<List<ProductVM>>(products);
                return new JsonResult(productVms);
            }
            return new JsonResult(new object[] { });
        }
     
        [HttpGet]
        public ActionResult<List<Product>> GetProductByPromotion(int promotionId)
        {
            var products = _unitOfWork.productPromotionRepository.GetAllCustom().Where(p => p.PromotionId == promotionId).Select(p => p.Product).ToList();


            if (products != null && products.Any())
            {
                var productVms = _mapper.Map<List<ProductVM>>(products);
                return Ok(productVms);
            }

            return NoContent(); // Trả về 204 nếu không tìm thấy sản phẩm nào
        }
        [HttpGet]
        public ActionResult<double> GetProductRating(int productId)
        {
            var data = _unitOfWork.InvoiceDetailRepository.GetAll().Where(p => p.ProductAttribute.ProductId == productId).ToList();
            if (data != null && data.Count > 0)
            {
                return Ok(Math.Round((decimal)data.Where(p=>p.Comment!=null).Average(p => p.Comment.Rating), 1));
            }
            return Ok(5);

        }
        [HttpGet]
        public ActionResult<int> GetProductRateCount(int productId)
        {
            var data = _unitOfWork.InvoiceDetailRepository.GetAll().Where(p => p.ProductAttribute.ProductId == productId).Count(p=>p.Comment!=null);
            return Ok(data);

        }
        [HttpGet]
        public ActionResult<ProductPaging> GetProductBySearch(string? search, int page = 1, int pageSize = 10)
        {
            var query = _unitOfWork.ProductRepository.GetAll()
                .Where(p => p.Name.ToLower().Contains(search.ToLower())); // Phân trang không phân biệt chữ hoa chữ thường

            var totalRecords = query.Count(); // Tổng số bản ghi
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize); // Tính tổng số trang

            var products = query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList(); // Lấy danh sách sản phẩm cho trang hiện tại

            if (products != null && products.Any())
            {
                var productVms = _mapper.Map<List<ProductVM>>(products);

                var productPaging = new ProductPaging
                {
                    Items = productVms,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalPages = totalPages,
                    TotalRecord = totalRecords
                };

                return Ok(productPaging);
            }

            return NoContent(); // Trả về 204 nếu không tìm thấy sản phẩm nào
        }

    }
}
