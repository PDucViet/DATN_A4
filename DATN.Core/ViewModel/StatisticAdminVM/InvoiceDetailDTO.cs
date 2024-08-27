using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.StatisticAdminVM
{
    public class InvoiceDetailDTO
    {   
        public DateTime CreateDate { get; set; }
        public decimal ShippingFee { get; set; }
        public decimal Revenue { get; set; }
        public int? VoucherUserId { get; set; }
    }
}
