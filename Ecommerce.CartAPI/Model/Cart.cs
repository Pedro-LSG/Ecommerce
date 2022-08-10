namespace Ecommerce.CartAPI.Model
{
    public class Cart
    {
        public CartHeader CartHeader { get; set; }
        public IEnumerable<CartDetail> cartDetails { get; set; }
    }
}
