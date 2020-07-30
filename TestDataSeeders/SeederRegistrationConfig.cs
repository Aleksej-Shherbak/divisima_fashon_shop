using System.Collections.Generic;
using TestDataSeeders.Seeders;

namespace TestDataSeeders
{
    public static class SeederRegistrationConfig
    {
        public static List<ISeeder> GetSeeders()
        {
            return new List<ISeeder>
            {
                // add new seeder here
                
                new BrandsSeeder(),
                new CategoriesSeeder(),
                new ProductsSeeder(),
            };
        }
    }
}