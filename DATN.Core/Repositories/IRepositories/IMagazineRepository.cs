using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IMagazineRepository:IBaseRepository<Magazine>
    {
        MagazinePaging GetMagazinePaging(MagazinePaging request);
    }
}
