using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.StatisticAdminVM
{
    public class IncomeByInvoiceDTO
    {
        public DateTime Date { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalIncome { get; set; }
        public int? VoucherUserId { get; set; }
    }
}
