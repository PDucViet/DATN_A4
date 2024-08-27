using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IProductAtributeRepository : IBaseRepository<ProductAttribute>
    {
        public ProductAttribute GetCustom(int id);

    }
}
