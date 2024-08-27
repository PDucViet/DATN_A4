using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class ProductPromotionRepository : BaseRepository<ProductPromotion>, IProductPromotionRepository
    {
        private readonly IMapper _mapper;
        public ProductPromotionRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public List<ProductPromotion> GetAllByProduct()
        {
            return Context.ProductPromotions.Include(p => p.Promotion).ToList();
        }

        public List<ProductPromotion> GetAllCustom() 
        { 
            return Context.ProductPromotions.Include(p=>p.Product).ThenInclude(p=>p.Images).Include(p=>p.Product).ThenInclude(p=>p.ProductAttributes).ToList();
        }
    }
}
