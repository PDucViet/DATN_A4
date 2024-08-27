using DATN.Core.Model.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ModelConfigurations.Product
{
    public class ProductAttributeConfigruration : IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.ToTable("ProductAttributes");
            builder.HasKey(pa => pa.Id);
            builder.Property(pa => pa.Quantity);
            builder.Property(pa => pa.PuscharPrice);
            builder.Property(pa => pa.ReleaseYear);
            builder.Property(pa => pa.SalePrice);
            builder.Property(pa => pa.Tax);
            builder.Property(pa => pa.ProductId).IsRequired();
            builder.Property(pa => pa.AttributeValueId).IsRequired();

            builder.HasOne(pa => pa.Product)
                   .WithMany(p => p.ProductAttributes)
                   .HasForeignKey(pa => pa.ProductId)
                   .OnDelete(DeleteBehavior.NoAction);  // Use NoAction to prevent cascade

            builder.HasOne(pa => pa.AttributeValue)
                   .WithMany(av => av.ProductAttributes)
                   .HasForeignKey(pa => pa.AttributeValueId)
                   .OnDelete(DeleteBehavior.NoAction);  // Use NoAction to prevent cascade
        }
    }
}
