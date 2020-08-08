using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Services.Products.Abstract;

namespace Services.Products.Concrete
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Product>> GetProductsWithBrandsBySellingScoresAndCategoryIdAsync(
            int? sellingScores = null,
            int? categoryId = null, int skip = 0,
            int take = 8)
        {
            if (!sellingScores.HasValue)
            {
                // TODO этот параметр надо брать из настроек приложения! Но пока так.
                sellingScores = 100;
            }

            // TODO сначала попробовать взять из редиса по ключу, где будет id категории, потом уже из базы
            var query = _dbContext.Products
                .Include(x => x.Brand)
                .Where(x => x.SaleScore > sellingScores);

            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            query = query.Skip(skip).Take(take);

            return await query.ToListAsync();
        }

        public async Task<List<Product>> GetProductsForShowingOnMainPageSlider()
        {
            // TODO сначала попробовать взять из редиса по ключу, где будет id категории, потом уже из базы
            var sliderProducts = await _dbContext.Products.Include(x => x.Brand)
                .Where(x => x.ShowOnMainPageSlider)
                .ToListAsync();

            return sliderProducts;
        }
    }
}