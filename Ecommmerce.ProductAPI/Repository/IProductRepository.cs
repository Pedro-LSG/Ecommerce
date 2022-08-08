using Ecommerce.ProductAPI.Data.ValueObjects;

namespace Ecommerce.ProductAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>> FindAll();
        Task<ProductVO> FindByIdentifier(long identifier);
        Task<ProductVO> Create(ProductVO productVo);
        Task<ProductVO> Update(ProductVO productVo);
        Task<bool> Delete(long identifier);
    }
}
