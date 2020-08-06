using System.Linq;
using System.Threading.Tasks;
using EntityFramework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shop.Models;

namespace Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            // TODO брать эту настройку из базы
            const int topSellingPerThisYear = 100;

            var topSellingProducts = await _dbContext.Products
                .Include(x => x.Category)
                .Include(x => x.Brand)
                .Where(x => x.SaleScore > topSellingPerThisYear).ToListAsync();

            var topSellingProductsCategory = await _dbContext.Products
                .Include(x => x.Category)
                .Where(x => x.SaleScore > topSellingPerThisYear)
                .Select(x => x.Category).Distinct().ToListAsync();

            var categories = await _dbContext.Categories.OrderByDescending(x => x.SortWeight)
                .ToListAsync();

            var sliderProducts = await _dbContext.Products.Include(x => x.Brand)
                .Where(x => x.ShowOnMainPageSlider)
                .ToListAsync();

            var homePageModel = new HomePageModel
            {
                Categories = categories,
                SliderProducts = sliderProducts,
                TopSellingProducts = topSellingProducts,
                TopSellingProductsCategory = topSellingProductsCategory,
            };

            return View(homePageModel);
        }
    }
}