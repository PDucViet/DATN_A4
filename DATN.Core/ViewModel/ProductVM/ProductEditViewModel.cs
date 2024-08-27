using DATN.Core.ViewModel.ProductVM.AtributeCreateVM;
using DATN.Core.ViewModel.PromotionVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Core.ViewModel.ProductCommentVM;
using System.Drawing;
using DATN.Core.ViewModel.ImagePath;
using DATN.Core.Enum;

namespace DATN.Core.ViewModel.ProductVM
{
    public class ProductEditViewModel : ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ProductStatus Status { get; set; }
        public int BrandId { get; set; }
        public int OriginId { get; set; }
        public string imagePath { get; set; }
        public ICollection<ImageVM>? Images { get; set; }

        public ProductEditViewModel()
        {
            Attributes = new List<ProductAttributeDetailVM>();
            listAttributes = new List<AttributesVM>();
            listPromotions = new List<PromotionVM.PromotionVM>();
            CommentOverviewVm = new CommentOverviewVM();
        }
        public ProductVM Product { get; set; }
        public List<ProductAttributeDetailVM> Attributes { get; set; }
        public List<AttributesVM> listAttributes { get; set; }
        public List<AttributeCreateVM> AddAtributes { get; set; }
        public List<PromotionVM.PromotionVM> listPromotions { get; set; }
        public CommentOverviewVM CommentOverviewVm { get; set; }

    }
}
