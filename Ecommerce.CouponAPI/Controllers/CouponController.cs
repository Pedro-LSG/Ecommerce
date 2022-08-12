using Ecommerce.CouponAPI.Data.ValueObjects;
using Ecommerce.CouponAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.CouponAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CouponController : ControllerBase
    {
        private ICouponRepository _repository;

        public CouponController(ICouponRepository repository)
        {
            _repository = repository ?? throw new
                ArgumentNullException(nameof(repository));
        }

        [HttpGet("{couponCode}")]
        public async Task<ActionResult<CouponVO>> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _repository.GetCouponByCouponCode(couponCode);
            if (coupon == null) return NotFound();
            return Ok(coupon);
        }
    }
}