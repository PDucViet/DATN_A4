using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class NewFeed
    {

        public int NewFeedId { get; set; }
        public int ProductId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(250)]
        public string Content { get; set; }
    }
}
