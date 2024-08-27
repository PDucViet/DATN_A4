using DATN.Core.ViewModel.ProductVM.AttributeUpdateVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductVM.AtributeCreateVM
{
    public class AttributeCreateVM
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsShow { get; set; }

        public List<AttributeValuesCreateVM> Values { get; set; }
    }
}
