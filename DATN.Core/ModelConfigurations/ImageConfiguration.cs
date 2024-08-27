using DATN.Core.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection.Emit;

namespace DATN.Core.ModelConfigurations
{
	public class ImageConfiguration : IEntityTypeConfiguration<Image>
	{
		public void Configure(EntityTypeBuilder<Image> builder)
		{
			//Configure one - to - many relationship
			builder
			.HasOne(i => i.Product)
			.WithMany(p => p.Images)
			.HasForeignKey(i => i.ProductId);
        }
	}
}
