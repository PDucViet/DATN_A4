using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.ViewModel.AndressVM;
using DATN.Core.ViewModel.ContactVM;
using DATN.Core.ViewModel.ProductVM;
using DATN.Core.ViewModel.PromotionVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PromotionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetAllActive()
        {
            var promotions = _unitOfWork.PromotionRepository.GetAll().Where(p=>p.From<DateTime.Now&&p.To>DateTime.Now&&p.IsActive==true).OrderByDescending(p=>p.From).ToList();
            if (promotions != null)
            {
                var promotionVms = _mapper.Map<List<PromotionVM>>(promotions);
                return Ok(promotionVms);
            }
            return NoContent(); // Trả về 204 nếu không có sản phẩm nào được tìm thấy
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int promotionId)
        {
            var promotion = await _unitOfWork.PromotionRepository.GetById(promotionId);
            if (promotion != null)
            {
                var promotionVm = _mapper.Map<PromotionVM>(promotion);
                return Ok(promotionVm);
            }
            return NoContent(); // Trả về 204 nếu không có sản phẩm nào được tìm thấy
        }
        [HttpGet("productId")]   
        public async Task<IActionResult> GetPromotionByProductId(int productId)
        {
            var promotion = _unitOfWork.productPromotionRepository.GetAllByProduct().Where(p => p.ProductId == productId).Select(p => p.Promotion).ToList();
            if (promotion!=null && promotion.Any())
            {
                var result = _mapper.Map<List<PromotionVM>>(promotion);
               return Ok(result);
            }
            return NoContent(); // Trả về 204 nếu không có sản phẩm nào được tìm thấy
        }


    }
}
