using DATN.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.AttributeVM.AtttributeVariation
{
    public class CreateAttributeVariantVM
    {
        [Required(ErrorMessage = "Không được để trống")]
        public string Value { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public bool IsShow { get; set; }
        //public int AttributeId { get; set; }
    }
}
