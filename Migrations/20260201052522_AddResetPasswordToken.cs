using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class AddResetPasswordToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordTokenExpiry",
                table: "Users",
                type: "datetime2",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResetPasswordTokenExpiry",
                table: "Users");

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
        }
    }
}
