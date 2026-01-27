using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderEase.Service.Services.Products;
using OrderEase.Service.Services.Products.Models;

namespace OrderEase.UI.Controllers
{
    public class ProductsController(IProductService productService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductCreateModel model)
        {
            return View();
        }

        [HttpGet("edit/{id:long}")]
        public async Task<IActionResult> Edit(long id)
        {
            try
            {
                var product = await productService.GetForUpdateAsync(id);
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Edit(long id, ProductUpdateModel model)
        {
            try
            {
                await productService.UpdateAsync(id, model);
                TempData["Success"] = "Product updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await productService.DeleteAsync(id);
                TempData["Success"] = "Product deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet("{id:long}")]
        public async Task<IActionResult> Details(long id)
        {
            try
            {
                var product = await productService.GetAsync(id);
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
