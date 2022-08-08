using AutoMapper;
using Ecommerce.ProductAPI.Data.ValueObjects;
using Ecommerce.ProductAPI.Model;

namespace Ecommerce.ProductAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<ProductVO, Product>();
                config.CreateMap<Product, ProductVO>();
            });

            return mappingConfig;
        }
    }
}
