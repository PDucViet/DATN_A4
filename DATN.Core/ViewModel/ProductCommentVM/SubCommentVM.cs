using DATN.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductCommentVM
{
    public class SubCommentVM
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } // Assuming you want to display user name
        public CommentType Type { get; set; }
    }
}
