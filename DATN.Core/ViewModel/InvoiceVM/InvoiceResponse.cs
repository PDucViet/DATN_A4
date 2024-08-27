using DATN.Core.ViewModel.InvoiceDetailVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.InvoiceVM
{
    public class InvoiceResponse
    {
        public int? InvoiceId { get; set; }
        public decimal? FinalPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public List<InvoiceDetailClientData> InvoiceDetails { get; set; }
    }
}
