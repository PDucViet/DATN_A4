using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Core.Enum;
using DATN.Core.Model;

namespace DATN.Core.ViewModel.ImagePath
{
	public class ImageVM
	{
		public int ImageId { get; set; }
		public int? TypeId {  get; set; }
        public ImageType? Type { get; set; }
        public string? ImagePath { get; set; }
        public int? ProductId { get; set; }
        public bool IsDefault { get; set; }
	}

    public class CreateImageProductVM
    {
        public string? ImagePath { get; set; }
        public int? ProductId { get; set; }
        [Required(ErrorMessage ="Không được để trống")]
        public bool IsDefault { get; set; }
    }

    public class UpdateImageProductVM
    {
        public int ImageId { get; set; }
        public string? ImagePath { get; set; }
        public int? ProductId { get; set; }
        [Required(ErrorMessage = "Không được để trống")]
        public bool IsDefault { get; set; }
    }
}
