using DATN.Core.Model;
using DATN.Core.Model.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ModelConfigurations.Product
{
    public class ProductConfiguration : IEntityTypeConfiguration<Model.Product.Product>
    {
        public void Configure(EntityTypeBuilder<Model.Product.Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.CreateBy).IsRequired(false);
            builder.Property(p => p.CreateAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.UpdateAt).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.Status).IsRequired();
            builder.HasMany(p => p.Attributes).WithOne(a => a.Product).HasForeignKey(a => a.ProductId);
            builder.HasMany(p => p.ProductAttributes).WithOne(pa => pa.Product).HasForeignKey(pa => pa.ProductId);
        }
    }
}

