using Ecommerce.Web.Models;
using Ecommerce.Web.Services.IServices;
using Ecommerce.Web.Utils;

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

        public async Task<IEnumerable<ProductModel>> FindAll()
        {
            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAsync<List<ProductModel>>();
        }
        public async Task<ProductModel> FindByIdentifier(long identifier)
        {
            var response = await _client.GetAsync($"{BasePath}/{identifier}");
            return await response.ReadContentAsync<ProductModel>();
        }
        public async Task<ProductModel> Create(ProductModel productModel)
        {
            var response = await _client.PostAsJson(BasePath, productModel);
            return await response.ReadContentAsync<ProductModel>();
        }
        public async Task<ProductModel> Update(ProductModel productModel)
        {
            var response = await _client.PutAsJson(BasePath, productModel);
            return await response.ReadContentAsync<ProductModel>();
        }
        public async Task<bool> Delete(long identifier)
        {
            var response = await _client.DeleteAsync($"{BasePath}/{identifier}");
            return await response.ReadContentAsync<bool>();
        }
    }
}
