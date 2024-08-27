using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModels.AuthenViewModel
{
    public class EmailLoginViewModel
    {
        public string? Name { get; set; }
        public string? GivenName { get; set; }
        public string? Surname { get; set; }
        public string? Password { get; set; }
        public bool IsNeedPassword { get; set; } = false;
        public string Email { get; set; }
    }
}
