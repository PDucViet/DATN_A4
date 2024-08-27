using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModels.SendMailVM
{
    public class SendMailVM
    {
        public string? Email { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Subject { get; set; }
    }
}
