using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.VisualBasic.CompilerServices;
using Services.Products.Abstract;
using Utils;

namespace Services.Products.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDistributedCache _cache;

        public CategoryService(ApplicationDbContext dbContext, IDistributedCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
        }

        public async Task<List<Category>> GetCategoriesBySellingScoresAsync(int? sellingScores = null)
        {
            if (!sellingScores.HasValue)
            {
                // TODO этот параметр надо брать из настроек приложения! Но пока так.
                sellingScores = 100;
            }

            var topSellingProductsCategoriesList =
                ObjectUtils.ConvertByteArrayToObject<List<Category>>(
                    (await _cache.GetAsync("topSellingProductsCategoriesList")));

            // TODO сначала попытаться взять это из редиса
            topSellingProductsCategoriesList = await _dbContext.Products.AsNoTracking()
                .Include(x => x.Category).AsNoTracking()
                .Where(x => x.SaleScore > sellingScores)
                .Select(x => x.Category).Distinct().ToListAsync();

            return topSellingProductsCategoriesList;
        }

        public async Task<List<Category>> GetCategoriesOrderedBySortWeight()
        {
            // TODO сначала попытаться взять это из редиса
            var categories = await _dbContext.Categories.OrderByDescending(x => x.SortWeight)
                .ToListAsync();

            return categories;
        }
    }
}