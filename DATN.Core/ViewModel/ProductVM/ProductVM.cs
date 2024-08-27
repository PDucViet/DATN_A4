using DATN.Core.Enum;
using DATN.Core.Model.Product;
using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.ImagePath;
using Microsoft.AspNetCore.Http;
using DATN.Core.ViewModel.ProductVM.AtributeCreateVM;
using System.ComponentModel.DataAnnotations;
using DATN.Core.ViewModel.ProductVM.CreateProduct2;

namespace DATN.Core.ViewModel.ProductVM
{
    public class ProductVM 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; } = 100;
        public double Rating { get; set; } = 5;
        public int RateCount { get; set; } = 0;
        public decimal? DefaultPrice { get; set; } = 10000000;
        public string? imagePath { get; set; }
        public int? ReleaseYear { get; set; }
        public Guid? CreateBy { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public int? Discount { get; set; } = 0;
        public ProductStatus Status { get; set; }
        public int? BrandId { get; set; }
        public int? OriginId { get; set; }
        public string BrandName { get; set; }
        public BrandVM.BrandVM? Brand { get; set; }
        //public int? OriginId { get; set; }
        public Origin? Origin { get; set; }
        public ICollection<CategoryProductVM>? CategoryProducts { get; set; }
        public ICollection<ProductAttribute>? ProductAttributes { get; set; }
        public ICollection<ImageVM>? Images { get; set; }
        //public List<AttributeCreateVM>? Attributes { get; set; }

    }
    public class CreateProductVM
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Guid? CreateBy { get; set; }

        public int? OriginId { get; set; }

        public ProductStatus Status { get; set; }

        public int? BrandId { get; set; }

        // Handling file uploads for images
        public IFormFile? DefaultImage { get; set; } // Main image for the product
        public List<IFormFile>? Files { get; set; } // Additional images for the product

        // For handling variants and dynamic attributes, which will likely need to be processed separately
        public string VariantsJson { get; set; } // JSON string containing variant data
        public string DynamicAttributesJson { get; set; } // JSON string containing dynamic attribute data
    }

    public class UpdateProductVM
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string? Description { get; set; }

        public Guid? CreateBy { get; set; }

        public int? OriginId { get; set; }

        public ProductStatus Status { get; set; }

        public int? BrandId { get; set; }

        // ID của các hình ảnh hiện tại cần được cập nhật hoặc xóa
        public List<int>? ImageIds { get; set; } // Hidden input từ form lưu các ID hình ảnh

        // ID của các hình ảnh cần xoá
        public List<int>? DeletedImageIds { get; set; }

        // Handling file uploads for images
        public IFormFile? DefaultImage { get; set; } // Main image for the product
        public List<IFormFile>? Files { get; set; } // Additional images for the product

        // For handling variants and dynamic attributes, which will likely need to be processed separately
        public string VariantsJson { get; set; } // JSON string containing variant data
        public string DynamicAttributesJson { get; set; } // JSON string containing dynamic attribute data
    }

    public class CreateProductApi
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public Guid? CreateBy { get; set; }

        public int? OriginId { get; set; }

        public ProductStatus Status { get; set; }

        public int? BrandId { get; set; }
    }

    public class UpdateProductApi
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

      
        public string? Description { get; set; }

        public Guid? CreateBy { get; set; }

        public int? OriginId { get; set; }

        public ProductStatus Status { get; set; }

        public int? BrandId { get; set; }
    }
}
