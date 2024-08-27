using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.TimeRangeVM;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
	public class CategoryTimerangeRepository : BaseRepository<CategoryTimeRange>, ICategoryTimeRangeRepository
	{
		private readonly IMapper _mapper;
		private readonly DATNDbContext _context;
		public CategoryTimerangeRepository(DATNDbContext context, IMapper mapper) : base(context)
		{
			_mapper = mapper;
			_context = context;
		}
        public new List<CategoryTimeRange> GetAll()
		{
			return _context.CategoryTimeRange.Include(p=>p.TimeRange).ToList();
		}

        public List<CategoryTimeRange> GetTimeRanebyCateId(int id)
		{
			return _context.CategoryTimeRange.Where(c => c.CategoryId == id ).Include(x=>x.TimeRange).ToList();
		}
        public List<CategoryTimeRange> GetCateTimeByTimeRangeId(int id)
        {
            return _context.CategoryTimeRange.Where(c => c.TimeRangeId == id ).Include(x => x.TimeRange).ToList();
        }
    }
}
