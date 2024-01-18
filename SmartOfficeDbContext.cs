using Microsoft.EntityFrameworkCore;
using SmartOffice.Models.MenuModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.UserModels;

namespace SmartOffice;

public class SmartOfficeDbContext : DbContext
{
    public DbSet<UserModel> SoUserTab { get; set; } = null!;
    // public DbSet<RestaurantModel> SoRestTab { get; set; } = null!;
    // public DbSet<MenuModel> SoMenuTab { get; set; } = null!;

    public SmartOfficeDbContext(DbContextOptions<SmartOfficeDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>().ToTable("SoUserTab");
        // modelBuilder.Entity<RestaurantModel>().ToTable("SoRestTab");
        // modelBuilder.Entity<MenuModel>().ToTable("SoMenuTab");
    }
}