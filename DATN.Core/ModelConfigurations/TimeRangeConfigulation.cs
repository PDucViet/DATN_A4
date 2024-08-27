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
    public class TimeRangeConfigulation : IEntityTypeConfiguration<TimeRange>
    {
        public void Configure(EntityTypeBuilder<TimeRange> builder)
        {
            builder.ToTable("TimeRange");
        }
    }
}
