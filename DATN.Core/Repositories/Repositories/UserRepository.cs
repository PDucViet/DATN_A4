using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Enum;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Models;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.Utitlities.FormatCurrency;
using DATN.Core.ViewModel.voucherVM;
using DATN.Core.ViewModels.Paging;
using DATN.Core.ViewModels.UserViewModel;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Repositories.Repositories
{
    public class UserRepository : BaseRepository<AppUser>, IUserRepository
    {
        private readonly IMapper _mapper;

        public UserRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public UserPaging GetUserPaging(UserPaging request)
        {
            var query = Context.Users.AsQueryable();
            if (request.LastLoginTimeFilter != LastLoginTimeFilter.All && request.LastLoginTimeFilter != null)
            {
                DateTime filterTime = DateTime.Now;

                switch (request.LastLoginTimeFilter)
                {
                    case LastLoginTimeFilter.LastWeek:
                        filterTime = DateTime.Now.AddDays(-7);
                        break;
                    case LastLoginTimeFilter.LastMonth:
                        filterTime = DateTime.Now.AddMonths(-1);
                        break;
                    case LastLoginTimeFilter.LastFourMonths:
                        filterTime = DateTime.Now.AddMonths(-4);
                        break;
                    case LastLoginTimeFilter.LastYear:
                        filterTime = DateTime.Now.AddYears(-1);
                        break;
                    default:
                        break;
                }

                query = query.Where(x => x.LastLoginTime >= filterTime && x.LastLoginTime <= DateTime.Now);
            }

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.FullName.Contains(searchTerm) ||
                                         x.Email.Contains(searchTerm) || x.PhoneNumber.Contains(searchTerm));
            }


            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            var listUserId = list.Select(c => c.Id);
            var lstInvoiceByUserId = Context.Invoices.AsQueryable().Where(c => listUserId.Contains(c.UserId)).ToList();
            var lstVoucherUsers = Context.VoucherUsers.AsQueryable().Where(c => listUserId.Contains(c.AppUserId))
                .ToList();
            var lstVoucherId = lstVoucherUsers.Select(c => c.VoucherId);
            var lstVoucher = Context.Vouchers.AsQueryable();
            var lstVoucherById = lstVoucher.Where(c => lstVoucherId.Contains(c.Id)).ToList();
            request.Items = _mapper.Map<List<UserVM>>(list);
            FormatCurrency formatCurrency = new FormatCurrency();
            foreach (var x in request.Items)
            {
                List<decimal> listFinalPrice = new List<decimal>();
                foreach (var invoice in lstInvoiceByUserId.Where(c=>c.UserId==x.Id))
                {
                    
                        var totalBill =(decimal) Context.InvoiceDetails.AsQueryable()
                            .Where(c => c.InvoiceId == invoice.InvoiceId).Sum(su => su.Quantity * su.NewPrice);
                        if (totalBill>1)
                        {
                            var getVoucher = lstVoucherUsers.FirstOrDefault(c => c.Id == invoice.VoucherUserId);
                            if (getVoucher != null)
                            {
                                var Voucher = lstVoucherById.FirstOrDefault(vou => vou.Id == getVoucher.VoucherId);
                                var discountValuePercent = (totalBill / 100) * Voucher.DiscountByPercent;
                                var final = totalBill - discountValuePercent  -Voucher.DiscountByPrice;
                                listFinalPrice.Add((decimal)final);
                            }
                            else
                            {
                                listFinalPrice.Add(totalBill);
                            }
                        }
                        else
                        {
                            listFinalPrice.Add(0);
                        }
                   
                   
                }
                
                //var listVoucherByCondition = lstVoucherUsers.Where(c => c.AppUserId == x.Id).Select(c => c.VoucherId);
                x.GrandTotalAmountPurchased = formatCurrency.GetCurrency(
                    Convert.ToDecimal(listFinalPrice.Sum(c=>c)));
                x.ListVoucherNameByUser = lstVoucherById.Where(c =>
                    lstVoucherUsers.Where(c => c.AppUserId == x.Id).Select(inv => inv.VoucherId).ToList()
                        .Contains(c.Id)).Select(c => c.Name);
            }

            request.ListVoucherDropDown = _mapper.Map<List<VoucherVM>>(lstVoucher);
            return request;
        }

        public async Task<int> AddVoucherToListUser(List<UserVoucherShowModal> input)
        {
            List<VoucherUser> voucherUsers = new List<VoucherUser>();
            foreach (var x in input)
            {
                if (!await Context.VoucherUsers.AnyAsync(c =>
                        c.AppUserId == x.UserId && c.VoucherId == Convert.ToInt32(x.VoucherId)))
                {
                    VoucherUser voucherUser = new VoucherUser();
                    voucherUser.VoucherId = Convert.ToInt32(x.VoucherId);
                    voucherUser.AppUserId = x.UserId;
                    voucherUser.From = Convert.ToDateTime(x.StartDate);
                    voucherUser.To = Convert.ToDateTime(x.EndDate);
                    voucherUsers.Add(voucherUser);
                }
            }

            await Context.VoucherUsers.AddRangeAsync(voucherUsers);
            await Context.SaveChangesAsync();
            return voucherUsers.Count();
        }

        public bool DeleteUser(Guid userId)
        {
            var user = Context.Users.FirstOrDefault(d => d.Id == userId);

            if (user == null)
            {
                return false;
            }

            user.isActive = !user.isActive;
            Context.SaveChanges();
            return user.isActive;
        }

        public int UpdateUser(AppUser user)
        {
            var existingUser = Context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser == null)
            {
                return 0;
            }
            else
            {
                Context.Entry(existingUser).CurrentValues.SetValues(user);
            }

            return Context.SaveChanges();
        }

        public AppUser GetUserByEmail(string email)
        {
            return Context.Users.FirstOrDefault(x => x.Email == email);
        }

        public async Task<IEnumerable<string>> GetListVoucherByUserId(Guid userId)
        {
            var lstVoucherUsers = Context.VoucherUsers.AsQueryable().Where(c =>c.AppUserId==userId)
                .ToList();
            var lstVoucherId = lstVoucherUsers.Select(c => c.VoucherId);
            var lstVoucher = Context.Vouchers.AsQueryable();
            var lstVoucherById = lstVoucher.Where(c => lstVoucherId.Contains(c.Id)).ToList();
            var result = lstVoucherById.Select(c => c.Name);
            return result;
        }

        public IEnumerable<AppUser> GetUsersByIds(List<Guid> userIds)
        {
            return Context.Users.Where(u => userIds.Contains(u.Id)).ToList();
        }

        public List<AppUser> GetUsersExport(string search, string lastLoginTimeFilter)
        {
            var query = Context.Users.Where(x => x.isActive == true).AsQueryable();
            if (lastLoginTimeFilter != LastLoginTimeType.All && lastLoginTimeFilter != null)
            {
                DateTime filterTime = DateTime.Now;

                switch (lastLoginTimeFilter)
                {
                    case LastLoginTimeType.LastWeek:
                        filterTime = DateTime.Now.AddDays(-7);
                        break;
                    case LastLoginTimeType.LastMonth:
                        filterTime = DateTime.Now.AddMonths(-1);
                        break;
                    case LastLoginTimeType.LastFourMonths:
                        filterTime = DateTime.Now.AddMonths(-4);
                        break;
                    case LastLoginTimeType.LastYear:
                        filterTime = DateTime.Now.AddYears(-1);
                        break;
                    default:
                        break;
                }

                query = query.Where(x => x.LastLoginTime >= filterTime && x.LastLoginTime <= DateTime.Now);
                if (!string.IsNullOrEmpty(search))
                {
                    string searchTerm = search.Trim().ToLower();
                    query = query.Where(x => x.FullName.Contains(searchTerm) ||
                                             x.Email.Contains(searchTerm) || x.PhoneNumber.Contains(searchTerm));
                }
            }

            return query.ToList();
        }

        public UserProfile GetUserProfile(UserProfile userProfile)
        {
            UserProfile response = new UserProfile();
            var user = Context.Users.Where(x => x.Id == userProfile.UserVM.Id).FirstOrDefault();
            response.UserVM = _mapper.Map<UserExportVM>(user);
            return response;
        }
    }
}