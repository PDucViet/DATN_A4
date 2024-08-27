using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
	public class Notification
	{
        [Key]
        public int NotificationId { get; set; }
        [MaxLength(250)]
        public string Content { get; set; }
        [MaxLength(250)]		
        
        public string Subject { get; set; }		
     

    }
}
