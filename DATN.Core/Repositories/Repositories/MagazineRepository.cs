using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.MagazineVM;
using DATN.Core.ViewModel.NotificationVM;
using DATN.Core.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class MagazineRepository: BaseRepository<Magazine>,IMagazineRepository
    {
        private readonly IMapper _mapper;
        public MagazineRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public MagazinePaging GetMagazinePaging(MagazinePaging request)
        {
            var query = Context.Magazines.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Caption.Contains(searchTerm));
            }

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<MagazineVM>>(list);

            return request;
        }
    }
}
