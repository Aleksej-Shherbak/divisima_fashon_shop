using EntityFramework;

namespace TestDataSeeders.Seeders
{
    public interface ISeeder
    {
        void RunSeeding(ApplicationDbContext context);
    }
}