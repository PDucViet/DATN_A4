using DATN.Core.Model.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class ProductTranslation
    {
        [Key]
        public int ProductTranslationId { get; set; }

        [Required]
        public int ProductEAVId { get; set; }
        public Product.Product? ProductEAV { get; set; }

        [Required]
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        [Required]
        [MaxLength(250)]
        public string TranslatedName { get; set; }

        [MaxLength(2000)]
        public string TranslatedDescription { get; set; }
    }
}
