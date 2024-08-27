using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        [Phone]
        //[RegularExpression(@"^(09|03|07|08|05)\d{8}$", ErrorMessage = "Nhập đúng định dạng số điện thoại Việt Nam")]
        public string Phone { get; set; } = string.Empty;
        [Required]
        [MaxLength (250)]
        public string Address { get; set; } = string.Empty;
        public bool IsRead { get; set; }
    }
}
