using DATN.Core.Model;
using DATN.Core.Model.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ModelConfigurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //builder.HasOne(c => c.Product).WithMany(p => p.Comments).HasForeignKey(p => p.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(c => c.AppUser).WithMany(p => p.Comments).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
