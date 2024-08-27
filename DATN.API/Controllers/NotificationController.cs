using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.NotificationVM;
using DATN.Core.ViewModel.ProductVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NotificationController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        public IActionResult GetNotificationPaging([FromBody] NotificationPaging request)
        {
            NotificationPaging partnerPaging = _unitOfWork.notificationRepository.GetPartnerPaging(request);
            return Ok(partnerPaging);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notification= _unitOfWork.notificationRepository.GetAll();
            if (notification != null)
            {
                var notificationVms = _mapper.Map<List<NotificationVM>>(notification);
                return Ok(notificationVms);
            }
            return NoContent();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var notification = await _unitOfWork.notificationRepository.GetById(id);
            if (notification == null)
            {
                return NotFound(); // 404 Not Found
            }
            var notificationDTO = _mapper.Map<Notification>(notification);
            return Ok(notificationDTO); // 200 OK
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update( [FromBody] NotificationVM notificationVm)
        {
            var notification= await _unitOfWork.notificationRepository.GetById(notificationVm.NotificationId);
            if (notification == null)
            {
               return NotFound();
            }
            _mapper.Map(notificationVm,notification);
            _unitOfWork.notificationRepository.Update(notification);
            _unitOfWork.SaveChanges();
            return Ok(notification);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NotificationVM notificationVm)
        {
            if (notificationVm == null)
            {
                return BadRequest("Notificaton data is null"); // 400 Bad Request
            }

            var notification = _mapper.Map<Notification>(notificationVm);
            _unitOfWork.notificationRepository.Create(notification);
            _unitOfWork.SaveChanges();

            return Ok(notification); // 201 Created
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var notification = await _unitOfWork.notificationRepository.GetById(id);
            if (notification == null)
            {
                return NotFound(); // 404 Not Found
            }

            _unitOfWork.notificationRepository.Delete(notification);
            _unitOfWork.SaveChanges();

            return Ok(notification); // 204 No Content
        }
    }

}
