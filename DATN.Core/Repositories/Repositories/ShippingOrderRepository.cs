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
    public class ShippingOrderRepository:BaseRepository<ShippingOrder> , IShippingOrderRepository
    {
        private readonly IMapper _mapper;
        public ShippingOrderRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

       
        public List<ShippingOrder> getshippingcustom()
        {
          return  Context.ShippingOrders.Include(x => x.Invoice).ToList();
        }
    }    
}
