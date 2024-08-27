using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModels.Paging;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class RoleRepository : BaseRepository<IdentityRole<Guid>>, IRoleRepository
    {
        public RoleRepository(DATNDbContext context) : base(context)
        {
        }

        public RolePaging GetRolePaging(RolePaging request)
        {
            var query = Context.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Name.Contains(searchTerm));
            }

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = list;
            return request;
        }
        public List<string> getRolesName()
        {
            return Context.Roles.Select(r => r.Name).ToList();
        }
    }
}
