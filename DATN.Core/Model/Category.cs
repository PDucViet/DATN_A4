using DATN.Core.Enum;
using DATN.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class Category : BaseEntity
    {
        public int? ParentCategoryId { get; set; }
        
        public bool IsVisible { get; set; } = true; // Menu hiển thị trên navmenu
        public bool IsOnList { get; set; } = true; // Menu hiển thị ở bảng chọn cate
        [Required]
        public int Level { get; set; }
        public string? ImageUrl { get; set; }
        [ForeignKey("ParentCategoryId")]
        public Category? ParentCategory { get; set; }
        public List<Category>? SubCategories { get; set; }
        public List<CategoryTranslation>? CategoryTranslations { get; set; }
        public List<Product.Product>? Products { get; set; }
        public List<CategoryTimeRange>? CategoryTimeRanges { get; set; }
        
    }
}
