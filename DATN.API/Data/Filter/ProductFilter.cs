namespace DATN.API.Data.Filter
{
    public class ProductFilter
    {
        public List<int>? BrandId { get; set; }
        public List<int>? CateId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int Cate {  get; set; }
    }
}
