using AutoMapper;
using DATN.Api.MailService;
using DATN.API.Helpers;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.Repositories.Repositories;
using DATN.Core.ViewModel.AndressVM;
using DATN.Core.ViewModel.InvoiceDetailVM;
using DATN.Core.ViewModel.InvoiceVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModels.SendMailVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailService _emailService;
        private readonly IMapper _mapper;

        public InvoiceController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, EmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        // GET: api/invoices
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = _unitOfWork.InvoiceRepository.GetAll();
            if (invoices == null || !invoices.Any())
            {
                return NoContent(); // 204 No Content
            }
            var invoiceDTOs = _mapper.Map<List<InvoiceVM>>(invoices);
            return Ok(invoiceDTOs); // 200 OK
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var invoice = await _unitOfWork.InvoiceRepository.GetById(id);
            if (invoice == null)
            {
                return NotFound(); // 404 Not Found
            }
            var invoiceDTO = _mapper.Map<InvoiceVM>(invoice);
            return Ok(invoiceDTO); // 200 OK
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = _unitOfWork.InvoiceRepository.GetByIdCustom(id);
            if (invoice == null)
            {
                return NotFound(); // 404 Not Found
            }
            var invoiceDTO = _mapper.Map<InvoiceVM>(invoice);
            return Ok(invoiceDTO); // 200 OK
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentRequest payment)
        {
            if (payment == null)
            {
                return BadRequest("Invoice data is null"); // 400 Bad Request
            }

            // Tạo đối tượng Invoice
            var invoice = new Invoice()
            {
                //TotalAmount = payment.TotalAmount,
                //Discount = payment.Discount,
                //FinalAmount = payment.FinalAmount,
                UserId = payment.UserId,
                CreateDate = DateTime.Now,
                InvoiceDetails = new List<InvoiceDetail>(),
                Note = $"{payment.PendingShippingOrder.to_name}-{payment.PendingShippingOrder.to_phone}-{payment.PendingShippingOrder.to_address}-{payment.PendingShippingOrder.to_ward_code}-{payment.PendingShippingOrder.to_district_id}-{payment.PendingShippingOrder.cod_amount}"
            };

            // Tạo đối tượng PaymentInfo
            var paymentMethod = new PaymentInfo()
            {
                PaymentMethod = payment.PaymentMethod,
                PaymentStatus = 0,
            };

            invoice.PaymentInfo = paymentMethod;

            // Thêm các chi tiết hóa đơn vào Invoice
            foreach (var item in payment.CartItems)
            {
                var product = await _unitOfWork.ProductRepository.GetById(item.ProductId);
                if (product == null)
                {
                    return BadRequest($"Product with ID {item.ProductId} not found");
                }
                var invoiceDetail = new InvoiceDetail()
                {
                    ProductAttributeId = item.ProductId,
                    Quantity = item.Quantity,
                    NewPrice = item.NewPrice,
                    OldPrice = item.OldPrice,
                    PuscharPrice = item.GiaNhap
                };
                invoice.InvoiceDetails.Add(invoiceDetail);
                foreach (var i in payment.CartItems)
                {
                    var productATTUpdate = await _unitOfWork.ProductAtributeRepository.GetById(i.ProductId);
                    productATTUpdate.Quantity -= i.Quantity;
                    _unitOfWork.ProductAtributeRepository.Update(productATTUpdate);
                }
                if (payment.VoucherId != 0)
                {
                    var voucher = _unitOfWork.voucherUserRepository.GetByIdCustom(payment.VoucherId);
                    invoice.VoucherUserId = voucher.Id;
                    invoice.VoucherUser = voucher;
                    voucher.IsDeleted = true;
                    _unitOfWork.voucherUserRepository.Update(voucher);
                }
            }
            if (payment.PaymentMethod == PaymentMethod.Cash)
            {
                invoice.Status = InvoiceStatus.Pending;
            }
            else
            {
                invoice.Status = InvoiceStatus.PaymentProcessing;
            }
            await _unitOfWork.InvoiceRepository.Create(invoice);
            var reponse = _unitOfWork.SaveChanges();
            if (reponse > 0)
            {



                return Ok(invoice); // 200 OK

            }
            return BadRequest();
        }
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(int invoiceId, int status, int? voucherId)
        {
            var invoice = _unitOfWork.InvoiceRepository.GetByIdCustom(invoiceId);
            if (voucherId != null)
            {
                var voucher = await _unitOfWork.voucherUserRepository.GetById(voucherId);
                if (status != 5 && status != 7)
                {
                    voucher.IsDeleted = true;
                    _unitOfWork.voucherUserRepository.Update(voucher);
                    invoice.VoucherUser = voucher;
                }
                else
                {
                    voucher.IsDeleted = false;
                    _unitOfWork.voucherUserRepository.Update(voucher);
                }
            }
            invoice.Status = (InvoiceStatus)Enum.GetValues(typeof(InvoiceStatus)).GetValue(status);
            _unitOfWork.InvoiceRepository.Update(invoice);
            _unitOfWork.SaveChanges();
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> ChangeStatus2(int invoiceId)
        {
            var invoice = _unitOfWork.InvoiceRepository.GetByIdCustom(invoiceId);
                     
                foreach (var item in invoice.InvoiceDetails)
                {
                    var productAttribute = await _unitOfWork.ProductAtributeRepository.GetById(item.ProductAttributeId);
                    productAttribute.Quantity += item.Quantity;
                    _unitOfWork.ProductAtributeRepository.Update(productAttribute);
                }           
            _unitOfWork.SaveChanges();
            return Ok();
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetInvoiceByUserId(Guid userId)
        {
            var invoices = _unitOfWork.InvoiceRepository.GetInvoiceByUserId(userId);
            if (invoices != null || invoices.Any())
            {
                var invoiceDTOs = _mapper.Map<List<InvoiceVM>>(invoices);
                return Ok(invoiceDTOs); // 200 OK
            }
            return NoContent(); // 204 No Content
        }
        // PUT: api/invoices/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, InvoiceVM invoiceDTO)
        {
            if (invoiceDTO == null || id != invoiceDTO.InvoiceId)
            {
                return BadRequest("Invoice data is null"); // 400 Bad Request
            }
            var invoice = await _unitOfWork.InvoiceRepository.GetById(id);
            invoice.Note = invoiceDTO.Note;
            _unitOfWork.InvoiceRepository.Update(invoice);
            _unitOfWork.SaveChanges();
            return Ok(invoice); // 200 OK
        }

        // DELETE: api/invoices/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await _unitOfWork.InvoiceRepository.GetById(id);
            if (invoice == null)
            {
                return NotFound(); // 404 Not Found
            }
            _unitOfWork.InvoiceRepository.Delete(invoice);
            _unitOfWork.SaveChanges();
            return Ok(invoice); // 200 OK
        }
        // phân trang
        [HttpPost]
        public IActionResult GetInvoicePaging([FromBody] InvoicePaging request)
        {
            InvoicePaging invoicePaging = _unitOfWork.InvoiceRepository.GetInvoicePaging(request);
            return Ok(invoicePaging);
        }

        [HttpGet("{userId}/{status}")]
        public async Task<IActionResult> GetInvoiceByStatusAndUserId(Guid userId, InvoiceStatus status)
        {
            var invoices = _unitOfWork.InvoiceRepository.GetInvoiceByStatusAndUserId(userId, status);
            if (invoices != null || invoices.Any())
            {
                return Ok(invoices); // 200 OK
            }
            return NoContent(); // 204 No Content
        }
    }
}
