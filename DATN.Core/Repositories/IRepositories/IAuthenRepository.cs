using DATN.Core.Models;
using DATN.Core.ViewModel.AuthenVM;
using DATN.Core.ViewModels.AuthenViewModel;
using DATN.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IAuthenRepository
    {
        Task<ResponseViewModel> AccessByEmail(EmailLoginViewModel request);
        Task<ResponseViewModel> CreateAccountByGoogle(AppUser account, string Password);
        Task<bool> IsAccountExisted(string email);
    }
}
