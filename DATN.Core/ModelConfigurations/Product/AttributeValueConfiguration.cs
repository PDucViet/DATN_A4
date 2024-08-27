using DATN.Core.Model.Product;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ModelConfigurations.Product
{
    public class AttributeValueConfiguration : IEntityTypeConfiguration<AttributeValue>
    {
        public void Configure(EntityTypeBuilder<AttributeValue> builder)
        {
            builder.ToTable("AttributeValues");
            builder.ToTable("AttributeValues");
            builder.HasKey(av => av.AtributeValueId);
            builder.Property(av => av.Value).IsRequired();
            builder.Property(av => av.IsActive).IsRequired();
            builder.Property(av => av.IsShow).IsRequired();
            builder.Property(av => av.Type).IsRequired();
            builder.HasMany(av => av.ProductAttributes).WithOne(pa => pa.AttributeValue).HasForeignKey(pa => pa.AttributeValueId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}