using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.GHNVM
{
    public class OrderData
    {
        public string order_code { get; set; }
        public string sort_code { get; set; }
        public string trans_type { get; set; }
        public string ward_encode { get; set; }
        public string district_encode { get; set; }
        public Fee? fee { get; set; }
        public int total_fee { get; set; }
        public DateTime expected_delivery_time { get; set; }
        public string operation_partner { get; set; }
    }
}
