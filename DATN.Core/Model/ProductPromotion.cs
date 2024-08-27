using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class ProductPromotion
    {

        public int ProductPromotionId { get; set; }
        public int PromotionId { get; set; }
        public Promotion? Promotion { get; set; }
        public Model.Product.Product? Product { get; set; }
        public int ProductId { get; set; }
    }
}
