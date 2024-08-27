using System.Reflection;

namespace DATN.API.Common.Models
{
    public class DataTableInfo
    {
        public string TableName { get; set; }
        public List<FieldInfo> FieldInfos { get; set; }
        public dynamic Datas { get; set; }
        public int TotalRecord { get; set; }

        public int CategoryTypeId { get; set; }
    }
}
