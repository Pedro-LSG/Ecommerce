using Ecommerce.ProductAPI.Data.ValueObjects;
using Ecommerce.ProductAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductVO>>> FindAll()
        {
            return Ok(await _productRepository.FindAll()); 
        }

        [HttpGet("{identifier}")]
        public async Task<IActionResult> FindByIdentifier(long identifier)
        {
            var product = await _productRepository.FindByIdentifier(identifier);
            return product.Id != 0 ? Ok(product) : BadRequest(/*new { Error = "Teste", StatusCode = 400 }*/);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductVO productVo)
        {
            if (productVo != null) return BadRequest();
            var product = await _productRepository.Create(productVo);
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductVO productVo)
        {
            if (productVo != null) return BadRequest();
            var product = await _productRepository.Update(productVo);
            return Ok(product);
        }

        [HttpDelete("{identifier}")]
        public async Task<ActionResult> Delete(long identifier)
        {
            var status = await _productRepository.Delete(identifier);
            return status ? Ok(status) : BadRequest();
            
        }
    }
}
