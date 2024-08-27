using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModels.Paging;

namespace DATN.Core.ViewModel.ProductCommentVM;

public class CommentOverviewVM:PagingRequestBase<CommentVM>
{
    public double Count1Star { get; set; }
    public double Count2Star { get; set; }
    public double Count3Star { get; set; }
    public double Count4Star { get; set; }
    public double Count5Star { get; set; }
    
    public int GrandTotalCount { get; set; }
    public double AVGRatingStar { get; set; }
    public int? StarRating { get; set; }
    public List<CommentVM> CommentVms { get; set; } = new List<CommentVM>();

}