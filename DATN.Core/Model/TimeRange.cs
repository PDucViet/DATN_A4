using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class TimeRange
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public List<CategoryTimeRange>? CategoryTimeRanges { get; set; }
    }
}
