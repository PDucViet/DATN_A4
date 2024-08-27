using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.AndressVM;
using DATN.Core.ViewModel.OriginVM;
using DATN.Core.ViewModel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        private readonly IMapper _mapper;
        public AddressRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public AddressPaging GetAddressPaging(AddressPaging request)
        {
            var query = Context.Addresss.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Description.Contains(searchTerm));
            }

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<AddressVM>>(list);

            return request;
        }
    }
}
