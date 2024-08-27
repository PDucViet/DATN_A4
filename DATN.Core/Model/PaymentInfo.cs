using DATN.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class PaymentInfo
    {
        public int PaymentInfoId { get; set; }
        // Foreign Key
        public int? InvoiceId { get; set; }
        public PaymentMethod? PaymentMethod { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }

        // Navigation property
        public Invoice? Invoice { get; set; }
    }
}
