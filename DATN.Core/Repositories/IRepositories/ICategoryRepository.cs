using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.IRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        public List<Category> GetLevel1AndChild(int categoryId);
        CategoryPaging CategoryPaging(CategoryPaging request);
        CategoryPagingLv1 CategoryPagingLv1(CategoryPagingLv1 request);
        CategoryPagingLv2 CategoryPagingLv2(CategoryPagingLv2 request);

        public Category GetCateByIdCustom(int id);

    }
}
