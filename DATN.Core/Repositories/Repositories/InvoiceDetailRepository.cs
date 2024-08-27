using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Core.Models;
using DATN.Core.ViewModel.InvoiceDetailVM;
using Microsoft.AspNetCore.Http;

namespace DATN.Core.Repositories.Repositories
{
    
    public class InvoiceDetailRepository : BaseRepository<InvoiceDetail>, IInvoiceDetailRepository
    {
        private readonly IMapper _mapper;
        private readonly DATNDbContext _context;
        public InvoiceDetailRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }
        public IEnumerable<InvoiceDetail> GetAll()
        {
            return _context.InvoiceDetails.Include(p => p.ProductAttribute).Include(p=>p.Comment);
        }
            public  List<InvoiceDetailForCommentVM> GetInvoiceDetailByInvoiceId(int invoiceId,Guid userId)
            {
                try
                {
                    var InvoinceDetail=_context.InvoiceDetails.Where(x => x.InvoiceId == invoiceId).Include(p=>p.ProductAttribute).ThenInclude(p=>p.Product).ToList();
                    var invoiceDetailForCommentVms= _mapper.Map<List<InvoiceDetailForCommentVM>>(InvoinceDetail);
                    var ProductAttributes = InvoinceDetail.Select(c => c.ProductAttributeId);
                    var listAtt = _context.ProductAttributes.AsQueryable().Where(c => ProductAttributes.Contains(c.Id)).ToList();
                    var listProductId = listAtt.Select(c => c.ProductId);
                    var listProduct = _context.Products.AsQueryable().Where(c => listProductId.Contains(c.Id));
                    var listImageByArrProuductId = _context.Images.AsQueryable().Where(c => listProductId.Contains((int)c.ProductId) && c.TypeId == 1).ToList();
                    var lstComment = _context.Comments.AsQueryable()
                        .Where(c => listProductId.Contains(c.ProductId)).ToList();
                    foreach (var x in invoiceDetailForCommentVms)
                    {
                        x.ProductId = listAtt.FirstOrDefault(c => c.Id == x.ProductAttributeId).ProductId;
                        x.ImagePath = listImageByArrProuductId.FirstOrDefault(c => c.ProductId == x.ProductId).ImagePath;
                        x.ProductName = listProduct.FirstOrDefault(c => c.Id == Convert.ToInt32(x.ProductId)).Name;

                        x.IsShowComment = lstComment
                            .Any(c => c.ProductId == x.ProductId && c.InvoiceDetailId ==x.InvoiceDetailId)?false:true;
                    }

                    return invoiceDetailForCommentVms;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
           
            }
    }
}
