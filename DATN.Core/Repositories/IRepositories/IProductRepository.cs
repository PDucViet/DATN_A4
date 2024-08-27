using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.ProductVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        List<Product> GetAllCustom();
        List<Product> GetAll();
        //Product GetById(int? id);
        public Product GetByIdCustom(int id);

        public ProductPaging ProductPaging(ProductPaging request);

    }
}
