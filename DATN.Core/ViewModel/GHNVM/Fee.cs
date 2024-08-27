using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.GHNVM
{
    public class Fee
    {
        public int main_service { get; set; }
        public int insurance { get; set; }
        public int cod_fee { get; set; }
        public int station_do { get; set; }
        public int station_pu { get; set; }
        public int Return { get; set; }
        public int R2S { get; set; }
        public int return_again { get; set; }
        public int coupon { get; set; }
        public int document_return { get; set; }
        public int double_check { get; set; }
        public int double_check_deliver { get; set; }
        public int pick_remote_areas_fee { get; set; }
        public int deliver_remote_areas_fee { get; set; }
        public int pick_remote_areas_fee_return { get; set; }
        public int deliver_remote_areas_fee_return { get; set; }
        public int cod_failed_fee { get; set; }
    }
}
