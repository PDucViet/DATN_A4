using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class Origin:BaseEntity
    {
        public ICollection<Product.Product>? Products { get; set; }

    }
}
