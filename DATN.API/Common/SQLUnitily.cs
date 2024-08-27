using DATN.API.Common.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DATN.API.Common
{
    public class SQLUnitily
    {
        public static DataTableInfo SelectData(SelectDataRequest input)
        {
            DataTableInfo dataTableInfo = new DataTableInfo();

            // Query condition search
            List<string> conditions = new List<string>();
            if (!string.IsNullOrEmpty(input.SearchText))
            {
                conditions.Add($"p.Name LIKE '%' + @searchText + '%'");
            }

            if (input.AttributeFilters != null)
            {
                foreach (var filter in input.AttributeFilters)
                {
                    conditions.Add($"at.Name = @attribute_{filter.Key} AND atvl.Value = @value_{filter.Key}");
                }
            }

            string whereClause = conditions.Any() ? "WHERE " + string.Join(" AND ", conditions) : "";

            // Query Paging
            string query = $@"
            SELECT pr.*, at.Name AS AttributeName, atvl.Value AS AttributeValue
            FROM {input.TableName} pr
            JOIN ProductAttributes prAt ON pr.Id = prAt.ProductId
            JOIN AttributeValues atvl ON prAt.AttributeValueId = atvl.AtributeValueId
            JOIN Attributes at ON atvl.AttributeId = at.Id
            {input.Query} 
            {whereClause}
            ORDER BY pr.Id 
            OFFSET @pageIndex ROWS FETCH NEXT @pageSize ROWS ONLY";

            // Query Total Record
            string countQuery = $@"
            SELECT COUNT(*)
            FROM {input.TableName} pr
            JOIN ProductAttributes prAt ON pr.Id = prAt.ProductId
            JOIN AttributeValues atvl ON prAt.AttributeValueId = atvl.AtributeValueId
            JOIN Attributes at ON atvl.AttributeId = at.Id
            {input.Query} 
            {whereClause}";

            using SqlConnection conn = new SqlConnection(input.ConnectionString);
            conn.Open();

            // Implementation get data
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@searchText", (object)input.SearchText ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@pageIndex", (input.PageIndex - 1) * input.PageSize);
            cmd.Parameters.AddWithValue("@pageSize", input.PageSize);

            if (input.AttributeFilters != null)
            {
                foreach (var filter in input.AttributeFilters)
                {
                    cmd.Parameters.AddWithValue($"@attribute_{filter.Key}", filter.Key);
                    cmd.Parameters.AddWithValue($"@value_{filter.Key}", filter.Value);
                }
            }

            using SqlDataAdapter sqlDA = new SqlDataAdapter(cmd);
            DataTable dataSource = new DataTable();
            sqlDA.Fill(dataSource);

            // Thực hiện truy vấn đếm tổng số bản ghi
            using SqlCommand countCmd = new SqlCommand(countQuery, conn);
            countCmd.Parameters.AddWithValue("@searchText", (object)input.SearchText ?? DBNull.Value);

            if (input.AttributeFilters != null)
            {
                foreach (var filter in input.AttributeFilters)
                {
                    countCmd.Parameters.AddWithValue($"@attribute_{filter.Key}", filter.Key);
                    countCmd.Parameters.AddWithValue($"@value_{filter.Key}", filter.Value);
                }
            }

            int totalRecord = (int)countCmd.ExecuteScalar();

            conn.Close();

            dataTableInfo.Datas = dataSource;
            dataTableInfo.TotalRecord = totalRecord;

            return dataTableInfo;
        }
    }
}
