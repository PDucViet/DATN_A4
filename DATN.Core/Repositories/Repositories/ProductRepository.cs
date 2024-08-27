using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.CategoryVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.ProductVM;
using DATN.Core.ViewModels.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public List<Product> GetAll()
        {
            return Context.Products.Include(p => p.Images).Include(b => b.Brand).Include(o => o.Origin).Include(p => p.CategoryProducts).ThenInclude(p => p.Category).Include(p => p.ProductAttributes).ToList();
        }
        public Product GetByIdCustom(int id)
        {
            return Context.Products.Include(p => p.Images).Include(b => b.Brand).Include(o => o.Origin).Include(p => p.ProductAttributes).FirstOrDefault(p => p.Id == id);
        }
        public List<Product> GetAllCustom()
        {
            return Context.Products.Include(p => p.Images).Include(p => p.CategoryProducts).Include(p => p.Brand).Include(p => p.ProductAttributes).ToList();
        }

        public ProductPaging ProductPaging(ProductPaging request)
        {
            var query = Context.Products.Include(p => p.Images).Include(b => b.Brand).Include(o => o.Origin).Include(p => p.CategoryProducts).ThenInclude(p => p.Category).Include(p => p.ProductAttributes).ToList().AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(searchTerm));
            }

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<ProductVM>>(list);

            return request;
        }

    }
}
