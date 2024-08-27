using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.AttributeVM.AtttributeVariation
{
    public class UpdateVlVariantVM
    {
        public int AtributeValueId { get; set; }
        public string Value { get; set; } 
        public bool IsActive { get; set; }
        public bool IsShow { get; set; } = true;
    }
}
