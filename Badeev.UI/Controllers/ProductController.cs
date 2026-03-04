using Badeev.UI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Badeev.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [Route("Catalog/{category?}")] // Настраиваем красивый URL
        public async Task<IActionResult> Index(string? category)
        {
            // Получаем категории для выпадающего списка
            var categoriesResponse = await _categoryService.GetCategoryListAsync();
            if (!categoriesResponse.Success) return NotFound(categoriesResponse.ErrorMessage);

            ViewData["categories"] = categoriesResponse.Data;
            ViewData["currentCategory"] = category == null
                ? "Все типы техники"
                : categoriesResponse.Data.FirstOrDefault(c => c.NormalizedName == category)?.Name;

            // Получаем саму технику
            var productResponse = await _productService.GetProductListAsync(category);
            if (!productResponse.Success)
            {
                ViewData["Error"] = productResponse.ErrorMessage;
            }

            return View(productResponse.Data);
        }
    }
}