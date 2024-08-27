using DATN.Core.Data;
using DATN.Core.Enum;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.InvoiceVM;
using DATN.Core.ViewModel.StatisticAdminVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly DATNDbContext _context;
        public StatisticRepository(DATNDbContext context)
        {
            _context = context;
        }
        private List<DateTime> GetDaysInMonth(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var daysInMonth = Enumerable.Range(0, (endDate - startDate).Days + 1)
                                        .Select(day => startDate.AddDays(day))
                                        .ToList();

            return daysInMonth;
        }
        private List<InvoiceDetailDTO> GetInvoiceDetails(DateTime startDate, DateTime endDate)
        {
            var query = from invoice in _context.Invoices
                        join shippingOrder in _context.ShippingOrders
                        on invoice.InvoiceId equals shippingOrder.InvoiceId into shippingOrdersGroup
                        from shippingOrder in shippingOrdersGroup.DefaultIfEmpty()
                        join detail in _context.InvoiceDetails
                        on invoice.InvoiceId equals detail.InvoiceId into detailsGroup
                        from detail in detailsGroup.DefaultIfEmpty()
                        where invoice.Status == InvoiceStatus.Success ||
                              invoice.Status == InvoiceStatus.PaymentSuccess ||
                              invoice.Status == InvoiceStatus.Delivery
                        group new { invoice, shippingOrder, detail } by new
                        {
                            invoice.InvoiceId,
                            invoice.CreateDate,
                            invoice.VoucherUserId,
                            shippingOrderPrice = shippingOrder != null ? shippingOrder.Price : 0
                        } into g
                        select new InvoiceDetailDTO
                        {
                            CreateDate = g.Key.CreateDate,
                            Revenue = (decimal)(g.Sum(x => x.detail.Quantity * x.detail.NewPrice) + g.Key.shippingOrderPrice),
                            ShippingFee = (decimal)g.Key.shippingOrderPrice,
                            VoucherUserId = g.Key.VoucherUserId
                        };

            var result = query
                .Where(x => x.CreateDate >= startDate && x.CreateDate <= endDate)
                .ToList();

            return result;
        }







        private List<IncomeByInvoiceDTO> GetInvoiceIncome(DateTime startDate, DateTime endDate)
        {
            var invoiceIncome = _context.Invoices
                .Where(invoice => invoice.Status == InvoiceStatus.Success || invoice.Status == InvoiceStatus.PaymentSuccess || invoice.Status == InvoiceStatus.Delivery)
                .Join(_context.InvoiceDetails,
                    invoice => invoice.InvoiceId,
                    detail => detail.InvoiceId,
                    (invoice, detail) => new
                    {
                        invoice.CreateDate,
                        detail.Quantity,
                        detail.NewPrice,
                        detail.PuscharPrice,
                        invoice.VoucherUserId
                    })
                .Where(x => x.CreateDate >= startDate && x.CreateDate <= endDate)
                .GroupBy(x => new { x.CreateDate.Date, x.VoucherUserId })
                .Select(g => new IncomeByInvoiceDTO
                {
                    Date = g.Key.Date,
                    TotalRevenue = g.Sum(x => (decimal)(x.Quantity * x.NewPrice)),
                    TotalIncome = g.Sum(x => (decimal)(x.Quantity * (x.NewPrice - x.PuscharPrice))),
                    VoucherUserId = g.Key.VoucherUserId
                })
                .OrderBy(g => g.Date)
                .ToList();

            return invoiceIncome;
        }


        private List<IncomeByDayVM> CalculateIncomeByDay(List<IncomeByInvoiceDTO> data)
        {
            var voucherUsers = _context.VoucherUsers.ToList();
            var vouchers = _context.Vouchers.ToList();

            // Tính toán thu nhập theo ngày
            var incomeByDay = data
                .GroupJoin(voucherUsers,
                    invoice => invoice.VoucherUserId,
                    voucherUser => voucherUser.Id,
                    (invoice, voucherUsers) => new
                    {
                        invoice.Date,
                        invoice.TotalRevenue,
                        invoice.TotalIncome,
                        VoucherUser = voucherUsers.FirstOrDefault()
                    })
                .GroupJoin(vouchers,
                    x => x.VoucherUser != null ? x.VoucherUser.VoucherId : (int?)null,
                    voucher => voucher.Id,
                    (x, vouchers) => new
                    {
                        x.Date,
                        x.TotalIncome,
                        x.TotalRevenue,
                        Voucher = vouchers.FirstOrDefault()
                    })
                .Select(x => new
                {
                    x.Date,
                    x.TotalIncome,
                    DiscountAmount = x.Voucher != null ?
                        (x.Voucher.DiscountByPrice ?? 0) +
                        (x.TotalRevenue * (x.Voucher.DiscountByPercent ?? 0) / 100) : 0
                })
                .GroupBy(x => x.Date.Date)
                .Select(g => new IncomeByDayVM
                {
                    Date = g.Key,
                    TotalIncomeAfterDiscount = g.Sum(x => x.TotalIncome - x.DiscountAmount)
                })
                .OrderBy(g => g.Date)
                .ToList();

                return incomeByDay;
        }



        private List<IncomeByMonthVM> CaculateIncomeByMonth(List<IncomeByInvoiceDTO> data, int year)
        {
            var voucherUsers = _context.VoucherUsers.ToList();
            var vouchers = _context.Vouchers.ToList();

            var incomeByMonth = data
                .GroupJoin(voucherUsers,
                    invoice => invoice.VoucherUserId,
                    voucherUser => voucherUser.Id, // Sửa lại để nối đúng khóa chính của VoucherUser
                    (invoice, voucherUsers) => new {
                        invoice.Date,
                        invoice.TotalRevenue,
                        invoice.TotalIncome,
                        VoucherUser = voucherUsers.FirstOrDefault()
                    })
                .GroupJoin(vouchers,
                    x => x.VoucherUser != null ? x.VoucherUser.VoucherId : (int?)null,
                    voucher => voucher.Id,
                    (x, vouchers) => new {
                        x.Date,
                        x.TotalIncome,
                        x.TotalRevenue,
                        Voucher = vouchers.FirstOrDefault()
                    })
                .Select(x => new
                {
                    x.Date,
                    x.TotalIncome,
                    DiscountAmount = x.Voucher != null ?
                        (x.Voucher.DiscountByPrice ?? 0) +
                        (x.TotalRevenue * (x.Voucher.DiscountByPercent ?? 0) / 100) : 0
                })
                .GroupBy(x => new { x.Date.Year, x.Date.Month })
                .Select(g => new IncomeByMonthVM
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalIncomeAfterDiscount = g.Sum(x => x.TotalIncome - x.DiscountAmount)
                })
                .OrderBy(g => g.Year).ThenBy(g => g.Month)
                .ToList();

            return incomeByMonth;
        }

        private List<RevenueByDayVM> CalculateRevenueByDay(List<InvoiceDetailDTO> invoiceDetails)
        {
            var voucherUsers = _context.VoucherUsers.ToList();
            var vouchers = _context.Vouchers.ToList();

            var revenueByDay = invoiceDetails
                .GroupJoin(voucherUsers,
                    invoice => invoice.VoucherUserId,
                    voucherUser => voucherUser.Id,
                    (invoice, voucherUsers) => new { invoice.CreateDate, invoice.Revenue, VoucherUser = voucherUsers.FirstOrDefault() })
                .GroupJoin(vouchers,
                    x => x.VoucherUser != null ? x.VoucherUser.VoucherId : (int?)null,
                    voucher => voucher.Id,
                    (x, vouchers) => new { x.CreateDate, x.Revenue, Voucher = vouchers.FirstOrDefault() })
                .Select(x => new
                {
                    x.CreateDate,
                    x.Revenue,
                    DiscountAmount = x.Voucher != null ?
                        (x.Voucher.DiscountByPrice ?? 0) +
                        (x.Revenue * (x.Voucher.DiscountByPercent ?? 0) / 100) : 0
                })
                .GroupBy(x => x.CreateDate.Date)
                .Select(g => new RevenueByDayVM
                {
                    Date = g.Key,
                    Revenue = g.Sum(x => x.Revenue - x.DiscountAmount)
                })
                .OrderBy(g => g.Date)
                .ToList();

            return revenueByDay;
        }
        private List<RevenueByMonthVM> CalculateRevenueByMonth(List<InvoiceDetailDTO> invoiceDetails, int year)
        {
            var voucherUsers = _context.VoucherUsers.ToList();
            var vouchers = _context.Vouchers.ToList();

            var revenueByMonth = invoiceDetails
                .GroupJoin(voucherUsers,
                    invoice => invoice.VoucherUserId,
                    voucherUser => voucherUser.Id,
                    (invoice, voucherUsers) => new { invoice.CreateDate, invoice.Revenue, VoucherUser = voucherUsers.FirstOrDefault() })
                .GroupJoin(vouchers,
                    x => x.VoucherUser != null ? x.VoucherUser.VoucherId : (int?)null,
                    voucher => voucher.Id,
                    (x, vouchers) => new { x.CreateDate, x.Revenue, Voucher = vouchers.FirstOrDefault() })
                .Select(x => new
                {
                    x.CreateDate,
                    x.Revenue,
                    DiscountAmount = x.Voucher != null ?
                        (x.Voucher.DiscountByPrice ?? 0) +
                        (x.Revenue * (x.Voucher.DiscountByPercent ?? 0) / 100) : 0
                })
                .GroupBy(x => new { x.CreateDate.Year, x.CreateDate.Month })
                .Select(g => new RevenueByMonthVM
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(x => x.Revenue - x.DiscountAmount)
                })
                .OrderBy(g => g.Year).ThenBy(g => g.Month)
                .ToList();

            return revenueByMonth;
        }


        public List<RevenueByDayVM> GetDailyRevenue(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Truy vấn hóa đơn và chi tiết hóa đơn
                var invoiceDetails = GetInvoiceDetails(startDate, endDate);

            // Tính toán doanh thu theo ngày
            var revenueByDay = CalculateRevenueByDay(invoiceDetails);
            var daysInMonth = GetDaysInMonth(date);
            var revenueByDayWithAllDates = daysInMonth.Select(day => new RevenueByDayVM
            {
                Date = day,
                Revenue = revenueByDay.FirstOrDefault(r => r.Date == day)?.Revenue ?? 0
            }).ToList();

            return revenueByDayWithAllDates;
        }



        public List<RevenueByMonthVM> GetMonthlyRevenue(DateTime date)
        {
            // Xác định tháng và năm từ biến DateTime
            var year = date.Year;
            var month = date.Month;

            // Tạo ngày bắt đầu và ngày kết thúc của tháng
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Truy vấn hóa đơn và chi tiết hóa đơn trong tháng
            var invoiceDetails = GetInvoiceDetails(startDate, endDate);

            // Tính toán doanh thu theo tháng
            var revenueByMonth = CalculateRevenueByMonth(invoiceDetails, year);

            // Tạo danh sách doanh thu cho từng tháng trong năm
            var monthsInYear = Enumerable.Range(1, 12).Select(m => new RevenueByMonthVM
            {
                Year = year,
                Month = m,
                Revenue = revenueByMonth.FirstOrDefault(r => r.Month == m)?.Revenue ?? 0
            }).ToList();

            return monthsInYear;
        }

        public List<TopsellingProductVM> GetTopsellingProductInMonth(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var topProducts = _context.InvoiceDetails
                .Where(id => id.Invoice.CreateDate >= startDate && id.Invoice.CreateDate <= endDate && (id.Invoice.Status == InvoiceStatus.Success || id.Invoice.Status == InvoiceStatus.PaymentSuccess || id.Invoice.Status == InvoiceStatus.Delivery))
                .GroupBy(id => id.ProductAttribute.ProductId) // Group theo ProductId
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(id => id.Quantity)
                })
                .OrderByDescending(g => g.TotalQuantity)
                .Take(5)
                .Join(_context.Products, // Thay đổi bảng join
                      g => g.ProductId,
                      p => p.Id,
                      (g, p) => new TopsellingProductVM
                      {
                          ProductName = p.Name, // Lấy tên sản phẩm từ bảng Products
                          Quantity = g.TotalQuantity
                      })
                .ToList();

            return topProducts;
        }
        public List<TopsellingProductVM> GetTopsellingProductInWeek(DateTime date)
        {
            var startDate = date.Date.AddDays(-(int)(date.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)date.DayOfWeek) + (int)DayOfWeek.Monday);
            var endDate = startDate.AddDays(6).AddDays(1).AddSeconds(-1);

            var topProducts = _context.InvoiceDetails
                .Where(id => id.Invoice.CreateDate >= startDate
                              && id.Invoice.CreateDate <= endDate
                              && (id.Invoice.Status == InvoiceStatus.Success
                              || id.Invoice.Status == InvoiceStatus.PaymentSuccess
                              || id.Invoice.Status == InvoiceStatus.Delivery))
                .GroupBy(id => id.ProductAttribute.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(id => id.Quantity)
                })
                .OrderByDescending(g => g.TotalQuantity)
                .Take(5)
                .Join(_context.Products,
                      g => g.ProductId,
                      p => p.Id,
                      (g, p) => new TopsellingProductVM
                      {
                          ProductName = p.Name,
                          Quantity = g.TotalQuantity
                      })
                .ToList();

            return topProducts;
        }


        public List<IncomeByDayVM> GetDailyIncome(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var invoiceIncome = GetInvoiceIncome(startDate, endDate);
            var incomeByDay = CalculateIncomeByDay(invoiceIncome);
            var daysInMonth = GetDaysInMonth(date);
            var incomeByDayWithAllDates = daysInMonth.Select(day => new IncomeByDayVM
            {
                Date = day,
                TotalIncomeAfterDiscount = incomeByDay.FirstOrDefault(r => r.Date == day)?.TotalIncomeAfterDiscount ?? 0
            }).ToList();
            return incomeByDayWithAllDates;
        }
        public List<IncomeByMonthVM> GetMonthlyIncome(DateTime date)
        {
            var year = date.Year;
            var month = date.Month;
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var invoiceIncome = GetInvoiceIncome(startDate, endDate);
            var incomeByMonth = CaculateIncomeByMonth(invoiceIncome, year);
            var monthsInYear = Enumerable.Range(1, 12).Select(m => new IncomeByMonthVM
            {
                Year = year,
                Month = m,
                TotalIncomeAfterDiscount = incomeByMonth.FirstOrDefault(r => r.Month == m)?.TotalIncomeAfterDiscount ?? 0
            }).ToList();
            return monthsInYear;
        }

        public List<AppGrowthByMonthVM> GetMonthlyAppGrowth(DateTime date)
        {
            var userGrowth = _context.Users
                .Where(u => u.LastLoginTime.Value.Year == date.Year && u.Description == "Customer")
                .GroupBy(u => u.LastLoginTime.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    UserCount = g.Count()
                }).ToList();
            var monthsInYear = Enumerable.Range(1, 12).Select(m => new AppGrowthByMonthVM
            {
                Month = m,
                Year = date.Year,
                UserCount = userGrowth.FirstOrDefault(r => r.Month == m)?.UserCount ?? 0
            }).ToList();

            return monthsInYear;

        }

        public List<AppGrowthByDayVM> GetDailyAppGrowth(DateTime date)
        {
            var userGrowth = _context.Users
                .Where(u => u.LastLoginTime.Value.Year == date.Year && u.LastLoginTime.Value.Month == date.Month && u.Description == "Customer")
                .GroupBy(u => u.LastLoginTime.Value.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    UserCount = g.Count()
                }).ToList();
            var daysInMonth = GetDaysInMonth(date);
            var userGrowthWithAllDates = daysInMonth.Select(day => new AppGrowthByDayVM
            {
                Date = day,
                UserCount = userGrowth.FirstOrDefault(r => r.Date == day)?.UserCount ?? 0
            }).ToList();
            return userGrowthWithAllDates;
        }
    }

}
