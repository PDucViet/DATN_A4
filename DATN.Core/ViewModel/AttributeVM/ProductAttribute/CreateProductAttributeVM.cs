using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.AttributeVM.ProductAttribute
{
    public class CreateProductAttributeVM
    {
        [Required(ErrorMessage = "Không được để trống")]
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public int? Tax { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? PuscharPrice { get; set; }
        public decimal? ReleaseYear { get; set; }
        public bool IsDefault { get; set; }
        public decimal? AfterDiscountPrice { get; set; }

        [Required(ErrorMessage = "Không được để trống")]
        public int AttributeValueId { get; set; }
    }
}
