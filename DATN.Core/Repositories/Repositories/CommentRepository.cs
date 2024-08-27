using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.MagazineVM;
using DATN.Core.ViewModel.Paging;
using DATN.Core.ViewModel.ProductCommentVM;
using DATN.Core.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DATN.Core.Repositories.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        private readonly IMapper _mapper;
        public CommentRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }
        public CommentPaging GetCommentPaging(CommentPaging request)
        {
            
            var query = Context.Comments.AsQueryable();

            /*if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string searchTerm = request.SearchTerm.Trim().ToLower();
                query = query.Where(x => x.Caption.Contains(searchTerm));
            }*/

            request.TotalRecord = query.Count();
            request.TotalPages = (int)Math.Ceiling(request.TotalRecord / (double)request.PageSize);
            var list = query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize).ToList();
            request.Items = _mapper.Map<List<CommentVM>>(list);

            return request;
        }
        public async Task<CommentOverviewVM> GetCommentPagingByProductId(CommentPaging request)
        {
            CommentOverviewVM commentOverviewVm = new CommentOverviewVM();
            var queryabler =  Context.Comments.AsQueryable().Where(c=>c.ProductId==request.ProductId);

            //overview
            commentOverviewVm.GrandTotalCount = queryabler.Count();
            if (commentOverviewVm.GrandTotalCount > 0)
            {
                commentOverviewVm.Count1Star =   Math.Ceiling( (Convert.ToDouble(queryabler.Count(c => c.Rating == 1))/Convert.ToDouble(commentOverviewVm.GrandTotalCount))*100);
                commentOverviewVm.Count2Star =  Math.Ceiling( (Convert.ToDouble(queryabler.Count(c => c.Rating == 2))/Convert.ToDouble(commentOverviewVm.GrandTotalCount))*100);
                commentOverviewVm.Count3Star = Math.Ceiling((Convert.ToDouble(queryabler.Count(c => c.Rating == 3))/Convert.ToDouble(commentOverviewVm.GrandTotalCount))*100);
                commentOverviewVm.Count4Star = Math.Ceiling( (Convert.ToDouble(queryabler.Count(c => c.Rating == 4))/Convert.ToDouble(commentOverviewVm.GrandTotalCount))*100);
                commentOverviewVm.Count5Star = 100 - Math.Ceiling((commentOverviewVm.Count1Star +
                                                                   commentOverviewVm.Count2Star +
                                                                   commentOverviewVm.Count3Star +
                                                                   commentOverviewVm.Count4Star));
                commentOverviewVm.AVGRatingStar = Math.Round(queryabler.Sum(c => c.Rating)<=0?0: Convert.ToDouble(queryabler.Sum(c => c.Rating)) /queryabler.Count(), 2) ;
            }

            if (request.StarRating.HasValue && request.StarRating!=0)
            {
                queryabler = queryabler.Where(c => c.Rating == request.StarRating);
            }
            var query = await queryabler.OrderByDescending(c=>c.Date).ToListAsync();
            commentOverviewVm.TotalRecord =  query.Count();
            commentOverviewVm.StarRating = request.StarRating;
            commentOverviewVm.CurrentPage = request.CurrentPage;
            commentOverviewVm.TotalPages = (int)Math.Ceiling(commentOverviewVm.TotalRecord / (double)request.PageSize)<=0?1:(int)Math.Ceiling(commentOverviewVm.TotalRecord / (double)request.PageSize);
            var list =  query.Skip((request.CurrentPage - 1) * request.PageSize).Take(request.PageSize);
             var result = _mapper.Map<List<CommentVM>>(list);
             foreach (var x in result)
             {
                 var user = Context.Users.AsQueryable().FirstOrDefault(c => c.Id == x.UserId);
                 if (user is not null)
                 {
                     x.UserName = user.UserName;
                 }
             }
            commentOverviewVm.CommentVms = result;
            return  commentOverviewVm;
        }
    }
}
