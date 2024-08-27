using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductVM.CreateProduct2
{
    public class ProductVariantVM
    {
        public int AttributeValueId { get; set; }
        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Tax { get; set; }
        public int ProductionYear { get; set; }
    }
}
