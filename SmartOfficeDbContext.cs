using Microsoft.EntityFrameworkCore;
using SmartOffice.Models.MenuModels;
using SmartOffice.Models.OrderModels;
using SmartOffice.Models.RestaurantModels;
using SmartOffice.Models.UserModels;

namespace SmartOffice;

public class SmartOfficeDbContext : DbContext
{
    public DbSet<UserModel> SoUserTab { get; set; } = null!;
    public DbSet<UserPaymentMethodModel> SoUserPaymentMethodTab { get; set; } = null!;
    public DbSet<PaymentMethodModel> SoPaymentMethodsTab { get; set; } = null!;
    public DbSet<RestaurantModel> SoRestaurantsTab { get; set; } = null!;
    public DbSet<RestaurantAddressModel> SoRestaurantAddressesTab { get; set; } = null!;
    public DbSet<ZipCodeCityModel> SoRestaurantZipCodeCitiesTab { get; set; } = null!;
    public DbSet<RestaurantOpeningAndDeliveryTimeModel> SoRestaurantOpeningAndDeliveryTimesTab { get; set; } = null!;
    public DbSet<DishModel> SoDishesTab { get; set; } = null!;
    public DbSet<OrderModel> SoOrdersTab { get; set; } = null!;
    public DbSet<OrderDetailsModel> SoOrderDetailsTab { get; set; } = null!;


