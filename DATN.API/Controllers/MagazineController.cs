using AutoMapper;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.MagazineVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MagazineController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MagazineController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult GetMagazinePaging([FromBody] MagazinePaging request)
        {
            MagazinePaging partnerPaging = _unitOfWork.MagazineRepository.GetMagazinePaging(request);
            return Ok(partnerPaging);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notification = _unitOfWork.MagazineRepository.GetAll();
            if (notification != null)
            {
                var notificationVms = _mapper.Map<List<MagazineVM>>(notification);
                return Ok(notificationVms);
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllActive()
        {
            var magazines = _unitOfWork.MagazineRepository.GetAll().Where(p=>p.Status==MagazineStatus.Starting).ToList();
            if (magazines != null)
            {
                var magazineVms = _mapper.Map<List<MagazineVM>>(magazines);
                return Ok(magazineVms);
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int magazineId)
        {
            var magazine = await _unitOfWork.MagazineRepository.GetById(magazineId);
            if (magazine != null)
            {
                var magazineVm = _mapper.Map<MagazineVM>(magazine);
                return Ok(magazineVm);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var notification = await _unitOfWork.MagazineRepository.GetById(id);
            if (notification == null)
            {
                return NotFound(); // 404 Not Found
            }
            var notificationDTO = _mapper.Map<Magazine>(notification);
            return Ok(notificationDTO); // 200 OK
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] MagazineVM notificationVm)
        {
            var notification = await _unitOfWork.MagazineRepository.GetById(notificationVm.MagazineId);
            if (notification == null)
            {
                return NotFound();
            }
            _mapper.Map(notificationVm, notification);
            _unitOfWork.MagazineRepository.Update(notification);
            _unitOfWork.SaveChanges();
            return Ok(notification);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MagazineVM notificationVm)
        {
            if (notificationVm == null)
            {
                return BadRequest("Notificaton data is null"); // 400 Bad Request
            }

            var notification = _mapper.Map<Magazine>(notificationVm);
            _unitOfWork.MagazineRepository.Create(notification);
            _unitOfWork.SaveChanges();

            return Ok(notification); // 201 Created
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var notification = await _unitOfWork.MagazineRepository.GetById(id);
            if (notification == null)
            {
                return NotFound(); // 404 Not Found
            }

            _unitOfWork.MagazineRepository.Delete(notification);
            _unitOfWork.SaveChanges();

            return Ok(notification); // 204 No Content
        }
    }
}
