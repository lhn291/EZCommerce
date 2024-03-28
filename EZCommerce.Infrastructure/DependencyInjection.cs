using EZCommerce.Infrastructure.Data.DbContext;
using EZCommerce.Infrastructure.Data.Interfaces;
using EZCommerce.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EZCommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();

            using (var serviceScope = services.BuildServiceProvider().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.EnsureCreated(); 
            }

            return services;
        }
    }
}
