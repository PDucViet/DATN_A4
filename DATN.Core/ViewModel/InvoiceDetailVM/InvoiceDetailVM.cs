using DATN.Core.Model.Product;
using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.InvoiceDetailVM
{
    public class InvoiceDetailVM
    {
        public int InvoiceDetailId { get; set; }
        public int? Quantity { get; set; }
        public int? InvoiceId { get; set; }
        public int? ProductAttributeId { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public Comment? Comment { get; set; }
        public ProductAttribute? ProductAttribute { get; set; }


        public InvoiceVM.InvoiceVM? Invoice { get; set; }
    }
}
