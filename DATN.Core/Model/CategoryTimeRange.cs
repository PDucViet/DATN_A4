using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class CategoryTimeRange
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int TimeRangeId { get; set; }
        public Category? Category { get; set; }
        public TimeRange? TimeRange { get; set; }
      
    }
}
