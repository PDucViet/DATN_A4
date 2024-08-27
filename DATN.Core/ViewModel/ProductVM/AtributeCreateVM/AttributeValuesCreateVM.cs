using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductVM.AtributeCreateVM
{
    public class AttributeValuesCreateVM
    {
        public string Value { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsShow { get; set; }
    }
}
