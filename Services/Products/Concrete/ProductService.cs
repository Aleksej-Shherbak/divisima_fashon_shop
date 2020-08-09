using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains;
using EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Services.Products.Abstract;
using Utils;

namespace Services.Products.Concrete
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDistributedCache _cache;


        public ProductService(ApplicationDbContext dbContext, IDistributedCache cache)
        {
            _dbContext = dbContext;
            _cache = cache;
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

            var redisKey = $"GetProductsWithBrandsBySellingScoresAndCategoryIdAsync" +
                           $"(categpryId={categoryId},sellingScores={sellingScores},skip={skip},take={take})"; 

            
            var productsFromRedis =
                await _cache.GetAsync(redisKey);

            if (productsFromRedis != null)
            {
                return ObjectUtils.ConvertByteArrayToObject<List<Product>>(productsFromRedis);
            }
            
            
            var query = _dbContext.Products.AsNoTracking()
                .Include(x => x.Brand).AsNoTracking()
                .Where(x => x.SaleScore > sellingScores);

            if (categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            query = query.Skip(skip).Take(take);

            var products = await query.ToListAsync();

            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            
            await _cache.SetAsync(redisKey,
                ObjectUtils.ConvertAnyObjectToByteArray(products),
                options);
            
            return products;
        }

        public async Task<List<Product>> GetProductsForShowingOnMainPageSlider()
        {
            
            var redisKey = "GetProductsForShowingOnMainPageSlider";
            
            var productsFromRedis =
                await _cache.GetAsync(redisKey);

            if (productsFromRedis != null)
            {
                return ObjectUtils.ConvertByteArrayToObject<List<Product>>(productsFromRedis);
            }
            
            var sliderProducts = await _dbContext.Products.AsNoTracking()
                .Include(x => x.Brand).AsNoTracking()
                .Where(x => x.ShowOnMainPageSlider)
                .ToListAsync();

            var options = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5));
            
            await _cache.SetAsync(redisKey,
                ObjectUtils.ConvertAnyObjectToByteArray(sliderProducts),
                options);
            
            return sliderProducts;
        }
    }
}