using DATN.Core.Model;
using DATN.Core.ViewModel.StatisticAdminVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IStatisticRepository
    {
        List<RevenueByMonthVM> GetMonthlyRevenue(DateTime date);
        List<RevenueByDayVM> GetDailyRevenue(DateTime date);
        List<TopsellingProductVM> GetTopsellingProductInMonth(DateTime date);
        List<TopsellingProductVM> GetTopsellingProductInWeek(DateTime date);
        List<IncomeByDayVM> GetDailyIncome(DateTime date);
        List<IncomeByMonthVM> GetMonthlyIncome(DateTime date);
        List<AppGrowthByMonthVM> GetMonthlyAppGrowth(DateTime date);
        List<AppGrowthByDayVM> GetDailyAppGrowth(DateTime date);
    }

}
