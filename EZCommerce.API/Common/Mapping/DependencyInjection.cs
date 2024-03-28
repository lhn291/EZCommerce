using AutoMapper;

namespace EZCommerce.API.Common.Mapping
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddMappingConfiguration(this IServiceCollection services)
        {
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
