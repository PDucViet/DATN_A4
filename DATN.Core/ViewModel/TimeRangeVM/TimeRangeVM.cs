using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.TimeRangeVM
{
    public class TimeRangeVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        public string Name { get; set; } = string.Empty;
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        public decimal MinPrice { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị phải lớn hơn hoặc bằng 0")]
        [Required(ErrorMessage = "Trường này là bắt buộc")]
        public decimal MaxPrice { get; set; }
        public IEnumerable<ValidationResult>? Validate(ValidationContext validationContext)
        {
            if (MaxPrice < MinPrice)
            {
                yield return new ValidationResult(
                    "Giá bắt đầu nhỏ hơn giá kết thúc",
                    new[] { "MaxPrice" }
                );
            }
        }
    }
}
