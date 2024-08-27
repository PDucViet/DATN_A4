using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.ImagePath;
using DATN.Core.ViewModel.ImageTypeVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.ProductVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Net.Http;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImagesTypeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private HttpClient _httpClient;


        public ImagesTypeController(IUnitOfWork unitOfWork, IMapper mapper, HttpClient httpClient)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var images = _unitOfWork.ImageTypeRepository.GetAll();
            if (images == null)
            {
                throw new Exception("Không tồn tại thuộc tính");
            }

            var imageVMs = _mapper.Map<IEnumerable<ImageTypeVM>>(images);

            return Ok(imageVMs);
        }

        // GET: api/image/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var image = await _unitOfWork.ImageTypeRepository.GetById(id);
            if (image == null)
            {
                return NotFound(); // 404 Not Found
            }
            var imageVm = _mapper.Map<ImageTypeVM>(image);
            return Ok(imageVm); // 200 OK
        }



        // POST: api/image
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ImageTypeVM imageVm)
        {
            if (imageVm == null)
            {
                return BadRequest("Attribute data is null"); // 400 Bad Request
            }
            var image = _mapper.Map<DATN.Core.Model.ImageType>(imageVm);
            _unitOfWork.ImageTypeRepository.Create(image);
            _unitOfWork.SaveChanges();
            return Ok(image); // 201 Created
        }

        // PUT: api/attributes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ImageTypeVM imageVm)
        {
            if (imageVm == null || id != imageVm.Id)
            {
                return BadRequest("Invalid data"); // 400 Bad Request
            }
            var imageFind = await _unitOfWork.ImageTypeRepository.GetById(id);
            if (imageFind == null)
            {
                return NotFound(); // 404 Not Found
            }
            _mapper.Map(imageVm, imageFind);
            _unitOfWork.ImageTypeRepository.Update(imageFind);
            _unitOfWork.SaveChanges();
            return Ok(imageFind);
        }

        // DELETE: api/attributes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _unitOfWork.ImageTypeRepository.GetById(id);
            if (image == null)
            {
                return NotFound(); // 404 Not Found
            }
            _unitOfWork.ImageTypeRepository.Delete(image);
            _unitOfWork.SaveChanges();
            return Ok(image);
        }
        
    }
}
