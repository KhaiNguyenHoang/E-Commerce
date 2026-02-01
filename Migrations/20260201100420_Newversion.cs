using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class Newversion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7119), new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7120) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7121), new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7121) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7122), new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7122) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7124), new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7124) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7126), new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7126) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7128), new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7129) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7130), new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(7130) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(6971), new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(6973) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(6974), new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(6975) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(6976), new DateTime(2026, 2, 1, 10, 4, 20, 614, DateTimeKind.Utc).AddTicks(6976) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