    public SmartOfficeDbContext(DbContextOptions<SmartOfficeDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /********************************
         *          User_Model          *
         ********************************/
        modelBuilder.Entity<UserModel>()
            .ToTable("UserTab")
            .HasKey(um => um.UserId);
        
        modelBuilder.Entity<UserModel>()
            .Property(um => um.UserId)
            .HasColumnName("User_ID");

        modelBuilder.Entity<UserModel>()
            .Property(u => u.UserName)
            .HasColumnName("User_Name");

        modelBuilder.Entity<UserModel>()
            .Property(u => u.UserPassword)
            .HasColumnName("User_Password");
        
        /********************************
         *  User_Payment_Method_Model   *
         ********************************/

        modelBuilder.Entity<UserPaymentMethodModel>()
            .ToTable("UserPaymentMethodTab")
            .HasKey(upmm => upmm.UserPaymentMethodId);
        
        modelBuilder.Entity<UserPaymentMethodModel>()
            .Property(upmm => upmm.UserPaymentMethodId)
            .HasColumnName("User_Payment_Method_ID");
            
        modelBuilder.Entity<UserPaymentMethodModel>()
            .Property(upmm => upmm.UserPaymentMethodAdditive)
            .HasColumnName("User_Payment_Method_Additive");
        
        /********************************
         *     Payment_Method_Model     *
         ********************************/

        modelBuilder.Entity<PaymentMethodModel>()
            .ToTable("PaymentMethodsTab")
            .HasKey(pmm => pmm.PaymentMethodId);
        
        modelBuilder.Entity<PaymentMethodModel>()
            .Property(pmm => pmm.PaymentMethodId)
            .HasColumnName("Payment_Method_ID");

        modelBuilder.Entity<PaymentMethodModel>()
            .Property(pmm => pmm.PaymentMethod)
            .HasColumnName("Payment_Method");
        
        /********************************
         *       Restaurant_Model       *
         ********************************/

        modelBuilder.Entity<RestaurantModel>()
            .ToTable("RestaurantsTab")
            .HasKey(rm => rm.RestaurantId);
            
        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.RestaurantId)
            .HasColumnName("Restaurant_ID");

        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.RestaurantName)
            .HasColumnName("Restaurant_Name");

        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.RestaurantType)
            .HasColumnName("Restaurant_Type");

        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.RestaurantPhoneNumber)
            .HasColumnName("Restaurant_Phone_Number");

        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.RestaurantDeliveryAvailability)
            .HasColumnName("Restaurant_Delivery_Availability");

        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.RestaurantOrderingMethodAppPhone)
            .HasColumnName("Restaurant_Ordering_Method_App_Phone");

        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.RestaurantMinimumOrderAmount)
            .HasColumnName("Restaurant_Minimum_Order_Amount");

        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.RestaurantDeliveryCosts)
            .HasColumnName("Restaurant_Delivery_Costs");

        modelBuilder.Entity<RestaurantModel>()
            .Property(rm => rm.RestaurantLieferandoLink)
            .HasColumnName("Restaurant_Lieferando_Link");
        
        /********************************
         *   Restaurant_Address_Model   *
         ********************************/

        modelBuilder.Entity<RestaurantAddressModel>()
            .ToTable("RestaurantAddressesTab")
            .HasKey(ram => ram.RestaurantAddressId);
            
        modelBuilder.Entity<RestaurantAddressModel>()
            .Property(ram => ram.RestaurantAddressId)
            .HasColumnName("Restaurant_Address_ID");

        modelBuilder.Entity<RestaurantAddressModel>()
            .Property(ram => ram.RestaurantStreet)
            .HasColumnName("Restaurant_Street");

        modelBuilder.Entity<RestaurantAddressModel>()
            .Property(ram => ram.RestaurantHouseNumber)
            .HasColumnName("Restaurant_House_Number");
        
        /********************************
         *     Restaurant_Z_C_Model     *
         ********************************/

        modelBuilder.Entity<ZipCodeCityModel>()
            .ToTable("ZipCodeCitiesTab")
            .HasKey(rzcm => rzcm.ZipCodeCityId);
            
        modelBuilder.Entity<ZipCodeCityModel>()
            .Property(rzcm => rzcm.ZipCodeCityId)
            .HasColumnName("Restaurant_Zip_Code_City_ID");

        modelBuilder.Entity<ZipCodeCityModel>()
            .Property(rzcm => rzcm.PostalCode)
            .HasColumnName("Restaurant_Postal_Code");

        modelBuilder.Entity<ZipCodeCityModel>()
            .Property(rzcm => rzcm.City)
            .HasColumnName("Restaurant_City");
        
        /********************************
         *    Restaurant_OADT_Model     *
         ********************************/

        modelBuilder.Entity<RestaurantOpeningAndDeliveryTimeModel>()
            .ToTable("RestaurantOpeningAndDeliveryTimesTab")
            .HasKey(rodtm => rodtm.RestaurantTimeId);
            
        modelBuilder.Entity<RestaurantOpeningAndDeliveryTimeModel>()
            .Property(rodtm => rodtm.RestaurantTimeId)
            .HasColumnName("Restaurant_Time_ID");

        modelBuilder.Entity<RestaurantOpeningAndDeliveryTimeModel>()
            .Property(rodtm => rodtm.OpeningTimeMonday)
            .HasColumnName("Opening_Time_Monday");

        modelBuilder.Entity<RestaurantOpeningAndDeliveryTimeModel>()
            .Property(rodtm => rodtm.OpeningTimeTuesday)
            .HasColumnName("Opening_Time_Tuesday");

        modelBuilder.Entity<RestaurantOpeningAndDeliveryTimeModel>()
            .Property(rodtm => rodtm.OpeningTimeWednesday)
            .HasColumnName("Opening_Time_Wednesday");

        modelBuilder.Entity<RestaurantOpeningAndDeliveryTimeModel>()
            .Property(rodtm => rodtm.OpeningTimeThursday)
            .HasColumnName("Opening_Time_Thursday");

        modelBuilder.Entity<RestaurantOpeningAndDeliveryTimeModel>()
            .Property(rodtm => rodtm.OpeningTimeFriday)
            .HasColumnName("Opening_Time_Friday");

        modelBuilder.Entity<RestaurantOpeningAndDeliveryTimeModel>()
            .Property(rodtm => rodtm.OpeningTimeSaturday)
            .HasColumnName("Opening_Time_Saturday");

        modelBuilder.Entity<RestaurantOpeningAndDeliveryTimeModel>()
            .Property(rodtm => rodtm.OpeningTimeSunday)
            .HasColumnName("Opening_Time_Sunday");

        modelBuilder.Entity<RestaurantOpeningAndDeliveryTimeModel>()
            .Property(rodtm => rodtm.DeliveryTimeFrom)
            .HasColumnName("Delivery_Time_From");
        
        /********************************
         *          Dish_Model          *
         ********************************/

        modelBuilder.Entity<DishModel>()
            .ToTable("DishesTab")
            .HasKey(dm => dm.DishId);
            
        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.DishId)
            .HasColumnName("Dish_ID");

        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.DishNumber)
            .HasColumnName("Dish_Number");

        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.DishCategory)
            .HasColumnName("Dish_Category");

        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.DishName)
            .HasColumnName("Dish_Name");

        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.DishContents)
            .HasColumnName("Dish_Contents");

        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.DishAdditionalOptions)
            .HasColumnName("Dish_Additional_Options");

        modelBuilder.Entity<DishModel>()
            .Property(dm => dm.DishPrice)
            .HasColumnName("Dish_Price");
        
        /********************************
         *         Order_Model          *
         ********************************/

        modelBuilder.Entity<OrderModel>()
            .ToTable("OrdersTab")
            .HasKey(om => om.OrderId);
            
        modelBuilder.Entity<OrderModel>()
            .Property(om => om.OrderId)
            .HasColumnName("Order_ID");

        modelBuilder.Entity<OrderModel>()
            .Property(om => om.OrderDate)
            .HasColumnName("Order_Date");

        /********************************
         *     Order_Detail_Model       *
         ********************************/
        
        modelBuilder.Entity<OrderDetailsModel>()
            .ToTable("OrderDetailsTab")
            .HasKey(odm => new { odm.OrderId, odm.UserId, odm.UserPaymentMethodId, odm.DishId });
        
        /********************************
         *     Relationship_Defines     *
         ********************************/

        modelBuilder.Entity<UserPaymentMethodModel>()
            .HasOne(upmm => upmm.PaymentMethod)
            .WithMany(pmm => pmm.UserPaymentMethods)
            .HasForeignKey(upmm => upmm.PaymentMethodId);
        
        modelBuilder.Entity<UserPaymentMethodModel>()
            .HasOne(upmm => upmm.User)
            .WithMany(um => um.UserPaymentMethods)
            .HasForeignKey(upmm => upmm.UserId);
        
        modelBuilder.Entity<RestaurantAddressModel>()
            .HasOne(ram => ram.Restaurant)
            .WithMany(rm => rm.RestaurantAddresses)
            .HasForeignKey(ram => ram.RestaurantId);
        
        modelBuilder.Entity<RestaurantAddressModel>()
            .HasOne(ram => ram.ZipCodeCity)
            .WithMany(rzcm => rzcm.RestaurantAddresses)
            .HasForeignKey(ram => ram.RestaurantZipCodeCityId);

        modelBuilder.Entity<RestaurantOpeningAndDeliveryTimeModel>()
            .HasOne(rodtm => rodtm.Restaurant)
            .WithOne(rm => rm.RestaurantOpeningAndDeliveryTime)
            .HasForeignKey<RestaurantModel>(rm => rm.RestaurantId);

        modelBuilder.Entity<DishModel>()
            .HasOne(dm => dm.Restaurant)
            .WithMany(rm => rm.Dishes)
            .HasForeignKey(dm => dm.RestaurantId);

        modelBuilder.Entity<OrderDetailsModel>()
            .HasOne(odm => odm.Order)
            .WithMany(om => om.OrderDetails)
            .HasForeignKey(odm => odm.OrderId);

        modelBuilder.Entity<OrderDetailsModel>()
            .HasOne(odm => odm.User)
            .WithMany(um => um.OrderDetails)
            .HasForeignKey(odm => odm.UserId);

        modelBuilder.Entity<OrderDetailsModel>()
            .HasOne(odm => odm.UserPaymentMethod)
            .WithMany(upmm => upmm.OrderDetails)
            .HasForeignKey(odm => odm.UserPaymentMethodId);

        modelBuilder.Entity<OrderDetailsModel>()
            .HasOne(odm => odm.Dish)
            .WithMany(dm => dm.OrderDetails)
            .HasForeignKey(odm => odm.DishId);
    }
}