using Ecommerce.Web.Models;
using Ecommerce.Web.Services.IServices;
using Ecommerce.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var products = await _productService.FindAll(token ?? throw new ArgumentNullException(nameof(token)));
            return View(products);
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductViewModel productModel)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.Create(productModel, token ?? throw new ArgumentNullException(nameof(token)));
                if (response != null)
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(productModel);
        }

        public async Task<IActionResult> ProductUpdate(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.FindByIdentifier(id, token ?? throw new ArgumentNullException(nameof(token)));
            if (product != null) return View(product);
            return NotFound();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductViewModel productModel)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.Update(productModel, token ?? throw new ArgumentNullException(nameof(token)));
                if (response != null)
                    return RedirectToAction(nameof(ProductIndex));
            }
            return View(productModel);
        }

        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var product = await _productService.FindByIdentifier(id, token ?? throw new ArgumentNullException(nameof(token)));
            if (product != null) return View(product);
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductViewModel productModel)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.Delete(productModel.Id, token ?? throw new ArgumentNullException(nameof(token)));
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
