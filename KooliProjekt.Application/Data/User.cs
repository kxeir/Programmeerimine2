using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KooliProjekt.Application.Data {

    public class User
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string firsName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string lastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string email { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string phoneNumber { get; set; }

        [StringLength(200)]
        public string address { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string password { get; set; } = string.Empty;

        public ICollection<Order> orders { get; set; } = new List<Order>();

    }
}
