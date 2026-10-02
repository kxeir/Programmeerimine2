using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class OrderLine
    {
        public int Id { get; set; }

        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }
        [Range(1, 1000)]
        public int Quantity { get; set; }

        [Required]
        public decimal TotalPrice => Price * Quantity;
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int OrderId { get; set; }
        public Order? Order { get; set; }
    }
}
