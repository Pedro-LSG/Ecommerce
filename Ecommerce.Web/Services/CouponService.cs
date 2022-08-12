using Ecommerce.Web.Models;
using Ecommerce.Web.Utils;
using System.Net.Http.Headers;

namespace Ecommerce.Web.Services
{
    public class CouponService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/coupon";

        public CouponService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CouponViewModel> GetCouponByCouponCode(string couponCode, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/{couponCode}");
            return await response.ReadContentAs<CouponViewModel>();
        }
    }
}
