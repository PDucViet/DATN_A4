using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.ImagePath;
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
    public class ImagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private HttpClient _httpClient;


        public ImagesController(IUnitOfWork unitOfWork, IMapper mapper, HttpClient httpClient)
        {

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        [HttpPost]
        public IActionResult GetImagePaging([FromBody] ImagePaging request)
        {
            ImagePaging Paging = _unitOfWork.imageReponsiroty.GetImagePaging(request);
            return Ok(Paging);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var images = _unitOfWork.imageReponsiroty.GetAll();
            if (images == null)
            {
                throw new Exception("Không tồn tại thuộc tính");
            }

            var imageVMs = _mapper.Map<IEnumerable<ImageVM>>(images);

            return Ok(imageVMs);
        }

        // GET: api/image/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var image = await _unitOfWork.imageReponsiroty.GetById(id);
            if (image == null)
            {
                return NotFound(); // 404 Not Found
            }
            var imageVm = _mapper.Map<ImageVM>(image);
            return Ok(imageVm); // 200 OK
        }

        // POST: api/image
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ImageVM imageVm)
        {
            if (imageVm == null)
            {
                return BadRequest("Attribute data is null"); // 400 Bad Request
            }
            var image = _mapper.Map<DATN.Core.Model.Image>(imageVm);
            _unitOfWork.imageReponsiroty.Create(image);
            _unitOfWork.SaveChanges();
            return Ok(image); // 201 Created
        }

        [HttpPost]
        public async Task<IActionResult> CreateImageProduct([FromBody] CreateImageProductVM imageVm)
        {
            if (imageVm == null)
            {
                return BadRequest("Attribute data is null"); // 400 Bad Request
            }
            var image = _mapper.Map<DATN.Core.Model.Image>(imageVm);
            _unitOfWork.imageReponsiroty.Create(image);
            _unitOfWork.SaveChanges();
            return Ok(image); // 201 Created
        }

        [HttpPut]
        public async Task<IActionResult> UpdateImageProduct([FromBody] UpdateImageProductVM imageVm)
        {

            if (imageVm == null)
            {
                return BadRequest("Attribute data is null"); // 400 Bad Request
            }

            var imageFind = await _unitOfWork.imageReponsiroty.GetById(imageVm.ImageId);
            if (imageFind == null)
            {
                return NotFound(); // 404 Not Found
            }
            var image = _mapper.Map<DATN.Core.Model.Image>(imageVm);
            _unitOfWork.imageReponsiroty.Update(image);
            _unitOfWork.SaveChanges();
            return Ok(image); // 201 Created
        }

        // PUT: api/attributes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ImageVM imageVm)
        {
            if (imageVm == null || id != imageVm.ImageId)
            {
                return BadRequest("Invalid data"); // 400 Bad Request
            }
            var imageFind = await _unitOfWork.imageReponsiroty.GetById(id);
            if (imageFind == null)
            {
                return NotFound(); // 404 Not Found
            }
            _mapper.Map(imageVm, imageFind);
            _unitOfWork.imageReponsiroty.Update(imageFind);
            _unitOfWork.SaveChanges();
            return NoContent(); // 204 No Content
        }

        // DELETE: api/attributes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var image = await _unitOfWork.imageReponsiroty.GetById(id);
            if (image == null)
            {
                return NotFound(); // 404 Not Found
            }
            _unitOfWork.imageReponsiroty.Delete(image);
            _unitOfWork.SaveChanges();
            return NoContent(); // 204 No Content
        }
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string data;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var arr = memoryStream.ToArray();
                data = Convert.ToBase64String(arr);
            }
            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(data), "image");

            var result = await _httpClient.PostAsync("https://api.imgbb.com/1/upload?key=618de95e498c5f4c920cb57d6ca7434d", formData);
            var a = await result.Content.ReadAsStringAsync();
            JObject jsonObject = JObject.Parse(a);
            var link = jsonObject.SelectToken("data.url").ToString();
            return Ok(link);
        }

        //[HttpPost]
        //public async Task<IActionResult> Upload2(IFormFile file)
        //{
        //    string data;
        //    byte[] arr;

        //    using (var fileStream = new FileStream(Path.GetTempFileName(), FileMode.Create))
        //    {
        //        await file.CopyToAsync(fileStream);
        //        fileStream.Position = 0; // Đặt lại vị trí về đầu stream trước khi đọc

        //        arr = new byte[fileStream.Length];
        //        await fileStream.ReadAsync(arr, 0, (int)fileStream.Length);
        //    }

        //    data = Convert.ToBase64String(arr);

        //    var formData = new MultipartFormDataContent();
        //    formData.Add(new StringContent(data), "image");

        //    var result = await _httpClient.PostAsync("https://api.imgbb.com/1/upload?key=618de95e498c5f4c920cb57d6ca7434d", formData);
        //    var responseContent = await result.Content.ReadAsStringAsync();
        //    JObject jsonObject = JObject.Parse(responseContent);
        //    var link = jsonObject.SelectToken("data.url").ToString();
        //    return Ok(link);
        //}
    }
}
