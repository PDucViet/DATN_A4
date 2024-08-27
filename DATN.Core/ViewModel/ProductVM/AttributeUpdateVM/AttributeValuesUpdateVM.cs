using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductVM.AttributeUpdateVM
{
    public class AttributeValuesUpdateVM
    {
        public int? AttributeValueId { get; set; }

        public int? AttributeId { get; set; }

        public int? ProductAttributeId { get; set; }

        //[Required(ErrorMessage = "Value is required")]
        //[StringLength(100, ErrorMessage = "Value must be less than 100 characters")]
        public string Value { get; set; }

        public bool IsActive { get; set; }

        public bool IsShow { get; set; }

        //[Required(ErrorMessage = "Quantity is required")]
        //[Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
        public int Quantity { get; set; }

        //[Required(ErrorMessage = "Tax is required")]
        //[Range(0, int.MaxValue, ErrorMessage = "Tax must be a positive number")]
        public int Tax { get; set; }

        //[Required(ErrorMessage = "Sale Price is required")]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Sale Price must be greater than 0")]
        public decimal SalePrice { get; set; }

        //[Range(0.01, double.MaxValue, ErrorMessage = "After Discount Price must be greater than 0")]
        public decimal AfterDiscountPrice { get; set; }

        //[Required(ErrorMessage = "Purchase Price is required")]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Purchase Price must be greater than 0")]
        public decimal PuscharPrice { get; set; }

        //[Range(1900, 2100, ErrorMessage = "Release Year must be between 1900 and 2100")]
        public int? ReleaseYear { get; set; }

        public bool? IsDefault { get; set; }
    }

    public class AttributeDynamicUpdateVM
    {
        public int? AttributeValueId { get; set; }
        public int? AttributeId { get; set; } 
        public string Value { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsShow { get; set; }
    }


}
