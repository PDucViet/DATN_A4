using DATN.Core.ViewModel.ProductCommentVM;
using DATN.Core.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.Paging
{
    public class CommentPaging: PagingRequestBase<CommentVM>
    {

        public int? ProductId { get; set; }
        public int? StarRating { get; set; }
    }
}
