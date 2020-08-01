using System.Collections;
using System.Collections.Generic;
using Domains;

namespace Shop.Models
{
    public class HomePageModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> SliderProducts { get; set; }
        public List<Product> TopSellingProducts { get; set; }
        public List<Category> TopSellingProductsCategory { get; set; }
    }
}