using DATN.Core.ViewModel.ProductVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.InvoiceDetailVM
{
    public class InvoiceDetailClientData
    {
        public int InvoiceDetailId { get; set; }
        public ViewModel.ProductVM.ProductVM Product { get; set; }
        public int Quantity { get; set; }
    }
}
