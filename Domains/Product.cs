using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains
{
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

        public DateTime CreatedAt { get; set; }

        public Product()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}