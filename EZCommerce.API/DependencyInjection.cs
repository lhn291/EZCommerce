using EZCommerce.API.Common.DBContext;
using EZCommerce.API.Common.Mapping;
using EZCommerce.API.Common.Swagger;
using EZCommerce.Infrastructure;

namespace EZCommerce.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLoggingConfiguration();
            services.AddDbConfiguration(configuration);
            services.AddInfrastructure();
            services.ConfigureSwagger();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddControllers();
            services.AddMappingConfiguration();

            return services;
        }
    }
}