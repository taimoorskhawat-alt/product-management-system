using Microsoft.EntityFrameworkCore;
using prodAPIPrac2.Models;

namespace prodAPIPrac2.data;

public class appdbcontext : DbContext
{
    public appdbcontext(DbContextOptions<appdbcontext> options) : base(options)
    {       }
    public DbSet<Product> products { get; set; }
    public DbSet<User> users { get; set; }
}
