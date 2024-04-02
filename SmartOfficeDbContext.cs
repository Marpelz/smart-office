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
    public DbSet<DishViewModel> SoDishTab { get; set; } = null!;
    // public DbSet<OrderModel> SoOrdersTab { get; set; } = null!;


    public SmartOfficeDbContext(DbContextOptions<SmartOfficeDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User-Model
        modelBuilder.Entity<UserModel>().ToTable("SoUserTab");
        
        // Restaurant-Model
        modelBuilder.Entity<RestaurantViewModel>().ToTable("SoRestTab")
            .HasKey(r => r.FoodorderRestaurantIdProp);
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantIdProp)
            .HasColumnName("RestId");
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantNameProp)
            .HasColumnName("RestName");
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantStreetProp)
            .HasColumnName("RestStreet");
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantZipcodeProp)
            .HasColumnName("RestZipcode");
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantCityProp)
            .HasColumnName("RestCity");
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantTypeProp)
            .HasColumnName("RestType");
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantPhonenumberProp)
            .HasColumnName("RestPhonenumber");
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantDeliveryYesNoProp)
            .HasColumnName("RestDelivery")
            .HasColumnType("VARCHAR(20)");
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantDeliveryTimeProp)
            .HasColumnName("RestDeliveryTime");
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantOrdertypeAppTelephoneProp)
            .HasColumnName("RestAppTelephone")
            .HasColumnType("VARCHAR(20)");
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantMinimalOrderValueProp)
            .HasColumnName("RestMinOrderValue");
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantDeliveryCostProp)
            .HasColumnName("RestDeliveryCost");
        
        modelBuilder.Entity<RestaurantViewModel>()
            .Property(r => r.FoodorderRestaurantLieferandoLinkProp)
            .HasColumnName("RestLieferandoLink");
        
        // Dish-Model
        modelBuilder.Entity<DishViewModel>().ToTable("SoDishTab")
            .HasKey(r => r.FoodorderDishIdProp);
        
        modelBuilder.Entity<DishViewModel>()
            .Property(r => r.FoodorderDishIdProp)
            .HasColumnName("DishId");
        
        modelBuilder.Entity<DishViewModel>()
            .Property(r => r.FoodorderDishRestaurantIdProp)
            .HasColumnName("DishRestId");
        
        modelBuilder.Entity<DishViewModel>().Ignore(r => r.FoodorderDishRestaurantIdPropObj);
        
        modelBuilder.Entity<DishViewModel>()
            .Property(r => r.FoodorderDishNumberProp)
            .HasColumnName("DishNumber");
        
        modelBuilder.Entity<DishViewModel>()
            .Property(r => r.FoodorderDishCategoryProp)
            .HasColumnName("DishCategory");
        
        modelBuilder.Entity<DishViewModel>()
            .Property(r => r.FoodorderDishDesignationProp)
            .HasColumnName("DishDesignation");
        
        modelBuilder.Entity<DishViewModel>()
            .Property(r => r.FoodorderDishContentsProp)
            .HasColumnName("DishContents");
        
        modelBuilder.Entity<DishViewModel>()
            .Property(r => r.FoodorderDishAdditionalSelectionProp)
            .HasColumnName("DishAdditionalSelection");
        
        modelBuilder.Entity<DishViewModel>()
            .Property(r => r.FoodorderDishPriceProp)
            .HasColumnName("DishPrice");
        
        // Relationships
        modelBuilder.Entity<DishViewModel>()
            .HasOne(d => d.Restaurant)
            .WithMany(r => r.Dishes)
            .HasForeignKey(d => d.FoodorderDishRestaurantIdProp);
    }
}