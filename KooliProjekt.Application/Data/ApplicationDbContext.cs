using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<   User> Users { get; set; }  
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }    
}