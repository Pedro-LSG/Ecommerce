using Ecommerce.Web.Models;

namespace Ecommerce.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAll();
        Task<ProductModel> FindByIdentifier(long identifier);
        Task<ProductModel> Create(ProductModel productModel);
        Task<ProductModel> Update(ProductModel productModel);
        Task<bool> Delete(long identifier);
    }
}
