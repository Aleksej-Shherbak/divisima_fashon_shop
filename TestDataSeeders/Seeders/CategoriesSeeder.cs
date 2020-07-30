using System.Collections.Generic;
using Domains;
using EntityFramework;

namespace TestDataSeeders.Seeders
{
    public class CategoriesSeeder : ISeeder
    {
        public void RunSeeding(ApplicationDbContext context)
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Dresses",
                },
                new Category
                {
                    Name = "Jeans",
                },
                new Category
                {
                    Name = "Footwear",
                },
                new Category
                {
                    Name = "Women's bags",
                    IsNew = true
                },
            };
            
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    }
}