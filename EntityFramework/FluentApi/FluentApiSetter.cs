using Domains;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.FluentApi
{
    public class FluentApiSetter
    {
        public static void SetupFluentApi(ModelBuilder modelBuilder)
        {
            // setup relations here ...

            modelBuilder.Entity<Product>()
                .HasIndex(x => x.BrandId);
            
            modelBuilder.Entity<Product>()
                .HasIndex(x => x.CategoryId);
            
            modelBuilder.Entity<Category>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Category)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Brand>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Brand)
                .OnDelete(DeleteBehavior.Cascade);
            
            

        }
    }
}