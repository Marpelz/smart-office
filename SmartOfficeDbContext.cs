using Microsoft.EntityFrameworkCore;
using SmartOffice.Models.MenuModels;
using SmartOffice.Models.OrderModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.UserModels;

namespace SmartOffice;

public class SmartOfficeDbContext : DbContext
{
    public DbSet<UserModel> SoUserTab { get; set; } = null!;
    public DbSet<RestaurantModel> SoRestTab { get; set; } = null!;
    public DbSet<DishModel> SoDishTab { get; set; } = null!;
    public DbSet<OrderModel> SoOrderTab { get; set; } = null!;
    public DbSet<OrderDetailsModel> SoOrderDetailsTab { get; set; } = null!;


    public SmartOfficeDbContext(DbContextOptions<SmartOfficeDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User-Model
        modelBuilder.Entity<UserModel>().ToTable("SoUserTab");
        
        // Restaurant-Model
        modelBuilder.Entity<RestaurantModel>().ToTable("SoRestTab")
            .HasKey(rm => rm.FoodorderRestaurantIdProp);
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantIdProp)
            .HasColumnName("RestId");
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantNameProp)
            .HasColumnName("RestName");
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantStreetProp)
            .HasColumnName("RestStreet");
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantZipcodeProp)
            .HasColumnName("RestZipcode");
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantCityProp)
            .HasColumnName("RestCity");
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantTypeProp)
            .HasColumnName("RestType");
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantPhonenumberProp)
            .HasColumnName("RestPhonenumber");
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantDeliveryYesNoProp)
            .HasColumnName("RestDelivery")
            .HasColumnType("VARCHAR(20)");
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantDeliveryTimeProp)
            .HasColumnName("RestDeliveryTime");
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantOrdertypeAppTelephoneProp)
            .HasColumnName("RestAppTelephone")
            .HasColumnType("VARCHAR(20)");
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantMinimalOrderValueProp)
            .HasColumnName("RestMinOrderValue");
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantDeliveryCostProp)
            .HasColumnName("RestDeliveryCost");
        
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.FoodorderRestaurantLieferandoLinkProp)
            .HasColumnName("RestLieferandoLink");
        
        // Dish-Model
        modelBuilder.Entity<DishModel>().ToTable("SoDishTab")
            .HasKey(dm => dm.FoodorderDishIdProp);
        
        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.FoodorderDishIdProp)
            .HasColumnName("DishId");
        
        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.FoodorderDishRestaurantIdProp)
            .HasColumnName("DishRestId");
        
        modelBuilder.Entity<DishModel>().Ignore(dm => dm.FoodorderDishRestaurantIdPropObj);
        
        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.FoodorderDishNumberProp)
            .HasColumnName("DishNumber");
        
        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.FoodorderDishCategoryProp)
            .HasColumnName("DishCategory");
        
        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.FoodorderDishDesignationProp)
            .HasColumnName("DishDesignation");
        
        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.FoodorderDishContentsProp)
            .HasColumnName("DishContents");
        
        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.FoodorderDishAdditionalSelectionProp)
            .HasColumnName("DishAdditionalSelection");
        
        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.FoodorderDishPriceProp)
            .HasColumnName("DishPrice");
        
        // Order-Model

        modelBuilder.Entity<OrderModel>().ToTable("SoOrderTab")
            .HasKey(om => om.FoodorderOrderIdProp);
           
        modelBuilder.Entity<OrderModel>()
            .Property(om => om.FoodorderOrderIdProp)
            .HasColumnName("OrderId");
        
        modelBuilder.Entity<OrderModel>()
            .Property(om => om.FoodorderRestaurantIdProp)
            .HasColumnName("RestaurantId");
        
        modelBuilder.Entity<OrderModel>()
            .Property(om => om.FoodorderUserIdProp)
            .HasColumnName("UserId");
        
        modelBuilder.Entity<OrderModel>()
            .Property(om => om.FoodorderOrderDateProp)
            .HasColumnName("OrderDate");
        
        // OrderDetails-Model
        
        modelBuilder.Entity<OrderDetailsModel>().ToTable("SoOrderDetailsTab")
            .HasKey(odm => odm.FoodorderOrderDetailsIdProp);
        
        modelBuilder.Entity<OrderDetailsModel>()
            .Property(odm => odm.FoodorderOrderDetailsIdProp)
            .HasColumnName("OrderDetailsId");
        
        modelBuilder.Entity<OrderDetailsModel>()
            .Property(odm => odm.FoodorderOrderIdProp)
            .HasColumnName("OrderId");
        
        modelBuilder.Entity<OrderDetailsModel>()
            .Property(odm => odm.FoodorderUserIdProp)
            .HasColumnName("UserId");
        
        modelBuilder.Entity<OrderDetailsModel>()
            .Property(odm => odm.FoodorderDishIdProp)
            .HasColumnName("DishId");
        
        modelBuilder.Entity<OrderDetailsModel>()
            .Property(odm => odm.FoodorderPaymentMethodProp)
            .HasColumnName("PaymentMethod");
        
        // Relationships
        modelBuilder.Entity<DishModel>()
            .HasOne(dm => dm.Restaurant)
            .WithMany(rm => rm.Dishes)
            .HasForeignKey(dm => dm.FoodorderDishRestaurantIdProp);
        
        modelBuilder.Entity<OrderModel>()
            .HasOne(om => om.Restaurant)
            .WithMany(rm => rm.Orders)
            .HasForeignKey(om => om.FoodorderRestaurantIdProp);
        
        modelBuilder.Entity<OrderModel>()
            .HasOne(om => om.User)
            .WithMany(rm => rm.Orders)
            .HasForeignKey(om => om.FoodorderUserIdProp);
        
        modelBuilder.Entity<OrderDetailsModel>()
            .HasOne(odm => odm.Order)
            .WithMany(om => om.OrderDetails)
            .HasForeignKey(om => om.FoodorderOrderIdProp);
        
        modelBuilder.Entity<OrderDetailsModel>()
            .HasOne(odm => odm.User)
            .WithMany(um => um.OrderDetails)
            .HasForeignKey(om => om.FoodorderUserIdProp);
        
        modelBuilder.Entity<OrderDetailsModel>()
            .HasOne(odm => odm.Dish)
            .WithMany(dm => dm.OrderDetails)
            .HasForeignKey(odm => odm.FoodorderDishIdProp);
    }
}