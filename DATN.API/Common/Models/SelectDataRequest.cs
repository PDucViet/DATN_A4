using DATN.API.Common.Models.Enum;

namespace DATN.API.Common.Models
{
    public class SelectDataRequest
    {
        public SelectDataRequest()
        {
            
        }

        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string SearchText { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string SortField { get; set; }
        public string SortOrder { get; set; }
        public string Query { get; set; }
        public int UserId { get; set; }
        public int Invoice { get; set; }
        public Dictionary<string, string> AttributeFilters { get; set; } // Key: Attribute Name, Value: Attribute Value

        public Order Order { get; set; }

    }
}
