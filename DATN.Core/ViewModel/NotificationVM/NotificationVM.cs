using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.NotificationVM
{
	public class NotificationVM
	{

		[Key]
		public int NotificationId { get; set; }
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        public string Subject { get; set; }

	}
}
