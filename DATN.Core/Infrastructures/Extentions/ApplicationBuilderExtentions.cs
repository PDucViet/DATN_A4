using DATN.Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.Infrastructures.Extentions
{
    public static class ApplicationBuilderExtentions
    {
        public static IApplicationBuilder MigrationDataBase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<DATNDbContext>();

            //may be seed data for initial here
            //using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // check migration pending
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            //var contributtors = scope.ServiceProvider.GetService<IdataseedContributor>();

            //foreach (var contributor in Contributors)
            //{
            //    contributor.seedAsync().getAwaiter.GetResult();
            //}

            return app;
        }
    }
}
