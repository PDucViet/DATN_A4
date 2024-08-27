using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.AndressVM;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class BrandRepository : BaseRepository<Brand>, IBrandRepository
    {
        private readonly IMapper _mapper;
        public BrandRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

		public BrandPaging brandPaging(BrandPaging request)
		{
			var query = Context.Brands.AsQueryable();

			if (!string.IsNullOrEmpty(request.SearchTerm))
			{
				string searchTerm = request.SearchTerm.Trim().ToLower();
				query = query.Where(x => x.Name.Contains(searchTerm));
			}

			request.TotalRecord = query.Count();
			request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
			var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
			request.Items = _mapper.Map<List<BrandVM>>(list);

			return request;
		}
	}
}
