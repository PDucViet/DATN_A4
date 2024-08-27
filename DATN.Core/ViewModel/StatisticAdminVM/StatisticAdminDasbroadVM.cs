using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.StatisticAdminVM
{
    public class StatisticAdminDasbroadVM
    {
       public List<RevenueByMonthVM> RevenueByMonth { get; set; } = new List<RevenueByMonthVM>();
        public List<RevenueByDayVM> RevenueByDay { get; set; } = new List<RevenueByDayVM>();
        public List<TopsellingProductVM> TopsellingProductInMonth { get; set; } = new List<TopsellingProductVM>();
        public List<TopsellingProductVM> TopsellingProductInWeek { get; set; } = new List<TopsellingProductVM>();
        public List<IncomeByMonthVM> IncomeByMonth { get; set; } = new List<IncomeByMonthVM>();
        public List<IncomeByDayVM> IncomeByDay { get; set; } = new List<IncomeByDayVM>();
        public List<AppGrowthByMonthVM> AppGrowthByMonth { get; set; } = new List<AppGrowthByMonthVM>();
        public List<AppGrowthByDayVM> AppGrowthByDay { get; set; } = new List<AppGrowthByDayVM>();
    }
}
