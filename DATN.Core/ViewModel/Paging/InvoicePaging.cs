using DATN.Core.Enum;
using DATN.Core.Model;
using DATN.Core.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.Paging
{
    public class InvoicePaging:PagingRequestBase<InvoiceVM.InvoiceVM>
    {
        public InvoiceStatus Status { get; set; } = InvoiceStatus.All;
    }
}
