using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Core.ViewModel.ProductCommentVM;

namespace DATN.Core.Repositories.IRepositories
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        CommentPaging GetCommentPaging(CommentPaging request);
        Task<CommentOverviewVM> GetCommentPagingByProductId(CommentPaging request);
    }
}
