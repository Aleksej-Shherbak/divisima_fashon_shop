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

            modelBuilder.Entity<Category>()
                .HasOne(x => x.ParentCategory)
                .WithMany(x => x.ChildrenCategories)
                .HasForeignKey(x => x.ParentCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>().HasIndex(x => x.ParentCategoryId);
        }
    }
}