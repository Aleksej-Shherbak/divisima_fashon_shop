using Microsoft.Extensions.DependencyInjection;
using Services.Products.Abstract;
using Services.Products.Concrete;

namespace Shop
{
    public static class Di
    {
        /// <summary>
        /// Здесь конфигурируем и устанавливаем те зависимости, которые отвечают за данные и работу с ними
        /// (сервисы, репозитории и т. д.)
        /// </summary>
        /// <param name="services"></param>
        public static void SetupDataDi(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

        }
    }
}