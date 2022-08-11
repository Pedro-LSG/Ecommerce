using Ecommerce.Web.Models;

namespace Ecommerce.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> FindAll(string token);
        Task<ProductViewModel> FindByIdentifier(long identifier, string token);
        Task<ProductViewModel> Create(ProductViewModel productModel, string token);
        Task<ProductViewModel> Update(ProductViewModel productModel, string token);
        Task<bool> Delete(long identifier, string token);
    }
}
