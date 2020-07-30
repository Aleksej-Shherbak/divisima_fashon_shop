using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Domains;
using EntityFramework;

namespace TestDataSeeders.Seeders
{
    public class BrandsSeeder : ISeeder
    {
        public  void RunSeeding(ApplicationDbContext context)
        {
            var faker = new Faker("ru");
            var brandsNames = new List<string>
            {
                "Dorothy Perkins", "United Colors of Benetton", "Love Republic", "Stradivarius", "Mango"
            };

            var brands = Enumerable.Range(0, 4).Select(x => new Brand
            {
                Description = faker.Random.Words(faker.Random.Number(10, 15)),
                Name = brandsNames[x],
            });

            context.Brands.AddRange(brands);
            context.SaveChanges();
        }
    }
}