using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModels
{
    public class ResponseViewModel
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? PrimaryKey { get; set; }
    }
}
