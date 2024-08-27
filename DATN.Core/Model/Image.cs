using DATN.Core.Enum;
using System.ComponentModel.DataAnnotations;

namespace DATN.Core.Model
{
    public class Image
	{
		[Key]
        public int? ImageId { get; set; }
		[MaxLength(250)]
        public string? ImagePath { get; set; }
		public int? TypeId { get; set; }
        public ImageType? Type { get; set; }
        public bool IsDefault { get; set; }
		// Foreign key
		public int? ProductId { get; set; }
		// Navigation property
		public Product.Product? Product { get; set; }
    }
}
