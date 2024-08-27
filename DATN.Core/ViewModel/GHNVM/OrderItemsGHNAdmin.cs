using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.GHNVM
{
    public class OrderItemsGHNAdmin
    {
        public string? name { get; set; }
        public int? quantity { get; set; }
        public int? weight { get; set; } = 5000;
        public int? length { get; set; } = 5;
        public int? width { get; set; } = 5;
        public int? height { get; set; } = 5;
    }
}
