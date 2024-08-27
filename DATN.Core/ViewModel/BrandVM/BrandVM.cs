using DATN.Core.Model.Product;
using DATN.Core.ViewModel.ImagePath;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.BrandVM
{
    public class BrandVM
    {
       
        public int BrandId { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; } =true;
        public string StatusDisplay => Status == true ? "Đang hoạt động" : "Dừng hoạt động";
        public string? ImageUrl { get; set; }
        public ICollection<ImageVM>? Images { get; set; } = new List<ImageVM>(); // Navigation property for one-to-many relationship with Image
        public ICollection<ProductVM.ProductVM>? ProductEAVs { get; set; }
    }
}
