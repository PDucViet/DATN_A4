using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class CategoryTranslation
    {
        [Key]
        public int CategoryTranslationId { get; set; }
        [Required]
        [MaxLength(150)]
        public string TranslatedName { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int LanguageId { get; set; }
        public Language? Language { get; set; }
    }
}
