using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartOffice.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrdersTab",
                columns: table => new
                {
                    Order_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Order_Date = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersTab", x => x.Order_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PaymentMethodsTab",
                columns: table => new
                {
                    Payment_Method_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Payment_Method = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethodsTab", x => x.Payment_Method_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RestaurantOpeningAndDeliveryTimesTab",
                columns: table => new
                {
                    Restaurant_Time_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Opening_Time_Monday = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Opening_Time_Tuesday = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Opening_Time_Wednesday = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Opening_Time_Thursday = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Opening_Time_Friday = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Opening_Time_Saturday = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Opening_Time_Sunday = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Delivery_Time_From = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantOpeningAndDeliveryTimesTab", x => x.Restaurant_Time_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserTab",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    User_Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    User_Password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTab", x => x.User_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ZipCodeCitiesTab",
                columns: table => new
                {
                    Restaurant_Zip_Code_City_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Restaurant_Postal_Code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Restaurant_City = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZipCodeCitiesTab", x => x.Restaurant_Zip_Code_City_ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RestaurantsTab",
                columns: table => new
                {
                    Restaurant_ID = table.Column<int>(type: "int", nullable: false),
                    Restaurant_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Restaurant_Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Restaurant_Phone_Number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Restaurant_Delivery_Availability = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Restaurant_Ordering_Method_App_Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Restaurant_Minimum_Order_Amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Restaurant_Delivery_Costs = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Restaurant_Lieferando_Link = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantsTab", x => x.Restaurant_ID);
                    table.ForeignKey(
                        name: "FK_RestaurantsTab_RestaurantOpeningAndDeliveryTimesTab_Restaura~",
                        column: x => x.Restaurant_ID,
                        principalTable: "RestaurantOpeningAndDeliveryTimesTab",
                        principalColumn: "Restaurant_Time_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserPaymentMethodTab",
                columns: table => new
                {
                    User_Payment_Method_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    User_Payment_Method_Additive = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PaymentMethodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPaymentMethodTab", x => x.User_Payment_Method_ID);
                    table.ForeignKey(
                        name: "FK_UserPaymentMethodTab_PaymentMethodsTab_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethodsTab",
                        principalColumn: "Payment_Method_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPaymentMethodTab_UserTab_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTab",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DishesTab",
                columns: table => new
                {
                    Dish_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Dish_Number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dish_Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dish_Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dish_Contents = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dish_Additional_Options = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dish_Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishesTab", x => x.Dish_ID);
                    table.ForeignKey(
                        name: "FK_DishesTab_RestaurantsTab_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantsTab",
                        principalColumn: "Restaurant_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RestaurantAddressesTab",
                columns: table => new
                {
                    Restaurant_Address_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Restaurant_Street = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Restaurant_House_Number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    RestaurantZipCodeCityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantAddressesTab", x => x.Restaurant_Address_ID);
                    table.ForeignKey(
                        name: "FK_RestaurantAddressesTab_RestaurantsTab_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "RestaurantsTab",
                        principalColumn: "Restaurant_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantAddressesTab_ZipCodeCitiesTab_RestaurantZipCodeCit~",
                        column: x => x.RestaurantZipCodeCityId,
                        principalTable: "ZipCodeCitiesTab",
                        principalColumn: "Restaurant_Zip_Code_City_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderDetailsTab",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserPaymentMethodId = table.Column<int>(type: "int", nullable: false),
                    DishId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetailsTab", x => new { x.OrderId, x.UserId, x.UserPaymentMethodId, x.DishId });
                    table.ForeignKey(
                        name: "FK_OrderDetailsTab_DishesTab_DishId",
                        column: x => x.DishId,
                        principalTable: "DishesTab",
                        principalColumn: "Dish_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailsTab_OrdersTab_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrdersTab",
                        principalColumn: "Order_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailsTab_UserPaymentMethodTab_UserPaymentMethodId",
                        column: x => x.UserPaymentMethodId,
                        principalTable: "UserPaymentMethodTab",
                        principalColumn: "User_Payment_Method_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetailsTab_UserTab_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTab",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DishesTab_RestaurantId",
                table: "DishesTab",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailsTab_DishId",
                table: "OrderDetailsTab",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailsTab_UserId",
                table: "OrderDetailsTab",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetailsTab_UserPaymentMethodId",
                table: "OrderDetailsTab",
                column: "UserPaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAddressesTab_RestaurantId",
                table: "RestaurantAddressesTab",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantAddressesTab_RestaurantZipCodeCityId",
                table: "RestaurantAddressesTab",
                column: "RestaurantZipCodeCityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPaymentMethodTab_PaymentMethodId",
                table: "UserPaymentMethodTab",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPaymentMethodTab_UserId",
                table: "UserPaymentMethodTab",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetailsTab");

            migrationBuilder.DropTable(
                name: "RestaurantAddressesTab");

            migrationBuilder.DropTable(
                name: "DishesTab");

            migrationBuilder.DropTable(
                name: "OrdersTab");

            migrationBuilder.DropTable(
                name: "UserPaymentMethodTab");

            migrationBuilder.DropTable(
                name: "ZipCodeCitiesTab");

            migrationBuilder.DropTable(
                name: "RestaurantsTab");

            migrationBuilder.DropTable(
                name: "PaymentMethodsTab");

            migrationBuilder.DropTable(
                name: "UserTab");

            migrationBuilder.DropTable(
                name: "RestaurantOpeningAndDeliveryTimesTab");
        }
    }
}
