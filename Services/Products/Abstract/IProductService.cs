using System.Collections.Generic;
using System.Threading.Tasks;
using Domains;

namespace Services.Products.Abstract
{
    public interface IProductService
    {
        /// <summary>
        /// Возвращает список продуктов
        /// </summary>
        /// <param name="sellingScores"></param>
        /// <param name="categoryId"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public Task<List<Product>> GetProductsWithBrandsBySellingScoresAndCategoryIdAsync(int? sellingScores = null,
            int? categoryId = null,
            int skip = 0, int take = 8);

        public Task<List<Product>> GetProductsForShowingOnMainPageSlider();
    }
}