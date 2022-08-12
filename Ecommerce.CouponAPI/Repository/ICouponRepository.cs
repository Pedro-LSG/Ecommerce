using Ecommerce.CouponAPI.Data.ValueObjects;

namespace Ecommerce.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponVO> GetCouponByCouponCode(string couponCode);
    }
}
