using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        public ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
