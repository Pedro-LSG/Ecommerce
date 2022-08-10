using Ecommerce.Web.Models;

namespace Ecommerce.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAll(string token);
        Task<ProductModel> FindByIdentifier(long identifier, string token);
        Task<ProductModel> Create(ProductModel productModel, string token);
        Task<ProductModel> Update(ProductModel productModel, string token);
        Task<bool> Delete(long identifier, string token);
    }
}
