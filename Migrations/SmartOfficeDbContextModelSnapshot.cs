﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartOffice;

#nullable disable

namespace SmartOffice.Migrations
{
    [DbContext(typeof(SmartOfficeDbContext))]
    partial class SmartOfficeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("SmartOffice.Models.MenuModels.DishModel", b =>
                {
                    b.Property<string>("FoodorderDishIdProp")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("DishId");

                    b.Property<string>("FoodorderDishAdditionalSelectionProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("DishAdditionalSelection");

                    b.Property<string>("FoodorderDishCategoryProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("DishCategory");

                    b.Property<string>("FoodorderDishContentsProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("DishContents");

                    b.Property<string>("FoodorderDishDesignationProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("DishDesignation");

                    b.Property<string>("FoodorderDishNumberProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("DishNumber");

                    b.Property<string>("FoodorderDishPriceProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("DishPrice");

                    b.Property<string>("FoodorderDishRestaurantIdProp")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("DishRestId");

                    b.HasKey("FoodorderDishIdProp");

                    b.HasIndex("FoodorderDishRestaurantIdProp");

                    b.ToTable("SoDishTab", (string)null);
                });

            modelBuilder.Entity("SmartOffice.Models.OrderModels.OrderDetailsModel", b =>
                {
                    b.Property<string>("FoodorderOrderDetailsIdProp")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("OrderDetailsId");

                    b.Property<string>("FoodorderDishIdProp")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("DishId");

                    b.Property<string>("FoodorderOrderIdProp")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("OrderId");

                    b.Property<string>("FoodorderPaymentMethodProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("PaymentMethod");

                    b.Property<int>("FoodorderUserIdProp")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("FoodorderOrderDetailsIdProp");

                    b.HasIndex("FoodorderDishIdProp");

                    b.HasIndex("FoodorderOrderIdProp");

                    b.HasIndex("FoodorderUserIdProp");

                    b.ToTable("SoOrderDetailsTab", (string)null);
                });

            modelBuilder.Entity("SmartOffice.Models.OrderModels.OrderModel", b =>
                {
                    b.Property<string>("FoodorderOrderIdProp")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("OrderId");

                    b.Property<string>("FoodorderOrderDateProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("OrderDate");

                    b.Property<string>("FoodorderRestaurantIdProp")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("RestaurantId");

                    b.Property<int>("FoodorderUserIdProp")
                        .HasColumnType("int")
                        .HasColumnName("UserId");

                    b.HasKey("FoodorderOrderIdProp");

                    b.HasIndex("FoodorderRestaurantIdProp");

                    b.HasIndex("FoodorderUserIdProp");

                    b.ToTable("SoOrderTab", (string)null);
                });

            modelBuilder.Entity("SmartOffice.Models.RestaurantModels.RestaurantModel", b =>
                {
                    b.Property<string>("FoodorderRestaurantIdProp")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("RestId");

                    b.Property<string>("FoodorderRestaurantCityProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("RestCity");

                    b.Property<string>("FoodorderRestaurantDeliveryCostProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("RestDeliveryCost");

                    b.Property<string>("FoodorderRestaurantDeliveryTimeProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("RestDeliveryTime");

                    b.Property<string>("FoodorderRestaurantDeliveryYesNoProp")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("RestDelivery");

                    b.Property<string>("FoodorderRestaurantLieferandoLinkProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("RestLieferandoLink");

                    b.Property<string>("FoodorderRestaurantMinimalOrderValueProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("RestMinOrderValue");

                    b.Property<string>("FoodorderRestaurantNameProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("RestName");

                    b.Property<string>("FoodorderRestaurantOrdertypeAppTelephoneProp")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("RestAppTelephone");

                    b.Property<string>("FoodorderRestaurantPhonenumberProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("RestPhonenumber");

                    b.Property<string>("FoodorderRestaurantStreetProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("RestStreet");

                    b.Property<string>("FoodorderRestaurantTypeProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("RestType");

                    b.Property<string>("FoodorderRestaurantZipcodeProp")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("RestZipcode");

                    b.HasKey("FoodorderRestaurantIdProp");

                    b.ToTable("SoRestTab", (string)null);
                });

            modelBuilder.Entity("SmartOffice.Models.UserModels.UserModel", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("ActivePaypal")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PaypalEmail")
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserPassword")
                        .HasColumnType("longtext");

                    b.HasKey("UserId");

                    b.ToTable("SoUserTab", (string)null);
                });

            modelBuilder.Entity("SmartOffice.Models.MenuModels.DishModel", b =>
                {
                    b.HasOne("SmartOffice.Models.RestaurantModels.RestaurantModel", "Restaurant")
                        .WithMany("Dishes")
                        .HasForeignKey("FoodorderDishRestaurantIdProp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("SmartOffice.Models.OrderModels.OrderDetailsModel", b =>
                {
                    b.HasOne("SmartOffice.Models.MenuModels.DishModel", "Dish")
                        .WithMany("OrderDetails")
                        .HasForeignKey("FoodorderDishIdProp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartOffice.Models.OrderModels.OrderModel", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("FoodorderOrderIdProp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartOffice.Models.UserModels.UserModel", "User")
                        .WithMany("OrderDetails")
                        .HasForeignKey("FoodorderUserIdProp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Order");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartOffice.Models.OrderModels.OrderModel", b =>
                {
                    b.HasOne("SmartOffice.Models.RestaurantModels.RestaurantModel", "Restaurant")
                        .WithMany("Orders")
                        .HasForeignKey("FoodorderRestaurantIdProp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SmartOffice.Models.UserModels.UserModel", "User")
                        .WithMany("Orders")
                        .HasForeignKey("FoodorderUserIdProp")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("SmartOffice.Models.MenuModels.DishModel", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("SmartOffice.Models.OrderModels.OrderModel", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("SmartOffice.Models.RestaurantModels.RestaurantModel", b =>
                {
                    b.Navigation("Dishes");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("SmartOffice.Models.UserModels.UserModel", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
