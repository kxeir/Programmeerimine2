using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KooliProjekt.Application.Data
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string OrderState { get; set; } = "pending";
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
