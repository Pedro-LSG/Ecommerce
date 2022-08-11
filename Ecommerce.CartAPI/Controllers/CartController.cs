using Ecommerce.CartAPI.Data.ValueObjects;
using Ecommerce.CartAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        [HttpGet("find-cart/{userIdentifier}")]
        [Authorize]
        public async Task<IActionResult> FindByIdentifier(string userIdentifier)
        {
            var cart = await _cartRepository.FindCartByUserIdentifier(userIdentifier);
            return cart != null ? Ok(cart) : BadRequest();
        }


        [HttpPost("add-cart")]
        [Authorize]
        public async Task<IActionResult> AddCart(CartVO cartVo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(cartVo);
            return cart != null ? Ok(cart) : BadRequest();
        }


        [HttpPut("update-cart")]
        [Authorize]
        public async Task<IActionResult> UpdateCart(CartVO cartVo)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(cartVo);
            return cart != null ? Ok(cart) : BadRequest();
        }

        [HttpDelete("remove-cart/{identifier}")]
        [Authorize]
        public async Task<IActionResult> RemoveCart(int identifier)
        {
            var cart = await _cartRepository.RemoveFromCart(identifier);
            return cart ? Ok(cart) : BadRequest();
        }
    }
}