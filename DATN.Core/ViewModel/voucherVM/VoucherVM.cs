using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.voucherVM
{
    public class VoucherVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống")]
        [Range(0, 100, ErrorMessage = "Nhập giá trị từ 0 đến 100")]
        public int? DiscountByPercent { get; set; } = 0;
        [Required(ErrorMessage = "Không được bỏ trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        public decimal? DiscountByPrice { get; set; } = 0;
    
    }
}
