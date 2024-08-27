using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.NotificationVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.TimeRangeVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TimeRangeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TimeRangeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult GetTimeRangePaging([FromBody] TimeRangePaging request)
        {
            TimeRangePaging partnerPaging = _unitOfWork.TimeRangeRepository.GetPartnerPaging(request);
            return Ok(partnerPaging);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var timeRange = _unitOfWork.TimeRangeRepository.GetAll();
            if (timeRange != null)
            {
                var timeRangesVM = _mapper.Map<List<TimeRangeVM>>(timeRange);
                return Ok(timeRangesVM);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var timeRange = await _unitOfWork.TimeRangeRepository.GetById(id);
            if (timeRange == null)
            {
                return NotFound(); // 404 Not Found
            }
            var timeRangesVM = _mapper.Map<TimeRangeVM>(timeRange);
            return Ok(timeRangesVM); // 200 OK
        }
        [HttpGet]
        public async Task<IActionResult> GetByCateId(int CateId)
        {
            var cate = await _unitOfWork.CategoryRepository.GetById(CateId);
            if (cate.Level == 2)
            {
                CateId = (int)cate.ParentCategoryId;
            }
            var timeRange = _unitOfWork.CategoryTimeRange.GetAll().Where(p=>p.CategoryId==CateId).Select(p=>p.TimeRange).ToList();
            if (timeRange == null)
            {
                return NotFound(); // 404 Not Found
            }
            var timeRangesVM = _mapper.Map<List<TimeRangeVM>>(timeRange);
            return Ok(timeRangesVM); // 200 OK
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] TimeRangeVM timeRangeVM)
        {
            if (string.IsNullOrEmpty(timeRangeVM.Id.ToString()))
            {
                return BadRequest("Id is null empty");
            }

            var timeRange = await _unitOfWork.TimeRangeRepository.GetById(timeRangeVM.Id);
            if (timeRange == null)
            {
                return NotFound();
            }

            _mapper.Map(timeRangeVM, timeRange); // Sử dụng AutoMapper để ánh xạ từ timeRangeVM vào timeRange

            _unitOfWork.TimeRangeRepository.Update(timeRange);
            _unitOfWork.SaveChanges();

            return Ok(timeRange);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TimeRangeVM timeRangeVM) 
        {
            if (timeRangeVM == null)
            {
                return BadRequest("Notificaton data is null"); // 400 Bad Request
            }

            var timeRange = _mapper.Map<TimeRange>(timeRangeVM);
            _unitOfWork.TimeRangeRepository.Create(timeRange);
            _unitOfWork.SaveChanges();

            return Ok(timeRange); // 201 Created
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var timeRange = await _unitOfWork.TimeRangeRepository.GetById(id);
            if (timeRange == null)
            {
                return NotFound(); // 404 Not Found
            }

            _unitOfWork.TimeRangeRepository.Delete(timeRange);
            _unitOfWork.SaveChanges();

            return Ok(timeRange); // 204 No Content
        }
    }
}
