namespace DATN.Core.ViewModels.UserViewModel;

public class VoucherUserVM
{
    public int VoucherId { get; set; }
    public int? DiscountByPercent { get; set; }
    public decimal? DiscountByPrice { get; set; }
    public decimal? MinOrderPrice { get; set; }
    public int? Total { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}