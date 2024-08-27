using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.PromotionVM
{
    public class PromotionVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Content { get; set; } //Hiển thị chi tiết nội dung khuyến mãi
        public string? HowToParticipate { get; set; } //Hiển thị chi tiết cách thức tham gia
        public double Percent { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool IsActive { get; set; }
        public string? BannerUrl { get; set; }
        public string? BackgroundColor { get; set; }
    }
}
