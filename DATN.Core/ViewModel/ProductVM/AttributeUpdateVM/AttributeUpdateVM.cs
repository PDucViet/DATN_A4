using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductVM.AttributeUpdateVM
{
    public class AttributeUpdateVM
    {
        public int Id { get; set; }
        public string AttributeName { get; set; }
        public bool IsActive { get; set; }
        public List<AttributeValuesUpdateVM> ValueVms { get; set; }
    }
}
