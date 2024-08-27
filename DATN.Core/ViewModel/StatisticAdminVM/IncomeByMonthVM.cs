using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.StatisticAdminVM
{
    public class IncomeByMonthVM
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalIncomeAfterDiscount { get; set; }
    }
}
