using DATN.Core.Enum;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductVM.CreateProduct2
{
    public class CreateProduct2VM
    {
        [Required(ErrorMessage = "Không được để trống")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public string Description { get; set; }
        public Guid CreateBy { get; set; } = new Guid();
        [Required(ErrorMessage = "Không được để trống")]
        public ProductStatus  Status { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public int BrandId { get; set; }
        // Danh sách biến thể sản phẩm
        public List<ProductVariantVM> VariantIds { get; set; } 

        // Danh sách thuộc tính động
        public List<DynamicAttributeVM> AttributeValueIds { get; set; } 
    }
}
