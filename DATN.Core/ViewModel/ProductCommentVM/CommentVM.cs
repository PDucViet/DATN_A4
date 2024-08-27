using DATN.Core.Enum;
using DATN.Core.Model.Product;
using DATN.Core.Model;
using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductCommentVM
{
    public class CommentVM
    {
        public int CommentId { get; set; }
        public int? ProductId { get; set; }
        public string? Content { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Guid? UserId { get; set; }
        public CommentType Type { get; set; }
        public int? InvoiceDetailId { get; set; }
        public int Rating { get; set; } = 0;
        public string? UserName { get; set; }
    }
}
