using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.CategoryVM
{
    public class CategoryTranslationVM
    {
        public int CategoryTranslationId { get; set; }
        public string TranslatedName { get; set; }

        public int CategoryId { get; set; }
        public CategoryVM Category { get; set; }
        public int LanguageId { get; set; }
        public LanguageVM Language { get; set; }
    }
}
