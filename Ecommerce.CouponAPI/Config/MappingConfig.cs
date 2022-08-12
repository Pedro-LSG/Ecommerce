using AutoMapper;
using Ecommerce.CouponAPI.Data.ValueObjects;
using Ecommerce.CouponAPI.Model;

namespace Ecommerce.CouponAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config => {
                config.CreateMap<CouponVO, Coupon>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
