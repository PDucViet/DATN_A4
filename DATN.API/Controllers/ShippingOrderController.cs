using AutoMapper;
using DATN.Api.MailService;
using DATN.API.Helpers;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.ViewModel.InvoiceVM;
using DATN.Core.ViewModel.ShippingOrderVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShippingOrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailService _emailService;


        public ShippingOrderController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, EmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _unitOfWork.ShippingOrderRepository.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetByUserId(Guid UserId)
        {
            var oders = _unitOfWork.ShippingOrderRepository.GetAll();
            var result = oders.Where(x => x.UserId == UserId).ToList();
            if (result != null)
            {
                return Ok(result);
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateShippingOrderRepuest request)
        {
            if (request == null)
            {
                return BadRequest("ShippingOrder data is null");
            }
            var shippingOrder = new ShippingOrder()
            {
                OrderCode = request.ShippingOrderCode,
                UserId = request.CustomerId,
                InvoiceId = request.InvoiceId,
                Price = request.ShippingFee
            };
            await _unitOfWork.ShippingOrderRepository.Create(shippingOrder);
            var result = _unitOfWork.SaveChanges();
            if (result == 0)
            {
                return BadRequest("Create ShippingOrder fail");
            }
            var user = await _userManager.FindByIdAsync(Convert.ToString(request.CustomerId));
            if (user != null)
            {
                var invoice = _unitOfWork.InvoiceRepository.GetByIdCustom(request.InvoiceId);
                var sendMail = InvoiceContent.GenerateContentMail(user, invoice);
                await _emailService.SendEmailAsync(sendMail);
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetByOrderCode(string code)
        {
            var orders = _unitOfWork.ShippingOrderRepository.getshippingcustom();
            var result = orders.Where(x=>x.OrderCode == code).FirstOrDefault();
         
            if (result != null)
            {
                return Ok(result);
            }
            return NoContent();
        }
    }
}
