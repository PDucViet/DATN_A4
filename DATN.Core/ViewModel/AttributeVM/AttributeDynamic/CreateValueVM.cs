using DATN.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.AttributeVM.AttributeDynamic
{
    public class CreateValueVM
    {
        public string Value { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsShow { get; set; } = true ; 
        public ValuesType ValuesType { get; set; } = ValuesType.dynamic;
    }
}
