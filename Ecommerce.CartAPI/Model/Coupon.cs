using Ecommerce.CartAPI.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.CartAPI.Model
{
    [Table("coupon")]
    public class Coupon : BaseEntity
    {
        [Column("coupon_code"), Required, StringLength(30)]
        public string CouponCode { get; set; }
        [Column("discount_code"), Required]
        public decimal DiscountAmount { get; set; }
    }
}
