using Ecommerce.Web.Models;

namespace Ecommerce.Web.Services.IServices
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCoupon(string code, string token);
    }
}
