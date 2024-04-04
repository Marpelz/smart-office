using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartOffice.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SoOrderTab",
                columns: table => new
                {
                    OrderId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RestaurantId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoOrderTab", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_SoOrderTab_SoRestTab_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "SoRestTab",
                        principalColumn: "RestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoOrderTab_SoUserTab_UserId",
                        column: x => x.UserId,
                        principalTable: "SoUserTab",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SoOrderDetailsTab",
                columns: table => new
                {
                    OrderDetailsId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OrderId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DishId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PaymentMethod = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoOrderDetailsTab", x => x.OrderDetailsId);
                    table.ForeignKey(
                        name: "FK_SoOrderDetailsTab_SoDishTab_DishId",
                        column: x => x.DishId,
                        principalTable: "SoDishTab",
                        principalColumn: "DishId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoOrderDetailsTab_SoOrderTab_OrderId",
                        column: x => x.OrderId,
                        principalTable: "SoOrderTab",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SoOrderDetailsTab_SoUserTab_UserId",
                        column: x => x.UserId,
                        principalTable: "SoUserTab",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_SoOrderDetailsTab_DishId",
                table: "SoOrderDetailsTab",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_SoOrderDetailsTab_OrderId",
                table: "SoOrderDetailsTab",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SoOrderDetailsTab_UserId",
                table: "SoOrderDetailsTab",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SoOrderTab_RestaurantId",
                table: "SoOrderTab",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_SoOrderTab_UserId",
                table: "SoOrderTab",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SoOrderDetailsTab");

            migrationBuilder.DropTable(
                name: "SoOrderTab");
        }
    }
}
