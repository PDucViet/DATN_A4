using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModels.UserViewModel
{
	public class UserVM
	{
		public Guid Id { get; set; }

        [EmailAddress(ErrorMessage = "Vui lòng nhập email hợp lệ")]

        public string Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }

        [Required]
        [RegularExpression(@"^(0[3|5|7|8|9])+([0-9]{8})$", ErrorMessage = "Vui lòng nhập số điện thoại hợp lệ")]

        public string PhoneNumber { get; set; }

		[Required]
		public string PasswordHash { get; set; }
		public string? Address { get; set; }
		public string? Description { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-ZÀ-ỹ\s'-]+$", ErrorMessage = "Vui lòng điền tên hợp lệ")]

        public string FullName { get; set; }
        
        [Required]
        public DateTime? Dob { get; set; }
        public string? SecurityStamp { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool isActive { get; set; }
        public bool? IsSentMail { get; set; }
        
        public string? GrandTotalAmountPurchased { get; set; }
        public IEnumerable<string?>? ListVoucherNameByUser { get; set; }
        public List<string?>? Roles { get; set; }
    }
}
