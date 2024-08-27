using DATN.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class Magazine
    {
        public int MagazineId { get; set; }
        public string Image { get; set; }
        public MagazineStatus Status { get; set; }
        public string Caption { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
