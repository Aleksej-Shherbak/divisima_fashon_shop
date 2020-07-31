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
                    SortWeight = 1000,
                },
                new Category
                {
                    Name = "Jeans",
                    SortWeight = 999,
                },
                new Category
                {
                    Name = "Shoes",
                    ChildrenCategories = new HashSet<Category>
                    {
                        new Category
                        {
                            Name = "Sneakers",
                        },
                        new Category
                        {
                            Name = "Sandals",
                        },
                        new Category
                        {
                            Name = "Formal Shoes",
                        },
                        new Category
                        {
                            Name = "Boots",
                        },
                        new Category
                        {
                            Name = "Flip Flops",
                            SortWeight = 998,
                        }
                    }
                },
                new Category
                {
                    Name = "Women's bags",
                    IsNew = true,
                    SortWeight = 997,

                },
            };
            
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    }
}