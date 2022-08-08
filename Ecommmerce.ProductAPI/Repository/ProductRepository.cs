using AutoMapper;
using Ecommerce.ProductAPI.Data.ValueObjects;
using Ecommerce.ProductAPI.Model;
using Ecommerce.ProductAPI.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _mySqlContext;
        private IMapper _mapper;

        public ProductRepository(MySqlContext mySqlContext, IMapper mapper)
        {
            _mySqlContext = mySqlContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            List<Product> products = await _mySqlContext.Products.ToListAsync();
            return _mapper.Map<List<ProductVO>>(products);
        }
        public async Task<ProductVO> FindByIdentifier(long identifier)
        {
            Product product = await _mySqlContext.Products.FirstOrDefaultAsync(p => p.Id == identifier) ?? new Product();
            return _mapper.Map<ProductVO>(product);
        }
        public async Task<ProductVO> Create(ProductVO productVo)
        {
            Product product = _mapper.Map<Product>(productVo);
            _mySqlContext.Products.Add(product);
            await _mySqlContext.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }
        public async Task<ProductVO> Update(ProductVO productVo)
        {
            Product product = _mapper.Map<Product>(productVo);
            _mySqlContext.Products.Update(product);
            await _mySqlContext.SaveChangesAsync();
            return _mapper.Map<ProductVO>(product);
        }
        public async Task<bool> Delete(long identifier)
        {
            try
            {
                Product product = await _mySqlContext.Products.FirstOrDefaultAsync(p => p.Id == identifier) ?? throw new Exception();
                _mySqlContext.Products.Remove(product);
                await _mySqlContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
