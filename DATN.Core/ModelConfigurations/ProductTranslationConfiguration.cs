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
    public class ProductTranslationConfiguration : IEntityTypeConfiguration<ProductTranslation>
    {
        public void Configure(EntityTypeBuilder<ProductTranslation> builder)
        {
            builder.ToTable(nameof(ProductTranslation));
            builder
                .HasOne(pt => pt.ProductEAV)
                .WithMany(p => p.ProductTranslations)
                .HasForeignKey(pt => pt.ProductEAVId);
            builder
                .HasOne(pt => pt.Language)
                .WithMany(l => l.ProductTranslations)
                .HasForeignKey(pt => pt.LanguageId);
        }
    }
}
