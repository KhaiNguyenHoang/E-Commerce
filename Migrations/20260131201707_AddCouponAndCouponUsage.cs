using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class AddCouponAndCouponUsage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DiscountType = table.Column<int>(type: "int", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumOrderAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaximumDiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UsageLimit = table.Column<int>(type: "int", nullable: true),
                    UsedCount = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CouponUsages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CouponId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponUsages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouponUsages_Coupons_CouponId",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CouponUsages_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CouponUsages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8610), new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8611) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8612), new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8613) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8614), new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8614) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8615), new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8615) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8616), new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8616) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8618), new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8618) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8619), new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8619) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8451), new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8452) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8454), new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8454) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8455), new DateTime(2026, 1, 31, 20, 17, 6, 726, DateTimeKind.Utc).AddTicks(8456) });

            migrationBuilder.CreateIndex(
                name: "IX_CouponUsages_CouponId",
                table: "CouponUsages",
                column: "CouponId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponUsages_OrderId",
                table: "CouponUsages",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CouponUsages_UserId",
                table: "CouponUsages",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CouponUsages");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8881), new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8882) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8883), new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8883) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8884), new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8885) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8886), new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8886) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8887), new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8887) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8888), new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8888) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8889), new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8890) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8768), new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8768) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8769), new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8770) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8771), new DateTime(2026, 1, 31, 20, 1, 23, 392, DateTimeKind.Utc).AddTicks(8771) });
        }
    }
}
