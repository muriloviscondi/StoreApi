using APIStory.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIStory.Context
{
  public class AppDbContext : IdentityDbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
    { }

    public DbSet<BuyProduct> BuyProducts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

  }
}
