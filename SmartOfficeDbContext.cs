using Microsoft.EntityFrameworkCore;
using SmartOffice.Models.MenuModels;
using SmartOffice.Models.OrderModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.UserModels;

namespace SmartOffice;

public class SmartOfficeDbContext : DbContext
{
    public DbSet<UserModel> SoUserTab { get; set; } = null!;
    public DbSet<RestaurantViewModel> SoRestTab { get; set; } = null!;
    public DbSet<MenuViewModel> SoMenuTab { get; set; } = null!;
    // public DbSet<OrderModel> SoOrderTab { get; set; } = null!;

    public SmartOfficeDbContext(DbContextOptions<SmartOfficeDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>().ToTable("SoUserTab");
        
        modelBuilder.Entity<RestaurantViewModel>().ToTable("SoRestTab")
            .HasKey(r => r.FoodorderRestaurantIdProp);
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantIdProp).HasColumnName("RestId");
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantNameProp).HasColumnName("RestName");
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantStreetProp).HasColumnName("RestStreet");
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantZipcodeProp).HasColumnName("RestZipcode");
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantCityProp).HasColumnName("RestCity");
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantTypeProp).HasColumnName("RestType");
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantPhonenumberProp).HasColumnName("RestPhonenumber");
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantDeliveryYesNoProp).HasColumnName("RestDelivery").HasColumnType("VARCHAR(20)");
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantDeliveryTimeProp).HasColumnName("RestDeliveryTime");
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantOrdertypeAppTelephoneProp).HasColumnName("RestAppTelephone").HasColumnType("VARCHAR(20)");
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantMinimalOrderValueProp).HasColumnName("RestMinOrderValue");
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantDeliveryCostProp).HasColumnName("RestDeliveryCost");
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantLieferandoLinkProp).HasColumnName("RestLieferandoLink");

        modelBuilder.Entity<MenuViewModel>().ToTable("SoMenuTab")
            .HasKey(r => r.FoodorderFoodMenuIdProp);
        modelBuilder.Entity<MenuViewModel>()
            .Property(r => r.FoodorderFoodMenuIdProp).HasColumnName("MenuId");
        modelBuilder.Entity<MenuViewModel>()
            .Property(r => r.FoodorderFoodRestaurantIdProp).HasColumnName("MenuRestId");
        modelBuilder.Entity<MenuViewModel>().Ignore(r => r.FoodorderFoodRestaurantIdPropObj);
        modelBuilder.Entity<MenuViewModel>()
            .Property(r => r.FoodorderFoodNumberProp).HasColumnName("MenuFoodNumber");
        modelBuilder.Entity<MenuViewModel>()
            .Property(r => r.FoodorderFoodCategoryProp).HasColumnName("MenuFoodCategory");
        modelBuilder.Entity<MenuViewModel>()
            .Property(r => r.FoodorderFoodDesignationProp).HasColumnName("MenuFoodDesignation");
        modelBuilder.Entity<MenuViewModel>()
            .Property(r => r.FoodorderFoodContentsProp).HasColumnName("MenuFoodContents");
        modelBuilder.Entity<MenuViewModel>()
            .Property(r => r.FoodorderFoodAdditionalSelectionProp).HasColumnName("MenuFoodAdditionalSelection");
        modelBuilder.Entity<MenuViewModel>()
            .Property(r => r.FoodorderFoodPriceProp).HasColumnName("MenuFoodPrice");




        // modelBuilder.Entity<OrderModel>().ToTable("SoOrderTab");
    }
}