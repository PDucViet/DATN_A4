using DATN.Core.Infrastructures;
using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.IRepositories
{
	public interface ICategoryTimeRangeRepository : IBaseRepository<CategoryTimeRange>
	{
		public new List<CategoryTimeRange> GetAll();

        public List<CategoryTimeRange> GetTimeRanebyCateId( int id);
		public List<CategoryTimeRange> GetCateTimeByTimeRangeId( int id);
	}
}
