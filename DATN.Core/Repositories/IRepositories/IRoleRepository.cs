using DATN.Core.Infrastructures;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IRoleRepository : IBaseRepository<IdentityRole<Guid>>
    {
        List<string> getRolesName();
        RolePaging GetRolePaging(RolePaging request);
    }
}
