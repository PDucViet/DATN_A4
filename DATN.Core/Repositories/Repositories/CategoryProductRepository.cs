using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class CategoryProductRepository : BaseRepository<CategoryProduct> ,ICategoryProductRepository
    {
        private readonly IMapper _mapper;
        public CategoryProductRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
    }
}
