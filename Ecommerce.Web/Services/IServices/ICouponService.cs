using Ecommerce.Web.Models;

namespace Ecommerce.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCouponByCouponCode(string couponCode);
    }
}
