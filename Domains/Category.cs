using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domains
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsNew { get; set; }
        public List<Product> Products { get; set; }
        public DateTime CreatedAt { get; set; }

        public Category()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}