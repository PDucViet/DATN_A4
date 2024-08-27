using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.CategoryVM
{
	public class CategoryRepon
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public int Level { get; set; }
		public bool IsVisible { get; set; }  // Mặc định là true, nghĩa là hiển thị
		public string IsVisibleDisplay => IsVisible == true ? "True" : "False";
		public bool IsOnList { get; set; } // Menu hiển thị ở bảng chọn cate
		public string IsOnlistDisplay => IsVisible == true ? "True" : "False";
		public string? ImageUrl { get; set; }
		public int? ParentCategoryId { get; set; }
	
		public List<TimeRangeVM.CategoryTimeRangeVM>? Ranger { get; set; }
        public List<string> RangerName { get; set; }

    }
}
