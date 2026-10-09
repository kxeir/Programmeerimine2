using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class Product
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(100)]
        public string description { get; set; }

        [Range(0.01, 10000.00)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal price { get; set; }

        public int productCategoryId { get; set; }
        public ProductCategory? productCategory { get; set; }
    }
}