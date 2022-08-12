using AutoMapper;
using Ecommerce.CouponAPI.Data.ValueObjects;
using Ecommerce.CouponAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.CouponAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly MySqlContext _context;
        private IMapper _mapper;

        public CouponRepository(MySqlContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CouponVO> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode) ?? new Model.Coupon();
            return _mapper.Map<CouponVO>(coupon);
        }
    }
}
