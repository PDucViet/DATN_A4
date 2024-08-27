using DATN.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model.Product
{
    public class AttributeValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AtributeValueId { get; set; }
        [Required]
        public string Value { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsShow { get; set; } = true;
        public int? AttributeId { get; set; }
        public ValuesType Type { get; set; }
        public virtual Attributes? Attributes { get; set; }
        public IEnumerable<ProductAttribute>? ProductAttributes { get; set; }

        public AttributeValue()
        {
            ProductAttributes = new HashSet<ProductAttribute>();
        }
    }
}
