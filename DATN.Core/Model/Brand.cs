using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public string? ImageUrl { get; set; }
        public ICollection<Product.Product>? ProductEAVs { get; set; }
    }
}
