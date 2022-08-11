using Ecommerce.CartAPI.Data.ValueObjects;

namespace Ecommerce.CartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartVO> FindCartByUserIdentifier(string id);
        Task<CartVO> SaveOrUpdateCart(CartVO cartVo);
        Task<bool> RemoveFromCart(long cartDetailId);
        Task<bool> ApplyCoupon(string userIdentifier, string couponCode);
        Task<bool> RemoveCoupon(string userIdentifier);
        Task<bool> ClearCart(string userIdentifier);

    }
}
