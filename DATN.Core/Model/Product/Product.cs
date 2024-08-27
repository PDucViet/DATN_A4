using DATN.Core.Enum;
using DATN.Core.ViewModel.BrandVM;
using DATN.Core.ViewModel.ProductAtAndressVM;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model.Product
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength]
        public string Description { get; set; } = string.Empty;
        public Guid? CreateBy { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime UpdateAt { get; set; } = DateTime.Now;
        public int? OriginId { get; set; }
        public ProductStatus Status { get; set; }
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public Origin? Origin { get; set; }
        public ICollection<CategoryProduct>? CategoryProducts { get; set; }
        public ICollection<ProductAddress>? ProductAddresss { get; set; }
        public ICollection<Interested>? Interesteds { get; set; }
        public ICollection<ProductTranslation>? ProductTranslations { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<ProductAttribute>? ProductAttributes { get; set; }
        public ICollection<Image>? Images { get; set; }
        public ICollection<ProductPromotion>? PromotionProducts { get; set;}
        public ICollection<Attributes>? Attributes { get; set;}
    }
}
