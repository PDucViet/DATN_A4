using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IAddressRepository : IBaseRepository<Address>
    {
        AddressPaging GetAddressPaging(AddressPaging request);
    }
}
