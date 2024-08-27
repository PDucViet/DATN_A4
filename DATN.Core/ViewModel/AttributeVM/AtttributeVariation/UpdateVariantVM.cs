using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.AttributeVM.AtttributeVariation
{
    public class UpdateVariantVM
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Không được để trống")]
		public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsShow { get; set; }

        public List<UpdateVlVariantVM> attributeValues { get; set; }
    }
}
