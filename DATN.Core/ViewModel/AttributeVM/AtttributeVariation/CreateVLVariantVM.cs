using DATN.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.AttributeVM.AttributeDynamic
{
    public class CreateVLVariantVM
    {
        [Required(ErrorMessage = "Không được để trống")]
        public string Value { get; set; }
        public int AttributeId { get; set; }
    }
}
