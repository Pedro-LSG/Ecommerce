using Ecommerce.CartAPI.Data.ValueObjects;
using Ecommerce.CartAPI.Messages;
using Ecommerce.CartAPI.RabbitMqSender;
using Ecommerce.CartAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private ICartRepository _cartRepository;
        private IRabbitMQMessageSender _rabbitMQMessageSender;
        private ICouponRepository _couponRepository;

        public CartController(ICartRepository cartRepository, IRabbitMQMessageSender rabbitMQMessageSender, ICouponRepository couponRepository)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
            _couponRepository = couponRepository ?? throw new ArgumentNullException(nameof(couponRepository));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindById(string id)
        {
            var cart = await _cartRepository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO vo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO vo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(vo);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int id)
        {
            var status = await _cartRepository.RemoveFromCart(id);
            if (!status) return BadRequest();
            return Ok(status);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO vo)
        {
            var status = await _cartRepository.ApplyCoupon(vo.CartHeader.UserId, vo.CartHeader.CouponCode ?? string.Empty);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> RemoveCoupon(string userId)
        {
            var status = await _cartRepository.RemoveCoupon(userId);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO vo)
        {
            if(vo?.UserId == null) return BadRequest();
            var cart = await _cartRepository.FindCartByUserId(vo.UserId);
            if (cart == null) return NotFound();

            if (!string.IsNullOrEmpty(vo.CouponCode))
            {
                string token = Request.Headers["Authorization"];
                CouponVO coupon = await _couponRepository.GetCouponByCouponCode(vo.CouponCode, token);

                if(vo.DiscountAmount != coupon.DiscountAmount)
                    return StatusCode(412);
            }

            vo.CartDetails = cart.CartDetails;
            vo.DateTime = DateTime.Now;

            _rabbitMQMessageSender.SendMessage(vo, "checkoutQueue");

            return Ok(vo);
        }
    }
}