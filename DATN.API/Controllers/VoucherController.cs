using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.ContactVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VoucherController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult GetVoucherPaging([FromBody] VoucherPaging request)
        {
            VoucherPaging voucherPaging = _unitOfWork.VoucherRepository.GetVoucherPaging(request);
            return Ok(voucherPaging);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vouchers = _unitOfWork.VoucherRepository.GetAll();

            if (vouchers != null)
            {
                var vouchersVM = _mapper.Map<List<VoucherVM>>(vouchers);
                return Ok(vouchersVM);
            }
            return NoContent(); // Trả về 204 nếu không tìm thấy newfeed
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var voucher = await _unitOfWork.VoucherRepository.GetById(id);
            if (voucher == null)
            {
                return NotFound(); // 404 Not Found
            }
            var result = _mapper.Map<Voucher>(voucher);
            return Ok(result); // 200 OK
        }
        [HttpPost]
        public async Task<IActionResult> Create(VoucherVM voucherVM)
        {
            if (voucherVM == null)
            {
                return BadRequest("Product data is null"); // 400 Bad Request
            }

            var voucher = _mapper.Map<Voucher>(voucherVM);
            await _unitOfWork.VoucherRepository.Create(voucher);
            _unitOfWork.SaveChanges();

            return Ok(voucher); // 201 Created
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var voucher = await _unitOfWork.VoucherRepository.GetById(id);
            if (voucher == null)
            {
                return NotFound(); // 404 Not Found
            }

            _unitOfWork.VoucherRepository.Delete(voucher);
            _unitOfWork.SaveChanges();

            return Ok(voucher);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VoucherVM voucherVM)

        {
            var voucher = await _unitOfWork.VoucherRepository.GetById(id);
            if (voucher == null)
            {
                return NotFound(); // 404 Not Found
            }

            _mapper.Map(voucherVM, voucher);
            _unitOfWork.VoucherRepository.Update(voucher);
            _unitOfWork.SaveChanges();

            return Ok(voucher);
        }
    }
}
