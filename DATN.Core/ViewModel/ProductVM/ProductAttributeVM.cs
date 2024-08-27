using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductVM
{
    public class ProductAttributeVM
    {
        public int Id { get; set; }


        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? Tax { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PuscharPrice { get; set; }
        public decimal AfterDiscountPrice { get; set; }

        public int? ReleaseYear { get; set; }
        public bool IsDefault { get; set; }

        public ProductVM? Product { get; set; }

        public int AttributeValueId { get; set; }

        public AttributeValueVM? AttributeValue { get; set; }
    }
}
