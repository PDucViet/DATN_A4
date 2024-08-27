using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.voucherVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IVoucherUserRepository: IBaseRepository<VoucherUser>
    {
        List<VoucherUser> GetVoucherByUser(Guid Id);
        public VoucherUser GetByIdCustom(int Id);

    }
}
