using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.NotificationVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DATNDbContext _dbContext;
        public LanguageController(IUnitOfWork unitOfWork, IMapper mapper, DATNDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notification = _unitOfWork.LanguageRepository.GetAll();
            if (notification != null)
            {
                var notificationVms = _mapper.Map<List<LanguageVM>>(notification);
                return Ok(notificationVms);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var notification = await _unitOfWork.LanguageRepository.GetById(id);
            if (notification == null)
            {
                return NotFound(); // 404 Not Found
            }
            var notificationDTO = _mapper.Map<Language>(notification);
            return Ok(notificationDTO); // 200 OK
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] LanguageVM notificationVm)
        {
            var notification = await _unitOfWork.LanguageRepository.GetById(notificationVm.Id);
            if (notification == null)
            {
                return NotFound();
            }
            _mapper.Map(notificationVm, notification);
            _unitOfWork.LanguageRepository.Update(notification);
            _unitOfWork.SaveChanges();
            return Ok(notification);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LanguageVM notificationVm)
        {
            if (notificationVm == null)
            {
                return BadRequest("Notificaton data is null"); // 400 Bad Request
            }
            else
            {
                var notification = _mapper.Map<Language>(notificationVm);
                _unitOfWork.LanguageRepository.Create(notification);
                _unitOfWork.SaveChanges();

                return Ok(notification); // 201 Created
            }

          
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var notification = await _unitOfWork.LanguageRepository.GetById(id);
            if (notification == null)
            {
                return NotFound(); // 404 Not Found
            }

            _unitOfWork.LanguageRepository.Delete(notification);
            _unitOfWork.SaveChanges();

            return Ok(notification); // 204 No Content
        }
    }
}

