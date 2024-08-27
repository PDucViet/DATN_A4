using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly IMapper _mapper;
        public CategoryRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public CategoryPaging CategoryPaging(CategoryPaging request)
        {
            var query = Context.categories.AsQueryable();
            query = query.Where(c => c.Level == 0);

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Name.Contains(searchTerm));
            }

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<CategoryVM>>(list);

            return request;
        }

        public CategoryPagingLv1 CategoryPagingLv1(CategoryPagingLv1 request)
        {
            var query = Context.categories.AsQueryable();
            query = query.Where(c => c.Level == 1);

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Name.Contains(searchTerm));
            }

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            //request.Items = _mapper.Map<List<Categoryrepon>>(list);
            var ListItem = new List<CategoryRepon>();
            foreach (var item in list)
            {
                var categoryRanger = Context.CategoryTimeRange.Where(x=>x.CategoryId == item.Id).Select(p=>p.TimeRange.Name).ToList();
                var obj = new CategoryRepon()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Level = item.Level,
                    ImageUrl = item.ImageUrl,
                    IsOnList = item.IsOnList,
                    IsVisible = item.IsVisible,
                    ParentCategoryId = item.ParentCategoryId,
                    RangerName = categoryRanger
                };
                ListItem.Add(obj);
            }
            request.Items = ListItem;
            //request.Items = new List<CategoryRepon> { new CategoryRepon() { } };

            return request;
        }

        CategoryPagingLv2 ICategoryRepository.CategoryPagingLv2(CategoryPagingLv2 request)
        {
            var query = Context.categories.AsQueryable();
            query = query.Where(c => c.Level == 2);

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Name.Contains(searchTerm));
            }

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            // request.Items = _mapper.Map<List<CategoryRepon>>(list);
            var ListItem = new List<CategoryRepon>();
            foreach (var item in list)
            {
                var categoryRanger = Context.CategoryTimeRange.Where(x => x.CategoryId == item.Id).Select(p => p.TimeRange.Name).ToList();
                var obj = new CategoryRepon()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Level = item.Level,
                    ImageUrl = item.ImageUrl,
                    IsOnList = item.IsOnList,
                    IsVisible = item.IsVisible,
                    ParentCategoryId = item.ParentCategoryId,
                    RangerName = categoryRanger
                };
                ListItem.Add(obj);
            }
            request.Items = ListItem;
            //request.Items = new List<CategoryRepon> { new CategoryRepon() { } };

            return request;


        }

        public List<Category> GetLevel1AndChild(int categoryId)
        {
            var a = Context.categories.Where(p=>p.ParentCategoryId == categoryId).Include(p => p.SubCategories).ToList();
            return a;
        }

        public List<Category> GetAll()
        {
            var a = Context.categories.Include(p => p.SubCategories).ToList();
            return a;
        }





        List<Category> ICategoryRepository.GetLevel1AndChild(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Category GetCateByIdCustom(int id)
        {
            return Context.categories.Where(c => c.Id == id).Include(x => x.CategoryTimeRanges).FirstOrDefault();
        }
    }
}
