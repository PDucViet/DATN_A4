using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.TimeRangeVM
{
    public class CategoryTimeRangeVM
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int TimeRangeId { get; set; }
        public CategoryVM.CategoryVM? Category { get; set; }
        public TimeRangeVM? TimeRange { get; set; }
    }
}
