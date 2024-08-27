using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductVM
{
    public class ProductFilterVM
    {
        public List<int>? BrandIds { get; set; }
        public decimal? MinRange { get; set; }
        public decimal? MaxRange { get; set; }
    }
}
