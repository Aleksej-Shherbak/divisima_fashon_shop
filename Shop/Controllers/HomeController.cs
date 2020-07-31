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
            var homePageModel = new HomePageModel
            {
                Categories = await _dbContext.Categories.OrderByDescending(x => x.SortWeight).ToListAsync(),
                SliderProducts = await _dbContext.Products.Include(x => x.Brand).
                    Where(x => x.ShowOnMainPageSlider).ToListAsync(),
            };
            
            return View(homePageModel);
        }
    }
}