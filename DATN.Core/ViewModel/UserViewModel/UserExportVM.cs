using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModels.UserViewModel
{
    public class UserExportVM
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool isActive { get; set; }
        public bool IsSentMail { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public string? Description { get; set; }
    }
}
