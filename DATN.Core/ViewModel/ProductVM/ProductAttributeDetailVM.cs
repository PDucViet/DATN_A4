using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.ProductVM
{
    public class ProductAttributeDetailVM
    {
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public bool IsActive { get; set; }
        public List<AttributeValueVM> ValueVms { get; set; }

    }
}
