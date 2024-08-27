using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class ProductAddress
    {
        
        public int ProductAddressID { get; set; }
        public int AddressID { get; set; }
        public Address Address { get; set; }
        public int ProductID { get; set; }
        public Model.Product.Product? ProductEAV { get; set; }
        public int Quantity { get; set; }
    }
}
