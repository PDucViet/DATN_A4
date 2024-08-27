

using DATN.Core.Infrastructures;
using DATN.Core.Models;
using DATN.Core.ViewModels.Paging;
using DATN.Core.ViewModels.UserViewModel;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IUserRepository:IBaseRepository<AppUser> 
    {
        UserPaging GetUserPaging(UserPaging request);
        Task<int> AddVoucherToListUser(List<UserVoucherShowModal> input);
        bool DeleteUser(Guid userId);
        int UpdateUser(AppUser user);
        AppUser GetUserByEmail(string email);
        Task<IEnumerable<string>> GetListVoucherByUserId(Guid userId);
        List<AppUser> GetUsersExport(string search, string lastLoginTimeFilter);
        IEnumerable<AppUser> GetUsersByIds(List<Guid> userIds);
        UserProfile GetUserProfile(UserProfile userProfile);
    }
}
