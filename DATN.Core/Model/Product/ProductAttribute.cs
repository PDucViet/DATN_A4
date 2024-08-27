using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model.Product
{
    public class ProductAttribute
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProductId { get; set; }

        public int? Quantity { get; set; }
        public int? Tax { get; set; }
        public decimal PuscharPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal AfterDiscountPrice { get; set; }
        public int? ReleaseYear { get; set; }
        public virtual Product? Product { get; set; }
        public bool IsDefault { get; set; } = false;

        [Required]
        public int AttributeValueId { get; set; }

        public virtual  AttributeValue? AttributeValue { get; set; }
    }
}
