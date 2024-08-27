using DATN.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Model
{
    public class Language : BaseEntity
    {
        public List<CategoryTranslation>? CategoryTranslations { get; set; }
        public List<ProductTranslation>? ProductTranslations { get; set; }
    }
}
