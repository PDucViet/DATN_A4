using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model.Product;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.ViewModel.ProductVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class AttributeValueRepository : BaseRepository<AttributeValue>, IAtributeValueRepository
    {
        private readonly DATNDbContext context;
        private readonly IMapper _mapper;

        public AttributeValueRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            this.context = context;
            _mapper = mapper;
        }

        public AttributeValue CreateCustom(AttributeValue attributeValue)
        {
             context.AttributeValues.Add(attributeValue);
            context.SaveChanges();

           return(attributeValue);
        }
    }
}
