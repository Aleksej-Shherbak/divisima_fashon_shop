using System.Collections.Generic;
using System.Threading.Tasks;
using Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Products.Abstract;
using Shop.Models;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger, IProductService productService,
            ICategoryService categoryService)
        {
            _logger = logger;
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var homePageModel = new HomePageModel
            {
                Categories = await _categoryService.GetCategoriesOrderedBySortWeight() ?? new List<Category>(),
                SliderProducts = await _productService.GetProductsForShowingOnMainPageSlider() ?? new List<Product>(),
                TopSellingProducts = await _productService.GetProductsWithBrandsBySellingScoresAndCategoryIdAsync() ?? new List<Product>(),
                TopSellingProductsCategory = await _categoryService.GetCategoriesBySellingScoresAsync() ?? new List<Category>(),
            };

            return View(homePageModel);
        }
    }
}