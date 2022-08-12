namespace Ecommerce.Web.Models
{
    public class CartDetailViewModel
    {
        public int Id { get; set; }
        public long CartHeaderId { get; set; }
        public CartHeaderViewModel? CartHeader { get; set; }
        public long ProductId { get; set; }
        public ProductViewModel Product { get; set; }
        public int Count { get; set; }
    }
}
