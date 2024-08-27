using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.ViewModel.AndressVM;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.TimeRangeVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _signInManager = signInManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = _unitOfWork.CategoryRepository.GetAll().Where(p => p.IsVisible == true && p.Level == 1);
            if (category != null)
            {
                var categoriesVm = _mapper.Map<List<CategoryVM>>(category);

                return Ok(categoriesVm);
            }
            return NoContent(); // Trả về 204 nếu không có sản phẩm nào được tìm thấy
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategoryByLevel1(int cateLevel1Id)
        {
            var category = _unitOfWork.CategoryRepository.GetAll().Where(p => p.Level == 2 && p.ParentCategoryId == cateLevel1Id);
            if (category != null)
            {
                var categoriesVm = _mapper.Map<List<CategoryVM>>(category);

                return Ok(categoriesVm);
            }
            return NoContent(); // Trả về 204 nếu không có sản phẩm nào được tìm thấy
        }
        [HttpPost]
        public async Task<IActionResult> GetAllBrandByLevel2(List<CategoryVM> CatesVM)
        {
            //var category = _unitOfWork.CategoryRepository.GetAll().Where(p => p.Level == 2 && p.ParentCategoryId == cateLevel1Id);
            var brands = _unitOfWork.ProductRepository.GetAllCustom()
                .Where(p => p.CategoryProducts.Any(cp => CatesVM.Select(p=>p.Id).Contains(cp.CategoryId))).GroupBy(p=>p.Brand).Select(p=>p.Key)
                .ToList();
            if (brands != null)
            {
                var brandsVm = _mapper.Map<List<BrandVM>>(brands);

                return Ok(brandsVm);
            }
            return NoContent(); // Trả về 204 nếu không có sản phẩm nào được tìm thấy
        }
        [HttpGet]
        public async Task<IActionResult> GetAll_Admin()
        {
            var category = _unitOfWork.CategoryRepository.GetAll().Where(p => p.Level == 0);
            if (category != null)
            {
                var categoriesVm = _mapper.Map<List<CategoryVM>>(category);

                return Ok(categoriesVm);
            }
            return NoContent(); // Trả về 204 nếu không có sản phẩm nào được tìm thấy
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategoryByLevel0(int categoryId)
        {
            var category = _unitOfWork.CategoryRepository.GetAll().Where(p => p.ParentCategoryId == categoryId && p.IsOnList == true).ToList();
            if (category != null)
            {
                var categoriesVm = _mapper.Map<List<CategoryVM>>(category);

                return Ok(categoriesVm);
            }
            return NoContent(); // Trả về 204 nếu không có sản phẩm nào được tìm thấy
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategoryLevel1()
        {
            var category = _unitOfWork.CategoryRepository.GetAll().Where(p => p.Level == 1);
            if (category != null)
            {
                var List = new List<CategoryRepon>();
             

                foreach (var c in category)

                {
                    var timeRange = _unitOfWork.CategoryTimeRange.GetTimeRanebyCateId(c.Id).Select(x => x.TimeRange.Name).ToList();
                    

                    var result = new CategoryRepon()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Level = c.Level,
                        IsVisible = c.IsVisible,
                        IsOnList = c.IsOnList,
                        ImageUrl = c.ImageUrl,
                        ParentCategoryId = c.ParentCategoryId,
                        RangerName = timeRange
                    };
                    List.Add(result);
                }

                return Ok(List);
            }
            return NoContent(); // Trả về 204 nếu không có sản phẩm nào được tìm thấy
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoryLevel2()
        {
            var category = _unitOfWork.CategoryRepository.GetAll().Where(p => p.Level == 2);
            if (category != null)
            {
                var List = new List<CategoryRepon>();


                foreach (var c in category)

                {
                    var timeRange = _unitOfWork.CategoryTimeRange.GetTimeRanebyCateId(c.Id).Select(x => x.TimeRange.Name).ToList();


                    var result = new CategoryRepon()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Description = c.Description,
                        Level = c.Level,
                        IsVisible = c.IsVisible,
                        IsOnList = c.IsOnList,
                        ImageUrl = c.ImageUrl,
                        ParentCategoryId = c.ParentCategoryId,
                        RangerName = timeRange
                    };
                    List.Add(result);
                }

                return Ok(List);
            }
            return NoContent(); // Trả về 204 nếu không có sản phẩm nào được tìm thấy
        }


        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound(); // 404 Not Found
            }
            var addressDTO = _mapper.Map<Category>(category);
            return Ok(addressDTO); // 200 OK
        }

        [HttpPost]
        public async Task<IActionResult> CreateLv0([FromBody] CategoryAdminVM category)
        {
            if (category == null)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }
            else if (category.Level != 0)
            {
                return BadRequest(ModelState); // 400 Bad Request
            }
            else
            {
                var result = _mapper.Map<Category>(category);
                _unitOfWork.CategoryRepository.Create(result);
                _unitOfWork.SaveChanges();

                return Ok(result); // 201 Created
            }


        }

        [HttpPost]
        public async Task<IActionResult> CreateLv1([FromBody] CategoryAdminCreatLv category)
        {
            if (category == null)
            {
                return BadRequest("Notificaton data is null"); // 400 Bad Request
            }
          
            else
            {

                var request = new Category()
                {
                   // Id = category.Id,
                    Level = category.Level,
                    Name = category.Name,
                    Description = category.Description,
                    ParentCategoryId = category.ParentCategoryId,
                    CategoryTimeRanges = new List<CategoryTimeRange>()
                };
                foreach (var item in category.RangerId)
                {
                    var rangetime = new CategoryTimeRange()
                    {
                        TimeRangeId = item


                    };
                    request.CategoryTimeRanges.Add(rangetime);
                }
                _unitOfWork.CategoryRepository.Create(request);
                int result = _unitOfWork.SaveChanges();
                if (result == 0)
                {
                    return BadRequest();
                }
                return Ok(request); // 201 Created
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLv2([FromBody] CategoryAdminCreatLv category)
        {
            if (category == null)
            {
                return BadRequest("Notificaton data is null"); // 400 Bad Request
            }
            else if (category.Level != 2)
            {
                return BadRequest("Notificaton data is null"); // 400 Bad Request
            }
            else
            {
                var request = new Category()
                {
                  
                    Level = category.Level,
                    Name = category.Name,
                    Description = category.Description,
                    ParentCategoryId = category.ParentCategoryId,
                    CategoryTimeRanges = new List<CategoryTimeRange>()
                };
                foreach (var item in category.RangerId)
                {
                    var rangetime = new CategoryTimeRange()
                    {
                        TimeRangeId = item


                    };
                    request.CategoryTimeRanges.Add(rangetime);
                }
                _unitOfWork.CategoryRepository.Create(request);
                int result = _unitOfWork.SaveChanges();
                if (result == 0)
                {
                    return BadRequest();
                }
                return Ok(request); // 201 Created
            }


        }

        [HttpPut("edit-category")]
        public async Task<IActionResult> Update([FromBody] CategoryAdminVM category)
        {
            var result = await _unitOfWork.CategoryRepository.GetById(category.Id);
            if (result == null)
            {
                return NotFound();
            }
            _mapper.Map(category, result);
            _unitOfWork.CategoryRepository.Update(result);
            _unitOfWork.SaveChanges();
            return Ok(result);
        }
        [HttpPut("edit-category-lv")]
        public async Task<IActionResult> UpdateLv([FromBody] CategoryEditAdminLv category)
        {
            var result = _unitOfWork.CategoryRepository.GetCateByIdCustom(category.Id);
            // var obj = _unitOfWork.CategoryTimeRange.GetById(category.RangerId);
            result.Level = category.Level;
            result.Name = category.Name;
            result.Description = category.Description;
            result.IsOnList = category.IsOnList;
            result.IsVisible = category.IsVisible;
            result.ParentCategoryId = category.ParentCategoryId;
            result.CategoryTimeRanges.Clear();
            
            _unitOfWork.SaveChanges();

           


            foreach (var item in category.RangerId)
            {                
                var Rgtime = new CategoryTimeRange()
                {
                    TimeRangeId = item,
                    CategoryId = category.Id


                };
                result.CategoryTimeRanges.Add(Rgtime);
                _unitOfWork.SaveChanges();

            }
            

            _unitOfWork.SaveChanges();
            return Ok(result);

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.CategoryRepository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            _unitOfWork.CategoryRepository.Delete(result);
            _unitOfWork.SaveChanges();
            return Ok(result);
        }


        [HttpPost]
        public IActionResult GetCategoryPaging([FromBody] CategoryPaging request)
        {
            CategoryPaging partnerPaging = _unitOfWork.CategoryRepository.CategoryPaging(request);
            return Ok(partnerPaging);
        }

        [HttpPost]
        public IActionResult GetCategoryPagingLv1([FromBody] CategoryPagingLv1 request)
        {
            CategoryPagingLv1 partnerPaging = _unitOfWork.CategoryRepository.CategoryPagingLv1(request);
            return Ok(partnerPaging);

            // var timeRange = _unitOfWork.CategoryTimeRange.GetTimeRanebyCateId(c.Id).Where(c=>c.isActive==true).Select(x => x.TimeRange.Name).ToList();
        }

        [HttpPost]
        public IActionResult GetCategoryPagingLv2([FromBody] CategoryPagingLv2 request)
        {
            CategoryPagingLv2 partnerPaging = _unitOfWork.CategoryRepository.CategoryPagingLv2(request);
            return Ok(partnerPaging);
        }

    }
}
