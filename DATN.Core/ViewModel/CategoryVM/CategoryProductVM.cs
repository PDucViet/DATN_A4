using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Core.Model.Product;

namespace DATN.Core.ViewModel.CategoryVM
{
    public class CategoryProductVM
    {
        public int CategoryProductId { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
    }
}
