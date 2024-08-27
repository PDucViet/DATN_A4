using DATN.Core.Model;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.Repositories.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.DependencyInjection
{
    public static class AddServicesExtention
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAttributeRepository, AttributeRepository>();
            services.AddScoped<IimageRepository, ImageReponsiroty>();
            services.AddScoped<IAtributeValueRepository, AttributeValueRepository>();
            services.AddScoped<IAuthenRepository, AuthenRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            return services;
        }
    }
}
