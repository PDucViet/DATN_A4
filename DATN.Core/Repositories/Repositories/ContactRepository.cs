
using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;

namespace DATN.Core.Repositories.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        private readonly IMapper _mapper;
        public ContactRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
    }
}
