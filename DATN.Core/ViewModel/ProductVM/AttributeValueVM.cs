using DATN.Core.Enum;
using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductVM
{
    public class AttributeValueVM
    {
        public int AtributeValueId { get; set; }
        public int? ProductAttributeId { get; set; }
		[Required(ErrorMessage = "Không được để trống")]
		public string Value { get; set; } = string.Empty;
        public int? AttributeID { get; set; }
        public int? Quantity { get; set; }
        public int? Tax { get; set; }
        public decimal SalePrice { get; set; }
        public decimal AfterDiscountPrice { get; set; }
        public decimal PuscharPrice { get; set; }
        public int? ReleaseYear { get; set; }
        public bool IsActive { get; set; }
        public ValuesType Type { get; set; }
        public bool IsShow { get; set; }
        public virtual Attributes? Attributes { get; set; }
    }
}
