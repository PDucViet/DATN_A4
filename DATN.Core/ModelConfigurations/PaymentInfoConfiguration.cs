using DATN.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ModelConfigurations
{
    public class PaymentInfoConfiguration : IEntityTypeConfiguration<PaymentInfo>
    {
        public void Configure(EntityTypeBuilder<PaymentInfo> builder)
        {
        }
    }
}
