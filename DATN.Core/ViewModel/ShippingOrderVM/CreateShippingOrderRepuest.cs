using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ShippingOrderVM
{
    public class CreateShippingOrderRepuest
    {
        public string ShippingOrderCode { get; set; }
        public double ShippingFee { get; set; }
        public Guid CustomerId { get; set; }
        public int InvoiceId { get; set; }
    }
}
