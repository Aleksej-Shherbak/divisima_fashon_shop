using System.Collections.Generic;
using System.Threading.Tasks;
using Domains;

namespace Services.Products.Abstract
{
    public interface ICategoryService
    {
        /// <summary>
        /// Возвращает категории с фильтрацией по очкам продаж
        /// </summary>
        /// <param name="sellingScores"></param>
        /// <returns></returns>
        public Task<List<Category>> GetCategoriesBySellingScoresAsync(int? sellingScores = null);

        public Task<List<Category>> GetCategoriesOrderedBySortWeight();
    }
}