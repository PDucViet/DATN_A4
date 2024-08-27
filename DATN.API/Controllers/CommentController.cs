using AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.ListProductCompVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.ProductAtAndressVM;
using DATN.Core.ViewModel.ProductCommentVM;
using DATN.Core.ViewModel.ProductVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //Phân trang
        //[HttpPost]
        //public IActionResult GetCommentPaging([FromBody] CommentPaging request)
        //{
        //    CommentPaging Paging = _unitOfWork.CommentRepository.GetCommentPaging(request);
        //    return Ok(Paging);
        //}

        // GET: api/comments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = _unitOfWork.commentRepository.GetAll().ToList();

            if (comments != null)
            {
                var commentsVM = _mapper.Map<List<CommentVM>>(comments);

                return Ok(commentsVM);
            }
            return NoContent();
        }

        // GET: api/comments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _unitOfWork.commentRepository.GetById(id);
            if (comment == null)
            {
                return NotFound(); // 404 Not Found
            }
            var commentVm = _mapper.Map<List<CommentVM>>(comment);
            return Ok(commentVm); // 200 OK
        }
        // GET: api/comments/{id}
        [HttpPost("comment-by-product-id")]
        public async Task<IActionResult> GetCommentPagingByProductId([FromBody]CommentPaging paging)
        {
            var comment = await _unitOfWork.commentRepository.GetCommentPagingByProductId(paging);
            if (comment == null)
            {
                return NotFound(); // 404 Not Found
            }
            return Ok(comment); // 200 OK
        }

        // POST: api/comments
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentVM commentVM)
        {
            try
            {
                if (commentVM == null)
                {
                    return BadRequest("Comment data is null"); // 400 Bad Request
                }
                var comment = _mapper.Map<Comment>(commentVM);
                _unitOfWork.commentRepository.Create(comment);
                _unitOfWork.SaveChanges();
                return CreatedAtAction(nameof(Get), comment); // 201 Created
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        // PUT: api/comments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CommentVM commentVM)
        {
            var comment = await _unitOfWork.commentRepository.GetById(id);
            if (comment == null || id != comment.CommentId)
            {
                return NotFound();
            }
            _mapper.Map(commentVM, comment);
            _unitOfWork.commentRepository.Update(comment);
            _unitOfWork.SaveChanges();

            return Ok();
        }

        // DELETE: api/comments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var comment = await _unitOfWork.commentRepository.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }

            _unitOfWork.commentRepository.Delete(comment);
            _unitOfWork.SaveChanges();

            return Ok();
        }
    }
}
