using Microsoft.EntityFrameworkCore;
using prodAPIPrac2.Models;
using prodAPIPrac2.Order_models;

namespace prodAPIPrac2.data;

public class appdbcontext : DbContext
{
    public appdbcontext(DbContextOptions<appdbcontext> options) : base(options)
    {       }
    public DbSet<Product> products { get; set; }
    public DbSet<User> users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Orders> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }
}
