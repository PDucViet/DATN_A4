using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ListProductCompVM
{
    public class ListProductCompVM
    {
        public string? BannerUrl { get; set; }
        public string? BackgroundColor { get; set; }
        public double Percent { get; set; } = 0;
        public List<ProductVM.ProductVM>? Products { get; set; }
    }
}
