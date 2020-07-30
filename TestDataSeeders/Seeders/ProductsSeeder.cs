using System.Linq;
using Bogus;
using Domains;
using EntityFramework;

namespace TestDataSeeders.Seeders
{
    public class ProductsSeeder : ISeeder
    {
        public void RunSeeding(ApplicationDbContext context)
        {
            var faker = new Faker("ru");

            var brandsList = context.Brands.ToList();
            var categoriesList = context.Categories.ToList();

            var products = Enumerable.Range(0, 30)
                .Select(x =>
                {
                    var category = faker.Random.ListItem(categoriesList);
                    return new Product
                    {
                        Brand = faker.Random.ListItem(brandsList),
                        Category = category,
                        Description = faker.Random.Words(faker.Random.Number(15, 28)),
                        Name = faker.Random.Words(faker.Random.Number(2, 4)),
                        Price = faker.Random.Number(20, 5000),
                        IsHit = faker.Random.ArrayElement(new[] {true, false}),
                        ShortDescription = faker.Random.Words(faker.Random.Number(8, 10)),
                        ImagePath = faker.Image.LoremFlickrUrl(264, 409, category.Name, false, true),
                    };
                });

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}