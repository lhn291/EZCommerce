using AutoMapper;
using EZCommerce.Common.Models.Products;
using EZCommerce.Core.Entities;

namespace EZCommerce.API.Common.Mapping
{
    public static class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductResponse>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name));

                config.CreateMap<Product, ProductRequest>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
