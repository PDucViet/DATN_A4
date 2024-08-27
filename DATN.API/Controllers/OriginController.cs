using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.NotificationVM;
using DATN.Core.ViewModel.OriginVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OriginController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OriginController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult GetOriginPaging([FromBody] OriginPaging request)
        {
            OriginPaging partnerPaging = _unitOfWork.originRepositoty.GetOriginPaging(request);
            return Ok(partnerPaging);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var origin = _unitOfWork.originRepositoty.GetAll();
            if (origin != null)
            {
                var originVms = _mapper.Map<List<OriginVM>>(origin);
                return Ok(originVms);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var origin = await _unitOfWork.originRepositoty.GetById(id);
            if (origin == null)
            {
                return NotFound(); // 404 Not Found
            }
            var originDTO = _mapper.Map<Origin>(origin);
            return Ok(originDTO); // 200 OK
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update( [FromBody] OriginVM originVm)
        {
            var origin = await _unitOfWork.originRepositoty.GetById(originVm.Id);
            if (origin == null)
            {
                return NotFound();
            }
            _mapper.Map(originVm, origin);
            _unitOfWork.originRepositoty.Update(origin);
            _unitOfWork.SaveChanges();
            return Ok(origin);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OriginVM originVm)
        {
            if (originVm == null)
            {
                return BadRequest("Notificaton data is null"); // 400 Bad Request
            }

            var origin = _mapper.Map<Origin>(originVm);
            _unitOfWork.originRepositoty.Create(origin);
            _unitOfWork.SaveChanges();

            return Ok(origin); // 201 Created
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var origin = await _unitOfWork.originRepositoty.GetById(id);
            if (origin == null)
            {
                return NotFound(); // 404 Not Found
            }

            _unitOfWork.originRepositoty.Delete(origin);
            _unitOfWork.SaveChanges();

            return Ok(origin); 
        }
    }
}
