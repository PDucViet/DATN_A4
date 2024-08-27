using DATN.Core.ViewModel.ProductVM.AtributeCreateVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.AttributeVM.AttributeDynamic
{
    public class CreateAttributeVM
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsShow { get; set; }
        public bool Type { get; set; }

        public UpdateValueVM Value { get; set; }
    }
}
