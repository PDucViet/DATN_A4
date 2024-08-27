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
    public class CategoryTranslationConfiguration : IEntityTypeConfiguration<CategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.ToTable("CategoryTranslation");


            builder
                .HasOne(ct => ct.Category)
                .WithMany(c => c.CategoryTranslations)
                .HasForeignKey(ct => ct.CategoryId);

            builder
                .HasOne(ct => ct.Language)
                .WithMany(l => l.CategoryTranslations)
                .HasForeignKey(ct => ct.LanguageId);
        }
    }
}
