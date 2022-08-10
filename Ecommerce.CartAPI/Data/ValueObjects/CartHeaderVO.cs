using Ecommerce.CartAPI.Model.Base;

namespace Ecommerce.CartAPI.Data.ValueObjects
{
    public class CartHeaderVO : BaseEntity
    {
        public string UserId { get; set; }
        public string CouponCode { get; set; }
    }
}
