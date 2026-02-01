using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseMainImageUrlLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MainImageUrl",
                table: "Products",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MainImageUrl",
                table: "Products",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

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
        }
    }
}
