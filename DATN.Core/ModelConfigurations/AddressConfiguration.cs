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
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(a => a.AddressID);
            builder.Property(b => b.AddressID).UseIdentityColumn();
            builder.HasMany(c => c.ProductAddresss).WithOne(d => d.Address).HasForeignKey(x => x.AddressID);
        }
    }
}
