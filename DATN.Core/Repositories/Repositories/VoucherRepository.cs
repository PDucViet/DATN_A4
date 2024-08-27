using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.OriginVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
    {
        private readonly IMapper _mapper;
        public VoucherRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public VoucherPaging GetVoucherPaging(VoucherPaging request)
        {
            var query = Context.Vouchers.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Name.Contains(searchTerm));
            }

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<VoucherVM>>(list);

            return request;
        }
    }


}
