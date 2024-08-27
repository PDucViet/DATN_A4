using DATN.Core.Enum;
using DATN.Core.Model;
using DATN.Core.ViewModel.UniqueConstrain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.MagazineVM
{
    public class MagazineVM
    {
        public int MagazineId { get; set; }
       
        public string? Image { get; set; }
        public string Caption { get; set; }
        public MagazineStatus Status { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
