using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.ImagePath;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.ProductCommentVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
	public class ImageReponsiroty : BaseRepository<DATN.Core.Model.Image>, IimageRepository
	{
		private readonly IMapper _mapper;
		public ImageReponsiroty(DATNDbContext context, IMapper mapper) : base(context)
		{
			_mapper = mapper;
		}
        public List<Image> GetAll()
        {
            return Context.Images.Include(p => p.Type).ToList();
        }
        public ImagePaging GetImagePaging(ImagePaging request)
        {
            var query = Context.Images.Where(p=>p.TypeId!=1).Include(p=>p.Type).AsQueryable();

            /*if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Caption.Contains(searchTerm));
            }*/

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<ImageVM>>(list);

            return request;
        }
    }
}
