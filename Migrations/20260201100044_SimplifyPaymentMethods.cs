using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyPaymentMethods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9298), new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9298) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9299), new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9301), new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9301) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9302), new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9303) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9304), new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9304) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9305), new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9305) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9306), new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9307) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9174), new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9175) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9177), new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9177) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9178), new DateTime(2026, 2, 1, 10, 0, 44, 422, DateTimeKind.Utc).AddTicks(9179) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6824), new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6825) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6826), new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6827) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6828), new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6828) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6830), new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6830) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6831), new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6832) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6833), new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6833) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6834), new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6667), new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6669) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6671), new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6672) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6673), new DateTime(2026, 2, 1, 5, 25, 22, 91, DateTimeKind.Utc).AddTicks(6673) });
        }
    }
}
