using DATN.Core.Infrastructures;
using DATN.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATN.Core.ViewModel.InvoiceDetailVM;

namespace DATN.Core.Repositories.IRepositories
{
    public interface IInvoiceDetailRepository : IBaseRepository<InvoiceDetail>
    {
        public List<InvoiceDetailForCommentVM> GetInvoiceDetailByInvoiceId(int invoiceId, Guid userId);
    }
}
