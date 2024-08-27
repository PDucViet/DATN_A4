using DATN.Core.Enum;
using DATN.Core.Model.Product;
using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product.Product Product { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser? AppUser { get; set; }
        public CommentType Type { get; set; }
        public int? InvoiceDetailId { get; set; }
        public InvoiceDetail? InvoiceDetail { get; set; }
        public int Rating { get; set; } = 5;

    }
}
