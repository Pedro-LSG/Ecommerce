using AutoMapper;
using Ecommerce.CartAPI.Data.ValueObjects;
using Ecommerce.CartAPI.Model;
using Ecommerce.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.CartAPI.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly MySqlContext _mySqlContext;
        private IMapper _mapper;

        public CartRepository(MySqlContext mySqlContext, IMapper mapper)
        {
            _mySqlContext = mySqlContext;
            _mapper = mapper;
        }

        public async Task<bool> ApplyCoupon(string userIdentifier, string couponCode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ClearCart(string userIdentifier)
        {
            try
            {
                var cartHeader = await _mySqlContext.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userIdentifier);

                if (cartHeader != null)
                {
                    _mySqlContext.CartDetails.RemoveRange(_mySqlContext.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id));
                    _mySqlContext.CartHeaders.Remove(cartHeader);
                    await _mySqlContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CartVO> FindCartByUserIdentifier(string id)
        {
            Cart cart = new Cart()
            {
                CartHeader = await _mySqlContext.CartHeaders.FirstOrDefaultAsync(c => c.UserId == id)
            };

            cart.CartDetails = _mySqlContext.CartDetails.Where(c => c.CartHeaderId == cart.CartHeader.Id).Include(c => c.Product);

            return _mapper.Map<CartVO>(cart);
        }

        public async Task<bool> RemoveCoupon(string userIdentifier)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartDetailsId)
        {
            try
            {
                CartDetail cartDetail = await _mySqlContext.CartDetails.FirstOrDefaultAsync(c => c.Id == cartDetailsId);

                int total = _mySqlContext.CartDetails.Where(c => c.CartHeaderId == cartDetail.CartHeaderId).Count();

                _mySqlContext.CartDetails.Remove(cartDetail);

                if (total == 1)
                {
                    var cartHeaderToRemove = await _mySqlContext.CartHeaders.FirstOrDefaultAsync(c => c.Id == cartDetail.CartHeaderId);
                    _mySqlContext.CartHeaders.Remove(cartHeaderToRemove);
                }
                await _mySqlContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CartVO> SaveOrUpdateCart(CartVO cartVo)
        {
            Cart cart = _mapper.Map<Cart>(cartVo);
            if (cart == null) throw new ArgumentNullException(nameof(cart));

            var product = await _mySqlContext.Products.FirstOrDefaultAsync(p => p.Id == cartVo.CartDetails.FirstOrDefault().ProductId);

            if (product == null)
            {
                _mySqlContext.Products.Add(cart.CartDetails.FirstOrDefault().Product);
                await _mySqlContext.SaveChangesAsync();
            }

            var cartHeader = await _mySqlContext.CartHeaders.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);

            if (cartHeader == null)
            {
                _mySqlContext.CartHeaders.Add(cart.CartHeader);
                await _mySqlContext.SaveChangesAsync();
                cart.CartDetails.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
                cart.CartDetails.FirstOrDefault().Product = null;
                _mySqlContext.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                await _mySqlContext.SaveChangesAsync();
            }
            else
            {
                var cartDetail = await _mySqlContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(p => p.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    p.CartHeaderId == cartHeader.Id);
                if (cartHeader == null)
                {
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeader.Id;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _mySqlContext.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                    await _mySqlContext.SaveChangesAsync();
                }
                else
                {
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetail.Count;
                    cart.CartDetails.FirstOrDefault().Id = cartDetail.Id;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
                    _mySqlContext.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    await _mySqlContext.SaveChangesAsync();
                }
            }
            return _mapper.Map<CartVO>(cart);


        }
    }
}
