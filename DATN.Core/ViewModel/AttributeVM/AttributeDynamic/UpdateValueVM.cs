using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.AttributeVM.AttributeDynamic
{
    public class UpdateValueVM
    {
        public int AtributeValueId { get; set; }
        public string Value { get; set; } = string.Empty;
        public int AttributeID { get; set; }
        public bool IsActive { get; set; }
        public bool IsShow { get; set; } = true;
    }
}
