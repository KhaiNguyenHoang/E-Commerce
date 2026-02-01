using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class AddCouponIdToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CouponId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1546), new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1546) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1548), new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1548) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1549), new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1549) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1551), new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1551) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1552), new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1552) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1554), new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1554) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1555), new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1555) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1262), new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1263) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1265), new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1265) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1266), new DateTime(2026, 2, 1, 4, 11, 20, 663, DateTimeKind.Utc).AddTicks(1266) });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponId",
                table: "Orders",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Coupons_CouponId",
                table: "Orders",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Coupons_CouponId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CouponId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CouponId",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8671), new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8671) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8672), new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8672) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8673), new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8674) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8675), new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8675) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8676), new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8676) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8678), new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8678) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8679), new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8679) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8556), new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8557) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8559), new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8559) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8560), new DateTime(2026, 2, 1, 3, 41, 59, 855, DateTimeKind.Utc).AddTicks(8561) });
        }
    }
}
