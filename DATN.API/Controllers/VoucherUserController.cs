using AutoMapper;
using DATN.Core.Infrastructures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VoucherUserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VoucherUserController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult GetVoucherByUser(Guid Id)
        {
            var result = _unitOfWork.voucherUserRepository.GetVoucherByUser(Id).Where(p=>p.IsDeleted==false);
            if(result != null && result.Any())
            {
                return Ok(result);
            }
            else return NoContent();
        }
        [HttpGet]
        public ActionResult GetVoucherCustom(int id) {
            var result = _unitOfWork.voucherUserRepository.GetByIdCustom(id);
            if (result != null)
            {
                return Ok(result);
            }
            else return NoContent();
        }
    }
}
