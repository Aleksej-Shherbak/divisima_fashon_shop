using System.Collections.Generic;
using Domains;

namespace Shop.Models
{
    public class HomePageModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> SliderProducts { get; set; }
    }
}