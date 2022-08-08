using Ecommerce.Web.Models;
using Ecommerce.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.FindAll();
            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.Create(productModel);
                if (response != null)
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(productModel);
        }

        public async Task<IActionResult> ProductUpdate(int id)
        {
            var product = await _productService.FindByIdentifier(id);
            if (product != null) return View(product);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.Update(productModel);
                if (response != null)
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(productModel);
        }

        public async Task<IActionResult> ProductDelete(int id)
        {
            var product = await _productService.FindByIdentifier(id);
            if (product != null) return View(product);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductModel productModel)
        {
            var response = await _productService.Delete(productModel.Id);
            if (response)
                return RedirectToAction(nameof(ProductIndex));

            return View(productModel);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
