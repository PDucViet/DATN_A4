using AutoMapper;
using DATN.Core.Infrastructures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InvoiceDetailController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet("{invoiceId}/{userId}")]
        public async Task<IActionResult> GetByInvoiceId(int invoiceId,Guid userId)
        {
            var invoiceDetails = _unitOfWork.InvoiceDetailRepository.GetInvoiceDetailByInvoiceId(invoiceId,userId);
            if (invoiceDetails != null)
            {
                return Ok(invoiceDetails);
            }
            return NoContent();
        }
    }
}
