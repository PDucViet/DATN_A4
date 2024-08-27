using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class CategoryProduct
    {
        [Key]
        public int CategoryProductId { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product.Product? Product { get; set; }
    }
}
