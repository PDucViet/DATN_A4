using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Enum
{
    public enum InvoiceStatus
    {
        Success,
        Pending,
        Delivery,
        PaymentProcessing,
        PaymentSuccess,
        PaymentFail,
        All,
        Cancel
    }
}
