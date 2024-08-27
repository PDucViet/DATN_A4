using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ShippingOrderVM
{
    public class CreateGHNOrderAdmin
    {
        public string? to_name { get; set; }
        public string? to_phone { get; set; }
        public string? to_address { get; set; }
        public string? to_ward_code { get; set; }
        public string? to_district_id { get; set; }
        public int? cod_amount { get; set; }
        public int payment_type_id { get; set; } = 2;
        public string required_note { get; set; } = "CHOXEMHANGKHONGTHU";
        public int height { get; set; } = 50;
        public int length { get; set; } = 50;
        public int width { get; set; } = 50;
        public int? weight { get; set; }
        public List<OrderItemsGHNAdmin>? Items { get; set; }
        public int? service_type_id { get; set; } = 5;   

    }
}
