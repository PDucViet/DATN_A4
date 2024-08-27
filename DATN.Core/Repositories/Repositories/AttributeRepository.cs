using AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Model;
using DATN.Core.Model.Product;
using DATN.Core.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Repositories.Repositories
{
    public class AttributeRepository : BaseRepository<Model.Product.Attributes>, IAttributeRepository
    {
        private readonly DATNDbContext context;
        private readonly IMapper _mapper;
        public AttributeRepository(DATNDbContext context, IMapper mapper) : base(context)
        {
            this.context = context;
            _mapper = mapper;
        }

        public Attributes CreateCustom(Attributes attribute)
        {
            context.Attributes.Add(attribute);
            context.SaveChanges();

            return (attribute);
        }
    }
}
