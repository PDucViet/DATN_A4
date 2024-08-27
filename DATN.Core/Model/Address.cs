using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class Address
    {
        
        public int AddressID { get; set; }
        public string Description { get; set; } = string.Empty;
        public ICollection<ProductAddress>? ProductAddresss { get; set; }
    }
}
