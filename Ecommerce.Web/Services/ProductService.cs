using Ecommerce.Web.Models;
using Ecommerce.Web.Services.IServices;
using Ecommerce.Web.Utils;
using System.Net.Http.Headers;

namespace Ecommerce.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public const string BasePath = "api/v1/product";
        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductViewModel>> FindAll(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAsync<List<ProductViewModel>>();
        }
        public async Task<ProductViewModel> FindByIdentifier(long identifier, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"{BasePath}/{identifier}");
            return await response.ReadContentAsync<ProductViewModel>();
        }
        public async Task<ProductViewModel> Create(ProductViewModel productModel, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsJson(BasePath, productModel);
            return await response.ReadContentAsync<ProductViewModel>();
        }
        public async Task<ProductViewModel> Update(ProductViewModel productModel, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PutAsJson(BasePath, productModel);
            return await response.ReadContentAsync<ProductViewModel>();
        }
        public async Task<bool> Delete(long identifier, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"{BasePath}/{identifier}");
            return await response.ReadContentAsync<bool>();
        }
    }
}
