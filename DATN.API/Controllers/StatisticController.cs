using DATN.Core.Infrastructures;
using DATN.Core.ViewModel.StatisticAdminVM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DATN.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatisticController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetStatisticAdminDasbroad([FromQuery]DateTime data)
        {
            var dailyRevenue = _unitOfWork.StatisticRepository.GetDailyRevenue(data);
            var monthlyRevenue = _unitOfWork.StatisticRepository.GetMonthlyRevenue(data);
            var dailyIncome = _unitOfWork.StatisticRepository.GetDailyIncome(data);
            var monthlyIncome = _unitOfWork.StatisticRepository.GetMonthlyIncome(data);
            var top5ProductInMonth = _unitOfWork.StatisticRepository.GetTopsellingProductInMonth(data);
            var top5ProductInWeek = _unitOfWork.StatisticRepository.GetTopsellingProductInWeek(data);
            var monthlyAppGrowth = _unitOfWork.StatisticRepository.GetMonthlyAppGrowth(data);
            var dailyAppGrowth = _unitOfWork.StatisticRepository.GetDailyAppGrowth(data);
            if (dailyRevenue == null || monthlyRevenue == null || top5ProductInMonth == null || top5ProductInWeek == null || dailyIncome == null || monthlyIncome == null)
            {
                return BadRequest();
            }
            var result = new StatisticAdminDasbroadVM()
            {
                RevenueByDay = dailyRevenue,
                RevenueByMonth = monthlyRevenue,
                TopsellingProductInMonth = top5ProductInMonth,
                TopsellingProductInWeek = top5ProductInWeek,
                IncomeByDay = dailyIncome,
                IncomeByMonth = monthlyIncome,
                AppGrowthByMonth = monthlyAppGrowth,
                AppGrowthByDay = dailyAppGrowth
            };
            return Ok(result);
            
        }
        [HttpGet]
        public async Task<IActionResult> GetDailyRevenue([FromQuery]DateTime data)
        {
            var result = _unitOfWork.StatisticRepository.GetDailyRevenue(data);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMonthlyRevenue([FromQuery]DateTime data)
        {
            var result = _unitOfWork.StatisticRepository.GetMonthlyRevenue(data);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetTop5ProductInMonth([FromQuery]DateTime data)
        {
            var result = _unitOfWork.StatisticRepository.GetTopsellingProductInMonth(data);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        } 
        [HttpGet]
        public async Task<IActionResult> GetTop5ProductInWeek([FromQuery]DateTime data)
        {
            var result = _unitOfWork.StatisticRepository.GetTopsellingProductInWeek(data);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }
    }
}
