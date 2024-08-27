using DATN.Core.Enum;
using DATN.Core.ViewModel.GHNVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.InvoiceVM
{
    public class PaymentRequest
    {
        //public decimal? TotalAmount { get; set; }
        //public decimal? Discount { get; set; }
        //public decimal? FinalAmount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public List<CartItem>? CartItems { get; set; }
        public Guid UserId { get; set; }
        public int VoucherId { get; set; }
        public CreateGHNOrderAdmin? PendingShippingOrder { get; set; }

    }
    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public double GiaNhap { get; set; }
    }
}
