using AutoMapper;
using Ecommerce.CartAPI.Data.ValueObjects;
using Ecommerce.CartAPI.Model;

namespace Ecommerce.CartAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<CartVO, CartVO>();
                config.CreateMap<CartVO, CartVO>();
            });

            return mappingConfig;
        }
    }
}
