using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Core.Model.Product;

namespace DATN.Core.ModelConfigurations.Product
{
    public class AttributeConfiguration : IEntityTypeConfiguration<Model.Product.Attributes>
    {
        public void Configure(EntityTypeBuilder<Model.Product.Attributes> builder)
        {
            builder.ToTable("Attributes");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.IsActive).IsRequired();
            builder.Property(a => a.ProductId);
            builder.Property(a => a.IsShow).IsRequired();
            builder.Property(a => a.Type).IsRequired();
            builder.HasMany(a => a.AttributeValues).WithOne(av => av.Attributes).HasForeignKey(av => av.AttributeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
