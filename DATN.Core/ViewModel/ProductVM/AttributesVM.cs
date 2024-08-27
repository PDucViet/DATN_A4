using DATN.Core.Enum;
using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductVM
{
    public class AttributesVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AttributeValueVM> attributeValues { get; set; }
        public bool IsActive { get; set; }
        public bool IsShow { get; set; } 
        public AttributeType Type { get; set; }
        public int? ProductId { get; set; }
        public ICollection<Product>? Products { get; set; }
        public int SelectedAttributeValueId { get; set; }
    }
}
