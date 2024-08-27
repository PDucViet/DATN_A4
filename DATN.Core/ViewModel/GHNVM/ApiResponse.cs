using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.GHNVM
{
    public class ApiResponse
    {
        public int code { get; set; }
        public string code_message_value { get; set; }
        public OrderData? data { get; set; }
        public string message { get; set; }
        public string message_display { get; set; }
    }
}
