using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.ContactVM;
using DATN.Core.ViewModel.ProductVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NewFeedController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NewFeedController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var newFeeds = _unitOfWork.newFeedRepository.GetAll();

            if (newFeeds != null)
            {
                var newFeedsVM = _mapper.Map<List<NewFeedVM>>(newFeeds);
                return Ok(newFeedsVM);
            }
            return NoContent(); // Trả về 204 nếu không tìm thấy newfeed
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewFeedVM newFeedVM)
        {
            if (newFeedVM == null)
            {
                return BadRequest("Product data is null"); // 400 Bad Request
            }

            var newFeed = _mapper.Map<NewFeed>(newFeedVM);
             await _unitOfWork.newFeedRepository.Create(newFeed);
            _unitOfWork.SaveChanges();

            return Ok(newFeed); // 201 Created
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var newFeed = await _unitOfWork.newFeedRepository.GetById(id);
            if (newFeed == null)
            {
                return NotFound(); // 404 Not Found
            }

            _unitOfWork.newFeedRepository.Delete(newFeed);
            _unitOfWork.SaveChanges();

            return NoContent(); // 204 No Content
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, NewFeedVM newFeedVM)

        {
            var newFeed = await _unitOfWork.newFeedRepository.GetById(id);
            if (newFeed == null)
            {
                return NotFound(); // 404 Not Found
            }

            _mapper.Map(newFeedVM, newFeed);
            _unitOfWork.newFeedRepository.Update(newFeed);
            _unitOfWork.SaveChanges();

            return NoContent(); // 204 No Content
        }
    }
}
