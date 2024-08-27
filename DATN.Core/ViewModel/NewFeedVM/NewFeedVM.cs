using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ContactVM
{
    public class NewFeedVM
    {
        public int NewFeedId { get; set; }
        public int ProductId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
