using DATN.Core.Enum;
using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model.Product
{
    public class Attributes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsShow { get; set; } = true;
        public int? ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public AttributeType Type { get; set; }
        public ICollection<AttributeValue> AttributeValues { get; set; }       

        public Attributes()
        {
            AttributeValues = new HashSet<AttributeValue>();
        }
    }
}
