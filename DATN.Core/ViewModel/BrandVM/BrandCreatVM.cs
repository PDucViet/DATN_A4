using DATN.Core.ViewModel.ImagePath;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.BrandVM
{
    public class BrandCreatVM
    {


        public int BrandId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-ZÀ-ỹ\s'-]+$", ErrorMessage = "Vui lòng điền tên hợp lệ")]
        public string Name { get; set; }
        public string? ImageUrl { get; set; }


    }
}
