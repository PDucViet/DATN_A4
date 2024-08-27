using DATN.Core.Enum;
using DATN.Core.ViewModels.UserViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Core.ViewModel.voucherVM;

namespace DATN.Core.ViewModels.Paging
{
    public class UserPaging:PagingRequestBase<UserVM>
    {
        public LastLoginTimeFilter? LastLoginTimeFilter { get; set; }
        public List<VoucherVM> ListVoucherDropDown { get; set; }
        public List<UserVoucherShowModal> UserVoucherShowModal { get; set; } = new List<UserVoucherShowModal>();

    }

    public class UserVoucherShowModal
    {
        public Guid UserId { get; set; }
        public int? VoucherId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string UserName  { get; set; }
        public string VoucherName  { get; set; }
    }
    
    
    
}
