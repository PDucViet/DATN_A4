using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.CategoryVM
{
    public class CategoryEditAdminLv
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; } = string.Empty;
		[Required(ErrorMessage = "Description is required.")]
		public string Description { get; set; } = string.Empty;
        public int Level { get; set; }
        public bool IsVisible { get; set; } 
        public bool IsOnList { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<int>? RangerId { get; set; } 
    }
}
