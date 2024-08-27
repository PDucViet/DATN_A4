using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using DATN.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class ProductAttributeRepository : BaseRepository<ProductAttribute>, IProductAtributeRepository
    {
        private readonly IMapper _mapper;

        public ProductAttributeRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public ProductAttribute GetCustom(int id)
        {
            return Context.ProductAttributes.Include(p=>p.AttributeValue).ThenInclude(p=>p.Attributes).Include(p => p.Product).ThenInclude(p=>p.Images).FirstOrDefault(p => p.Id == id);
        }

    }
}
