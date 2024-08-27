using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DATN.Core.Model
{
    public class Voucher : BaseEntity
    {
        public string Name { get; set; }
        public int? DiscountByPercent { get; set; }
        public decimal? DiscountByPrice { get; set; }
        public ICollection<VoucherUser>? VoucherUsers { get; set; }
    }
}
