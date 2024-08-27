﻿using DATN.Core.Infrastructures;
using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IProductPromotionRepository : IBaseRepository<ProductPromotion>
    {
        List<ProductPromotion> GetAllCustom();
        List<ProductPromotion> GetAllByProduct();
    }
}
