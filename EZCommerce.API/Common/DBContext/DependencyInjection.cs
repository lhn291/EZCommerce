using EZCommerce.Infrastructure.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace EZCommerce.API.Common.DBContext
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
