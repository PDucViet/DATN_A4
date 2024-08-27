using DATN.Core.ViewModel.AttributeVM.AttributeDynamic;
using DATN.Core.ViewModel.ProductVM.AtributeCreateVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.AttributeVM.AtttributeVariation
{
    public class CreateVariantVM
    {
        [Required(ErrorMessage = "Không được để trống")]
        
        public string Name { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public bool IsActive { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public bool IsShow { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public bool Type { get; set; }
        [Required(ErrorMessage = "Không được để trống")]

        //public List<CreateVLVariantVM> Value { get; set; }

        public List<int> AttributeValueId { get; set; }
    }
}
