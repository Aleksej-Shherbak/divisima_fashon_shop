using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains
{
    [Serializable]
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Name { get; set; }
       
        public string ShortDescription { get; set; }
       
        public string Description { get; set; }

        public string MainSliderImagePath { get; set; }

        public bool ShowOnMainPageSlider { get; set; }

        public int Price { get; set; }    
        
        public string ImagePath { get; set; }

        public bool IsHit { get; set; }
        
        public int CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }

        /// <summary>
        /// Бизнес логика такова. За каждую продажу продукт получает очки продаж. По количеству
        /// этих очков продукт участвует в рекламных акциях. Например, в отображении на главной.
        /// Каждый год количество очков уравнивается. Продуктам - победителям с прошлого года
        /// начисляется небольшое преимущество. Но у других продуктов в этом году будет шанс
        /// обогнать прошлогодних, потому что это преимущество реально небольшое.
        /// </summary>
        public int SaleScore { get; set; }

        /// <summary>
        /// Флаг означает, что продукт участвует в распродаже
        /// </summary>
        public bool OnSale { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public Product()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}