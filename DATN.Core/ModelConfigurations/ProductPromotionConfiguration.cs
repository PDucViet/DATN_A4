using DATN.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ModelConfigurations
{
    public class ProductPromotionConfiguration : IEntityTypeConfiguration<ProductPromotion>
    {
        public void Configure(EntityTypeBuilder<ProductPromotion> builder)
        {

            builder.ToTable("ProductPromotion");
            builder.HasKey(a => a.ProductPromotionId);
            builder.Property(b => b.ProductPromotionId).UseIdentityColumn();
            builder.HasOne(c => c.Promotion).WithMany(d => d.ProductPromotions).HasForeignKey(x => x.PromotionId);
            builder.HasOne(c=>c.Product).WithMany(d=>d.PromotionProducts).HasForeignKey(x => x.PromotionId);
         
        }
        
    }
}
