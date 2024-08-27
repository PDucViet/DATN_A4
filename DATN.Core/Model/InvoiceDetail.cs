using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class InvoiceDetail
    {
        [Key]
        public int InvoiceDetailId { get; set; }
        public int Quantity { get; set; } = 1;
        // Foreign Key
        public int? InvoiceId { get; set; }
        public int ProductAttributeId { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }
        public double PuscharPrice { get; set; }
        public Comment? Comment { get; set; }

        // Navigation property
        public Invoice? Invoice { get; set; }
        public Product.ProductAttribute ProductAttribute { get; set; }
    }
}
